using bb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomADPanel : MonoBehaviour
{
    [SerializeField] GameObject RoomOpenImg = null;
    [SerializeField] GameObject RoomLockImg = null;
    [SerializeField] Text RoomMasterNick = null;
    [SerializeField] RawImage RoomTypeImg = null;

    private SRoomInfo _RoomInfo = null;

    public void initRoomADPanel(SRoomInfo RoomInfo_)
    {
        _RoomInfo = RoomInfo_;
        bool IsLock = _RoomInfo.Password.Length > 0;

        RoomOpenImg.SetActive(!IsLock);
        RoomLockImg.SetActive(IsLock);

        RoomMasterNick.text = _RoomInfo.MasterUser;

        RoomTypeImg.texture = Resources.Load<Texture>("GUI/Contents/" + CGlobal.GetPlayModeADImage(_RoomInfo.Mode));
    }

    public void OnClickRoomInfo()
    {
        var SceneLobby = CGlobal.GetScene<CSceneLobby>();
        if (SceneLobby != null)
        {
            SceneLobby.ShopRoomInfoPopup(_RoomInfo);
        }
    }
}
