using bb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipPopup : ModalDialog
{
    [SerializeField] GameObject ToolTipScroll = null;
    [SerializeField] Text ToolTipText = null;
    [SerializeField] Text ToolTipScrollText = null;
    [SerializeField] GameObject ToolTipScrollContent = null;
    [SerializeField] Button _closeButton;

    public static int ToolTipCheckLength = 200;
    protected override void Awake()
    {
        base.Awake();

        _closeButton.onClick.AddListener(_backButtonClicked);
    }
    protected override void OnDestroy()
    {
        _closeButton.onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public void init(EText textName)
    {
        var message = CGlobal.MetaData.getText(textName);

        if(message.Length > ToolTipCheckLength)
        {
            ToolTipScroll.SetActive(true);
            ToolTipText.gameObject.SetActive(false);
            ToolTipScrollText.gameObject.SetActive(true);
            ToolTipScrollText.text = message;
            ToolTipScrollContent.transform.localPosition = new Vector3(0.0f, ToolTipScrollContent.GetComponent<RectTransform>().rect.height * -1, 0.0f);
        }
        else
        {
            ToolTipScroll.SetActive(false);
            ToolTipText.gameObject.SetActive(true);
            ToolTipScrollText.gameObject.SetActive(false);
            ToolTipText.text = message;
        }
    }

    async void _backButtonClicked()
    {
        await backButtonPressed();
    }
}
