using System;
using UnityEngine;

public static class CVibration
{
#if UNITY_ANDROID && !UNITY_EDITOR
    static AndroidJavaClass _UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    static AndroidJavaObject _CurrentActivity = _UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    static AndroidJavaObject _Vibrator = _CurrentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#endif
    static void DoNotDeleteMe()
    {
        Handheld.Vibrate(); // 아니 이게 쓰이지도 않는데 왜 이게 없으면 다른 On 함수 쓰는데 에러가 나는거야???
    }
    public static void On(Int64 Milliseconds_)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _Vibrator.Call("vibrate", Milliseconds_);
#endif
    }
    public static void On(Int64[] Pattern_, Int32 Repeat_)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _Vibrator.Call("vibrate", Pattern_, Repeat_);
#endif
    }
    public static void Off()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _Vibrator.Call("cancel");
#endif
    }
}