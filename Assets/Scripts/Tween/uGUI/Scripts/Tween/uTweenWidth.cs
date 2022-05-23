using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uTools
{
	[AddComponentMenu("uTools/Tween/Tween Width(uTools)")]
	public class uTweenWidth : uTweenValue
	{
		RectTransform mRectTransform;
		public RectTransform cachedRectTransform { get { if (mRectTransform == null) mRectTransform = GetComponent<RectTransform>(); return mRectTransform; } }

		public float width
		{
			get { return cachedRectTransform.sizeDelta.x; }
			set
			{
				if (cachedRectTransform == null)
					return;

				var sizeDelta = cachedRectTransform.sizeDelta;
				sizeDelta.x = value;
				cachedRectTransform.sizeDelta = sizeDelta;
			}
		}

		protected override void ValueUpdate(float value, bool isFinished)
		{
			width = value;
		}

		[ContextMenu("Assume value of 'From'")]
		public override void SetCurrentValueToStart() { width = from; }
	}
}
