using System;
using System.Buffers;
using ForCSharpTesting.LeetCodeSolutions.Common;

namespace ForCSharpTesting;

public class Program
{
    static void Main(string[] args)
    {
        //var newBytes = new byte[100 * 1024 * 1024];
        //for (int i = 0; i < newBytes.Length; i++)
        //{
        //    newBytes[i] = (byte)(i % byte.MaxValue);
        //}
        //File.WriteAllBytes(@"D:\Projects\Adve\Code\adve-community-ui\public\images\mocks\location-bridges\100MB.dat", newBytes);
        //return;
        
        var smallFilePath = @"D:\Projects\Adve\Code\adve-community-ui\public\images\mocks\location-bridges\bridge3.png";
        var largeFilePath = @"D:\Projects\Adve\Code\adve-community-ui\public\images\mocks\location-bridges\bridge4.png";
        var veryLargeFilePath = @"D:\Projects\Adve\Code\adve-community-ui\public\images\mocks\location-bridges\100MB.dat";



        byte[] bytes = null;

        for (int i = 0; i < 1000; i++)
        {
            try
            {
                using (var fileStream = new FileStream(veryLargeFilePath, FileMode.Open, FileAccess.Read))
                {
                    //bytes = new byte[fileStream.Length];
                    bytes = ArrayPool<byte>.Shared.Rent((int)fileStream.Length);

                    fileStream.Read(bytes, 0, bytes.Length);
                }
            }
            finally
            {
                // Возвращаем массив в пул
                if (bytes is not null)
                {
                    ArrayPool<byte>.Shared.Return(bytes);
                }
            }
        }

        Console.WriteLine($"TotalAllocatedBytes: {GC.GetTotalAllocatedBytes()}");
        Console.WriteLine($"TotalMemory: {GC.GetTotalMemory(true)}");
    }

    class A { }

    class B : A
    {
    }
}