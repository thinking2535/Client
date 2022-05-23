using System;

namespace rso
{
    namespace core
    {
        public static class SteadyDateTime
        {
            static DateTime _Cache = DateTime.Now;

            public static DateTime Now
            {
                get
                {
                    var now = DateTime.Now;
                    if (now > _Cache)
                        _Cache = now;

                    return _Cache;
                }
            }
        }
    }
}