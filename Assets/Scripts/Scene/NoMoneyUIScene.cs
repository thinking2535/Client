public class NoMoneyUIScene : BaseScene
{
    public new void init()
    {
        base.init();

        CGlobal.MoneyUI.hide(); // 다음Scene 의 init 이 호출 된 후에 직전 Scene의 OnDestory가 호출되므로 NoMoneyUIScene을 두어 처리
    }
}
