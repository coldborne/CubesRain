using System;

namespace Logic
{
    public static class RandomGenerator
    {
        private static Random s_random = new Random();

        public static int Next(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue + 1);
        }
    }
}