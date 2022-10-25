using bb;
using System.Threading.Tasks;
using UnityEngine;

public static class GlobalFunction
{
    public static Task<dynamic> ShowResourceNotEnough(EResource Resource_)
    {
        return CGlobal.curScene.pushNoticePopup(true, EText.Global_Popup_ResourceNotEnough,CGlobal.GetResourceText(Resource_));
    }
    public static bool isBackButtonPressed()
    {
        // Backbutton에 가장 먼저 반응하는 팝업에 대한 일반적인 Pop을 Main.Update에서 처리하기 위해서는
        // 팝업에 대한 Backbutton또는 Touch에 대한 개별처리(팝업 애니메이션을 스킵 한다든지)를 제거하고
        // Key 입력 이벤트를 Consume 할 수 있는 KeyInputController 를 사용하여 처리할것.

        return Input.GetKeyDown(KeyCode.Escape);
    }
}
