using bb;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemSet : MonoBehaviour
{
    [SerializeField] GameObject RewardResource = null;
    [SerializeField] GameObject RewardCharacter = null;

    [SerializeField] Image RewardResourceImage = null;
    [SerializeField] Text RewardResourceText = null;

    [SerializeField] Image RewardCharacterImage = null;
    [SerializeField] Text RewardCharacterText = null;
    public void Init(SRewardMeta MetaData_, bool IsAD_)
    {
        switch(MetaData_.Type)
        {
            case ERewardType.Character:
                SetPortrate(CGlobal.MetaData.GetCharacterIconName(MetaData_.Data));
                RewardCharacterText.gameObject.SetActive(false);
                break;
            case ERewardType.Resource_Ticket:
                SetResource(EResource.Ticket, MetaData_.Data, IsAD_);
                break;
            case ERewardType.Resource_Gold:
                SetResource(EResource.Gold, MetaData_.Data, IsAD_);
                break;
            case ERewardType.Resource_Dia:
                SetResource(EResource.Dia, MetaData_.Data, IsAD_);
                break;
            case ERewardType.Resource_CP:
                SetResource(EResource.CP, MetaData_.Data, IsAD_);
                break;
            default:
                Debug.Log("Reward Type Error !!!");
                break;
        }
    }
    private void SetResource(EResource ResourceType_, Int32 Count_, bool IsAD_)
    {
        RewardResource.SetActive(true);
        RewardCharacter.SetActive(false);
        RewardResourceImage.sprite = Resources.Load<Sprite>(CGlobal.GetResourcesIconFile(ResourceType_));
        RewardResourceText.text = (IsAD_ ? Count_ * 2 : Count_).ToString();
    }
    private void SetPortrate(string IconName_)
    {
        RewardResource.SetActive(false);
        RewardCharacter.SetActive(true);
        RewardCharacterImage.sprite = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + IconName_);
    }
}
