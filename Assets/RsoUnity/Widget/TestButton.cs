using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
public sealed class TestButton : Button
{
    [MenuItem("GameObject/UI/TextButton")]
    static void _createTestButton(MenuCommand menuCommand)
    {
        var newObject = new GameObject("textButton");

        // 메뉴로클릭하여 textButton을 생성시 어떤 오브젝트도 마우스로 선택하지 않아서
        // menuCommand.context가 null인 경우 아래 Undo.RegisterCreatedObjectUndo 를 호출하면
        // 객체가 생성되지 않음
        // 하지만 아래 라인만 빼면 menuCommand.context 가 null 이어도 생성이 됨
        Undo.RegisterCreatedObjectUndo(newObject, "Create TextButton"); // 실행취소에 등록

        //newObject.AddComponent<TestButton>();
        //newObject.transform.parent = ((GameObject)menuCommand.context).transform;
        if (menuCommand.context != null)
            newObject.transform.SetParent(((GameObject)menuCommand.context).transform);
    }

    [SerializeField] Text _text;
    //protected override void Awake()
    //{
    //    base.Awake();

    //    image = gameObject.AddComponent<Image>();

    //    var textObject = new GameObject("text");
    //    textObject.AddComponent<Text>();
    //    textObject.transform.SetParent(transform);

    //    Selection.activeObject = this;
    //}
    protected override void OnDestroy()
    {
        onClick.RemoveAllListeners();

        base.OnDestroy();
    }
    public string text
    {
        get => _text.text;
        set => _text.text = value;
    }
    public void setPressedSprite(Sprite sprite)
    {
        var copyOfSpriteState = spriteState;
        copyOfSpriteState.pressedSprite = sprite;
        spriteState = copyOfSpriteState;
    }
    public void setHighlightedSprite(Sprite sprite)
    {
        var copyOfSpriteState = spriteState;
        copyOfSpriteState.highlightedSprite = sprite;
        spriteState = copyOfSpriteState;
    }
    public void setSelectedSprite(Sprite sprite)
    {
        var copyOfSpriteState = spriteState;
        copyOfSpriteState.selectedSprite = sprite;
        spriteState = copyOfSpriteState;

    }
    public void setDisabledSprite(Sprite sprite)
    {
        var copyOfSpriteState = spriteState;
        copyOfSpriteState.disabledSprite= sprite;
        spriteState = copyOfSpriteState;

    }
}
#endif