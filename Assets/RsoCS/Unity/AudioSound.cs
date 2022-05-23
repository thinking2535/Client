using System;
using System.Collections.Generic;
using UnityEngine;

namespace rso.unity
{
    public class CAudioSound : CAudio
    {
        AudioClip[] _Clips = null;
        public CAudioSound(AudioSource Source_, string[] ClipPaths_) :
            base(Source_)
        {
            _Clips = new AudioClip[ClipPaths_.Length];

            for (Int32 i = 0; i < ClipPaths_.Length; ++i)
                _Clips[i] = Resources.Load<AudioClip>(ClipPaths_[i]);
        }
        public void SetVolume(float Volume_)
        {
            _Source.volume = Volume_;
        }
        public void PlayOneShot(Int32 ClipIndex_)
        {
            _Source.PlayOneShot(_Clips[ClipIndex_]);
        }
        public void PlayOneShot(Int32 ClipIndex_, float VolumeScale_)
        {
            _Source.PlayOneShot(_Clips[ClipIndex_], VolumeScale_);
        }
    }
}