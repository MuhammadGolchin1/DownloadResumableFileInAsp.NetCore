internal class Program
{
    private static void Main(string[] args)
    {
        long fileSizeInBytes = 1000000000; // به عنوان مثال 1 گیگابایت

        byte[] largeByteArray = new byte[fileSizeInBytes];

        Random random = new Random();
        random.NextBytes(largeByteArray);

        string filePath = "D:\\hugeByteArray1G.dat"; 

        File.WriteAllBytes(filePath, largeByteArray);

        Console.WriteLine($" file Created in : {filePath}");
        Console.ReadLine();
    }

}