namespace Securitybot
{
    internal class ChatbotEngine
    {
        private readonly Dictionary<string, List<string>> topicResponses;
        private readonly Dictionary<string, string> topicShortcuts;

        public ChatbotEngine()
        {
            topicShortcuts = new Dictionary<string, string>
            {
                { "a", "password" },
                { "b", "phishing" },
                { "c", "privacy" }
            };

            topicResponses = new Dictionary<string, List<string>>
            {
                {
                    "password",
                    new List<string>
                    {
                        "A strong password should be long, unique, and difficult to guess. Avoid using names, birthdays, or simple number patterns."
                    }
                },
                {
                    "phishing",
                    new List<string>
                    {
                        "Phishing messages often try to rush you into clicking a link. Check the sender, spelling, and web address before you respond."
                    }
                },
                {
                    "scam",
                    new List<string>
                    {
                        "Scams often promise prizes, refunds, or urgent account fixes. If something feels suspicious, verify it through the official website or support number."
                    }
                },
                {
                    "privacy",
                    new List<string>
                    {
                        "Protect your privacy by checking app permissions, limiting what you share publicly, and using strong security settings on your accounts."
                    }
                },
                {
                    "browsing",
                    new List<string>
                    {
                        "Safe browsing means using trusted websites, checking for HTTPS, and avoiding downloads from unknown sources."
                    }
                }
            };
        }

        public string GetOpeningMessage()
        {
            return "Welcome to SecurityBot.";
        }

        public string GetProgressMessage()
        {
            return "Choose a topic or type your own question:" + Environment.NewLine
                + "A) Password safety" + Environment.NewLine
                + "B) Phishing awareness" + Environment.NewLine
                + "C) Privacy protection";
        }

        public string GetResponse(string userMessage)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userMessage))
                {
                    return "Please type a message before pressing Send.";
                }

                string? topic = FindTopic(userMessage);

                if (topic != null)
                {
                    return topicResponses[topic][0];
                }

                return "I am not sure I understand that topic yet. Try asking me about passwords, phishing, scams, privacy, or safe browsing.";
            }
            catch (Exception)
            {
                return "Sorry, something went wrong while I was processing your message. Please try again.";
            }
        }

        private string? FindTopic(string userMessage)
        {
            List<string> words = new List<string>(userMessage.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));

            foreach (string word in words)
            {
                if (topicShortcuts.TryGetValue(word, out string? shortcutTopic))
                {
                    return shortcutTopic;
                }

                if (topicResponses.TryGetValue(word, out List<string>? responses) && responses.Count > 0)
                {
                    return word;
                }
            }

            return null;
        }
    }
}
