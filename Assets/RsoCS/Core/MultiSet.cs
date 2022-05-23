using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace rso
{
    namespace core
    {
        public class MultiSetEnum<TKey> : IEnumerator
        {
            SortedDictionary<TKey, Int32>.Enumerator _Current;
            Int32 _ValueCurrent;
            bool _First = true;
            public MultiSetEnum(SortedDictionary<TKey, Int32> Datas_)
            {
                _Current = Datas_.GetEnumerator();
            }
            public bool MoveNext()
            {
                if (_First)
                {
                    if (!_Current.MoveNext())
                        return false;

                    _ValueCurrent = 0;
                    _First = false;
                }

                if (_ValueCurrent == _Current.Current.Value)
                {
                    if (!_Current.MoveNext())
                        return false;

                    _ValueCurrent = 1;
                }
                else
                {
                    ++_ValueCurrent;
                }

                return true;
            }
            public void Reset()
            {
            }
            object IEnumerator.Current
            {
                get
                {
                    return _Current;
                }
            }
            public TKey Current
            {
                get
                {
                    return _Current.Current.Key;
                }
            }
        }
        public class MultiSet<TKey> : IEnumerable
        {
            Int32 _Count = 0;
            SortedDictionary<TKey, Int32> _Datas = new SortedDictionary<TKey, Int32>();
            public void Add(TKey Key_)
            {
                if (_Datas.ContainsKey(Key_))
                    ++_Datas[Key_];
                else
                    _Datas[Key_] = 1;

                ++_Count;
            }
            public bool ContainsKey(TKey Key_)
            {
                return _Datas.ContainsKey(Key_);
            }
            public Int32 Count
            {
                get
                {
                    return _Count;
                }
            }
            public void Remove(TKey Key_)
            {
                Int32 Value;
                if (!_Datas.TryGetValue(Key_, out Value))
                    return;

                _Count -= Value;
                _Datas.Remove(Key_);
            }
            public void RemoveFirst()
            {
                if (_Datas.Count == 0)
                    return;

                --_Datas[_Datas.First().Key];

                if (_Datas.First().Value == 0)
                    _Datas.Remove(_Datas.First().Key);

                --_Count;
            }
            public void RemoveLast()
            {
                if (_Datas.Count == 0)
                    return;

                --_Datas[_Datas.Last().Key];

                if (_Datas.Last().Value == 0)
                    _Datas.Remove(_Datas.Last().Key);

                --_Count;
            }
            public void Clear()
            {
                _Datas.Clear();
                _Count = 0;
            }
            public TKey First()
            {
                return _Datas.First().Key;
            }
            public TKey Last()
            {
                return _Datas.Last().Key;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            public MultiSetEnum<TKey> GetEnumerator()
            {
                return new MultiSetEnum<TKey>(_Datas);
            }
        }
    }
}