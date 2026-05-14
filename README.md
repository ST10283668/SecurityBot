[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/TQBaksvT)

# SecurityBot - Cybersecurity Awareness Assistant

Student number: ST10283668

SecurityBot is a Windows Forms cybersecurity awareness chatbot created for Programming 2A Part 2. The application teaches users about common online safety topics in a simple conversation format.

## Part 2 Features

- Windows Forms graphical interface.
- Voice greeting using `welcome.wav`.
- ASCII-style SecurityBot banner displayed inside the GUI.
- Dictionary and List based cybersecurity responses.
- Keyword recognition for password safety, phishing, scams, privacy, and safe browsing.
- Random response selection so the bot does not always repeat the same answer.
- Follow-up handling for messages such as `tell me more`, `another tip`, `explain more`, `I'm confused`, and `I don't know`.
- User memory for the user's name and favourite cybersecurity topic.
- Recall feature for questions such as `what is my name?`.
- Sentiment detection for angry, confused, sad, and happy messages.
- Friendly fallback response for unsupported topics.
- Try-catch exception handling around message processing and audio playback.

## How To Run

1. Open the project in Visual Studio.
2. Restore NuGet packages if Visual Studio asks.
3. Build the project.
4. Run the project.
5. Type your answers in the input box at the bottom of the window and press Send.

The bot starts by asking for your name. It then asks for your favourite cybersecurity topic:

```text
A) Password safety
B) Phishing awareness
C) Privacy protection
D) Scam prevention
E) Safe browsing
```

## Example Questions

```text
password
phishing
privacy
another tip
tell me more
what is my name?
I am confused
I am angry about scams
```

## Project Structure

- `Program.cs` starts the Windows Forms application.
- `MainForm.cs` contains the graphical user interface.
- `ChatbotEngine.cs` contains the chatbot response logic.
- `UserMemory.cs` stores user details used during the conversation.
- `AudioGreeting.cs` plays the WAV greeting.
- `Display.cs` stores the ASCII banner text.
- `.github/workflows/build.yml` contains the GitHub Actions build workflow.

## Submission Notes

- The repository contains the source code and multimedia greeting file.
- GitHub Actions is configured to restore and build the project.
- The video presentation link can be added here before final ARC submission.
- A screenshot of a successful GitHub Actions run should be added to the repository or README before final submission.

## References

1. Pieterse, H. 2021. The Cyber Threat Landscape in South Africa: A 10-Year Review. The African Journal of Information and Communication, 28(28).
2. IBM. What is phishing? Available at: https://www.ibm.com/think/topics/phishing
3. Pat or JK. ASCII Art Generator. Available at: https://www.patorjk.com/software/taag/
