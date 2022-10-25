using System.Collections.Generic;

namespace rso.gameutil
{
    public class CClosedRank<TKey, TValue> : SortedDictionary<TKey, TValue>
    {
        public KeyValuePair<TKey, TValue>? Get(TKey Key_)
        {
            var keys = new List<TKey>(Keys);
            var index = keys.BinarySearch(Key_);
            if (index < 0)
                index = ~index;

            if (index >= keys.Count && keys.Count > 0)
                --index; // 가장 마지막 것으로 선택

            if (index >= keys.Count)
                return null;
            else
                return new KeyValuePair<TKey, TValue>(keys[index], this[keys[index]]);
        }
    }
}