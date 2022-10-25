using bb;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CreatePopup : ModalDialog
{
    [SerializeField] Text _title = null;
    [SerializeField] InputField _NickName = null;
    [SerializeField] Button _OkButton = null;

    protected override void Awake()
    {
        base.Awake();

        _title.text = CGlobal.MetaData.getText(EText.Global_Popup_Notice);

        _NickName.placeholder.GetComponent<Text>().text = CGlobal.MetaData.getText(EText.LoginScene_MakeNickName);
        _OkButton.onClick.AddListener(OnClickOk);
        _OkButton.GetComponentInChildren<Text>().text = CGlobal.MetaData.getText(EText.Global_Button_Ok);
    }
    protected override void OnDestroy()
    {
        _OkButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public async void OnClickOk()
    {
        CGlobal.Sound.PlayOneShot((Int32)ESound.Ok);

        if (!await CGlobal.curScene.checkNicknameAndPushNoticePopup(_NickName.text))
            return;

        CGlobal.curScene.popDialog(_NickName.text);
    }
}
