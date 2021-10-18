using BusinessLogic;
using System;
using System.Threading.Tasks;

namespace AEMTestIV
{
    class Program
    {
        Processor processor = new Processor();
        static void Main(string[] args)
        {
            Program program = new Program();
            if (program.Login())
            {
                Console.WriteLine("Login Success");
                Console.WriteLine("Press 'A' for Well Actual...");
                Console.WriteLine("Press 'D' for Well Dummy...");
                Console.WriteLine("Press 'E' to Exit...");

                var key = Console.ReadKey().Key;
                while (key != ConsoleKey.E)
                {

                    
                    if (key == ConsoleKey.A)
                    {
                        program.GetPlatformWellActual();
                    }
                    else if (key == ConsoleKey.D)
                    {
                        program.GetPlatformWellDummy();
                    }

                    key = Console.ReadKey().Key;
                }
            }
            else
            {
                Console.WriteLine("Login Failed");
            }

            Console.WriteLine("Press 'any key' to Exit...");
            Console.ReadKey();
        }

        public bool Login()
        {
            Task<string> task = Task.Run<string>(async () => await processor.Login(new DomainModel.LoginModel { username = "user@aemenersol.com", password = "Test@123" }));
            string token = task.Result;
            Console.WriteLine("Token : " + token);
            if (!string.IsNullOrEmpty(token))
                return true;
            else
                return false;
        }

        public void GetPlatformWellActual()
        {
            //Task<string> task = Task.Run<string>(async () => await processor.GetPlatformWellDummy());
            Task<string> task = Task.Run<string>(async () => await processor.GetPlatformWellActual());
            string result = task.Result;
            Console.WriteLine("GetPlatformWellActual : Completed.");

        }

        public void GetPlatformWellDummy()
        {
            Task<string> task = Task.Run<string>(async () => await processor.GetPlatformWellDummy());
            
            string result = task.Result;
            //Console.WriteLine("Result : " + result);
            Console.WriteLine("GetPlatformWellDummy : Completed.");

        }
    }
}
