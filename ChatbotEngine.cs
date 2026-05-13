namespace Securitybot
{
    internal class ChatbotEngine
    {
        public string GetOpeningMessage()
        {
            return "Hello! Welcome to SecurityBot. I am here to help South African citizens learn safer cybersecurity habits.";
        }

        public string GetProgressMessage()
        {
            return "For now, type a message below. In the next updates, I will add topic recognition, memory, random tips, and sentiment detection.";
        }

        public string GetResponse(string userMessage)
        {
            if (string.IsNullOrWhiteSpace(userMessage))
            {
                return "Please type a message before pressing Send.";
            }

            return "Thanks for your message. My full cybersecurity response engine will be added in the next progression step.";
        }
    }
}
