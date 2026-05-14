using System.Drawing;
using System.Windows.Forms;

namespace Securitybot
{
    internal class MainForm : Form
    {
        private readonly ChatbotEngine chatbotEngine;
        private readonly TextBox chatDisplay;
        private readonly TextBox messageInput;
        private readonly Button sendButton;

        public MainForm()
        {
            chatbotEngine = new ChatbotEngine();

            Text = "SecurityBot - Cybersecurity Awareness Assistant";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(900, 650);
            BackColor = Color.FromArgb(35, 28, 22);

            TableLayoutPanel layout = CreateLayout();
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
            Controls.Add(layout);

            Load += MainForm_Load;
            Shown += MainForm_Shown;
            sendButton.Click += SendButton_Click;
            messageInput.KeyDown += MessageInput_KeyDown;
        }

        private TableLayoutPanel CreateLayout()
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
