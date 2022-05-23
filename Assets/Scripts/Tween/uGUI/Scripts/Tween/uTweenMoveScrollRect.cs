using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace uTools
{
    [AddComponentMenu("uTools/Tween/Tween Move ScrollRect (uTools)")]
    public class uTweenMoveScrollRect : uTweener
    {
        public Vector2 from;
        public Vector2 to;
        public Vector2 prevValue = Vector2.zero;

        RectTransform mRectTransform;
        ScrollRect mScrollRect;

        public ScrollRect cachedScrollRect { get { if (mScrollRect == null) mScrollRect = GetComponent<ScrollRect>(); return mScrollRect; } }
        public RectTransform cachedRectTransform { get { if (mRectTransform == null) mRectTransform = GetComponent<RectTransform>(); return mRectTransform; } }

        public Vector2 value
        {
            get { return prevValue; }
            set
            {
                if (prevValue == Vector2.zero)
                    prevValue = from;

                if (cachedScrollRect != null)
                {
                    PointerEventData eventData = new PointerEventData(EventSystem.current);
                    eventData.scrollDelta = value - prevValue;
                    cachedScrollRect.OnScroll(eventData);
                }

                prevValue = value;
            }
        }

        protected override void OnUpdate(float factor, bool isFinished)
        {
            value = from + factor * (to - from);
        }

        public static uTweenMoveScrollRect Begin(ScrollRect scrollRect, Vector2 from, Vector2 to, float duration = 1f, float delay = 0f)
        {
            uTweenMoveScrollRect comp = uTweener.Begin<uTweenMoveScrollRect>(scrollRect.gameObject, duration);
            comp.from = from;
            comp.to = to;
            comp.duration = duration;
            comp.delay = delay;
            if (duration <= 0)
            {
                comp.Sample(1, true);
                comp.enabled = false;
            }
            return comp;
        }

        [ContextMenu("Set 'From' to current value")]
        public override void SetStartToCurrentValue() { from = value; }

        [ContextMenu("Set 'To' to current value")]
        public override void SetEndToCurrentValue() { to = value; }

        [ContextMenu("Assume value of 'From'")]
        public override void SetCurrentValueToStart() { value = from; }

        [ContextMenu("Assume value of 'To'")]
        public override void SetCurrentValueToEnd() { value = to; }
    }
}
