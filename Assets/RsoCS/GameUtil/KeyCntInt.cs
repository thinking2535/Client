using System;
using System.Collections.Generic;
using System.Linq;

namespace rso
{
    namespace gameutil
    {
		public class CKeyCntInt<_TKey> : Dictionary<_TKey, Int32>
		{
			public CKeyCntInt<_TKey> Plus(_TKey Key_, Int32 Cnt_)
			{
				if (Cnt_ > 0)
				{
                    if (TryGetValue(Key_, out Int32 Cnt))
                    {
                        if (Cnt + Cnt_ == 0)
                            Remove(Key_);
                        else if (Cnt + Cnt_ > Cnt)
                                this[Key_] = Cnt + Cnt_;
                        else
                            this[Key_] = Int32.MaxValue;
                    }
                    else
                    {
                        Add(Key_, Cnt_);
                    }
				}
				else
				{
                    if (TryGetValue(Key_, out Int32 Cnt))
                    {
                        if (Cnt + Cnt_ == 0)
                            Remove(Key_);
                        else if (Cnt + Cnt_ < Cnt)
                            this[Key_] = Cnt + Cnt_;
                        else
                            this[Key_] = Int32.MinValue;
                    }
                    else
                    {
                        Add(Key_, Cnt_);
                    }
				}

				return this;
			}
            public CKeyCntInt<_TKey> Plus(KeyValuePair<_TKey, Int32> KeyCnt_)
            {
                return Plus(KeyCnt_.Key, KeyCnt_.Value);
            }
            public CKeyCntInt<_TKey> Minus(_TKey Key_, Int32 Cnt_)
            {
                return Plus(Key_, -Cnt_);
            }
            public CKeyCntInt<_TKey> Minus(KeyValuePair<_TKey, Int32> KeyCnt_)
			{
                return Minus(KeyCnt_.Key, KeyCnt_.Value);
			}
			public CKeyCntInt<_TKey> Plus(Dictionary<_TKey, Int32> KeyCnts_)
			{
				foreach (var i in KeyCnts_)
					Plus(i);

				return this;
			}
			public CKeyCntInt<_TKey> Minus(Dictionary<_TKey, Int32> KeyCnts_)
			{
                foreach (var i in KeyCnts_)
					Minus(i);

				return this;
			}
			public CKeyCntInt<_TKey> Multied(Int32 Cnt_)
			{
                if (Cnt_ != 0)
                {
                    foreach (var i in Keys.ToArray())
                        this[i] = this[i] * Cnt_;
                }
                else
                {
                    Clear();
                }

                return this;
			}
			public CKeyCntInt<_TKey> Dived(Int32 Cnt_)
			{
                foreach (var i in Keys.ToArray())
                {
                    var Cnt = this[i];
                    if ((Cnt /= Cnt_) == 0)
                        Remove(i);
                    else
                        this[i] = Cnt;
                }

				return this;
			}
			public bool LessThan(Dictionary<_TKey, Int32> KeyCnts_)
			{
				foreach (var i in this)
				{
                    if (!KeyCnts_.ContainsKey(i.Key))
                        return false;

                    if (i.Value >= KeyCnts_[i.Key])
                        return false;
				}

				return true;
			}
			public bool LessThanEqual(Dictionary<_TKey, Int32> KeyCnts_)
            {
				foreach (var i in this)
				{
                    if (!KeyCnts_.ContainsKey(i.Key))
                        return false;

                    if (i.Value > KeyCnts_[i.Key])
                        return false;
				}

				return true;
			}
			public bool GreaterThan(Dictionary<_TKey, Int32> KeyCnts_)
            {
				foreach (var i in KeyCnts_)
				{
                    if (!KeyCnts_.ContainsKey(i.Key))
                        return false;

                    if (i.Value <= KeyCnts_[i.Key])
                        return false;
				}

				return true;
			}
			public bool GreaterThanEqual(Dictionary<_TKey, Int32> KeyCnts_)
            {
				foreach (var i in KeyCnts_)
				{
                    if (!KeyCnts_.ContainsKey(i.Key))
                        return false;

                    if (i.Value < KeyCnts_[i.Key])
                        return false;
				}

				return true;
			}
			public Int32 TotalCnt()
			{
				Int32 Cnt = 0;

				foreach (var i in this)
					Cnt += i.Value;

				return Cnt;
			}
		}
    }
}