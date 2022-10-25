using UnityEngine;

namespace rso.unity
{
    public class CAudio
    {
        protected AudioSource _Source = null;
        public bool mute { get { return _Source.mute; } set { _Source.mute = value; } }
        public CAudio(AudioSource Source_)
        {
            _Source = Source_;
            _Source.loop = false;
        }
    }
}
