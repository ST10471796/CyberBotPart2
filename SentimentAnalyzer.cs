using System.Collections.Generic;

namespace CyberBotPart2
{
    public enum Sentiment { Neutral, Worried, Curious, Frustrated, Happy }

    public class SentimentAnalyzer
    {
        private List<string> worriedWords = new List<string>
        { "worried", "scared", "afraid", "anxious", "nervous", "concerned" };

        private List<string> curiousWords = new List<string>
        { "curious", "wonder", "interesting", "learn", "tell me", "explain", "how does" };

        private List<string> frustratedWords = new List<string>
        { "frustrated", "annoying", "difficult", "hard", "confusing", "stuck" };

        private List<string> happyWords = new List<string>
        { "happy", "great", "awesome", "excellent", "good", "fantastic" };

        public Sentiment Analyze(string userInput)
        {
            string lower = userInput.ToLower();

            foreach (string word in worriedWords)
                if (lower.Contains(word))
                    return Sentiment.Worried;

            foreach (string word in frustratedWords)
                if (lower.Contains(word))
                    return Sentiment.Frustrated;

            foreach (string word in curiousWords)
                if (lower.Contains(word))
                    return Sentiment.Curious;

            foreach (string word in happyWords)
                if (lower.Contains(word))
                    return Sentiment.Happy;

            return Sentiment.Neutral;
        }

        public string GetResponse(Sentiment sentiment, string userName)
        {
            switch (sentiment)
            {
                case Sentiment.Worried:
                    return "I understand your concern, " + userName + ". Let me give you some practical advice to help you feel more secure.";
                case Sentiment.Frustrated:
                    return "I know cybersecurity can feel overwhelming, " + userName + ". Let me break it down simply for you.";
                case Sentiment.Curious:
                    return "That's a great question, " + userName + "! I'm glad you're curious about staying safe online.";
                case Sentiment.Happy:
                    return "That's wonderful, " + userName + "! It's great to see you engaged with cybersecurity awareness.";
                default:
                    return "";
            }
        }
    }
}