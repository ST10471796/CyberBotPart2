namespace CyberBotPart2
{
    public static class MemoryStore
    {
        public static string UserName { get; set; }
        public static string UserInterest { get; set; }

        public static string GetWelcomeBack()
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                if (!string.IsNullOrEmpty(UserInterest))
                    return "Welcome back, " + UserName + "! I remember you like " + UserInterest + ".";
                else
                    return "Welcome back, " + UserName + "!";
            }
            return "Welcome to the Cybersecurity Bot!";
        }
    }
}