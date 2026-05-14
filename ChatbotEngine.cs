namespace Securitybot
{
    internal class ChatbotEngine
    {
        private enum ConversationStep
        {
            AskingName,
            AskingFavouriteTopic,
            Ready
        }

        private readonly Random random;
        private readonly UserMemory userMemory;
        private readonly Dictionary<string, List<string>> topicResponses;
        private readonly Dictionary<string, string> topicShortcuts;
        private readonly Dictionary<string, string> sentimentWords;
        private readonly Dictionary<string, string> sentimentResponses;
        private ConversationStep conversationStep;
        private string? previousTopic;
        private string? previousAnswer;

        public ChatbotEngine()
        {
            random = new Random();
            userMemory = new UserMemory();
            conversationStep = ConversationStep.AskingName;

            topicShortcuts = new Dictionary<string, string>
            {
                { "a", "password" },
                { "b", "phishing" },
                { "c", "privacy" },
                { "d", "scam" },
                { "e", "browsing" }
            };

            sentimentWords = new Dictionary<string, string>
            {
                { "angry", "angry" },
                { "mad", "angry" },
                { "frustrated", "angry" },
                { "confused", "confused" },
                { "unsure", "confused" },
                { "lost", "confused" },
                { "sad", "sad" },
                { "upset", "sad" },
                { "unhappy", "sad" },
                { "happy", "happy" },
                { "good", "happy" },
                { "great", "happy" }
            };

            sentimentResponses = new Dictionary<string, string>
            {
                { "angry", "I understand why that would be frustrating. Let us slow it down and deal with one cybersecurity step at a time." },
                { "confused", "That is okay. Cybersecurity can feel confusing at first, so I will explain it another way." },
                { "sad", "I am sorry you are feeling that way. Online safety problems can be stressful, but you are doing the right thing by asking." },
                { "happy", "I am glad to hear that. Since you are feeling positive, let us build on that with a useful safety habit." }
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
            return "Welcome to SecurityBot. What is your name?";
        }

        public string GetProgressMessage()
        {
            return "After that, I will ask for your favourite cybersecurity topic.";
        }

        private string GetTopicMenu()
        {
            return "Choose your favourite cybersecurity topic:" + Environment.NewLine
                + "A) Password safety" + Environment.NewLine
                + "B) Phishing awareness" + Environment.NewLine
                + "C) Privacy protection" + Environment.NewLine
                + "D) Scam prevention" + Environment.NewLine
                + "E) Safe browsing";
        }

        public string GetResponse(string userMessage)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userMessage))
                {
                    return "Please type a message before pressing Send.";
                }

                if (conversationStep == ConversationStep.AskingName)
                {
                    return SaveName(userMessage);
                }

                if (conversationStep == ConversationStep.AskingFavouriteTopic)
                {
                    return SaveFavouriteTopic(userMessage);
                }

                if (IsNameQuestion(userMessage))
                {
                    return userMemory.HasName()
                        ? $"Your name is {userMemory.UserName}."
                        : "I do not know your name yet.";
                }

                string? topic = FindTopic(userMessage);
                string? sentiment = FindSentiment(userMessage);

                if (sentiment != null)
                {
                    return BuildSentimentResponse(sentiment, topic);
                }

                if (topic != null)
                {
                    return SelectTopicResponse(topic);
                }

                if (IsFollowUp(userMessage))
                {
                    return ContinuePreviousTopic();
                }

                return GetUnknownTopicResponse();
            }
            catch (Exception)
            {
                return "Sorry, something went wrong while I was processing your message. Please try again.";
            }
        }

        private string SaveName(string userMessage)
        {
            userMemory.UserName = userMessage.Trim();
            conversationStep = ConversationStep.AskingFavouriteTopic;

            return $"Nice to meet you, {userMemory.UserName}." + Environment.NewLine + GetTopicMenu();
        }

        private string SaveFavouriteTopic(string userMessage)
        {
            string? topic = FindTopic(userMessage);

            if (topic == null)
            {
                return "Please choose A, B, C, D, or E, or type a topic like password, phishing, privacy, scam, or browsing.";
            }

            userMemory.FavouriteTopic = topic;
            conversationStep = ConversationStep.Ready;
            previousTopic = topic;

            return $"Great, {userMemory.UserName}. I will remember that you are interested in {topic}." + Environment.NewLine
                + SelectTopicResponse(topic);
        }

        private string? FindTopic(string userMessage)
        {
            List<string> words = GetWords(userMessage);

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

        private string? FindSentiment(string userMessage)
        {
            List<string> words = GetWords(userMessage);

            foreach (string word in words)
            {
                if (sentimentWords.TryGetValue(word, out string? sentiment))
                {
                    return sentiment;
                }
            }

            return null;
        }

        private bool IsNameQuestion(string userMessage)
        {
            List<string> words = GetWords(userMessage);
            bool hasWhat = false;
            bool hasMy = false;
            bool hasName = false;

            foreach (string word in words)
            {
                if (word == "what")
                {
                    hasWhat = true;
                }
                else if (word == "my")
                {
                    hasMy = true;
                }
                else if (word == "name")
                {
                    hasName = true;
                }
            }

            return hasWhat && hasMy && hasName;
        }

        private string BuildSentimentResponse(string sentiment, string? topic)
        {
            string response = sentimentResponses[sentiment];

            if (sentiment == "confused" && previousTopic != null)
            {
                return response + Environment.NewLine + ContinuePreviousTopic();
            }

            if (topic != null)
            {
                return response + Environment.NewLine + SelectTopicResponse(topic);
            }

            if (userMemory.HasFavouriteTopic())
            {
                return response + Environment.NewLine + "Because you are interested in "
                    + userMemory.FavouriteTopic + ", here is a related tip:" + Environment.NewLine
                    + SelectTopicResponse(userMemory.FavouriteTopic);
            }

            return response;
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
            List<string> words = GetWords(userMessage);
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

        private List<string> GetWords(string userMessage)
        {
            string cleanedMessage = userMessage.ToLower()
                .Replace("?", "")
                .Replace(".", "")
                .Replace(",", "")
                .Replace("!", "")
                .Replace("'", "");

            return new List<string>(cleanedMessage.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }

        private string ContinuePreviousTopic()
        {
            if (previousTopic == null)
            {
                return "I can explain more once we have started a topic. Choose A, B, or C to begin.";
            }

            return "Here is another way to think about it:" + Environment.NewLine + SelectTopicResponse(previousTopic);
        }

        private string GetUnknownTopicResponse()
        {
            if (userMemory.HasFavouriteTopic())
            {
                return "I am not sure I understand that topic yet. Since you like "
                    + userMemory.FavouriteTopic
                    + ", you can ask for another tip about it, or ask about passwords, phishing, scams, privacy, or safe browsing.";
            }

            return "I am not sure I understand that topic yet. Try asking me about passwords, phishing, scams, privacy, or safe browsing.";
        }
    }
}
