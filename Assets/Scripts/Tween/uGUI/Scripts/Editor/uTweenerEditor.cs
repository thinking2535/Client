using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using System.Collections.Generic;

namespace uTools
{
	[CustomEditor(typeof(uTweener), true)]
	public class uTweenerEditor : Editor
	{
        List<uTweener> tweenList = new List<uTweener>(0);
        bool playTween = false;

        private void OnEnable()
        {
            if (Application.isPlaying == false)
            {
                EditorApplication.update -= Update;
                EditorApplication.update += Update;
            }
        }

        private void OnDisable()
        {
            EditorApplication.update -= Update;
        }

        public override void OnInspectorGUI()
		{
			base.DrawDefaultInspector();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Play Tween"))
			{
                PlayTween();
            }

            if (GUILayout.Button("Play All"))
            {
                PlayTween(true);
            }

            GUILayout.EndHorizontal();
        }

        void Update()
        {
            if (Application.isPlaying)
                return;

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

        void PlayTween(bool includeChilds = false)
        {
            StopTweenPlay();

            tweenList.Clear();
            if (includeChilds)
            {
                uTweener[] tweenArray = (target as MonoBehaviour).GetComponents<uTweener>();
                if (tweenArray != null || tweenArray.Length > 0)
                    tweenList.AddRange(tweenArray);
            }
            else
            {
                uTweener tween = target as uTweener;
                if (tween != null)
                    tweenList.Add(tween);
            }
            
            foreach (uTweener tween in tweenList)
            {
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