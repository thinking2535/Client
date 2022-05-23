using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace uTools {
	[AddComponentMenu("uTools/Internal/Play Tween All(uTools)")]
	public class uPlayTweenAll : MonoBehaviour, uIPointHandler {
        public bool includeInChildren = true;
        public bool isTrigger = true;
        public PlayDirection playDirection = PlayDirection.Forward;
		public Trigger trigger = Trigger.OnPointerClick;
        public bool useDelayTween = false;
        public float delayTweenInterval = 0.0f;

        public uTweener[] tweenTargets
        {
            get
            {
                if (includeInChildren)
                    return GetComponentsInChildren<uTweener>();

                return GetComponents<uTweener>();
            }
        }

        void OnEnable() {
            TriggerPlay(Trigger.OnEnable);
        }

        void Start()
        {
            TriggerPlay(Trigger.Start);
        }

        public void OnPointerEnter (PointerEventData eventData) {
			TriggerPlay (Trigger.OnPointerEnter);
		}

		public void OnPointerDown (PointerEventData eventData) {
			TriggerPlay (Trigger.OnPointerDown);
		}

		public void OnPointerClick (PointerEventData eventData) {
			TriggerPlay (Trigger.OnPointerClick);
		}

		public void OnPointerUp (PointerEventData eventData) {
			TriggerPlay (Trigger.OnPointerUp);
		}

		public void OnPointerExit (PointerEventData eventData) {
			TriggerPlay (Trigger.OnPointerExit);
		}

		private void TriggerPlay(Trigger _trigger) {
            if (isTrigger == false)
                return;

			if (_trigger == trigger) {
				Play();
			}
		}

		/// <summary>
		/// Play this instance.
		/// </summary>
		private void Play() {
            uTweener[] tweenTarget = null;
            if (includeInChildren)
                tweenTarget = GetComponentsInChildren<uTweener>();
            else
                tweenTarget = GetComponents<uTweener>();

            if (playDirection == PlayDirection.Toggle)
            {
                for (int ii = 0; ii < tweenTarget.Length; ++ii)
                {
                    tweenTarget[ii].Toggle();
                }
			}
			else
            {
                for (int ii = 0; ii < tweenTarget.Length; ++ii)
                {
                    if (useDelayTween)
                        tweenTarget[ii].delay = ii * delayTweenInterval;

                    tweenTarget[ii].Stop();
                    tweenTarget[ii].Play(playDirection);
                }
			}
		}

        public void PlayNow()
        {
            Play();
        }

        public void Reset()
        {
            uTweener[] tweenTarget = null;
            if (includeInChildren)
                tweenTarget = GetComponentsInChildren<uTweener>();
            else
                tweenTarget = GetComponents<uTweener>();

            foreach (var tw in tweenTarget)
            {
                if (tw == null)
                    continue;

                tw.Reset();
            }
        }
    }
}