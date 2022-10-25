using System;
using System.Collections.Generic;

namespace rso
{
    namespace core
    {
        public static class Extension
        {
            public static DateTime BaseDateTime()
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
            public static Int64 ToTimePointTicks(this DateTime seld)
            {
                return (seld.ToUniversalTime() - BaseDateTime()).Ticks;
            }
            public static TimePoint ToTimePoint(this DateTime slef)
            {
                return new TimePoint(slef);
            }
            public static Int64 TotalMillisecondsLong(this TimeSpan self)
            {
                return self.Ticks / 10000;
            }
            public static Int64 TotalSecondsLong(this TimeSpan self)
            {
                return self.Ticks / 10000000;
            }
            public static Int64 TotalMinutesLong(this TimeSpan self)
            {
                return self.Ticks / 600000000;
            }
            public static Int64 TotalHoursLong(this TimeSpan self)
            {
                return self.Ticks / 36000000000;
            }
            public static Int64 TotalDaysLong(this TimeSpan self)
            {
                return self.Ticks / 864000000000;
            }
            public static Int64 TotalMillisecondsLongCeil(this TimeSpan self)
            {
                if (self.Ticks % 10000 != 0)
                    return (self.TotalMillisecondsLong() + (self.Ticks >= 0 ? 1 : -1));

                return self.TotalMillisecondsLong();
            }
            public static Int64 TotalSecondsLongCeil(this TimeSpan self)
            {
                if (self.Ticks % 10000000 != 0)
                    return (self.TotalSecondsLong() + (self.Ticks >= 0 ? 1 : -1));

                return self.TotalSecondsLong();
            }
            public static Int64 TotalMinutesLongCeil(this TimeSpan self)
            {
                if (self.Ticks % 600000000 != 0)
                    return (self.TotalMinutesLong() + (self.Ticks >= 0 ? 1 : -1));

                return self.TotalMinutesLong();
            }
            public static Int64 TotalHoursLongCeil(this TimeSpan self)
            {
                if (self.Ticks % 36000000000 != 0)
                    return (self.TotalHoursLong() + (self.Ticks >= 0 ? 1 : -1));

                return self.TotalHoursLong();
            }
            public static Int64 TotalDaysLongCeil(this TimeSpan self)
            {
                if (self.Ticks % 864000000000 != 0)
                    return (self.TotalDaysLong() + (self.Ticks >= 0 ? 1 : -1));

                return self.TotalDaysLong();
            }
            public static TimeSpan FromDays(Int64 Days_)
            {
                return TimeSpan.FromTicks(Days_ * 864000000000);
            }
            public static TimeSpan FromHours(Int64 Hours_)
            {
                return TimeSpan.FromTicks(Hours_ * 36000000000);
            }
            public static TimeSpan FromMinutes(Int64 Minutes_)
            {
                return TimeSpan.FromTicks(Minutes_ * 600000000);
            }
            public static TimeSpan FromSeconds(Int64 Seconds_)
            {
                return TimeSpan.FromTicks(Seconds_ * 10000000);
            }
            public static TimeSpan FromMilliseconds(Int64 Milliseconds_)
            {
                return TimeSpan.FromTicks(Milliseconds_ * 10000);
            }
            public static Int32 IndexNotOfAny(this string self, char[] anyOf, Int32 startIndex, Int32 length)
            {
                for (Int32 i = startIndex; i < length; ++i)
                {
                    bool AllNotMatch = true;

                    foreach (var a in anyOf)
                        if (a == self[i])
                        {
                            AllNotMatch = false;
                            break;
                        }

                    if (AllNotMatch)
                        return i;
                }

                return -1;
            }
            public static Int32 IndexNotOfAny(this string self, char[] anyOf, Int32 startIndex)
            {
                return IndexNotOfAny(self, anyOf, startIndex, self.Length - startIndex);
            }
            public static Int32 IndexNotOfAny(this string self, char[] anyOf)
            {
                return IndexNotOfAny(self, anyOf, 0, self.Length);
            }
            public static Int32 LastIndexNotOfAny(this string self, char[] anyOf, Int32 startIndex, Int32 length)
            {
                for (Int32 i = length; i > startIndex; --i)
                {
                    bool AllNotMatch = true;

                    foreach (var a in anyOf)
                        if (a == self[i - 1])
                        {
                            AllNotMatch = false;
                            break;
                        }

                    if (AllNotMatch)
                        return i - 1;
                }

                return -1;
            }
            public static Int32 LastIndexNotOfAny(this string self, char[] anyOf, Int32 startIndex)
            {
                return LastIndexNotOfAny(self, anyOf, startIndex, self.Length - startIndex);
            }
            public static Int32 LastIndexNotOfAny(this string self, char[] anyOf)
            {
                return LastIndexNotOfAny(self, anyOf, 0, self.Length);
            }
            public static KeyValuePair<TKey, TValue> GetPair<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            {
                return new KeyValuePair<TKey, TValue>(key, dictionary[key]);
            }
        }
    }
}