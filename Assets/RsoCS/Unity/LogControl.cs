using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace rso.unity
{
    public class CLogControl
    {
        public delegate void FLogCallback(string Condition_, string StackTrace_, LogType Type_);
        struct _SLog
        {
            public DateTime Time;
            public string Condition;
        }

        FLogCallback _LogCallback;
        Int32 _MaxLogSize = 0;
        Int64[] _Versions = null;
        string _FullPath = "";
        List<_SLog> _Logs = new List<_SLog>(); // write and clear when exception, error and assert accur
        void LogCallback(string Condition_, string StackTrace_, LogType Type_)
        {
            _LogCallback?.Invoke(Condition_, StackTrace_, Type_);

            if (Type_ == LogType.Log)
            {
                _Logs.Add(new _SLog { Time = DateTime.Now, Condition = Condition_ });
                if (_Logs.Count > _MaxLogSize)
                    _Logs.RemoveAt(0);
                return;
            }

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
            if (_Logs.Count > 0)
            {
                foreach (var i in _Logs)
                    streamWriter.WriteLine(i.Time.ToString("[yyyy.MM.dd. HH:mm:ss]") + i.Condition);

                streamWriter.WriteLine();
                _Logs.Clear();
            }

            streamWriter.WriteLine("[" + Type_.ToString() + "]\n" + Condition_);
            streamWriter.WriteLine();


            streamWriter.WriteLine("[StackTrace]\n" + StackTrace_);

            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            streamWriter.WriteLine("[StackTrace Diagnostics]\n" + stackTrace.ToString());
            streamWriter.Close();

            CBase.ApplicationPause();
        }
        public CLogControl(Int32 MaxLogSize_, Int64[] Versions_, string Directory_, FLogCallback LogCallback_ = null)
        {
            _LogCallback = LogCallback_;
            _MaxLogSize = MaxLogSize_;
            _Versions = Versions_;
            _FullPath = Path.GetFullPath(Application.persistentDataPath + "/" + Directory_);
            Directory.CreateDirectory(_FullPath);
            Application.logMessageReceived += LogCallback;
        }
    }
}
