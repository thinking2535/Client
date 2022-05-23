using bb;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RankingRewardPanel : MonoBehaviour
{
    [SerializeField] GameObject _GuageSmallBar = null;
    [SerializeField] GameObject _GuageLargeBar = null;
    [SerializeField] GameObject _RewardObject = null;
    [SerializeField] GameObject _RewardNoRankObject = null;
    [SerializeField] Text _GuageSmallBarText = null;
    [SerializeField] Text _GuageLargeBarText = null;
    //[SerializeField] Image _GuageLargeBarTierIcon = null;
    [SerializeField] Text _GuageLargeBarTierText = null;

    [SerializeField] Text _RewardText = null;
    [SerializeField] Image _RewardImage = null;
    [SerializeField] Image _RewardCheckImage = null;
    [SerializeField] Button _RewardBtn = null;

    [SerializeField] Text _RewardNoRankText = null;
    [SerializeField] Image _RewardNoRankImage = null;
    [SerializeField] Image _RewardNoRankCheckImage = null;
    [SerializeField] Button _RewardNoRankBtn = null;

    [SerializeField] ParticleSystem _ICanRewardEffect = null;

    private SRankTierClientMeta _RankData;
    public SRankRewardMeta RankRewardData;
    public void Init(SRankTierClientMeta RankData_, SRankRewardMeta RankRewardData_)
    {
        _RankData = RankData_;
        RankRewardData = RankRewardData_;
        _GuageSmallBar.SetActive(false);
        _GuageLargeBar.SetActive(true);
        _RewardObject.SetActive(false);
        _RewardNoRankObject.SetActive(false);
        _RewardBtn.gameObject.SetActive(false);
        _RewardNoRankBtn.gameObject.SetActive(false);
        _GuageSmallBarText.text = _RankData.MinPoint.ToString();
        _GuageLargeBarText.text = _RankData.MinPoint.ToString();
        //_GuageLargeBarTierIcon.sprite = Resources.Load<Sprite>("Textures/"+ _RankData.TextureName);
        _GuageLargeBarTierText.text = _RankData.Level.ToString();

        if (CGlobal.MetaData.RankRewardViewList.ContainsKey(RankRewardData.RewardCode))
        {
            var RewardMeta = CGlobal.MetaData.RankRewardViewList[RankRewardData.RewardCode];
            if (RewardMeta.ETextName == EText.Null)
            {
                _RewardText.text = RewardMeta.Count.ToString();
            }
            else
            {
                _RewardText.text = CGlobal.MetaData.GetText(RewardMeta.ETextName);
            }
            var SpriteImg = Resources.Load<Sprite>("Textures/" + RewardMeta.TextureName);
            if(SpriteImg == null)
                SpriteImg = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + RewardMeta.TextureName);
            _RewardImage.sprite = SpriteImg;
            _RewardImage.SetNativeSize();
            _RewardObject.SetActive(true);
        }
        SetRewardCheckImage(false);
        _ICanRewardEffect.Stop();
        _ICanRewardEffect.transform.localPosition = new Vector3(0.0f, 270.0f,0.0f);
    }
    public void Init(SRankRewardMeta RankRewardData_)
    {
        _RankData = null;
        RankRewardData = RankRewardData_;
        _GuageSmallBar.SetActive(true);
        _GuageLargeBar.SetActive(false);
        _RewardObject.SetActive(false);
        _RewardNoRankObject.SetActive(false);
        _RewardBtn.gameObject.SetActive(false);
        _RewardNoRankBtn.gameObject.SetActive(false);
        _GuageSmallBarText.text = RankRewardData_.Point.ToString();
        if (CGlobal.MetaData.RankRewardViewList.ContainsKey(RankRewardData.RewardCode))
        {
            var RewardMeta = CGlobal.MetaData.RankRewardViewList[RankRewardData.RewardCode];
            if (RewardMeta.ETextName == EText.Null)
            {
                _RewardNoRankText.text = RewardMeta.Count.ToString();
            }
            else
            {
                _RewardNoRankText.text = CGlobal.MetaData.GetText(RewardMeta.ETextName);
            }
            var SpriteImg = Resources.Load<Sprite>("Textures/" + RewardMeta.TextureName);
            if (SpriteImg == null)
                SpriteImg = Resources.Load<Sprite>(CGlobal.MetaData.GetPortImagePath() + RewardMeta.TextureName);
            _RewardNoRankImage.sprite = SpriteImg;
            _RewardNoRankImage.SetNativeSize();
            _RewardNoRankObject.SetActive(true);
        }
        SetRewardCheckImage(false);
        _ICanRewardEffect.Stop();
        _ICanRewardEffect.transform.localPosition = new Vector3(0.0f, 130.0f, 0.0f);
    }
    public void Init(SRankTierClientMeta RankData_)
    {
        _RankData = RankData_;
        RankRewardData = null;
        _GuageSmallBar.SetActive(false);
        _GuageLargeBar.SetActive(true);
        _RewardObject.SetActive(false);
        _RewardNoRankObject.SetActive(false);
        _RewardBtn.gameObject.SetActive(false);
        _RewardNoRankBtn.gameObject.SetActive(false);
        _GuageSmallBarText.text = _RankData.MinPoint.ToString();
        _GuageLargeBarText.text = _RankData.MinPoint.ToString();
        //_GuageLargeBarTierIcon.sprite = Resources.Load<Sprite>("Textures/" + _RankData.TextureName);
        _GuageLargeBarTierText.text = _RankData.Level.ToString();
        SetRewardCheckImage(false);
        _ICanRewardEffect.Stop();
    }

    public void SetRewardCheckImage(bool IsGet_)
    {
        _RewardCheckImage.gameObject.SetActive(IsGet_);
        _RewardNoRankCheckImage.gameObject.SetActive(IsGet_);
    }
    public void SetRewardGetCheck(bool IsGet_)
    {
        _RewardBtn.gameObject.SetActive(IsGet_);
        _RewardNoRankBtn.gameObject.SetActive(IsGet_);
        if (IsGet_)
            _ICanRewardEffect.Play();
        else
            _ICanRewardEffect.Stop();
    }

    public void GetReward()
    {
        SetRewardGetCheck(false);
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _ICanRewardEffect.Stop();
        CGlobal.NetControl.Send(new SRankRewardNetCs());
        CGlobal.RewardPopup.ViewPopup(EText.Global_Button_Receive, CGlobal.MetaData.RewardItems[RankRewardData.RewardCode].RewardList);
        CGlobal.LoginNetSc.User.LastGotRewardRankIndex++;
    }
}
