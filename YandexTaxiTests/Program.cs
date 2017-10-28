using System;
using YandexTaxiTests;

namespace YandexTaxiTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = MatrixSpiralFilling.FillMatrix(6);
            MatrixSpiralFilling.PrintMatrix(m, -3);

            var idx = UnorderedSubstring.FindIndex("hggtyug7ghh7jf5lkj8kljk6fgg", "7ghf7jh");
            Console.WriteLine(idx);
        }
    }
}
