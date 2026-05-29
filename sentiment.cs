using System;

namespace CyberBot_Part_Two
{
    public class sentiment
    {
        public string feeling_response(string input)
        {
            input = input.ToLower();

            // worried
            if (input.Contains("worried"))
            {
                return "It's understandable to feel worried about online threats. Always avoid suspicious links and protect your personal information.";
            }

            // frustrated
            else if (input.Contains("frustrated"))
            {
                return "I understand you're frustrated. Cybersecurity issues can be stressful, but I will help you step by step.";
            }

            // confused
            else if (input.Contains("confused"))
            {
                return "No worries, cybersecurity can be confusing at first. I will explain it in a simple way.";
            }

            // angry
            else if (input.Contains("angry"))
            {
                return "I understand you're angry. Let's solve the problem together calmly.";
            }

            // sad
            else if (input.Contains("sad"))
            {
                return "I'm sorry you're feeling sad. Remember that online safety problems can usually be fixed.";
            }

            // happy
            else if (input.Contains("happy"))
            {
                return "That's wonderful to hear! Stay positive and stay cyber safe.";
            }

            return "";
        }
    }
}