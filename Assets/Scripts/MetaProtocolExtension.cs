using bb;
using System;
using UnityEngine;

public static class MetaProtocolExtension
{
    const string _IconDirectory = "GUI/Port/";
    public static Sprite GetSprite(this SCharacterMeta Self_)
    {
        return Resources.Load<Sprite>(_IconDirectory + Self_.meta.IconName);
    }
    public static Texture GetTexture(this SCharacterMeta Self_)
    {
        return Resources.Load<Texture>(_IconDirectory + Self_.meta.IconName);
    }
    public static string GetText(this SCharacterMeta Self_)
    {
        return CGlobal.MetaData.getText(Self_.meta.name);
    }
    public static string GetDescriptionText(this SCharacterMeta Self_)
    {
        return CGlobal.MetaData.getText(Self_.typeMeta.description);
    }
    public static GradeInfo getGradeInfo(this SCharacterMeta self)
    {
        return CGlobal.getGradeInfo(self.grade);
    }
    public static string getGradeText(this SCharacterMeta self)
    {
        var gradeInfo = self.getGradeInfo();
        var gradeText = CGlobal.MetaData.getText(gradeInfo.textName);

        if (gradeInfo.subGradeCount > 1)
        {
            var subGradeString = "";
            for (Int32 i = 0; i < self.subGrade + 1; ++i)
                subGradeString += "I";

            if (subGradeString.Length > 0)
                gradeText += (" " + subGradeString);
        }

        return gradeText;
    }
    public static Int32 getCostValue(this ExchangeValue self, Int32 targetData)
    {
        return (Int32)(targetData * self.rate); // targetData 가 정수이고 최소 1 이므로 Ceiling 하지 않음
    }
    public static ResourceTypeData getCost(this CharacterTypeMeta self)
    {
        var costType = self.getCostType();
        if (costType == EResource.Null)
            return new ResourceTypeData();

        return new ResourceTypeData(costType, self.CostValue);
    }
    public static EResource getCostType(this CharacterTypeMeta self)
    {
        if (self.howToGet == EResource.Ticket.ToString())
            return EResource.Ticket;

        else if (self.howToGet == EResource.Gold.ToString())
            return EResource.Gold;

        else if (self.howToGet == EResource.Dia00.ToString())
            return EResource.Dia00;

        else if (self.howToGet == EResource.Dia01.ToString())
            return EResource.Dia01;

        else if (self.howToGet == EResource.Dia02.ToString())
            return EResource.Dia02;

        else if (self.howToGet == EResource.Dia03.ToString())
            return EResource.Dia03;

        else
            return EResource.Null;
    }
    public static bool isNFTCharacter(this CharacterTypeMeta self)
    {
        return self.howToGet == "NFT";
    }
    public static bool isShopCharacter(this CharacterTypeMeta self)
    {
        return self.getCostType() != EResource.Null;
    }
    public static bool isRewardCharacter(this CharacterTypeMeta self)
    {
        return self.howToGet == "Reward";
    }
    public static bool canBuy(this CharacterTypeMeta self)
    {
        return self.isShopCharacter() || self.isNFTCharacter();
    }
}