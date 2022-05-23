using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;

namespace uTools
{
    [CustomEditor(typeof(uPlayTweenAll), true)]
    public class uPlayTweenAllEditor : Editor
    {
        List<uTweener> tweenList = new List<uTweener>(0);
        bool playTween = false;

        private void OnEnable()
        {
            EditorApplication.update -= Update;
            EditorApplication.update += Update;
        }

        private void OnDisable()
        {
            EditorApplication.update -= Update;
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Play Now"))
            {
                if (Application.isPlaying)
                {
                    uPlayTweenAll playTween = (target as uPlayTweenAll);
                    if (playTween != null)
                        playTween.PlayNow();
                }
                else
                {
                    PlayTween();
                }
            }

            GUILayout.EndHorizontal();
        }

        void Update()
        {
            if (tweenList.Count <= 0 || playTween == false)
                return;

            bool playComplete = true;
            foreach (uTweener tween in tweenList)
            {
                if (tween.enabled == false)
                    continue;

                tween.Update();

                playComplete = false;
            }

            if (playComplete)
                StopTweenPlay();
        }

        void PlayTween()
        {
            StopTweenPlay();

            var playTweenAll = target as uPlayTweenAll;
            if (playTweenAll == null)
                return;

            tweenList.Clear();
			if (playTweenAll.includeInChildren)
				playTweenAll.GetComponentsInChildren<uTweener>(tweenList);
			else
				playTweenAll.GetComponents<uTweener>(tweenList);

            for(int i = 0; i < tweenList.Count; ++i)
            {
                uTweener tween = tweenList[i];

				if (playTweenAll.useDelayTween)
					tween.delay = i * playTweenAll.delayTweenInterval;

                tween.Stop();
                tween.Play();
            }

            if (tweenList.Count > 0)
                playTween = true;
        }

        void StopTweenPlay()
        {
            playTween = false;
            tweenList.Clear();
        }
    }
}
#endif