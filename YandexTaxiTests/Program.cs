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

            //TestDataSize();
        }

        static void TestDataSize()
        {
            Int32 i = 1;
            Int64 j = 1;
            int k = 1;
            long m = 1;

            Console.WriteLine($"Int32: {System.Runtime.InteropServices.Marshal.SizeOf(i)}");
            Console.WriteLine($"Int64: {System.Runtime.InteropServices.Marshal.SizeOf(j)}");
            Console.WriteLine($"int: {System.Runtime.InteropServices.Marshal.SizeOf(k)}");
            Console.WriteLine($"long: {System.Runtime.InteropServices.Marshal.SizeOf(m)}");

            System.Numerics.BigInteger ii = new System.Numerics.BigInteger(1087878767787678678);
            System.Numerics.BigInteger jj = new System.Numerics.BigInteger(1087878767787678678);
            Console.WriteLine($"BigInteger: {ii * jj * jj}");
        }
    }

}
