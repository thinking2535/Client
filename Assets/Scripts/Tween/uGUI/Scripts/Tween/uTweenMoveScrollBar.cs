using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace uTools
{
    [AddComponentMenu("uTools/Tween/Tween MoveScrollBar(uTools)")]
    public class uTweenMoveScrollBar : uTweenValue
    {
        public bool horizontal = false;
        public bool vertical = false;

        private ScrollRect mScrollRect;
        private ScrollRect scrollRect
        {
            get
            {
                if (mScrollRect == null)
                    mScrollRect = GetComponent<ScrollRect>();

                return mScrollRect;
            }
        }

        float mNormalizedPosition = 0f;
        public float normalizedPosition
        {
            get
            {
                return mNormalizedPosition;
            }
            set
            {
                SetValue(value);
                mNormalizedPosition = value;
            }
        }

        protected override void ValueUpdate(float value, bool isFinished)
        {
            normalizedPosition = value;
        }

        void SetValue(float value)
        {
            if (horizontal)
            {
                scrollRect.horizontalNormalizedPosition = value;
            }

            if (vertical)
            {
                scrollRect.verticalNormalizedPosition = value;
            }
        }

        public override void SetStartToCurrentValue()
        {
            if (horizontal)
            {
                from = scrollRect.horizontalNormalizedPosition;
            }
            else if (vertical)
            {
                from = scrollRect.verticalNormalizedPosition;
            }
        }
    }
}
