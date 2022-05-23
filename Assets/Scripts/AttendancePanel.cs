//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class AttendancePanel : MonoBehaviour
//{
//    [SerializeField] Int32 Type = 0;
//    [SerializeField] Text RewardTitle = null;
//    [SerializeField] GameObject RewardIconAni = null;
//    [SerializeField] GameObject RewardIcon = null;
//    [SerializeField] GameObject[] RewardIconEff = null;
//    [SerializeField] RewardItemSet[] RewardInfoList = null;

//    public void InitPanel(Int32 AttendanceCount_)
//    {
//        var Meta = CGlobal.MetaData.AttendanceRewardMetas[AttendanceCount_];
//        RewardTitle.text = string.Format(CGlobal.MetaData.GetText(EText.Attendance_Popup_DayText), Meta.AttendanceCode);
//        if (Type == 0)
//        {
//            RewardInfoList[0].Init(CGlobal.MetaData.GetRewardList(Meta.RewardCode)[0], false);
//        }
//        else
//        {
//            var List = CGlobal.MetaData.GetRewardList(Meta.RewardCode);
//            for (int i = 0; i < List.Count; ++i)
//            {
//                RewardInfoList[i].Init(List[i],false);
//            }
//        }
//        if(AttendanceCount_ <= (CGlobal.LoginNetSc.User.AttendanceCounter % global.c_Attendance_Max))
//        {
//            RewardIconAni.SetActive(false);
//            RewardIcon.SetActive(true);
//            foreach (var i in RewardIconEff)
//                i.SetActive(false);
//        }
//        else
//        {
//            RewardIconAni.SetActive(false);
//            RewardIcon.SetActive(false);
//            foreach (var i in RewardIconEff)
//                i.SetActive(false);
//        }
//    }
//    public void ShowGetAni()
//    {
//        RewardIconAni.SetActive(true);
//        RewardIcon.SetActive(false);
//        foreach (var i in RewardIconEff)
//            i.SetActive(true);
//    }
//    public void EndAni()
//    {
//        RewardIconAni.SetActive(false);
//        RewardIcon.SetActive(true);
//        foreach (var i in RewardIconEff)
//            i.SetActive(false);
//        CGlobal.NetControl.Send<SAttendanceRewardNetCs>(new SAttendanceRewardNetCs());
//    }
//}
