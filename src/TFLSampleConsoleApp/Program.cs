using System;
using System.Threading.Tasks;

namespace TFLSampleConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\n\n Transport of London : Consume Road API :\n");
            Console.Write("---------------------------------------------------\n");
            Console.Write(" Enter The Road Name : ");
            string roadName = Console.ReadLine();

            // Call GetDataWithAuthentication method with input provided by user
            RoadAPIClient.GetDataWithAuthentication(roadName).Wait();

            Console.ReadLine();
        }
    }
}
