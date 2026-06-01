using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberBotPart2
{
    public partial class MainForm : Form
    {
        private RichTextBox chatDisplay;
        private TextBox userInput;
        private Button sendButton;
        private Button clearButton;
        private Label statusLabel;

        private SentimentAnalyzer sentiment;
        private bool waitingForName = true;
        private bool waitingForInterest = false;

        public MainForm()
        {
            InitializeComponent();
            SetupUI();
            StartBot();
        }

        private void SetupUI()
        {
            this.Text = "Cybersecurity Chatbot - Part 2";
            this.Size = new Size(900, 550);
            this.BackColor = Color.FromArgb(30, 30, 45);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Chat display
            chatDisplay = new RichTextBox();
            chatDisplay.Location = new Point(12, 12);
            chatDisplay.Size = new Size(860, 380);
            chatDisplay.BackColor = Color.FromArgb(20, 20, 30);
            chatDisplay.ForeColor = Color.LightGreen;
            chatDisplay.Font = new Font("Consolas", 10);
            chatDisplay.ReadOnly = true;

            // User input
            userInput = new TextBox();
            userInput.Location = new Point(12, 400);
            userInput.Size = new Size(700, 30);
            userInput.BackColor = Color.FromArgb(50, 50, 70);
            userInput.ForeColor = Color.White;
            userInput.KeyPress += UserInput_KeyPress;

            // Send button
            sendButton = new Button();
            sendButton.Text = "Send";
            sendButton.Location = new Point(720, 398);
            sendButton.Size = new Size(70, 35);
            sendButton.BackColor = Color.FromArgb(0, 120, 215);
            sendButton.ForeColor = Color.White;
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.Click += SendButton_Click;

            // Clear button
            clearButton = new Button();
            clearButton.Text = "Clear";
            clearButton.Location = new Point(798, 398);
            clearButton.Size = new Size(70, 35);
            clearButton.BackColor = Color.FromArgb(200, 50, 50);
            clearButton.ForeColor = Color.White;
            clearButton.FlatStyle = FlatStyle.Flat;
            clearButton.Click += (s, e) => chatDisplay.Clear();

            // Status label
            statusLabel = new Label();
            statusLabel.Text = "Ready";
            statusLabel.Location = new Point(12, 440);
            statusLabel.Size = new Size(860, 25);
            statusLabel.ForeColor = Color.LightGray;

            // Add controls
            this.Controls.Add(chatDisplay);
            this.Controls.Add(userInput);
            this.Controls.Add(sendButton);
            this.Controls.Add(clearButton);
            this.Controls.Add(statusLabel);
        }

        private void StartBot()
        {
            sentiment = new SentimentAnalyzer();
            AudioManager.PlayVoiceGreeting();

            AddMessage("=========================================", Color.Cyan);
            AddMessage("   CYBERSECURITY AWARENESS CHATBOT", Color.Cyan);
            AddMessage("=========================================", Color.Cyan);
            AddMessage("", Color.White);
            AddMessage("I am here to help you stay safe online.", Color.LightGreen);
            AddMessage("", Color.White);
            AddMessage("What is your name?", Color.LightGreen);

            waitingForName = true;
            statusLabel.Text = "Waiting for name...";
        }

        private void AddMessage(string text, Color color)
        {
            if (chatDisplay.InvokeRequired)
            {
                chatDisplay.Invoke(new Action(() => AddMessage(text, color)));
                return;
            }

            chatDisplay.SelectionColor = color;
            chatDisplay.AppendText(text + "\n");
            chatDisplay.ScrollToCaret();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            ProcessInput();
        }

        private void UserInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                ProcessInput();
            }
        }

        private void ProcessInput()
        {
            string input = userInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AddMessage("You: " + input, Color.LightBlue);
            userInput.Clear();
            statusLabel.Text = "Thinking...";
            Application.DoEvents();

            string response = "";

            // Get name
            if (waitingForName)
            {
                MemoryStore.UserName = input;
                response = "Nice to meet you, " + MemoryStore.UserName + "! What cybersecurity topic interests you? (passwords, phishing, privacy)";
                AddMessage("Bot: " + response, Color.LightGreen);
                waitingForName = false;
                waitingForInterest = true;
                statusLabel.Text = "Ready";
                return;
            }

            // Get interest
            if (waitingForInterest)
            {
                MemoryStore.UserInterest = input;
                response = "Great! I will remember that you like " + MemoryStore.UserInterest + ". Type 'help' to see what I can do.";
                AddMessage("Bot: " + response, Color.LightGreen);
                waitingForInterest = false;
                statusLabel.Text = "Ready";
                return;
            }

            // Exit command
            if (input.ToLower() == "exit")
            {
                response = "Goodbye " + MemoryStore.UserName + "! Stay safe online!";
                AddMessage("Bot: " + response, Color.Magenta);
                statusLabel.Text = "Session ended";
                return;
            }

            // Sentiment detection
            Sentiment sentimentResult = sentiment.Analyze(input);
            string sentimentResponse = sentiment.GetResponse(sentimentResult, MemoryStore.UserName);
            if (!string.IsNullOrEmpty(sentimentResponse))
            {
                AddMessage("Bot: " + sentimentResponse, Color.LightYellow);
            }

            // Process keywords
            string lower = input.ToLower();

            if (lower == "help")
            {
                response = ResponseManager.GetHelp();
            }
            else if (lower.Contains("password"))
            {
                response = ResponseManager.GetRandomPasswordTip();
            }
            else if (lower.Contains("phish"))
            {
                response = ResponseManager.GetRandomPhishingTip();
            }
            else if (lower.Contains("privacy"))
            {
                response = "Protect your privacy by using strong passwords, enabling two-factor authentication, and being careful what you share on social media.";
            }
            else if (lower.Contains("browsing"))
            {
                response = "Always check for HTTPS in website addresses. Avoid clicking suspicious links and keep your browser updated.";
            }
            else if (lower.Contains("another tip") || lower.Contains("tell me more"))
            {
                response = ResponseManager.GetRandomGeneralTip();
            }
            else if (lower.Contains("hello") || lower.Contains("hi"))
            {
                response = "Hello " + MemoryStore.UserName + "! How can I help you today?";
            }
            else
            {
                response = "I don't understand that. Try: passwords, phishing, privacy, or help";
            }

            AddMessage("Bot: " + response, Color.LightGreen);
            statusLabel.Text = "Ready";
        }

        // This method is required by the designer - DO NOT DELETE
        private void MainForm_Load(object sender, EventArgs e)
        {
            // This method is called when the form loads
            // No additional startup code needed here since StartBot() is called in constructor
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(820, 253);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load_1);
            this.ResumeLayout(false);

        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}