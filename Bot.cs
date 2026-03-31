using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

            Console.WriteLine("What can i help you with today ?");
            Console.WriteLine("A)I have a cybersecurity problem");
            Console.WriteLine("B) I am interested in learning about cybersecurity ");
            Console.WriteLine("Please select an option (A or B):");
            string Response = Console.ReadLine().ToUpper();

            if (Response == "A")
            {
                Console.WriteLine("What problem do you have?");
                Console.WriteLine("A)I think I have been phished");
                Console.WriteLine("B) My password is no longer secure");
                Console.WriteLine("C) I have been sent a suspicious link");
                Console.Write("Please select an option (A, B or C):");
                string problemResponse = Console.ReadLine().ToUpper();

                if (problemResponse == "A")
                    Console.WriteLine("Bot: Stop clicking any links, report the email and change any compromised passwords.");
                else if (problemResponse == "B")
                    Console.WriteLine("Bot: I would take this very seriously and change your passwords immediately, and in the future try to get a 2 step method to help protect the password.");
                else if (problemResponse == "C")
                    Console.WriteLine("Bot: Its good that you can identify something out of the usual. My advice is to report the link and delete it.");
                else
                    Console.WriteLine("Bot: Invalid option. Please choose A, B or C.");
            }
            else if (Response == "B")
            {
                Console.WriteLine("What would you like to learn about ?");
                Console.WriteLine("A)Phishing");
                Console.WriteLine("B)Password safety");
                Console.WriteLine("C)Safe browsing");
                Console.WriteLine("Please select an option A, B or C.");
                string learnResponse = Console.ReadLine().ToUpper();

                if (learnResponse == "A")
                    Console.WriteLine("Bot: Phishing happens when an intruder gains access to your personal information through false pretenses.");
                else if (learnResponse == "B")
                    Console.WriteLine("Bot: Password safety refers to how unpredictable and secure your password is. Always make sure to never share your password and never make it too predictable.");
                else if (learnResponse == "C")
                    Console.WriteLine("Bot: Safe browsing means that when you are on the internet you should always be aware that websites could be unsafe. Only use reliable websites.");
                else
                    Console.WriteLine("Bot: Invalid response. Please choose from A, B and C.");
            }
            else
            {
                Console.WriteLine("Bot: Invalid option. Try again.");
            }

            Console.WriteLine("Thank you " + userName + "!");
        }
    }
}
