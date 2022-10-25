using bb;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RankingRewardPanel : MonoBehaviour
{
    [SerializeField] GameObject _GuageSmallBar = null;
    [SerializeField] GameObject _GuageLargeBar = null;
    [SerializeField] Text _GuageSmallBarText = null;
    [SerializeField] Text _GuageLargeBarText = null;
    [SerializeField] Text _GuageLargeBarTierText = null;
    [SerializeField] Text _RewardText = null;
    [SerializeField] Image _RewardImage = null;
    [SerializeField] Image _RewardCheckImage = null;
    [SerializeField] Button _RewardBtn = null;

    [SerializeField] ParticleSystem _ICanRewardEffect = null;

    Int32 _rewardIndex;
    public SRankReward rankReward = null;
    void Awake()
    {
        _RewardBtn.onClick.AddListener(_getReward);
    }
    void OnDestroy()
    {
        _RewardBtn.onClick.RemoveAllListeners();
    }
    public void InitForFirst()
    {
        _GuageSmallBar.SetActive(true);
        _GuageLargeBar.SetActive(false);
        _RewardBtn.gameObject.SetActive(false);
        _GuageSmallBarText.text = "0";
        _GuageLargeBarText.text = "";
        _GuageLargeBarTierText.text = "";
        _ICanRewardEffect.Stop();
    }
    public void Init(Int32 rewardIndex)
    {
        _rewardIndex = rewardIndex;
        rankReward = CGlobal.MetaData.RankRewards[_rewardIndex];

        var RankData = CGlobal.MetaData.RankTiers.ContainsKey(rankReward.Meta.point) ? CGlobal.MetaData.RankTiers[rankReward.Meta.point] : null;
        _GuageSmallBar.SetActive(RankData == null);
        _GuageLargeBar.SetActive(RankData != null);
        _RewardCheckImage.gameObject.SetActive(false);

        if (RankData != null)
        {
            _RewardBtn.image.sprite = Resources.Load<Sprite>("GUI/Buttons/img_tab_9btnYelOn");
            var spriteState = new SpriteState();
            spriteState.pressedSprite = Resources.Load<Sprite>("GUI/Buttons/img_tab_9btnYelOff");
            spriteState.disabledSprite = Resources.Load<Sprite>("GUI/Buttons/img_tab_9btnYelOff");
            _RewardBtn.spriteState = spriteState;

            _GuageSmallBarText.text = RankData.MaxPoint.ToString();
            _GuageLargeBarText.text = RankData.MaxPoint.ToString();
            _GuageLargeBarTierText.text = RankData.Level.ToString();
            _RewardImage.SetNativeSize();
        }
        else
        {
            _RewardBtn.image.sprite = Resources.Load<Sprite>("GUI/Buttons/img_tab_9btnBlueOn");
            var spriteState = new SpriteState();
            spriteState.pressedSprite = Resources.Load<Sprite>("GUI/Buttons/img_tab_9btnBlueOff");
            spriteState.disabledSprite = Resources.Load<Sprite>("GUI/Buttons/img_tab_9btnBlueOff");
            _RewardBtn.spriteState = spriteState;

            _GuageSmallBarText.text = rankReward.Meta.point.ToString();
        }

        _RewardText.text = rankReward.Reward.GetText();
        _RewardImage.sprite = rankReward.Reward.getBigSprite();
        _ICanRewardEffect.Stop();

        update();
    }
    public void update()
    {
        _RewardBtn.enabled = canReward();

        if (_RewardBtn.enabled)
        {
            _RewardCheckImage.sprite = Resources.Load<Sprite>("GUI/Contents/img_rankRewardArrow");
            _ICanRewardEffect.Play();
        }
        else
        {
            if (_rewardIndex < CGlobal.LoginNetSc.User.NextRewardRankIndex)
                _RewardCheckImage.sprite = Resources.Load<Sprite>("Textures/Lobby_Resources/ico_check");

            _ICanRewardEffect.Stop();
        }

        if (_RewardCheckImage.sprite != null)
            _RewardCheckImage.gameObject.SetActive(true);
    }
    public bool canReward()
    {
        return _rewardIndex == CGlobal.LoginNetSc.User.NextRewardRankIndex && CGlobal.LoginNetSc.User.Point >= rankReward.Meta.point;
    }
    void _getReward()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);
        _ICanRewardEffect.Stop();
        CGlobal.NetControl.Send(new SRankRewardNetCs());
        CGlobal.ProgressCircle.Activate();
    }
}
