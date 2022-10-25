using bb;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] ResourceWidget[] _ResourceWidgets = new ResourceWidget[(Int32)EResource.Max];
    [SerializeField] Button _toolTipButton;
    [SerializeField] Button _PlusButton = null;
    [SerializeField] GameObject _DropDown = null;
    [SerializeField] Button _DropDownButton = null;
    Image _DropDownPanel = null;
    Int32[] _LastResources = null;
    private bool IsActiveDropDown;
    Canvas _canvas;
    UnityAction _fToolTipButtonClicked;

    private void Awake()
    {
        gameObject.SetActive(false);
        _toolTipButton.onClick.AddListener(_toolTipButtonClicked);
        _PlusButton.onClick.AddListener(OnClickPlus);
        _DropDownButton.onClick.AddListener(OnClickDropDown);
        _DropDown.SetActive(false);
        _DropDownPanel = GetComponent<Image>();
        _DropDownPanel.raycastTarget = false;
        IsActiveDropDown = false;

        _LastResources = CGlobal.getEmptyResources();
        for (Int32 i = 0; i < _ResourceWidgets.Length; ++i)
            _ResourceWidgets[i].Init(CGlobal.GetResourceSprite((EResource)i), CGlobal.MetaData.MaxResources[i], _LastResources[i]);

        _canvas = GetComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.planeDistance = BaseScene.planeDistance;
    }
    private void OnDestroy()
    {
        _DropDownButton.onClick.RemoveAllListeners();
        _PlusButton.onClick.RemoveAllListeners();
        _toolTipButton.onClick.RemoveAllListeners();
    }
    public async void OptionView()
    {
//#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (CGlobal.ConsoleWindow.ShowWithPassWordUserInput())
            return;
//#endif

        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        await CGlobal.curScene.pushSettingPopup();
    }
    public void UpdateResource(EResource Resource_)
    {
        _LastResources[(Int32)Resource_] = CGlobal.LoginNetSc.User.Resources[(Int32)Resource_];
        _ResourceWidgets[(Int32)Resource_].SetValue(_LastResources[(Int32)Resource_]);
    }
    public void UpdateResources()
    {
        for (EResource i = 0; i < EResource.Max; ++i)
            UpdateResource(i);
    }
    public void UpdateResourceImmediately(EResource Resource_)
    {
        _LastResources[(Int32)Resource_] = CGlobal.LoginNetSc.User.Resources[(Int32)Resource_];
        _ResourceWidgets[(Int32)Resource_].SetValueWithoutAnimation(_LastResources[(Int32)Resource_]);
    }
    public void UpdateResourcesImmediately()
    {
        for (EResource i = 0; i < EResource.Max; ++i)
            UpdateResourceImmediately(i);
    }
    async void OnClickPlus()
    {
        var targetResource = EResource.Ticket;
        var exchangeValue = CGlobal.MetaData.getExchangeValue(targetResource);
        const Int32 targetResourceMinValue = 1;

        if (!CGlobal.doesHaveCost(exchangeValue.costResourceType, exchangeValue.getCostValue(targetResourceMinValue)))
        {
            await GlobalFunction.ShowResourceNotEnough(exchangeValue.costResourceType);
            return;
        }

        var resourceFreeSpace = CGlobal.getResourceFreeSpace(CGlobal.LoginNetSc.User.Resources[(Int32)targetResource], targetResource);
        if (resourceFreeSpace == 0)
        {
            await CGlobal.curScene.pushNoticePopup(true, EText.ReachedMaximumLimit);
            return;
        }

        var targetResourceMaxValueCanBuy = (Int32)(CGlobal.LoginNetSc.User.Resources[(Int32)exchangeValue.costResourceType] / exchangeValue.rate);
        // 절사 로 인한 오차 보정
        // BuyingResourcePopup 내부에서 실시간 계산 해도 되지만 // 슬라이더를 구매 가용범위와 일치하도록 설정하는 것이 정확도가 올라 갈 수 있으니 여기서 min, max를 정확하게 설정
        if (CGlobal.doesHaveCost(exchangeValue.costResourceType, exchangeValue.getCostValue(targetResourceMaxValueCanBuy + 1)))
            ++targetResourceMaxValueCanBuy;

        var targetResourceValue = await CGlobal.curScene.pushBuyingResourcePopup(
            targetResource,
            targetResourceMinValue,
            Math.Min(targetResourceMaxValueCanBuy, resourceFreeSpace),
            exchangeValue,
            EText.ShopScene_Popup_CheckBuy,
            CGlobal.GetResourceText(EResource.Ticket));
        if (targetResourceValue is null)
            return;

        CGlobal.ProgressCircle.Activate();
        CGlobal.NetControl.Send(new SBuyResourceNetCs(new SResourceTypeData(targetResource, (Int32)targetResourceValue)));
    }
    public void show(Camera camera, UnityAction fToolTipButtonClicked)
    {
        _canvas.worldCamera = camera;
        _canvas.sortingLayerName = CGlobal.sortingLayerMoneyUI;
        _fToolTipButtonClicked = fToolTipButtonClicked;
        gameObject.SetActive(true);
        UpdateResources();
    }
    public void hide()
    {
        _fToolTipButtonClicked = null;
        gameObject.SetActive(false);
    }
    void _toolTipButtonClicked()
    {
//#if UNITY_EDITOR || DEVELOPMENT_BUILD
        CGlobal.ConsoleWindow.InputPassword('a');
//#endif
        _fToolTipButtonClicked?.Invoke();
    }
    #region drop down
    private void OnClickDropDown()
    {
        IsActiveDropDown = !IsActiveDropDown;
        SetActiveDropDown(IsActiveDropDown);
    }
    private void SetActiveDropDown(bool active)
    {
        _DropDown.SetActive(active);
        _DropDownPanel.raycastTarget = active;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //if( eventData.pointerPressRaycast.gameObject != _DropDownButton.gameObject || eventData.pointerPressRaycast.gameObject != _DropDown)
        {
            IsActiveDropDown = false;
            SetActiveDropDown(false);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
    }
    #endregion
}
