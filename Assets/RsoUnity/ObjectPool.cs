using rso.Base;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace rso.unity
{
    public class CObjectPool
    {
        public struct SIterator
        {
            public string PrefabName;
            public CListB<GameObject>.SIterator Iterator;

            public SIterator(string PrefabName_, CListB<GameObject>.SIterator Iterator_)
            {
                PrefabName = PrefabName_;
                Iterator = Iterator_;
            }
            public GameObject gameObject
            {
                get
                {
                    return Iterator.Data;
                }
            }
        }
        Dictionary<string, CListB<GameObject>> _Pool = new Dictionary<string, CListB<GameObject>>();
        ~CObjectPool()
        {
            Dispose();
        }

        public SIterator New(string PrefabName_, Vector3 LocalPosition_, Transform Parent_)
        {
            CListB<GameObject> Container;

            if (!_Pool.TryGetValue(PrefabName_, out Container))
            {
                Container = new CListB<GameObject>(
                        (object[] Params_) =>
                        {
                            return (GameObject)UnityEngine.Object.Instantiate(Resources.Load((string)Params_[0]), (Vector3)Params_[1], Quaternion.identity);
                        },
                        (GameObject GameObject_, object[] Params_) =>
                        {
                            GameObject_.transform.localPosition = (Vector3)Params_[1];
                        });

                _Pool.Add(PrefabName_, Container);
            }

            var Iterator = Container.NewBuf(PrefabName_, LocalPosition_);

            if (Parent_ != null)
                Iterator.Data.transform.SetParent(Parent_, false);

            Iterator.Data.SetActive(true);

            return new SIterator(PrefabName_, Iterator);
        }
        public void Delete(SIterator Iterator_)
        {
            Iterator_.gameObject.SetActive(false);
            _Pool[Iterator_.PrefabName].Remove(Iterator_.Iterator);
        }
        public void Reserve(string PrefabName_, Int32 Size_)
        {
            CListB<GameObject> Container;

            if (!_Pool.TryGetValue(PrefabName_, out Container))
            {
                Container = new CListB<GameObject>(
                        (object[] Params_) =>
                        {
                            return (GameObject)UnityEngine.Object.Instantiate(Resources.Load((string)Params_[0]), (Vector3)Params_[1], Quaternion.identity);
                        },
                        (GameObject GameObject_, object[] Params_) =>
                        {
                            GameObject_.transform.localPosition = (Vector3)Params_[1];
                        });

                _Pool.Add(PrefabName_, Container);
            }

            var Obj = Container.ReserveBuf(PrefabName_, new Vector3());
            Obj.SetActive(false);
        }
        public void Dispose()
        {
            foreach (var i in _Pool)
            {
                foreach (var o in i.Value)
                    GameObject.Destroy(o);

                i.Value.Clear();
            }

            _Pool.Clear();
        }
    }
}
