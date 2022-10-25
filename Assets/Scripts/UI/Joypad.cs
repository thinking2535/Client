using UnityEngine;
using UnityEngine.UI;

public class Joypad : MonoBehaviour
{
    SpriteRenderer _stick;

    void Awake()
    {
        var frameGameObject = new GameObject("frame");
        frameGameObject.transform.localScale = new Vector3(0.18f, 0.18f, 1.0f);
        frameGameObject.transform.SetParent(transform, false);
        var frameSpriteRenderer = frameGameObject.AddComponent<SpriteRenderer>();
        frameSpriteRenderer.sprite = Resources.Load<Sprite>("Textures/joystick_bg");

        var stickGameObject = new GameObject("stick");
        stickGameObject.transform.localScale = new Vector3(0.26f, 0.26f, 1.0f);
        stickGameObject.transform.SetParent(transform, false);
        _stick = stickGameObject.AddComponent<SpriteRenderer>();
        _stick.sprite = Resources.Load<Sprite>("Textures/joystick");
    }
    public void setStickLocalPosition(Vector2 localPosition)
    {
        _stick.transform.localPosition = localPosition;
    }
}
