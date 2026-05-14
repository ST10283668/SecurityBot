[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/TQBaksvT)

# SecurityBot - Cybersecurity Awareness Assistant

Student number: ST10283668

SecurityBot is a Windows Forms cybersecurity awareness chatbot created for Programming Part 2. The application gives  users information about common online safety  in a simple conversation format.

## Part 2 Features

- Windows Forms graphical interface.
- Voice greeting using `welcome.wav`.
- ASCII-style SecurityBot banner displayed inside the GUI.
- Dictionary and List based cybersecurity responses.
- Keyword recognition for password safety, phishing, scams, privacy, and safe browsing.
- Random response selection so the bot does not always repeat the same answer.
- Follow-up handling for messages 
- User memory for the user's name and favourite cybersecurity topic.
- Sentiment detection for angry, confused, sad, and happy messages.
- Friendly fallback response for unsupported topics.
 

## How To Run

1. Open the project in Visual Studio.
2 Run the project.
3. Type your answers in the input box at the bottom of the window and press Send.

The bot starts by asking for your name. It then asks for your favourite cybersecurity topic:

The user can then proceed to answer and will receive an answer until they are satitified 

## Project Structure

- `Program.cs` starts the Windows Forms application.
- `MainForm.cs` contains the graphical user interface.
- `ChatbotEngine.cs` contains the chatbot response logic.
- `UserMemory.cs` stores user details used during the conversation.
- `AudioGreeting.cs` plays the WAV greeting.
- `Display.cs` stores the ASCII banner text.




## References

### Cybersecurity References

1. Pieterse, H. 2021. The Cyber Threat Landscape in South Africa: A 10-Year Review. The African Journal of Information and Communication, 28(28). Available at: https://doi.org/10.23962/10539/32213
2. South African Police Service. Be Alert: Phishing, Vishing and SMishing. Available at: https://www.saps.gov.za/alert/all_shing.php
3. South African Police Service. Safety Awareness: Fraud and Scams. Available at: https://www.saps.gov.za/alert/safety_awareness_fraud_scams.php
4. Cybersecurity and Infrastructure Security Agency. Recognize and Report Phishing. Available at: https://www.cisa.gov/secure-our-world/recognize-and-report-phishing
5. Cybersecurity and Infrastructure Security Agency. 4 Things You Can Do To Keep Yourself Cyber Safe. Available at: https://www.cisa.gov/4-things-you-can-do-keep-yourself-cyber-safe
6. South African Government. Government urges vigilance against rising cyber threats. Available at: https://www.gov.za/news/media-statements/government-urges-vigilance-against-rising-cyber-threats-03-oct-2025
7. Pat or JK. ASCII Art Generator. Available at: https://www.patorjk.com/software/taag/

### Programming References

1. Microsoft Learn. Windows Forms overview. Used as a reference for creating the graphical user interface with forms, labels, text boxes, buttons, and layout controls. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/overview/
2. Microsoft Learn. `Dictionary<TKey,TValue>` class. Used as a reference for storing keyword shortcuts, sentiment words, and cybersecurity topic responses as key-value pairs. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2
3. Microsoft Learn. `List<T>` class. Used as a reference for storing multiple chatbot responses for each cybersecurity topic. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1
4. Microsoft Learn. Generics in .NET. Used as a reference for generic collections such as `Dictionary<string, List<string>>`. Available at: https://learn.microsoft.com/en-us/dotnet/standard/generics/
5. Microsoft Learn. `System.Random` class. Used as a reference for selecting random responses from the response lists. Available at: https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-random
6. Microsoft Learn. C# enumerations. Used as a reference for the `ConversationStep` enum that controls the chatbot conversation flow. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/enums
7. Microsoft Learn. Exception handling in C#. Used as a reference for `try-catch` error handling in the chatbot and audio playback logic. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/exception-handling
8. Microsoft Learn. `SoundPlayer` class. Used as a reference for playing the WAV greeting file when the application starts. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.media.soundplayer
