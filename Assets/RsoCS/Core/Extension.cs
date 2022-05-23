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
            public static Int64 ToTimePointTicks(this DateTime Self_)
            {
                return (Self_.ToUniversalTime() - BaseDateTime()).Ticks;
            }
            public static TimePoint ToTimePoint(this DateTime Self_)
            {
                return new TimePoint(Self_);
            }
            public static Int64 TotalMillisecondsLong(this TimeSpan Self_)
            {
                return Self_.Ticks / 10000;
            }
            public static Int64 TotalSecondsLong(this TimeSpan Self_)
            {
                return Self_.Ticks / 10000000;
            }
            public static Int64 TotalMinutesLong(this TimeSpan Self_)
            {
                return Self_.Ticks / 600000000;
            }
            public static Int64 TotalHoursLong(this TimeSpan Self_)
            {
                return Self_.Ticks / 36000000000;
            }
            public static Int64 TotalDaysLong(this TimeSpan Self_)
            {
                return Self_.Ticks / 864000000000;
            }
            public static Int64 TotalMillisecondsLongCeil(this TimeSpan Self_)
            {
                if (Self_.Ticks % 10000 != 0)
                    return (Self_.TotalMillisecondsLong() + (Self_.Ticks >= 0 ? 1 : -1));

                return Self_.TotalMillisecondsLong();
            }
            public static Int64 TotalSecondsLongCeil(this TimeSpan Self_)
            {
                if (Self_.Ticks % 10000000 != 0)
                    return (Self_.TotalSecondsLong() + (Self_.Ticks >= 0 ? 1 : -1));

                return Self_.TotalSecondsLong();
            }
            public static Int64 TotalMinutesLongCeil(this TimeSpan Self_)
            {
                if (Self_.Ticks % 600000000 != 0)
                    return (Self_.TotalMinutesLong() + (Self_.Ticks >= 0 ? 1 : -1));

                return Self_.TotalMinutesLong();
            }
            public static Int64 TotalHoursLongCeil(this TimeSpan Self_)
            {
                if (Self_.Ticks % 36000000000 != 0)
                    return (Self_.TotalHoursLong() + (Self_.Ticks >= 0 ? 1 : -1));

                return Self_.TotalHoursLong();
            }
            public static Int64 TotalDaysLongCeil(this TimeSpan Self_)
            {
                if (Self_.Ticks % 864000000000 != 0)
                    return (Self_.TotalDaysLong() + (Self_.Ticks >= 0 ? 1 : -1));

                return Self_.TotalDaysLong();
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
            public static Int32 IndexNotOfAny(this string Self_, char[] anyOf, Int32 startIndex, Int32 length)
            {
                for (Int32 i = startIndex; i < length; ++i)
                {
                    bool AllNotMatch = true;

                    foreach (var a in anyOf)
                        if (a == Self_[i])
                        {
                            AllNotMatch = false;
                            break;
                        }

                    if (AllNotMatch)
                        return i;
                }

                return -1;
            }
            public static Int32 IndexNotOfAny(this string Self_, char[] anyOf, Int32 startIndex)
            {
                return IndexNotOfAny(Self_, anyOf, startIndex, Self_.Length - startIndex);
            }
            public static Int32 IndexNotOfAny(this string Self_, char[] anyOf)
            {
                return IndexNotOfAny(Self_, anyOf, 0, Self_.Length);
            }
            public static Int32 LastIndexNotOfAny(this string Self_, char[] anyOf, Int32 startIndex, Int32 length)
            {
                for (Int32 i = length; i > startIndex; --i)
                {
                    bool AllNotMatch = true;

                    foreach (var a in anyOf)
                        if (a == Self_[i - 1])
                        {
                            AllNotMatch = false;
                            break;
                        }

                    if (AllNotMatch)
                        return i - 1;
                }

                return -1;
            }
            public static Int32 LastIndexNotOfAny(this string Self_, char[] anyOf, Int32 startIndex)
            {
                return LastIndexNotOfAny(Self_, anyOf, startIndex, Self_.Length - startIndex);
            }
            public static Int32 LastIndexNotOfAny(this string Self_, char[] anyOf)
            {
                return LastIndexNotOfAny(Self_, anyOf, 0, Self_.Length);
            }
            public static KeyValuePair<TKey, TValue> GetPair<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            {
                return new KeyValuePair<TKey, TValue>(key, dictionary[key]);
            }
        }
    }
}