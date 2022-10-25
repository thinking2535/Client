using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace rso.unity
{
    public class CAudioMusic : CAudio
    {
        System.Random _Rand = new System.Random(DateTime.Now.Second);
        AudioClip[][] _Clips = null;
        float _VolumeScale = 1.0f;
        Int32 _CurPlayListIndex = 0;
        Int32 _CurClipIndex = 0;
        bool _IsPlaying = false;
        bool _IsStopping = false;
        bool _Random = false;
        CLinear _Linear = new CLinear();
        public bool repeatOne = false;
        public void SetRandom(bool Random_)
        {
            _Random = Random_;

            if (Random_)
                _SetNext();
        }
        public bool GetRandom()
        {
            return _Random;
        }
        void _SetNext()
        {
            if (!repeatOne && _Clips[_CurPlayListIndex].Length > 1)
            {
                if (GetRandom())
                    _CurClipIndex += _Rand.Next(1, _Clips[_CurPlayListIndex].Length);
                else
                    ++_CurClipIndex;

                if (_CurClipIndex >= _Clips[_CurPlayListIndex].Length)
                    _CurClipIndex %= _Clips[_CurPlayListIndex].Length;

                _Source.clip = _Clips[_CurPlayListIndex][_CurClipIndex];
            }
        }
        void _Stop()
        {
            _SetNext();
            _IsStopping = true;
            _Linear.SetDuration(1.0f, 1.0f, 0.0f);
        }
        public CAudioMusic(AudioSource Source_, string[][] ClipPaths_) :
            base(Source_)
        {
            Debug.Assert(ClipPaths_.Length > 0, "Invalid ClipPaths_.Length");
            _Clips = new AudioClip[ClipPaths_.Length][];

            for (Int32 PlayListIndex = 0; PlayListIndex < ClipPaths_.Length; ++PlayListIndex)
            {
                Debug.Assert(ClipPaths_[PlayListIndex].Length > 0, "Invalid ClipPaths_[PlayListIndex].Length");
                _Clips[PlayListIndex] = new AudioClip[ClipPaths_[PlayListIndex].Length];

                for (Int32 ClipIndex = 0; ClipIndex < ClipPaths_[PlayListIndex].Length; ++ClipIndex)
                    _Clips[PlayListIndex][ClipIndex] = Resources.Load<AudioClip>(ClipPaths_[PlayListIndex][ClipIndex]);
            }

            _Source.clip = _Clips[_CurPlayListIndex][_CurClipIndex];
        }
        public void SetVolume(float Volume_)
        {
            _VolumeScale = Volume_;

            if (!_IsStopping)
                _Source.volume = Volume_;
        }
        public void Play()
        {
            if (_IsPlaying)
            {
                if (!_IsStopping)
                    _Stop();
            }
            else
            {
                _IsPlaying = true;

                if (!_IsStopping)
                    _Source.Play();
            }
        }
        public void Stop()
        {
            if (!_IsPlaying)
                return;

            _IsPlaying = false;

            if (!_IsStopping)
                _Stop();
        }
        public void SetPlayList(Int32 Index_)
        {
            if (Index_ < 0 || Index_ >= _Clips.Length)
                return;

            _CurPlayListIndex = Index_;

            if (GetRandom())
                _CurClipIndex = _Rand.Next(0, _Clips[_CurPlayListIndex].Length);
            else
                _CurClipIndex = 0;

            _Source.clip = _Clips[_CurPlayListIndex][_CurClipIndex];
        }
        public bool IsPlaying()
        {
            return _Source.isPlaying;
        }
        public void Update()
        {
            if (_IsStopping)
            {
                if (_Source.isPlaying)
                {
                    float Scale = 0.0f;
                    if (_Linear.Get(ref Scale))
                    {
                        _Source.volume = (Scale * _VolumeScale);
                    }
                    else
                    {
                        _IsStopping = false;
                        _Source.volume = _VolumeScale;

                        if (_IsPlaying) // FadeOut 되었으나 곧바로 Play() 한 경우이므로 현재곡 재생
                            _Source.Play();
                        else
                            _Source.Stop();
                    }
                }
                else
                {
                    _IsStopping = false;
                    _Source.volume = _VolumeScale;
                }
            }
            else if (_IsPlaying && !_Source.isPlaying)
            {
                _SetNext();
                _Source.Play();
            }
        }
    }
}