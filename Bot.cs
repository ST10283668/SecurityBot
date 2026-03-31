using System;
using System.Collections.Generic;
using System.Text;

namespace Securitybot
{
    internal class Bot
    {
        public static void CyberChat()
        {
            Console.WriteLine("What is your name?");
            string userName = Console.ReadLine();
            Console.WriteLine("Hello " + userName + " how are you doing today?");
            Console.WriteLine("A) Doing good");
            Console.WriteLine("B) Not so good");
            Console.WriteLine("C) Okay");
            Console.Write("Please select an option (A, B, or C): ");
            string userResponse = Console.ReadLine().ToUpper();

            if (userResponse == "A")
            {
                Console.WriteLine("That's great to hear! How can I assist you with your cybersecurity needs today?");
            }
            else if (userResponse == "B")
            {
                Console.WriteLine("I'm sorry to hear that. If there's anything I can do to help with your cybersecurity concerns, please let me know.");
            }
            else if (userResponse == "C")
            {
                Console.WriteLine("If you have any questions or need assistance with cybersecurity, feel free to ask.");
            }
            else
            {
                Console.WriteLine("Invalid option selected. Please choose A, B, or C.");
            }
        }
    }
}

