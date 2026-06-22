# SecurityBot

Student number: ST10283668

SecurityBot is a Windows Forms application that teaches users about cybersecurity. I started the project as a console chatbot in Part 1, changed it to a GUI in Part 2, and added tasks, reminders, a quiz, NLP commands and an activity log for Part 3.

## Features

### Chatbot

- Asks for the user's name and favourite cybersecurity topic.
- Gives advice about passwords, phishing, scams, privacy and safe browsing.
- Uses dictionaries and lists to store keywords and responses.
- Gives different responses using `Random`.
- Remembers the previous topic and handles follow-up questions.
- Detects emotions such as angry, confused, sad and happy.

### Task Assistant

- Adds cybersecurity tasks with a title, description, category and reminder date.
- Saves and loads tasks with a local SQLite database.
- Marks tasks as complete.
- Deletes selected tasks.
- Shows pending, upcoming and overdue reminder totals.

### Quiz Game

- Contains 10 cybersecurity questions.
- Uses four multiple-choice answers for each question.
- Gives feedback after every answer.
- Keeps the user's score and allows the quiz to restart.

### NLP Commands

The chat recognises simple commands such as:

```text
show tasks
add task
start quiz
play quiz
show activity log
open log
```

### Activity Log

- Records task, quiz and chatbot actions.
- Shows five entries at first.
- The Show More button displays another five entries.

## How To Run

1. Open `Securitybot.slnx` or `Securitybot.csproj` in Visual Studio.
2. Allow Visual Studio to restore the NuGet packages.
3. Build the solution.
4. Run the project.

The project needs Windows because it uses Windows Forms and `SoundPlayer`.

## Main Files

- `Program.cs` starts the application.
- `MainForm.cs` contains the tabs and GUI controls.
- `ChatbotEngine.cs` contains the chatbot and NLP logic.
- `UserMemory.cs` stores the user's name and favourite topic.
- `TaskItem.cs` represents a cybersecurity task.
- `TaskRepository.cs` handles the SQLite task database.
- `QuizQuestion.cs` represents a quiz question.
- `ActivityLogEntry.cs` represents an activity log item.
- `AudioGreeting.cs` plays the WAV greeting.
- `Display.cs` stores the ASCII heading.

## GitHub Actions

The workflow in `.github/workflows/build.yml` restores the project and checks that it builds after a push or pull request.

## Video

The final video link will be added here before submission.

## References

1. Microsoft Learn. *Windows Forms overview*. I used this for the Windows Forms layout and controls. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/overview/
2. SQLite. *CREATE TABLE*. I used the SQLite documentation when creating the local task table. Available at: https://www.sqlite.org/lang_createtable.html
3. GitHub Docs. *Building and testing .NET*. I used this for the GitHub Actions build workflow. Available at: https://docs.github.com/en/actions/tutorials/build-and-test-code/net
4. OWASP Cheat Sheet Series. *Authentication Cheat Sheet*. I used this for password and account-security information. Available at: https://owasp.org/www-project-cheat-sheets/cheatsheets/Authentication_Cheat_Sheet.html
5. Cybersecurity and Infrastructure Security Agency. *Recognize and Report Phishing*. I used this for phishing advice and quiz content. Available at: https://www.cisa.gov/secure-our-world/recognize-and-report-phishing
