using System.Collections.Generic;

namespace rso.gameutil
{
    public class Rank<TKey, TValue> : SortedDictionary<TKey, TValue>
    {
        public KeyValuePair<TKey, TValue>? Get(TKey Key_)
        {
            var keys = new List<TKey>(Keys);
            var index = keys.BinarySearch(Key_);
            if (index >= 0)
            {
                return new KeyValuePair<TKey, TValue>(keys[index], this[keys[index]]);
            }
            else
            {
                if (~index - 1 < 0)
                    return null;

                return new KeyValuePair<TKey, TValue>(keys[~index - 1], this[keys[~index - 1]]);
            }
        }
    }
}