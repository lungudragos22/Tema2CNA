using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using Tema2_Client.Protos;

namespace Tema2_Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Tema2_Client.Protos.Main.MainClient(channel);
            Console.WriteLine("Birthday:");
            string birthday = Console.ReadLine();
            DateTime dateTime;
            try
            {
                DateTime.TryParse(birthday,out dateTime);
                var request = new Tema2_Client.Protos.RequestZodiac() { UserData = new UserData { Birthday = birthday } };
                var response = await client.GetZodiacAsync(request);
                Console.WriteLine($"Your zodiac is {response.Zodiac.ZodiacName}");
            }
            catch
            {
                Console.WriteLine("Incorect birthday format");
            }
        }
    }
}
