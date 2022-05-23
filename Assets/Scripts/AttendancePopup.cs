//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AttendancePopup : MonoBehaviour
//{
//    [SerializeField] AttendancePanel[] _AttendancePanels = null;
//    [SerializeField] GameObject[] _ParentCanvases = null;

//    public void ShowAttendancePopup()
//    {
//        gameObject.SetActive(true);
//        int count = 1;
//        foreach(var i in _AttendancePanels)
//        {
//            i.InitPanel(count);
//            count++;
//        }

//        foreach (var Obj in _ParentCanvases)
//            Obj.SetActive(false);
//    }
//    public void GetEffect(Int64 AttendanceCount_)
//    {
//        _AttendancePanels[AttendanceCount_ % global.c_Attendance_Max].ShowGetAni();
//    }
//    public void Close()
//    {
//        gameObject.SetActive(false);
//        foreach (var Obj in _ParentCanvases)
//            Obj.SetActive(true);
//    }
//}
