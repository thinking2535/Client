using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace rso
{
    namespace core
    {
        // 전체를 순회할 수 있는 it을 중간에 find 할 수 없으므로  MultiMap 의 Value는 수정할 수 없는 것으로 한다. class 형식은 불가피하게 수정이 가능하나 상관없음.
        public class MultiMapEnum<TKey, TValue> : IEnumerator
        {
            SortedDictionary<TKey, Queue<TValue>>.Enumerator _Current;
            Queue<TValue>.Enumerator _ValueCurrent;
            bool _First = true;
            public MultiMapEnum(SortedDictionary<TKey, Queue<TValue>> Datas_)
            {
                _Current = Datas_.GetEnumerator();
            }
            public bool MoveNext()
            {
                if (_First)
                {
                    if (!_Current.MoveNext())
                        return false;

                    _ValueCurrent = _Current.Current.Value.GetEnumerator();
                    _First = false;
                }

                if (!_ValueCurrent.MoveNext())
                {
                    if (!_Current.MoveNext())
                        return false;

                    _ValueCurrent = _Current.Current.Value.GetEnumerator();
                    _ValueCurrent.MoveNext();
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
            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    return new KeyValuePair<TKey, TValue>(_Current.Current.Key, _ValueCurrent.Current);
                }
            }
        }
        public class MultiMap<TKey, TValue> : IEnumerable
        {
            Int32 _Count = 0;
            SortedDictionary<TKey, Queue<TValue>> _Datas = new SortedDictionary<TKey, Queue<TValue>>();
            public void Add(TKey Key_, TValue Value_)
            {
                Queue<TValue> EqualRange;
	            if (!_Datas.TryGetValue(Key_, out EqualRange))
	            {
                    EqualRange = new Queue<TValue>();
                    _Datas[Key_] = EqualRange;
	            }

                EqualRange.Enqueue(Value_);

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
                Queue<TValue> Value;
                if (!_Datas.TryGetValue(Key_, out Value))
                    return;

                _Count -= Value.Count;
                _Datas.Remove(Key_);
            }
            public void RemoveFirst()
            {
                if (_Datas.Count == 0)
                    return;

                _Datas.First().Value.Dequeue();

                if (_Datas.First().Value.Count == 0)
                    _Datas.Remove(_Datas.First().Key);

                --_Count;
            }
            public void Clear()
            {
                _Datas.Clear();
                _Count = 0;
            }
            public KeyValuePair<TKey, TValue> First()
            {
                return new KeyValuePair<TKey, TValue>(_Datas.First().Key, _Datas.First().Value.Peek());
            }
            public TValue[] ToArray(TKey Key_)
            {
                return _Datas[Key_].ToArray();
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            public Queue<TValue>.Enumerator GetEqualEnumerator(TKey Key_)
            {
                return _Datas.GetValue(Key_).GetEnumerator();
            }
            public MultiMapEnum<TKey, TValue> GetEnumerator()
            {
                return new MultiMapEnum<TKey, TValue>(_Datas);
            }
        }
    }
}