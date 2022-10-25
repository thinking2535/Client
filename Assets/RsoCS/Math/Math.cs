using System;

namespace rso.math
{
    public static class CMath
    {
        public const double c_PI = 3.14159265358979323846;
        public const double c_2_PI = (c_PI * 2.0);
        public const double c_PI_2 = (c_PI * 0.5);
        public const float c_PI_F = 3.14159265358979323846f;
        public const float c_2_PI_F = (c_PI_F * 2.0f);
        public const float c_PI_F_2 = (c_PI_F * 0.5f);

        public static double Erf(double x)
        {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            Int32 sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }
        public static double Atanh(double x)
        {
            return 0.5 * Math.Log((1 + x) / (1 - x));
        }
    }
}