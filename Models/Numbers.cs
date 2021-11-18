using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exploration3.Models
{
    public class Numbers
    {
        public static int[] numbers;
        public static DateTime startTime;
        public static DateTime endTime;
        private Numbers()
        {

        }
        public void SetNumbers(int[] input)
        {
            numbers = input;
        }
        public int[] GetNumbers()
        {
            return numbers;
        }
    }
}
