using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitedItemSet : MonoBehaviour
{
    [SerializeField] GameObject LimitedResource = null;
    [SerializeField] GameObject LimitedCharacter = null;

    [SerializeField] Image LimitedResourceImage = null;
    [SerializeField] Text LimitedResourceText = null;

    [SerializeField] Image LimitedCharacterImage = null;
    public void Init(SRewardMeta MetaData_)
    {
        switch (MetaData_.Type)
        {
            case ERewardType.Character:
                SetPortrate(CGlobal.MetaData.GetCharacterIconName(MetaData_.Data));
                break;
            case ERewardType.Resource_Ticket:
                SetResource(EResource.Ticket, MetaData_.Data);
                break;
            case ERewardType.Resource_Gold:
                SetResource(EResource.Gold, MetaData_.Data);
                break;
            case ERewardType.Resource_Dia:
                SetResource(EResource.Dia, MetaData_.Data);
                break;
            case ERewardType.Resource_CP:
                SetResource(EResource.CP, MetaData_.Data);
                break;
            default:
                Debug.Log("Limited Type Error !!!");
                break;
        }
    }
    private void SetResource(EResource ResourceType_, Int32 Count_)
    {
        LimitedResource.SetActive(true);
        LimitedCharacter.SetActive(false);
        LimitedResourceImage.sprite = Resources.Load<Sprite>(CGlobal.GetResourcesLimitedIconFile(ResourceType_));
        LimitedResourceText.text = Count_.ToString();
    }
    private void SetPortrate(string IconName_)
    {
        LimitedResource.SetActive(false);
        LimitedCharacter.SetActive(true);
        LimitedCharacterImage.sprite = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + IconName_);
    }
}
