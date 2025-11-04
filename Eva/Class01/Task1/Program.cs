using System.IO;
using Task1.Model;

namespace Task1
{
    internal class Program
    {
        static int Main(string[] args)
        {
            string filePath;
            do
            {
                Console.WriteLine("Adja meg az eleresi utat!");
                filePath = Console.ReadLine();
            }
            while (System.IO.File.Exists(filePath) && System.IO.Path.GetExtension(filePath) == ".txt");

            IDocumentStatistics documentStatistics = new DocumentStatistics(filePath);

            try
            {
                documentStatistics.Load();
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine("Error");
                return -1;
            }
            return 0;
        }
    }
}
