using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Input.GetKeyDown, GetKeyUp 은 Update 한 프레임에 같은키가 모두 true 일 수 있으며, 순서가 없기 때문에 Down먼저 조회할것.
/// 또한 매 프레임마다 초기화 되므로 매 프레임 조회하지 않으면 조회 안 될 수 있음
/// 같은 식으로 getKeyDown, getKeyUp 순으로 호출할것.
/// </summary>
public class KeyInputController
{
    HashSet<KeyCode> _downKeys = new HashSet<KeyCode>();
    HashSet<KeyCode> _upKeys = new HashSet<KeyCode>();

    /// <summary>
    /// call in main update
    /// </summary>
    public void update()
    {
        _downKeys.Clear();
        _upKeys.Clear();
    }
    public bool getKeyDown(KeyCode keyCode)
    {
#if !UNITY_ANDROID && !UNITY_EDITOR
        return false;
#endif

        if (_downKeys.Contains(keyCode))
            return false;

        if (!Input.GetKeyDown(keyCode))
            return false;

        _downKeys.Add(keyCode);
        return true;
    }
    public bool getKeyUp(KeyCode keyCode)
    {
#if !UNITY_ANDROID && !UNITY_EDITOR
        return false;
#endif

        if (_upKeys.Contains(keyCode))
            return false;

        if (!Input.GetKeyUp(keyCode))
            return false;

        _upKeys.Add(keyCode);
        return true;
    }
}
