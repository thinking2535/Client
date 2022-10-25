using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace rso.unity
{
    public class CLogControl
    {
        public delegate void FLogCallback(string Condition_, string StackTrace_, LogType Type_);

        FLogCallback _LogCallback;
        Int64[] _Versions = null;
        string _FullPath = "";
        void LogCallback(string Condition_, string StackTrace_, LogType Type_)
        {
            _LogCallback?.Invoke(Condition_, StackTrace_, Type_);

            var FileName = _FullPath + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + (DateTime.Now.Ticks % 10000000).ToString() + "_" + Type_.ToString();
            var streamWriter = File.AppendText(FileName + ".txt");

            if (_Versions.Length > 0)
            {
                streamWriter.WriteLine("[Versions]");

                string Versions = _Versions[0].ToString();

                for (Int32 i = 1; i < _Versions.Length; ++i)
                    Versions += (", " + _Versions[i].ToString());

                streamWriter.WriteLine(Versions);
                streamWriter.WriteLine();
            }

            streamWriter.WriteLine("[Logs]");
            streamWriter.WriteLine(DateTime.Now.ToString("[yyyy.MM.dd. HH:mm:ss]") + Condition_);

            streamWriter.WriteLine();
            streamWriter.WriteLine("[" + Type_.ToString() + "]\n" + Condition_);
            streamWriter.WriteLine();


            streamWriter.WriteLine("[StackTrace]\n" + StackTrace_);

            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            streamWriter.WriteLine("[StackTrace Diagnostics]\n" + stackTrace.ToString());
            streamWriter.Close();

            if (Type_ != LogType.Log)
                CUnity.ApplicationPause();
        }
        public CLogControl(Int64[] Versions_, string Directory_, FLogCallback LogCallback_ = null)
        {
            _LogCallback = LogCallback_;
            _Versions = Versions_;
            _FullPath = Path.GetFullPath(Application.persistentDataPath + "/" + Directory_);
            Directory.CreateDirectory(_FullPath);
            Application.logMessageReceived += LogCallback;
        }
    }
}
