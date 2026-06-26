using System;

namespace CyberBot_Part_Two
{//start of namespace
    public class sentiment
    {//start of class
        public string feeling_response(string input)
        {//start of method
            input = input.ToLower();

            // worried
            if (input.Contains("worried"))
            {//start of if
                return "It's understandable to feel worried about online threats. Always avoid suspicious links and protect your personal information.";
            }//end of if

            // frustrated
            else if (input.Contains("frustrated"))
            {//start of else if
                return "I understand you're frustrated. Cybersecurity issues can be stressful, but I will help you step by step.";
            }//end of else if

            // confused
            else if (input.Contains("confused"))
            {//start of else if
                return "No worries, cybersecurity can be confusing at first. I will explain it in a simple way.";
            }//end of else if

            // angry
            else if (input.Contains("angry"))
            {//start of else if
                return "I understand you're angry. Let's solve the problem together calmly.";
            }//end of else if

            // sad
            else if (input.Contains("sad"))
            {//start of else if
                return "I'm sorry you're feeling sad. Remember that online safety problems can usually be fixed.";
            }//end of else if

            // happy
            else if (input.Contains("happy"))
            {//start of else if
                return "That's wonderful to hear! Stay positive and stay cyber safe.";
            }//end of else if

            return "";

        }//end of method
    }//end of class
}// end of namespace