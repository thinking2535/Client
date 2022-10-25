using bb;
using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class CUnitReward
{
    public abstract void SetToLimitedItemSet(LimitedItemSet limitedItemSet);
    public abstract RewardItemSet GetRewardItemSet();
    public abstract Sprite GetSprite();
    public abstract Sprite getBigSprite();
    public abstract string GetText();
    public abstract Task NotifyToCurrentScene();
}
public class CUnitRewardResource : CUnitReward
{
    public SResourceTypeData resourceTypeData;
    public CUnitRewardResource(SResourceTypeData resourceTypeData)
    {
        this.resourceTypeData = resourceTypeData;
    }
    public override void SetToLimitedItemSet(LimitedItemSet limitedItemSet)
    {
        limitedItemSet.SetResource(GetSprite(), resourceTypeData.Data);
    }
    public override RewardItemSet GetRewardItemSet()
    {
        return UnityEngine.Object.Instantiate<RewardItemSet>(Resources.Load<RewardItemSet>("Prefabs/UI/complex/RewardItemSetResource"));
    }
    public override Sprite GetSprite()
    {
        return CGlobal.GetResourceSprite(resourceTypeData.Type);
    }
    public override Sprite getBigSprite()
    {
        return CGlobal.GetResourceBigSprite(resourceTypeData.Type, resourceTypeData.Data);
    }
    public override string GetText()
    {
        return resourceTypeData.Data.ToString();
    }
    public override Task NotifyToCurrentScene()
    {
        // 보상 연출을 늦게 할 경우 비용지불에 대한 처리도 늦게 되고,
        // 비용 처리만 Cs 보내는 시점에 처리 할 경우 진행 가능한 오류 발생시 롤백 문제가 있고
        // Sc시점에는 비용을 알 수 없으므로 그냥 Sc시점에 연출 해버림.
        return Task.CompletedTask;
    }
}
public class CUnitRewardCharacter : CUnitReward
{
    public SCharacterMeta Meta;
    public bool alreadyHave;
    public CUnitRewardCharacter(SCharacterMeta Meta_, bool alreadyHave)
    {
        Meta = Meta_;
        this.alreadyHave = alreadyHave;
    }
    public override void SetToLimitedItemSet(LimitedItemSet limitedItemSet)
    {
        limitedItemSet.SetCharacter(GetSprite());
    }
    public override RewardItemSet GetRewardItemSet()
    {
        return UnityEngine.Object.Instantiate<RewardItemSet>(Resources.Load<RewardItemSet>("Prefabs/UI/complex/RewardItemSetCharacter"));
    }
    public override Sprite GetSprite()
    {
        return Meta.GetSprite();
    }
    public override Sprite getBigSprite()
    {
        return GetSprite();
    }
    public override string GetText()
    {
        return Meta.GetText();
    }
    public override async Task NotifyToCurrentScene()
    {
        await CGlobal.curScene.pushCharacterInfoPopup(Meta, true, alreadyHave);
    }
}