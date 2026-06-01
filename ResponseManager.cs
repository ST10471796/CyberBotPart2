using System;

namespace CyberBotPart2
{
    public class ResponseManager
    {
        private static Random rand = new Random();

        public static string GetRandomPasswordTip()
        {
            string[] tips = {
                "Use a password manager to create and store unique passwords for each account.",
                "Enable Two-Factor Authentication whenever possible for extra security.",
                "Never share your passwords with anyone, not even IT support.",
                "A strong password has at least 12 characters with uppercase, lowercase, numbers, and symbols.",
                "Avoid using personal information like birthdays or pet names in your passwords.",
                "Change your passwords immediately if you suspect a breach."
            };
            return tips[rand.Next(tips.Length)];
        }

        public static string GetRandomPhishingTip()
        {
            string[] tips = {
                "Always check the sender's email address carefully - scammers use fake addresses.",
                "Hover over links before clicking to see where they actually go.",
                "Legitimate companies never ask for your password via email.",
                "Look for spelling and grammar mistakes - common signs of phishing emails.",
                "When in doubt, contact the company directly using official channels.",
                "Don't click links or download attachments from unknown senders."
            };
            return tips[rand.Next(tips.Length)];
        }

        public static string GetRandomGeneralTip()
        {
            string[] tips = {
                "Keep your software and operating system updated regularly.",
                "Use a VPN when connecting to public Wi-Fi networks.",
                "Back up your important files to an external drive or cloud service.",
                "Review your bank statements regularly for unauthorized transactions.",
                "Be careful what you share on social media - criminals use it against you.",
                "Use unique answers for security questions or treat them like passwords."
            };
            return tips[rand.Next(tips.Length)];
        }

        public static string GetHelp()
        {
            return "I can help with: passwords, phishing, privacy, browsing, another tip. Type 'exit' to quit.";
        }
    }
}