namespace Securitybot
{
    internal class ChatbotEngine
    {
        private readonly Random random;
        private readonly Dictionary<string, List<string>> topicResponses;
        private readonly Dictionary<string, string> topicShortcuts;
        private string? previousTopic;
        private string? previousAnswer;

        public ChatbotEngine()
        {
            random = new Random();

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
                        "A strong password should be long, unique, and difficult to guess. Avoid using names, birthdays, or simple number patterns.",
                        "Use a different password for every important account so one leak does not put everything at risk.",
                        "A password manager can help you create and store strong passwords without needing to remember each one."
                    }
                },
                {
                    "phishing",
                    new List<string>
                    {
                        "Phishing messages often try to rush you into clicking a link. Check the sender, spelling, and web address before you respond.",
                        "Do not enter personal details through a link in an unexpected message. Open the official website yourself instead.",
                        "Be careful with messages that create panic, such as account closures, fake deliveries, or urgent payment requests."
                    }
                },
                {
                    "scam",
                    new List<string>
                    {
                        "Scams often promise prizes, refunds, or urgent account fixes. If something feels suspicious, verify it through the official website or support number.",
                        "Never send money or personal details because someone pressured you online. Pause and verify the story first.",
                        "If a deal looks too good to be true, check reviews, contact details, and the website address before trusting it."
                    }
                },
                {
                    "privacy",
                    new List<string>
                    {
                        "Protect your privacy by checking app permissions, limiting what you share publicly, and using strong security settings on your accounts.",
                        "Review your social media privacy settings so strangers cannot easily see your personal information.",
                        "Only give apps the permissions they really need. A calculator app should not need your contacts or location."
                    }
                },
                {
                    "browsing",
                    new List<string>
                    {
                        "Safe browsing means using trusted websites, checking for HTTPS, and avoiding downloads from unknown sources.",
                        "Avoid downloading files from pop-ups or unknown websites because they may contain malware.",
                        "Check the website address carefully. Fake websites often use small spelling changes to look legitimate."
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
                    return SelectTopicResponse(topic);
                }

                if (IsFollowUp(userMessage))
                {
                    return ContinuePreviousTopic();
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

        private string SelectTopicResponse(string topic)
        {
            List<string> responses = topicResponses[topic];
            int responseIndex = random.Next(responses.Count);
            string selectedResponse = responses[responseIndex];

            if (responses.Count > 1 && selectedResponse == previousAnswer)
            {
                responseIndex++;

                if (responseIndex >= responses.Count)
                {
                    responseIndex = 0;
                }

                selectedResponse = responses[responseIndex];
            }

            previousTopic = topic;
            previousAnswer = selectedResponse;
            return selectedResponse;
        }

        private bool IsFollowUp(string userMessage)
        {
            List<string> words = new List<string>(userMessage.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));
            bool hasTell = false;
            bool hasMore = false;
            bool hasAnother = false;
            bool hasTip = false;
            bool hasExplain = false;
            bool hasConfused = false;
            bool hasKnow = false;

            foreach (string word in words)
            {
                if (word == "tell")
                {
                    hasTell = true;
                }
                else if (word == "more")
                {
                    hasMore = true;
                }
                else if (word == "another")
                {
                    hasAnother = true;
                }
                else if (word == "tip")
                {
                    hasTip = true;
                }
                else if (word == "explain")
                {
                    hasExplain = true;
                }
                else if (word == "confused")
                {
                    hasConfused = true;
                }
                else if (word == "know")
                {
                    hasKnow = true;
                }
            }

            return (hasTell && hasMore) || (hasAnother && hasTip) || (hasExplain && hasMore) || hasConfused || hasKnow;
        }

        private string ContinuePreviousTopic()
        {
            if (previousTopic == null)
            {
                return "I can explain more once we have started a topic. Choose A, B, or C to begin.";
            }

            return "Here is another way to think about it:" + Environment.NewLine + SelectTopicResponse(previousTopic);
        }
    }
}
