using System;
using System.Collections.Generic;
using UnityEngine;

namespace rso.unity
{
    using SceneCreator = System.Func<Scene>;
    public class Scene : MonoBehaviour
    {
        public new Camera camera { get; private set; }
        protected virtual void Awake()
        {
            camera = GetComponentInChildren<Camera>();
        }
        protected virtual void Start()
        {
        }
        protected virtual void Update()
        {
        }
        protected virtual void OnDestroy()
        {
        }
    }
    public class SceneController
    {
        public static T create<T>(string prefabName) where T : Scene
        {
            var obj = UnityEngine.Object.Instantiate(Resources.Load<T>(prefabName), Vector3.zero, Quaternion.identity);
            obj.name = prefabName;
            return obj;
        }
        public static T create<T>(T prefab) where T : Scene
        {
            var obj = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.name = typeof(T).ToString();
            return obj;
        }
        Stack<SceneCreator> _sceneCreators = new Stack<SceneCreator>();
        public Scene curScene { get; private set; }
        public Int32 count => _sceneCreators.Count;
        public bool isEmpty => _sceneCreators.Count == 0;
        public bool isNotEmpty => _sceneCreators.Count > 0;
        public SceneController(SceneCreator sceneCreator)
        {
            _push(sceneCreator);
        }
        ~SceneController()
        {
            clear();
        }
        public void set(SceneCreator sceneCreator)
        {
            _pop();
            _push(sceneCreator);
        }
        public void push(SceneCreator sceneCreator)
        {
            GameObject.Destroy(curScene.gameObject);
            _push(sceneCreator);
        }
        public void clearAndPush(SceneCreator sceneCreator)
        {
            clear();
            _push(sceneCreator);
        }
        void _push(SceneCreator sceneCreator)
        {
            _sceneCreators.Push(sceneCreator);
            curScene = sceneCreator();
        }
        public void pop()
        {
            if (isEmpty)
                return;

            _pop();

            if (isEmpty)
                return;

            curScene = _sceneCreators.Peek()();
        }
        void _pop()
        {
            GameObject.Destroy(curScene.gameObject);
            _sceneCreators.Pop();
        }
        public void clear()
        {
            if (isEmpty)
                return;

            GameObject.Destroy(curScene.gameObject);
            _sceneCreators.Clear();
        }
    }
}