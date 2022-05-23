using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace uTools {
	[AddComponentMenu("uTools/Tween/Tween Object Position(uTools)")]
	public class uTweenObjectPosition : uTweener
    {
        public enum MoveType { Fixed_Local, Fixed_World, Relative_Local, Relative_World }
        public MoveType _moveType = MoveType.Relative_Local;

        public RectTransform fromObjectPos;
        public RectTransform toObjectPos;

        public Vector3 from;
		public Vector3 to;

        private Vector3 orgPosition;		

		private Transform mTransform;		
		public Transform cachedTransform
        {
            get
            {
                if (mTransform == null)
                    mTransform = GetComponent<Transform>();
                return mTransform;
            }
        }

        protected void Awake()
        {
            orgPosition = cachedTransform.position;
        }

        public override void Play(PlayDirection dir = PlayDirection.Forward)
        {
            if (fromObjectPos != null)
                from = fromObjectPos.position;

            if (toObjectPos != null)
                to = toObjectPos.position;

            base.Play(dir);
        }

        protected override void OnUpdate (float factor, bool isFinished)
		{
            if(_moveType == MoveType.Fixed_Local)
            {
                cachedTransform.localPosition = from + factor * (to - from);
            }
            else if (_moveType == MoveType.Fixed_World)
            {
                cachedTransform.position = from + factor * (to - from);
            }
            else if (_moveType == MoveType.Relative_Local)
            {
                Vector3 fromPosition = orgPosition + from.x * cachedTransform.right + from.y * cachedTransform.up + from.z * cachedTransform.forward;
                Vector3 toPosition = orgPosition + to.x * cachedTransform.right + to.y * cachedTransform.up + to.z * cachedTransform.forward;
                cachedTransform.position = Vector3.Lerp(fromPosition, toPosition, factor);
            }
            else if (_moveType == MoveType.Relative_World)
            {
                cachedTransform.position = orgPosition + (from + factor * (to - from));
            }
        }

        public static uTweenObjectPosition Begin(GameObject _go, float _duration, MoveType _moveType, Vector3 _from, Vector3 _to)
        {
            uTweenObjectPosition comp = _go.GetComponent<uTweenObjectPosition>();
            if (comp == null)
                comp = _go.AddComponent<uTweenObjectPosition>();

            comp.Init();
            comp.duration = _duration;
            comp._moveType = _moveType;
            comp.from = _from;
            comp.to = _to;
            comp.enabled = true;
            comp.Play();

            return comp;
        }

        [ContextMenu("Assume value of 'From'")]
        public override void SetCurrentValueToStart() { Sample(0.0f, false); }
    }
}