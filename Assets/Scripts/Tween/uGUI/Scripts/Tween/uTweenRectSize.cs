using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace uTools
{
    [AddComponentMenu("uTools/Tween/Tween RectSize(uTools)")]
    public class uTweenRectSize : uTweener
    {
        public Vector2 from;
        public Vector2 to;

        RectTransform mRectTransform;
        public RectTransform cachedRectTransform { get { if (mRectTransform == null) mRectTransform = GetComponent<RectTransform>(); return mRectTransform; } }

        public Vector2 value
        {
            get  { return cachedRectTransform.sizeDelta; }
            set
            {
                if (cachedRectTransform == null)
                    return;

                cachedRectTransform.sizeDelta = value;
            }
        }

        protected override void OnUpdate(float factor, bool isFinished)
        {
            value = from + factor * (to - from);
        }

        [ContextMenu("Assume value of 'From'")]
        public override void SetCurrentValueToStart() { value = from; }

        public static uTweenRectSize Begin(GameObject _go, float _duration, Vector2 _from, Vector2 _to)
        {
            uTweenRectSize comp = _go.GetComponent<uTweenRectSize>();
            if (comp == null)
                comp = _go.AddComponent<uTweenRectSize>();

            comp.Init();

            comp.from = _from;
            comp.to = _to;
            comp.duration = _duration;
            comp.enabled = true;

            return comp;
        }
    }
}
