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

            Label titleLabel = CreateTitleLabel();
            TextBox logoBox = CreateLogoBox();
            chatDisplay = CreateChatDisplay();
            messageInput = CreateMessageInput();
            sendButton = CreateSendButton();

            Controls.Add(titleLabel);
            Controls.Add(logoBox);
            Controls.Add(chatDisplay);
            Controls.Add(messageInput);
            Controls.Add(sendButton);

            Load += MainForm_Load;
            sendButton.Click += SendButton_Click;
            messageInput.KeyDown += MessageInput_KeyDown;
        }

        private Label CreateTitleLabel()
        {
            return new Label
            {
                Text = "Cybersecurity Awareness Assistant",
                Dock = DockStyle.Top,
                Height = 55,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 247, 230),
                BackColor = Color.FromArgb(204, 91, 31)
            };
        }

        private TextBox CreateLogoBox()
        {
            return new TextBox
            {
                Text = Display.GetAsciiLogo(),
                Dock = DockStyle.Top,
                Height = 165,
                Multiline = true,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                ScrollBars = ScrollBars.None,
                Font = new Font("Consolas", 8, FontStyle.Regular),
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
                Dock = DockStyle.Bottom,
                Height = 42,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(255, 247, 230),
                BackColor = Color.FromArgb(67, 51, 39),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private Button CreateSendButton()
        {
            return new Button
            {
                Text = "Send",
                Dock = DockStyle.Bottom,
                Height = 42,
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
            AddBotMessage(chatbotEngine.GetProgressMessage());
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
            chatDisplay.AppendText($"You: {message}{Environment.NewLine}{Environment.NewLine}");
        }

        private void AddBotMessage(string message)
        {
            chatDisplay.AppendText($"SecurityBot: {message}{Environment.NewLine}{Environment.NewLine}");
        }
    }
}
