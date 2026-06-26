using System.Collections.Generic;

namespace CyberBot_Part_Two
{
    public class Quiz_Question_Load
    {
        // Populates the provided questions list with predefined quiz questions and answers
        public void autoLoadQuiz(ref List<Question_in_quiz> questions)
        {
            // Initialize the list with a set of cybersecurity-related questions
            questions = new List<Question_in_quiz>()
            {
                new Question_in_quiz
                {
                    Text = "What is phishing?",  // Question text
                    correctAnswer = "Tricking to steal data",  // Correct answer
                    wrongAnswer = new List<string>{ "Data backup", "Safe login", "Password tips" }  // Distractors
                },
                new Question_in_quiz
                {
                    Text = "What is password safety?",
                    correctAnswer = "Unique & strong passwords",
                    wrongAnswer = new List<string>{ "Share with friends", "Short passwords", "Common words" }
                },
                new Question_in_quiz
                {
                    Text = "What is safe browsing?",
                    correctAnswer = "Use trusted sites",
                    wrongAnswer = new List<string>{ "Click all links", "Visit unknown pages", "Enable pop-ups" }
                },
                new Question_in_quiz
                {
                    Text = "Phishing email sign?",
                    correctAnswer = "Urgent or strange links",
                    wrongAnswer = new List<string>{ "Good grammar", "Known sender", "Unsubscribe button" }
                },
                new Question_in_quiz
                {
                    Text = "Strong password?",
                    correctAnswer = "P@55w0rD!#987",
                    wrongAnswer = new List<string>{ "Password123", "qwerty2024", "123456789" }
                },
                new Question_in_quiz
                {
                    Text = "When to update password?",
                    correctAnswer = "Every 3–6 months",
                    wrongAnswer = new List<string>{ "Yearly", "Never", "Only if hacked" }
                },
                new Question_in_quiz
                {
                    Text = "Risk of reused passwords?",
                    correctAnswer = "One hack = all at risk",
                    wrongAnswer = new List<string>{ "Typing delay", "Site error", "No effect" }
                },
                new Question_in_quiz
                {
                    Text = "Unsafe site sign?",
                    correctAnswer = "Typos & pop-ups",
                    wrongAnswer = new List<string>{ "HTTPS shown", "Fast load", "No ads" }
                },
                new Question_in_quiz
                {
                    Text = "Safe on public Wi-Fi?",
                    correctAnswer = "Use VPN / avoid private info",
                    wrongAnswer = new List<string>{ "Bank online", "File share", "Shop online" }
                },
                new Question_in_quiz
                {
                    Text = "Flagged site action?",
                    correctAnswer = "Leave right away",
                    wrongAnswer = new List<string>{ "Ignore it", "Refresh page", "Click through" }
                },
            };
        }
    }
}