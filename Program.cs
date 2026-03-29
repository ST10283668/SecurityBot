namespace Securitybot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Display.WelcomeMessage();
            
           ChatBot bot = new ChatBot();
           bot.Botname = "SecurityBot";


            Console.WriteLine("What is your name?");
            bot.UserName = Console.ReadLine();


            bot.Greeting = "Hello ,"+bot.UserName+"  I am "+bot.Botname+", here to  help with Cyber Security needs ";
            Console.WriteLine(bot.Greeting);    

        }
    }
}
