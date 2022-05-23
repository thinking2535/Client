using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
#if UNITY_ANDROID
    private class Payload
    {
        public string json = "";
    }
    private class PayloadJson
    {
        public string purchaseToken = "";
    }
#endif
    private class Receipt
    {
        public string Payload = "";
    }
    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        foreach(var i in CGlobal.MetaData.ShopIAPMetas)
        {
            builder.AddProduct(i.Key, ProductType.Consumable);
        }

        UnityPurchasing.Initialize(this, builder);
    }
    public ProductMetadata GetPidInfo(string Pid_)
    {
        var product = m_StoreController.products.WithID(Pid_);
        return product.metadata;
    }
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                CGlobal.ProgressLoading.VisibleProgressLoading();
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                CGlobal.SystemPopup.ShowPopup(String.Format(CGlobal.MetaData.GetText(EText.ShopPopup_Text_Error_Code), "Not available"), PopupSystem.PopupType.Confirm, null);
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
            CGlobal.SystemPopup.ShowPopup(String.Format(CGlobal.MetaData.GetText(EText.ShopPopup_Text_Error_Code), "Not initialized"), PopupSystem.PopupType.Confirm, null);
        }
    }
    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            CGlobal.SystemPopup.ShowPopup(String.Format(CGlobal.MetaData.GetText(EText.ShopPopup_Text_Error_Code), "Not initialized"), PopupSystem.PopupType.Confirm, null);
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            CGlobal.SystemPopup.ShowPopup(String.Format(CGlobal.MetaData.GetText(EText.ShopPopup_Text_Error_Code), "Not supported"), PopupSystem.PopupType.Confirm, null);
        }
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        bool isCheck = false;
        foreach (var i in CGlobal.MetaData.ShopIAPMetas)
        {
            if (String.Equals(args.purchasedProduct.definition.id, i.Key, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                isCheck = true;
                string PurchaseToken = "";
                Debug.Log("receipt : = " + args.purchasedProduct.receipt);
                var jsonReceipt = JsonUtility.FromJson<Receipt>(args.purchasedProduct.receipt);
#if UNITY_EDITOR
                PurchaseToken = jsonReceipt.Payload;
#elif UNITY_ANDROID
                Debug.Log("Payload : = " + jsonReceipt.Payload);
                var jsonPayload = JsonUtility.FromJson<Payload>(jsonReceipt.Payload);
                var json = JsonUtility.FromJson<PayloadJson>(jsonPayload.json);
                PurchaseToken = json.purchaseToken;
#elif UNITY_IOS
                PurchaseToken = "{\"receipt-data\" : \""+jsonReceipt.Payload+"\"}";
#endif
                Debug.Log("PurchaseToken : = " + PurchaseToken);
                CGlobal.NetControl.Send(new SPurchaseNetCs(args.purchasedProduct.definition.id, PurchaseToken));
                break;
            }
        }
        if(!isCheck)
        {
            CGlobal.ProgressLoading.InvisibleProgressLoading();
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            CGlobal.SystemPopup.ShowPopup(String.Format(CGlobal.MetaData.GetText(EText.ShopPopup_Text_Error_Code), "Unrecognized product"), PopupSystem.PopupType.Confirm, null);
        }
        return PurchaseProcessingResult.Pending;
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        CGlobal.ProgressLoading.InvisibleProgressLoading();
        switch (failureReason)
        {
            case PurchaseFailureReason.UserCancelled:
                CGlobal.SystemPopup.ShowPopup(EText.ShopPopup_Text_Cancel, PopupSystem.PopupType.Confirm);
                break;
            case PurchaseFailureReason.DuplicateTransaction:
                CGlobal.SystemPopup.ShowPopup(EText.ShopPopup_Text_Error_Duplicate, PopupSystem.PopupType.Confirm);
                break;
            default:
                CGlobal.SystemPopup.ShowPopup(String.Format(CGlobal.MetaData.GetText(EText.ShopPopup_Text_Error_Code), failureReason), PopupSystem.PopupType.Confirm, null);
                break;
        }
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
    public void Consum(string Pid_)
    {
        var product = m_StoreController.products.WithID(Pid_);
        m_StoreController.ConfirmPendingPurchase(product);
    }
}
