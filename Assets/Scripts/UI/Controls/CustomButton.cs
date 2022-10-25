using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButton : Button, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum TransitionType { ColorTint, SpriteSwap, ActivateObject }

    [System.Serializable]
    public struct ActivateState
    {
        public GameObject _normal;
        public GameObject _highlighted;
        public GameObject _pressed;
        public GameObject _disabled;
    }

    [System.Serializable]
    public class TransitionInfo
    {
        public TransitionType _transitionType;
        public Graphic _targetGraphic;
        public ColorBlock _colors;
        public SpriteState _spriteState;
        public ActivateState _activateState;

#if UNITY_EDITOR
        public bool _foldOut { get; set; }
#endif
    }

    public List<TransitionInfo> transitionInfos;
    public float pressedScale = 0.9f;
    public float pressedScaleDuration = 0.1f;

    protected Vector3 localScale = Vector3.one;

    public bool repeatEnabled;
    public float repeatTime = 0.3f;

    private float delayTime = 0.5f;
    private float elaspeTime = 0.0f;
    private bool pressed = false;
    private int clickCount = 0;

    protected override void Awake()
    {
        base.Awake();
        this.targetGraphic = null;

        localScale = transform.localScale;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        this.ChangeTransition(interactable ? Selectable.SelectionState.Normal : Selectable.SelectionState.Disabled, true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        transform.localScale = localScale;

        pressed = false;
    }
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        this.targetGraphic = null;
    }
#endif
    private void Update()
    {
        if (repeatEnabled == false || pressed == false)
            return;

        //if (IsStart == false)
        //    return;

        if (delayTime > 0f)
        {
            delayTime -= Time.deltaTime;
            return;
        }

        //
        elaspeTime += Time.deltaTime;
        //repeatTime = interval;
        if (elaspeTime > repeatTime)
        {
            clickCount++;

            //if (button != null)
            //    button.onClick.Invoke();
            onClick.Invoke();

            elaspeTime = 0.0f;
        }
    }
    protected void ChangeTransition(Selectable.SelectionState state, bool instant)
    {
        //
        if (state == Selectable.SelectionState.Pressed)
        {
            // scale..
            uTools.uTweenScale.Begin(gameObject, transform.localScale, pressedScale * localScale, instant ? 0.0f : pressedScaleDuration);

        }
        else
        {
            // scale
            uTools.uTweenScale.Begin(gameObject, transform.localScale, localScale, instant ? 0.0f : pressedScaleDuration);
        }

        //
        if (transitionInfos != null && transitionInfos.Count > 0)
        {
            var enumerator = transitionInfos.GetEnumerator();
            while (enumerator.MoveNext())
            {
                TransitionInfo info = enumerator.Current;
                if (info._transitionType != TransitionType.ActivateObject && info._targetGraphic == null)
                    continue;

                Image image = info._targetGraphic as Image;
                switch (state)
                {
                    case Selectable.SelectionState.Normal:
                        if (info._transitionType == TransitionType.ColorTint)
                        {
                            info._targetGraphic.CrossFadeColor(info._colors.normalColor, instant ? 0.0f : info._colors.fadeDuration, true, true);
                        }
                        else if (image != null && info._transitionType == TransitionType.SpriteSwap)
                        {
                            image.overrideSprite = null;
                        }
                        else if (info._transitionType == TransitionType.ActivateObject)
                        {
                            if (info._activateState._highlighted != null) info._activateState._highlighted.SetActive(false);
                            if (info._activateState._pressed != null) info._activateState._pressed.SetActive(false);
                            if (info._activateState._disabled != null) info._activateState._disabled.SetActive(false);

                            if (info._activateState._normal != null)
                            {
                                info._activateState._normal.SetActive(false);
                                info._activateState._normal.SetActive(true);
                            }
                        }
                        break;

                    case Selectable.SelectionState.Highlighted:
                        if (info._transitionType == TransitionType.ColorTint)
                        {
                            info._targetGraphic.CrossFadeColor(info._colors.highlightedColor, instant ? 0.0f : info._colors.fadeDuration, true, true);
                        }
                        else if (image != null && info._transitionType == TransitionType.SpriteSwap)
                        {
                            image.overrideSprite = info._spriteState.highlightedSprite;
                        }
                        else if (info._transitionType == TransitionType.ActivateObject)
                        {
                            if (info._activateState._normal != null) info._activateState._normal.SetActive(false);
                            if (info._activateState._pressed != null) info._activateState._pressed.SetActive(false);
                            if (info._activateState._disabled != null) info._activateState._disabled.SetActive(false);

                            if (info._activateState._highlighted != null)
                            {
                                info._activateState._highlighted.SetActive(false);
                                info._activateState._highlighted.SetActive(true);
                            }
                        }
                        break;

                    case Selectable.SelectionState.Pressed:
                        if (info._transitionType == TransitionType.ColorTint)
                        {
                            info._targetGraphic.CrossFadeColor(info._colors.pressedColor, instant ? 0.0f : info._colors.fadeDuration, true, true);
                        }
                        else if (image != null && info._transitionType == TransitionType.SpriteSwap)
                        {
                            image.overrideSprite = info._spriteState.pressedSprite;
                        }
                        else if (info._transitionType == TransitionType.ActivateObject)
                        {
                            if (info._activateState._normal != null) info._activateState._normal.SetActive(false);
                            if (info._activateState._highlighted != null) info._activateState._highlighted.SetActive(false);
                            if (info._activateState._disabled != null) info._activateState._disabled.SetActive(false);

                            if (info._activateState._pressed != null)
                            {
                                info._activateState._pressed.SetActive(false);
                                info._activateState._pressed.SetActive(true);
                            }
                        }
                        break;

                    case Selectable.SelectionState.Disabled:
                        if (info._transitionType == TransitionType.ColorTint)
                        {
                            info._targetGraphic.CrossFadeColor(info._colors.disabledColor, instant ? 0.0f : info._colors.fadeDuration, true, true);
                        }
                        else if (image != null && info._transitionType == TransitionType.SpriteSwap)
                        {
                            image.overrideSprite = info._spriteState.disabledSprite;
                        }
                        else if (info._transitionType == TransitionType.ActivateObject)
                        {
                            if (info._activateState._normal != null) info._activateState._normal.SetActive(false);
                            if (info._activateState._highlighted != null) info._activateState._highlighted.SetActive(false);
                            if (info._activateState._pressed != null) info._activateState._pressed.SetActive(false);

                            if (info._activateState._disabled != null)
                            {
                                info._activateState._disabled.SetActive(false);
                                info._activateState._disabled.SetActive(true);
                            }
                        }
                        break;
                }
            }
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable)
            return;

        this.ChangeTransition(Selectable.SelectionState.Pressed, false);

        delayTime = 0.5f;
        clickCount = 0;
        elaspeTime = 0.0f;
        pressed = true;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable)
            return;

        if (pressed )//&& _isPointerIn)
            this.ChangeTransition(Selectable.SelectionState.Highlighted, false);
        else
            this.ChangeTransition(Selectable.SelectionState.Normal, false);

        delayTime = 0.5f;
        pressed = false;
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!interactable)
            return;

        this.ChangeTransition(Selectable.SelectionState.Highlighted, false);

        pressed = false;
        //isPointerIn = true;
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!interactable)
            return;

        this.ChangeTransition(Selectable.SelectionState.Normal, false);

        pressed = false;
        //isPointerIn = false;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (clickCount > 0 || pressed)
        {
            eventData.eligibleForClick = false;
            clickCount = 0;
        }

        base.OnPointerClick(eventData);
    }


}
