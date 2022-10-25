using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.Selectable;

public class TextButton : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] Text _text;
    public Image image
    {
        get => _button.image;
        set => _button.image = value;
    }
    public string text
    {
        get => _text.text;
        set => _text.text = value;
    }
    public void AddListener(UnityAction call)
    {
        _button.onClick.AddListener(call);
    }
    void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
