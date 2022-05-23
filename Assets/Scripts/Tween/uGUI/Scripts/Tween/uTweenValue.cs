using UnityEngine;
using System.Collections;

namespace uTools {
	public class uTweenValue : uTweener {

		public float from;
		public float to;

		float mValue;

		public float value {
			get { return mValue;}
			set { 
				mValue = value;
			}
		}

		virtual protected void ValueUpdate(float value, bool isFinished) {}

		protected override void OnUpdate (float factor, bool isFinished) {
			value = from + factor * (to - from);
			ValueUpdate(value, isFinished);		
		}

        public static T Begin<T>(GameObject _go, float _duration, float _from, float _to) where T : uTweenValue
        {
            T comp = _go.GetComponent<T>();
            if (comp == null)
                comp = _go.AddComponent<T>();

            comp.Init();

            comp.from = _from;
            comp.to = _to;
            comp.duration = _duration;
            comp.enabled = true;

            return comp;
        }

        [ContextMenu("Assume value of 'From'")]
        public override void SetCurrentValueToStart() { value = from; }
    }
}
