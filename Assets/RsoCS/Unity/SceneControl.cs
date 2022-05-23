namespace rso.unity
{
    public class CSceneControl
    {
        public CScene CurScene { get; private set; } = null;
        CScene _NextScene;
        public void SetNext(CScene Scene_)
        {
            if (CurScene != null)
            {
                _NextScene = Scene_; // don't care _NextScene is valid or not
                CurScene.Exit();
            }
            else
            {
                CurScene = Scene_;
                CurScene.Create();
                CurScene.Enter();
            }
        }
        public void SetNextForce(CScene Scene_)
        {
            _NextScene = null;

            if (CurScene != null)
                CurScene.Clear();

            CurScene = Scene_;
            CurScene.Create();
            CurScene.Enter();
        }
        public bool HaveNext()
        {
            return (_NextScene != null);
        }
        public bool Update()
        {
            if (CurScene == null)
                return false;

            if (CurScene.Update())
                return true;

            CurScene.Clear();
            CurScene = _NextScene;

            if (_NextScene == null)
                return false;

            _NextScene = null;
            CurScene.Create();
            CurScene.Enter();
            return true;
        }
        public void Clear()
        {
            if (_NextScene != null)
                _NextScene = null;

            if (CurScene != null)
                CurScene.Exit();
        }
    }
}