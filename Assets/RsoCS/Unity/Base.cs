using rso.gameutil;
using UnityEditor;
using UnityEngine;

namespace rso.unity
{
    // 유니티 중력 가속도 공식
    // 변위 = 0.5 * g * t*t + (현재v + 다음FixedUpdate시점의v) * 0.5 * t    ## (현재v + 다음FixedUpdate시점의v) * 0.5 = 현재v와 다음v의 중간
    // 변위 = 0.5 * t * (g *  t + 현재v + 다음FixedUpdate시점의v)

    public static class CBase
    {
        public static EOS GetOS()
        {
#if UNITY_ANDROID
            return EOS.Android;
#elif UNITY_IOS
    		return EOS.iOS;
#else
            return EOS.iOS;
#endif
        }
        public static string GetDataPath()
        {
#if UNITY_EDITOR
            return Application.dataPath + "/";
#else
            return Application.persistentDataPath + "/";
#endif
        }
        public static void ApplicationQuit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
        public static void ApplicationPause()
        {
#if UNITY_EDITOR
            EditorApplication.isPaused = true;
#else
            Application.Quit();
#endif
        }
        public static bool BackPushed()
        {
#if UNITY_ANDROID || UNITY_EDITOR
            return Input.GetKeyDown(KeyCode.Escape);
#else
            return false;
#endif
        }
    }
}