using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace uTools
{
    [AddComponentMenu("uTools/Tween/Tween Height(uTools)")]
    public class uTweenHeight : uTweenValue
    {
        RectTransform mRectTransform;
        public RectTransform cachedRectTransform { get { if (mRectTransform == null) mRectTransform = GetComponent<RectTransform>(); return mRectTransform; } }

        public float height
        {
            get  { return cachedRectTransform.sizeDelta.y; }
            set
            {
                if (cachedRectTransform == null)
                    return;

                var sizeDelta = cachedRectTransform.sizeDelta;
                sizeDelta.y = value;
                cachedRectTransform.sizeDelta = sizeDelta;
            }
        }

        protected override void ValueUpdate(float value, bool isFinished)
        {
            height = value;
        }

        [ContextMenu("Assume value of 'From'")]
        public override void SetCurrentValueToStart() { height = from; }
    }
}
