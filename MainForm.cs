using System.Drawing;
using System.Windows.Forms;

namespace Securitybot
{
    internal class MainForm : Form
    {
        private readonly ChatbotEngine chatbotEngine;
        private readonly TaskRepository taskRepository;
        private readonly List<TaskItem> cybersecurityTasks;
        private readonly List<QuizQuestion> quizQuestions;
        private readonly List<ActivityLogEntry> activityLogEntries;
        private readonly TextBox chatDisplay;
        private readonly TextBox messageInput;
        private readonly Button sendButton;
        private TabControl? mainTabs;
        private TabPage? chatPage;
        private TabPage? taskPage;
        private TabPage? quizPage;
        private TabPage? activityLogPage;
        private TextBox? taskTitleInput;
        private TextBox? taskDescriptionInput;
        private DateTimePicker? taskReminderPicker;
        private ComboBox? taskCategoryBox;
        private Label? taskReminderSummaryLabel;
        private ListBox? taskListBox;
        private Label? quizProgressLabel;
        private Label? quizQuestionLabel;
        private Label? quizFeedbackLabel;
        private RadioButton? quizOptionA;
        private RadioButton? quizOptionB;
        private RadioButton? quizOptionC;
        private RadioButton? quizOptionD;
        private Button? quizSubmitButton;
        private Button? quizRestartButton;
        private ListBox? activityLogListBox;
        private Button? showMoreLogButton;
        private int currentQuizIndex;
        private int quizScore;
        private bool quizAnswered;
        private int visibleLogCount = 5;

        public MainForm()
        {
            chatbotEngine = new ChatbotEngine();
            taskRepository = new TaskRepository();
            cybersecurityTasks = new List<TaskItem>();
            quizQuestions = CreateQuizQuestions();
            activityLogEntries = new List<ActivityLogEntry>();

            Text = "SecurityBot - Cybersecurity Awareness Assistant";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(900, 650);
            BackColor = Color.FromArgb(35, 28, 22);

            mainTabs = CreateMainTabs();
            TableLayoutPanel layout = CreateChatLayout();
            Label titleLabel = CreateTitleLabel();
            TextBox logoBox = CreateLogoBox();
            chatDisplay = CreateChatDisplay();
            messageInput = CreateMessageInput();
            sendButton = CreateSendButton();

            layout.Controls.Add(titleLabel, 0, 0);
            layout.Controls.Add(logoBox, 0, 1);
            layout.Controls.Add(chatDisplay, 0, 2);
            layout.Controls.Add(messageInput, 0, 3);
            layout.Controls.Add(sendButton, 0, 4);

            chatPage = CreateTabPage("Chat");
            chatPage.Controls.Add(layout);

            taskPage = CreateTabPage("Task Assistant");
            taskPage.Controls.Add(CreateTaskAssistantLayout());

            quizPage = CreateTabPage("Quiz Game");
            quizPage.Controls.Add(CreateQuizLayout());

            activityLogPage = CreateTabPage("Activity Log");
            activityLogPage.Controls.Add(CreateActivityLogLayout());

            mainTabs.TabPages.Add(chatPage);
            mainTabs.TabPages.Add(taskPage);
            mainTabs.TabPages.Add(quizPage);
            mainTabs.TabPages.Add(activityLogPage);
            Controls.Add(mainTabs);

            Load += MainForm_Load;
            Shown += MainForm_Shown;
            sendButton.Click += SendButton_Click;
            messageInput.KeyDown += MessageInput_KeyDown;
        }

        private TabControl CreateMainTabs()
        {
            return new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
        }

        private TabPage CreateTabPage(string title)
        {
            return new TabPage
            {
                Text = title,
                BackColor = Color.FromArgb(35, 28, 22),
                ForeColor = Color.FromArgb(35, 28, 22)
            };
        }

        private TableLayoutPanel CreateChatLayout()
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                BackColor = Color.FromArgb(35, 28, 22),
                Padding = new Padding(12)
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 105));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));

            return layout;
        }

        private TableLayoutPanel CreateTaskAssistantLayout()
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 8,
                Padding = new Padding(16),
                BackColor = Color.FromArgb(35, 28, 22)
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 92));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            Label heading = new Label
            {
                Text = "Cybersecurity Task Assistant",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 247, 230)
            };

            taskTitleInput = CreateTaskTextBox("Task title");
            taskDescriptionInput = CreateTaskTextBox("Task description");
            taskDescriptionInput.Multiline = true;

            taskReminderPicker = new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd MMM yyyy HH:mm",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                CalendarForeColor = Color.FromArgb(35, 28, 22),
                CalendarMonthBackground = Color.FromArgb(255, 247, 230)
            };

            taskCategoryBox = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            taskCategoryBox.Items.AddRange(new object[]
            {
                "Password safety",
                "Phishing awareness",
                "Privacy settings",
                "Device updates",
                "Safe browsing"
            });
            taskCategoryBox.SelectedIndex = 0;

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            Button addTaskButton = CreateTaskButton("Add Task");
            Button completeTaskButton = CreateTaskButton("Mark Complete");
            Button deleteTaskButton = CreateTaskButton("Delete");
            addTaskButton.Click += AddTaskButton_Click;
            completeTaskButton.Click += CompleteTaskButton_Click;
            deleteTaskButton.Click += DeleteTaskButton_Click;
            buttonPanel.Controls.Add(addTaskButton);
            buttonPanel.Controls.Add(completeTaskButton);
            buttonPanel.Controls.Add(deleteTaskButton);

            taskReminderSummaryLabel = new Label
            {
                Text = "Reminder summary will appear here.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 247, 230)
            };

            taskListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(35, 28, 22),
                BackColor = Color.FromArgb(255, 247, 230),
                BorderStyle = BorderStyle.FixedSingle
            };
            taskListBox.Items.Add("Saved tasks will appear here.");

            layout.Controls.Add(heading, 0, 0);
            layout.SetColumnSpan(heading, 2);
            layout.Controls.Add(CreateTaskLabel("Title"), 0, 1);
            layout.Controls.Add(taskTitleInput, 1, 1);
            layout.Controls.Add(CreateTaskLabel("Description"), 0, 2);
            layout.Controls.Add(taskDescriptionInput, 1, 2);
            layout.Controls.Add(CreateTaskLabel("Reminder"), 0, 3);
            layout.Controls.Add(taskReminderPicker, 1, 3);
            layout.Controls.Add(CreateTaskLabel("Category"), 0, 4);
            layout.Controls.Add(taskCategoryBox, 1, 4);
            layout.Controls.Add(buttonPanel, 1, 5);
            layout.Controls.Add(taskReminderSummaryLabel, 0, 6);
            layout.SetColumnSpan(taskReminderSummaryLabel, 2);
            layout.Controls.Add(taskListBox, 0, 7);
            layout.SetColumnSpan(taskListBox, 2);

            return layout;
        }

        private Label CreateTaskLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 247, 230)
            };
        }

        private TextBox CreateTaskTextBox(string placeholderText)
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(35, 28, 22),
                BackColor = Color.FromArgb(255, 247, 230),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = placeholderText
            };
        }

        private Button CreateTaskButton(string text)
        {
            Button button = new Button
            {
                Text = text,
                Width = 135,
                Height = 36,
                Margin = new Padding(0, 4, 8, 4),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 28, 22),
                BackColor = Color.FromArgb(255, 176, 77),
                FlatStyle = FlatStyle.Flat
            };

            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private TableLayoutPanel CreateQuizLayout()
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 8,
                Padding = new Padding(18),
                BackColor = Color.FromArgb(35, 28, 22)
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 78));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            quizProgressLabel = CreateQuizLabel("Question 1 of 10 | Score: 0", 12, FontStyle.Bold);
            quizQuestionLabel = CreateQuizLabel(string.Empty, 12, FontStyle.Regular);
            quizOptionA = CreateQuizOption();
            quizOptionB = CreateQuizOption();
            quizOptionC = CreateQuizOption();
            quizOptionD = CreateQuizOption();
            quizFeedbackLabel = CreateQuizLabel("Choose an answer and press Submit.", 10, FontStyle.Regular);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            quizSubmitButton = CreateTaskButton("Submit Answer");
            quizRestartButton = CreateTaskButton("Restart Quiz");
            quizSubmitButton.Click += QuizSubmitButton_Click;
            quizRestartButton.Click += QuizRestartButton_Click;
            buttonPanel.Controls.Add(quizSubmitButton);
            buttonPanel.Controls.Add(quizRestartButton);

            layout.Controls.Add(CreateQuizLabel("Cybersecurity Mini Quiz", 15, FontStyle.Bold), 0, 0);
            layout.Controls.Add(quizQuestionLabel, 0, 1);
            layout.Controls.Add(quizOptionA, 0, 2);
            layout.Controls.Add(quizOptionB, 0, 3);
            layout.Controls.Add(quizOptionC, 0, 4);
            layout.Controls.Add(quizOptionD, 0, 5);
            layout.Controls.Add(quizFeedbackLabel, 0, 6);
            layout.Controls.Add(buttonPanel, 0, 7);

            ShowQuizQuestion();
            return layout;
        }

        private Label CreateQuizLabel(string text, int size, FontStyle style)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", size, style),
                ForeColor = Color.FromArgb(255, 247, 230)
            };
        }

        private RadioButton CreateQuizOption()
        {
            return new RadioButton
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(255, 247, 230),
                AutoSize = false
            };
        }

        private TableLayoutPanel CreateActivityLogLayout()
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(18),
                BackColor = Color.FromArgb(35, 28, 22)
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));

            activityLogListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(35, 28, 22),
                BackColor = Color.FromArgb(255, 247, 230),
                BorderStyle = BorderStyle.FixedSingle
            };

            showMoreLogButton = CreateTaskButton("Show More");
            showMoreLogButton.Click += ShowMoreLogButton_Click;

            layout.Controls.Add(CreateQuizLabel("Activity Log", 15, FontStyle.Bold), 0, 0);
            layout.Controls.Add(activityLogListBox, 0, 1);
            layout.Controls.Add(showMoreLogButton, 0, 2);

            RefreshActivityLog();
            return layout;
        }

        private Label CreateTitleLabel()
        {
            return new Label
            {
                Text = "Cybersecurity Awareness Assistant",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 247, 230),
                BackColor = Color.FromArgb(204, 91, 31)
            };
        }

        private TextBox CreateLogoBox()
        {
            return new TextBox
            {
                Text = Display.GetAsciiLogo(),
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                ScrollBars = ScrollBars.None,
                Font = new Font("Consolas", 6, FontStyle.Regular),
                ForeColor = Color.FromArgb(255, 190, 112),
                BackColor = Color.FromArgb(35, 28, 22),
                TabStop = false
            };
        }

        private TextBox CreateChatDisplay()
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(255, 247, 230),
                BackColor = Color.FromArgb(52, 42, 34),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private TextBox CreateMessageInput()
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(255, 247, 230),
                BackColor = Color.FromArgb(67, 51, 39),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Type your response here"
            };
        }

        private Button CreateSendButton()
        {
            return new Button
            {
                Text = "Send",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 28, 22),
                BackColor = Color.FromArgb(255, 176, 77),
                FlatStyle = FlatStyle.Flat
            };
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            AudioGreeting.PlayGreeting();
            LoadTasksFromDatabase();
            AddBotMessage(chatbotEngine.GetOpeningMessage());

            string progressMessage = chatbotEngine.GetProgressMessage();

            if (!string.IsNullOrWhiteSpace(progressMessage))
            {
                AddBotMessage(progressMessage);
            }
        }

        private void MainForm_Shown(object? sender, EventArgs e)
        {
            messageInput.Focus();
        }

        private void SendButton_Click(object? sender, EventArgs e)
        {
            SendCurrentMessage();
        }

        private void MessageInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendCurrentMessage();
            }
        }

        private void SendCurrentMessage()
        {
            try
            {
                string message = messageInput.Text.Trim();

                if (string.IsNullOrWhiteSpace(message))
                {
                    AddBotMessage(chatbotEngine.GetResponse(message));
                    return;
                }

                AddUserMessage(message);
                messageInput.Clear();

                string? commandIntent = chatbotEngine.GetCommandIntent(message);

                if (commandIntent != null)
                {
                    HandleChatCommand(commandIntent);
                    return;
                }

                AddBotMessage(chatbotEngine.GetResponse(message));
                AddActivityLog("Chatbot responded to a user message.");
            }
            catch (Exception ex)
            {
                AddBotMessage("Something went wrong while reading your message. Please try again.");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void AddTaskButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (taskTitleInput == null || taskDescriptionInput == null || taskReminderPicker == null || taskCategoryBox == null)
                {
                    MessageBox.Show("Task controls are not ready yet.", "Task Assistant");
                    return;
                }

                string title = taskTitleInput.Text.Trim();

                if (string.IsNullOrWhiteSpace(title))
                {
                    MessageBox.Show("Please enter a task title.", "Task Assistant");
                    return;
                }

                TaskItem task = new TaskItem
                {
                    Title = title,
                    Description = taskDescriptionInput.Text.Trim(),
                    ReminderDate = taskReminderPicker.Value,
                    Category = taskCategoryBox.Text,
                    IsComplete = false
                };

                task.Id = taskRepository.AddTask(task);
                cybersecurityTasks.Add(task);
                RefreshTaskList();
                ClearTaskInputs();
                AddActivityLog("Task added: " + task.Title);
                MessageBox.Show("Task saved to the database.", "Task Assistant");
            }
            catch (Exception ex)
            {
                MessageBox.Show("The task could not be saved. Please try again.", "Task Assistant");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void CompleteTaskButton_Click(object? sender, EventArgs e)
        {
            try
            {
                TaskItem? selectedTask = GetSelectedTask();

                if (selectedTask == null)
                {
                    MessageBox.Show("Please select a task first.", "Task Assistant");
                    return;
                }

                if (selectedTask.IsComplete)
                {
                    MessageBox.Show("This task is already marked as complete.", "Task Assistant");
                    return;
                }

                taskRepository.MarkTaskComplete(selectedTask.Id);
                selectedTask.IsComplete = true;
                RefreshTaskList();
                AddActivityLog("Task completed: " + selectedTask.Title);
                MessageBox.Show("Task marked as complete.", "Task Assistant");
            }
            catch (Exception ex)
            {
                MessageBox.Show("The task could not be updated. Please try again.", "Task Assistant");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void DeleteTaskButton_Click(object? sender, EventArgs e)
        {
            try
            {
                TaskItem? selectedTask = GetSelectedTask();

                if (selectedTask == null)
                {
                    MessageBox.Show("Please select a task first.", "Task Assistant");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this task?",
                    "Task Assistant",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                taskRepository.DeleteTask(selectedTask.Id);
                cybersecurityTasks.Remove(selectedTask);
                RefreshTaskList();
                AddActivityLog("Task deleted: " + selectedTask.Title);
                MessageBox.Show("Task deleted.", "Task Assistant");
            }
            catch (Exception ex)
            {
                MessageBox.Show("The task could not be deleted. Please try again.", "Task Assistant");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void QuizSubmitButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (quizAnswered)
                {
                    MoveToNextQuizQuestion();
                    return;
                }

                int selectedIndex = GetSelectedQuizIndex();

                if (selectedIndex < 0)
                {
                    SetQuizFeedback("Please choose A, B, C, or D before submitting.");
                    return;
                }

                QuizQuestion question = quizQuestions[currentQuizIndex];
                quizAnswered = true;

                if (selectedIndex == question.CorrectIndex)
                {
                    quizScore++;
                    SetQuizFeedback("Correct. " + question.Explanation);
                    AddActivityLog($"Quiz question {currentQuizIndex + 1} answered correctly.");
                }
                else
                {
                    SetQuizFeedback("Not quite. " + question.Explanation);
                    AddActivityLog($"Quiz question {currentQuizIndex + 1} answered incorrectly.");
                }

                if (quizSubmitButton != null)
                {
                    quizSubmitButton.Text = currentQuizIndex == quizQuestions.Count - 1 ? "Finish Quiz" : "Next Question";
                }

                UpdateQuizProgress();
            }
            catch (Exception ex)
            {
                SetQuizFeedback("Something went wrong in the quiz. Please try again.");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void QuizRestartButton_Click(object? sender, EventArgs e)
        {
            currentQuizIndex = 0;
            quizScore = 0;
            quizAnswered = false;
            ShowQuizQuestion();
            AddActivityLog("Quiz restarted.");
        }

        private void MoveToNextQuizQuestion()
        {
            if (currentQuizIndex >= quizQuestions.Count - 1)
            {
                ShowQuizResults();
                return;
            }

            currentQuizIndex++;
            quizAnswered = false;
            ShowQuizQuestion();
        }

        private void ShowQuizQuestion()
        {
            if (quizQuestionLabel == null || quizOptionA == null || quizOptionB == null || quizOptionC == null || quizOptionD == null)
            {
                return;
            }

            QuizQuestion question = quizQuestions[currentQuizIndex];
            quizQuestionLabel.Text = question.Question;
            quizOptionA.Text = question.GetOptionText(0);
            quizOptionB.Text = question.GetOptionText(1);
            quizOptionC.Text = question.GetOptionText(2);
            quizOptionD.Text = question.GetOptionText(3);
            quizOptionA.Checked = false;
            quizOptionB.Checked = false;
            quizOptionC.Checked = false;
            quizOptionD.Checked = false;
            SetQuizOptionsEnabled(true);
            SetQuizFeedback("Choose an answer and press Submit.");

            if (quizSubmitButton != null)
            {
                quizSubmitButton.Text = "Submit Answer";
            }

            UpdateQuizProgress();
        }

        private void ShowQuizResults()
        {
            if (quizQuestionLabel == null)
            {
                return;
            }

            quizQuestionLabel.Text = $"Quiz complete. Your final score is {quizScore} out of {quizQuestions.Count}.";
            SetQuizOptionsEnabled(false);
            SetQuizFeedback("Restart the quiz to try again and improve your cybersecurity knowledge.");
            AddActivityLog($"Quiz completed with score {quizScore} out of {quizQuestions.Count}.");

            if (quizSubmitButton != null)
            {
                quizSubmitButton.Text = "Quiz Finished";
                quizSubmitButton.Enabled = false;
            }
        }

        private int GetSelectedQuizIndex()
        {
            if (quizOptionA != null && quizOptionA.Checked)
            {
                return 0;
            }

            if (quizOptionB != null && quizOptionB.Checked)
            {
                return 1;
            }

            if (quizOptionC != null && quizOptionC.Checked)
            {
                return 2;
            }

            if (quizOptionD != null && quizOptionD.Checked)
            {
                return 3;
            }

            return -1;
        }

        private void UpdateQuizProgress()
        {
            if (quizProgressLabel != null)
            {
                quizProgressLabel.Text = $"Question {currentQuizIndex + 1} of {quizQuestions.Count} | Score: {quizScore}";
            }
        }

        private void SetQuizFeedback(string message)
        {
            if (quizFeedbackLabel != null)
            {
                quizFeedbackLabel.Text = message;
            }
        }

        private void SetQuizOptionsEnabled(bool isEnabled)
        {
            if (quizOptionA != null)
            {
                quizOptionA.Enabled = isEnabled;
            }

            if (quizOptionB != null)
            {
                quizOptionB.Enabled = isEnabled;
            }

            if (quizOptionC != null)
            {
                quizOptionC.Enabled = isEnabled;
            }

            if (quizOptionD != null)
            {
                quizOptionD.Enabled = isEnabled;
            }

            if (quizSubmitButton != null)
            {
                quizSubmitButton.Enabled = true;
            }
        }

        private List<QuizQuestion> CreateQuizQuestions()
        {
            return new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What is the safest way to handle a suspicious email link?",
                    Options = new List<string> { "Click it quickly", "Forward it to friends", "Check the sender and avoid the link", "Reply with your password" },
                    CorrectIndex = 2,
                    Explanation = "Suspicious links should be checked carefully and avoided if they cannot be verified."
                },
                new QuizQuestion
                {
                    Question = "Which password is strongest?",
                    Options = new List<string> { "John2006", "password123", "Summer", "River!Candle72#Cloud" },
                    CorrectIndex = 3,
                    Explanation = "Long, unique passwords with mixed characters are harder to guess."
                },
                new QuizQuestion
                {
                    Question = "What does two-factor authentication add?",
                    Options = new List<string> { "A second layer of account protection", "A faster internet connection", "A public profile", "A weaker password" },
                    CorrectIndex = 0,
                    Explanation = "Two-factor authentication adds another check besides only the password."
                },
                new QuizQuestion
                {
                    Question = "What should you do before downloading software?",
                    Options = new List<string> { "Use any pop-up", "Check that the source is trusted", "Disable antivirus", "Share your banking PIN" },
                    CorrectIndex = 1,
                    Explanation = "Trusted sources reduce the risk of downloading malware."
                },
                new QuizQuestion
                {
                    Question = "Which detail can be a sign of phishing?",
                    Options = new List<string> { "Urgent threats", "Official app store update", "Known contact number", "Clear privacy policy" },
                    CorrectIndex = 0,
                    Explanation = "Phishing often uses panic and urgency to pressure people into mistakes."
                },
                new QuizQuestion
                {
                    Question = "What is safe browsing?",
                    Options = new List<string> { "Clicking every advert", "Ignoring browser warnings", "Using trusted sites and checking addresses", "Downloading unknown attachments" },
                    CorrectIndex = 2,
                    Explanation = "Checking websites and avoiding unknown downloads helps keep browsing safer."
                },
                new QuizQuestion
                {
                    Question = "Why should app permissions be reviewed?",
                    Options = new List<string> { "To give every app full access", "To limit unnecessary access to personal data", "To remove passwords", "To make scams easier" },
                    CorrectIndex = 1,
                    Explanation = "Limiting permissions helps protect privacy."
                },
                new QuizQuestion
                {
                    Question = "What should you do if a deal online looks too good to be true?",
                    Options = new List<string> { "Pay immediately", "Verify the seller and website first", "Send your ID number", "Ignore all reviews" },
                    CorrectIndex = 1,
                    Explanation = "Scams often use fake deals, so verification is important."
                },
                new QuizQuestion
                {
                    Question = "What is malware?",
                    Options = new List<string> { "Helpful software only", "Software designed to harm or steal data", "A strong password", "A privacy setting" },
                    CorrectIndex = 1,
                    Explanation = "Malware is malicious software that can damage devices or steal information."
                },
                new QuizQuestion
                {
                    Question = "What should you do after a password may be compromised?",
                    Options = new List<string> { "Reuse it everywhere", "Post it online", "Change it and enable extra security", "Ignore it" },
                    CorrectIndex = 2,
                    Explanation = "Changing the password and adding extra protection reduces the damage."
                }
            };
        }

        private void LoadTasksFromDatabase()
        {
            try
            {
                cybersecurityTasks.Clear();
                cybersecurityTasks.AddRange(taskRepository.GetTasks());
                RefreshTaskList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Saved tasks could not be loaded.", "Task Assistant");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void HandleChatCommand(string commandIntent)
        {
            if (mainTabs == null)
            {
                return;
            }

            if (commandIntent == "tasks" && taskPage != null)
            {
                mainTabs.SelectedTab = taskPage;
                AddBotMessage("I opened the Task Assistant. You can add a cybersecurity task with a reminder.");
                AddActivityLog("NLP command opened the Task Assistant.");
            }
            else if (commandIntent == "quiz" && quizPage != null)
            {
                mainTabs.SelectedTab = quizPage;
                AddBotMessage("I opened the Cybersecurity Mini Quiz. Choose an answer and press Submit.");
                AddActivityLog("NLP command opened the quiz.");
            }
            else if (commandIntent == "log" && activityLogPage != null)
            {
                mainTabs.SelectedTab = activityLogPage;
                AddBotMessage("I opened the Activity Log.");
                AddActivityLog("NLP command opened the Activity Log.");
            }
        }

        private void AddActivityLog(string summary)
        {
            activityLogEntries.Insert(0, new ActivityLogEntry
            {
                Summary = summary
            });

            RefreshActivityLog();
        }

        private void RefreshActivityLog()
        {
            if (activityLogListBox == null)
            {
                return;
            }

            activityLogListBox.Items.Clear();

            if (activityLogEntries.Count == 0)
            {
                activityLogListBox.Items.Add("No activity recorded yet.");
            }
            else
            {
                int itemsToShow = Math.Min(visibleLogCount, activityLogEntries.Count);

                for (int index = 0; index < itemsToShow; index++)
                {
                    activityLogListBox.Items.Add(activityLogEntries[index].GetDisplayText());
                }
            }

            if (showMoreLogButton != null)
            {
                showMoreLogButton.Enabled = visibleLogCount < activityLogEntries.Count;
            }
        }

        private void ShowMoreLogButton_Click(object? sender, EventArgs e)
        {
            visibleLogCount += 5;
            RefreshActivityLog();
        }

        private void RefreshTaskList()
        {
            if (taskListBox == null)
            {
                return;
            }

            taskListBox.Items.Clear();

            if (cybersecurityTasks.Count == 0)
            {
                taskListBox.Items.Add("No saved tasks yet.");
                UpdateReminderSummary();
                return;
            }

            foreach (TaskItem task in cybersecurityTasks)
            {
                taskListBox.Items.Add(task.GetDisplayText());
            }

            UpdateReminderSummary();
        }

        private void UpdateReminderSummary()
        {
            if (taskReminderSummaryLabel == null)
            {
                return;
            }

            int pendingCount = 0;
            int dueSoonCount = 0;
            int overdueCount = 0;
            DateTime now = DateTime.Now;

            foreach (TaskItem task in cybersecurityTasks)
            {
                if (task.IsComplete)
                {
                    continue;
                }

                pendingCount++;

                if (task.ReminderDate < now)
                {
                    overdueCount++;
                }
                else if (task.ReminderDate <= now.AddDays(1))
                {
                    dueSoonCount++;
                }
            }

            taskReminderSummaryLabel.Text = $"Reminders: {pendingCount} pending | {dueSoonCount} due within 24 hours | {overdueCount} overdue";
        }

        private TaskItem? GetSelectedTask()
        {
            if (taskListBox == null || taskListBox.SelectedIndex < 0)
            {
                return null;
            }

            if (taskListBox.Items.Count != cybersecurityTasks.Count)
            {
                return null;
            }

            return cybersecurityTasks[taskListBox.SelectedIndex];
        }

        private void ClearTaskInputs()
        {
            if (taskTitleInput != null)
            {
                taskTitleInput.Clear();
            }

            if (taskDescriptionInput != null)
            {
                taskDescriptionInput.Clear();
            }

            if (taskCategoryBox != null)
            {
                taskCategoryBox.SelectedIndex = 0;
            }
        }

        private void AddUserMessage(string message)
        {
            chatDisplay.AppendText($"{message}{Environment.NewLine}{Environment.NewLine}");
        }

        private void AddBotMessage(string message)
        {
            chatDisplay.AppendText($"{message}{Environment.NewLine}{Environment.NewLine}");
        }
    }
}
