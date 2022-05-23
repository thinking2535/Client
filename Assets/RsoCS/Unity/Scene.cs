using UnityEngine;

namespace rso.unity
{
    public abstract class CScene
    {
        string _PrefabName;
        Vector3 _Pos;
        bool _Active;
        protected bool _Exit = false;
        protected GameObject _Object = null;
        public CScene(string PrefabName_, Vector3 Pos_, bool Active_)
        {
            _PrefabName = PrefabName_;
            _Pos = Pos_;
            _Active = Active_;
        }
        public void Create()
        {
            if (_Object != null)
                return;

            _Object = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(_PrefabName), _Pos, Quaternion.identity);
            _Object.name = _PrefabName;

            if (!_Active)
                _Object.SetActive(false);
        }
        public void Clear()
        {
            if (_Object != null)
            {
                GameObject.Destroy(_Object);
                _Object = null;
            }

            Dispose();
        }
        public void Exit()
        {
            _Exit = true;
        }
        public abstract void Dispose();
        public abstract void Enter();
        public abstract bool Update(); // false 를 반환하면 내부의 소멸 처리가 끝났다고 간주함 (Enter가 호출되었다면 소멸처리 하고 return false 할것)
    }
}
