using System;

namespace Generic.Maths
{
    public static class GenMath
    {
        public static int ToSign(Boolean b) { if (b) { return 1; } else { return -1; } }
        public static bool IsEven(int i) { return i % 2 == 0; }

        public static double RandomBetween(this Random random, double min, double max)
        {
            return min + random.NextDouble()*(max - min);
        }

        public static bool InRange(this float x, float min, float max)
        {
            return min < x && x < max;
        }


        public static int GreatestCommonDivisor(int first, int second)
        {
            if (second == 0)
                return first;
            return GreatestCommonDivisor(second, first % second);
        }

        public static Boolean ClockWiseBetween(int from, int value, int to)
        {
            return ((from < value && value < to) // 012
                    || (to < from && from < value) //201
                    || (value < to && to < from) //120
                   );
        }

        public static bool InOrOnRange(this float x, float min, float max)
        {
            return min <= x && x <= max;
        }

        public static bool InRange(this int x, int min, int max)
        {
            return min < x && x < max;
        }

        public static bool InOrOnRange(this int x, int min, int max)
        {
            return min <= x && x <= max;
        }

        public static bool InRange(this double x, double min, double max)
        {
            return min < x && x < max;
        }

        public static bool InOrOnRange(this double x, double min, double max)
        {
            return min <= x && x <= max;
        }
    }
}
