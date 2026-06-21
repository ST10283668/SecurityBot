using System.Drawing;
using System.Windows.Forms;

namespace Securitybot
{
    internal class MainForm : Form
    {
        private readonly ChatbotEngine chatbotEngine;
        private readonly TaskRepository taskRepository;
        private readonly List<TaskItem> cybersecurityTasks;
        private readonly TextBox chatDisplay;
        private readonly TextBox messageInput;
        private readonly Button sendButton;
        private TextBox? taskTitleInput;
        private TextBox? taskDescriptionInput;
        private DateTimePicker? taskReminderPicker;
        private ComboBox? taskCategoryBox;
        private ListBox? taskListBox;

        public MainForm()
        {
            chatbotEngine = new ChatbotEngine();
            taskRepository = new TaskRepository();
            cybersecurityTasks = new List<TaskItem>();

            Text = "SecurityBot - Cybersecurity Awareness Assistant";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(900, 650);
            BackColor = Color.FromArgb(35, 28, 22);

            TabControl mainTabs = CreateMainTabs();
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

            TabPage chatPage = CreateTabPage("Chat");
            chatPage.Controls.Add(layout);

            TabPage taskPage = CreateTabPage("Task Assistant");
            taskPage.Controls.Add(CreateTaskAssistantLayout());

            mainTabs.TabPages.Add(chatPage);
            mainTabs.TabPages.Add(taskPage);
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
                RowCount = 7,
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
            layout.Controls.Add(taskListBox, 0, 6);
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
                AddBotMessage(chatbotEngine.GetResponse(message));
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
            MessageBox.Show("Mark complete will be connected to the database in the next update.", "Task Assistant");
        }

        private void DeleteTaskButton_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Delete will be connected to the database in the next update.", "Task Assistant");
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
                return;
            }

            foreach (TaskItem task in cybersecurityTasks)
            {
                taskListBox.Items.Add(task.GetDisplayText());
            }
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
