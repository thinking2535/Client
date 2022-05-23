using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] Text _txtMsg = null;
    public void Show(string Msg_)
    {
        _txtMsg.text = Msg_;
        gameObject.SetActive(true);
    }
    public void Ok()
    {
        gameObject.SetActive(false);
        CGlobal.SceneSetNext(new CSceneIntro());
    }
}
