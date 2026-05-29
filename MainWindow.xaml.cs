
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CyberBot_Part_Two
{//start of namespace

    public partial class MainWindow : Window
    {//start of class

        //creating an instance for the class Array
        ArrayList reply = new ArrayList();
        ArrayList ignore = new ArrayList();
        user_name check_name = new user_name();

        // sentiment object
        sentiment feelings = new sentiment();

        // variables
        string username = string.Empty;
        string pre_question = string.Empty;
        int counting = 0;

        public MainWindow()
        {//start of constructor

            InitializeComponent();

            new respond(reply, ignore);

            //creating an instance for the class voice_greeting
            voice_greeting greet = new voice_greeting();

            //call the voice method
            greet.PlaySound();

        }//end of constructor

        //proceed event handler
        private void proceed(object sender, RoutedEventArgs e)
        {// show welcome message

            //Hide home page grid and set Username grid visible
            home_grid.Visibility = Visibility.Hidden;
            username_grid.Visibility = Visibility.Visible;

        }//end of proceed event handler

        //submit name event handler
        private void submit_name(object sender, RoutedEventArgs e)
        {// Get the username from the design

            username = check_name.submit_name(usernames_input, chats);

            //Hide username page grid and set chats grid visible
            username_grid.Visibility = Visibility.Hidden;
            chat_grid.Visibility = Visibility.Visible;

        }//end of submit name event handler

        //send event handler
        private void send(object sender, RoutedEventArgs e)
        {// Store the raw question for processing

            // Get the question
            string rawQuestion = question.Text.Trim();

            if (string.IsNullOrWhiteSpace(rawQuestion))
            {//start of if statement
                error_method("CyberBot", "Please enter a question.");
                return;
            }//end of if statement

            // Remove special characters
            string questions = RemoveSpecialCharacters(rawQuestion);

            pre_question = questions;

            // Save previous question
            pre_question = questions;

            // Show user message
            error_method(username, rawQuestion);

            // detect feelings
            // detect feelings
            string feelingReply = feelings.feeling_response(rawQuestion);

            if (!string.IsNullOrWhiteSpace(feelingReply))
            {//stsrt of if statement
                error_method("CyberBot", feelingReply);

                // continue cybersecurity topic automatically
                ai_check(questions);

                return;
            }// end of if statement

            // ai response
            auto_show_interest();
            ai_check(questions);

        }// end of send event handler

        //start of ai_chat method
        private void ai_check(string questions)
        {// Remove special characters and clean the question

            // Check if user entered anything meaningful
            if (string.IsNullOrWhiteSpace(questions))
            {//start of if statement
                error_method("CyberBot", "Please enter a valid question.");
                question.Clear();
                return;
            }// end of if statement

            // Variables for processing
            string[] words = questions.ToLower().Split(
                new char[] { ' ', ',', '.', '?', '!', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries);

            // follow up conversation support
            if (questions.ToLower().Contains("more")
                || questions.ToLower().Contains("another")
                || questions.ToLower().Contains("explain"))
            {//start of if statement
                questions = pre_question;
            }// end of if statement

            bool found = false;
            string message = string.Empty;

            Random indexer = new Random();

            List<string> per_word = new List<string>();
            List<string> answers_found = new List<string>();

            // Process each word
            foreach (string word in words)
            {//start fo foreach loop
                // Skip ignored words
                if (word.Length < 3 || ignore.Contains(word.ToLower()))
                    continue;

                per_word.Clear();

                // INTEREST MEMORY
                if (word.Contains("interested"))
                {//start of if statement
                    string store_interests = string.Empty;
                    bool found_interest = false;

                    HashSet<string> currentInterests = new HashSet<string>();

                    foreach (string interest in words)
                    {//start of foreach loop
                        string clean = interest.ToLower().Trim();

                        clean = Regex.Replace(clean, @"[^a-zA-Z0-9\s]", "");

                        if (!ignore.Contains(clean)
                            && clean != "interested"
                            && clean != "and"
                            && clean != "in"
                            && clean.Length >= 3)
                        {//start of if statement
                            found_interest = true;
                            currentInterests.Add(clean);
                        }// end of if statement
                    }// Join interests into a string

                    store_interests = string.Join(", ", currentInterests);

                    if (found_interest && !string.IsNullOrWhiteSpace(store_interests))
                    {//start of if statement
                        string filename = "interested_topic.txt";

                        bool userFound = false;

                        if (File.Exists(filename))
                        {//start of if statement
                            string[] lines = File.ReadAllLines(filename);

                            for (int i = 0; i < lines.Length; i++)
                            {//start of for loop
                                if (lines[i].StartsWith(username))
                                {// start of if statement
                                    userFound = true;

                                    string existing =
                                        lines[i].Replace(username + " interested in:", "")
                                        .ToLower();

                                    HashSet<string> existingSet =
                                        new HashSet<string>(
                                            existing.Split(',')
                                            .Select(x => x.Trim())
                                            .Where(x => x != "")
                                        );

                                    foreach (string item in currentInterests)
                                    {//start of foreach loop
                                        existingSet.Add(item);
                                    }// end of foreach loop

                                    string finalList = string.Join(", ", existingSet);

                                    lines[i] = username + " interested in: " + finalList;

                                    File.WriteAllLines(filename, lines);

                                    message += "Great! I added "
                                        + store_interests
                                        + " to your interests.\n";

                                    break;
                                }// end of if statement
                            }// end of for loop
                        }// end of if statement

                        if (!userFound)
                        {//start of if statement
                            File.AppendAllText(
                                filename,
                                username + " interested in: "
                                + store_interests + "\n"
                            );

                            message += "Great! I will remember that you are interested in "
                                + store_interests + ".\n";
                        }// end of if statement
                    }// end of if statement
                    else
                    {//start of else statement
                        message += "Please specify your interests clearly.\n";
                    }// end of else statement
                }// end of if statement

                // Search for matching answers
                bool wordFound = false;

                foreach (string answer in reply)
                {//start of foreach loop
                    if (answer.ToLower().Contains(word))
                    {//start of if statement
                        wordFound = true;
                        per_word.Add(answer);
                    }// end of if statement
                }// end of foreach loop

                // RANDOM RESPONSE
                if (wordFound && per_word.Count > 0)
                {//start of if statement
                    found = true;

                    int indexing = indexer.Next(0, per_word.Count);

                    answers_found.Add(per_word[indexing]);
                }// end of if statement
            }// end of foreach loop

            // Show responses
            if (found && answers_found.Count > 0)
            {//start of if statement
                answers_found = answers_found.Distinct().ToList();

                foreach (string per_answer in answers_found)
                {//start of foreach loop
                    message += per_answer + "\n";
                }// end of foreach loop

                error_method("CyberBot", message.TrimEnd('\n'));

                chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
            }// end of if statement
            else
            {//start of else statement
                // fallback messages
                string[] fallbackMessages =
                {//start of array
                    "I'm sorry, I don't understand that. Could you rephrase your question?",
                    "I didn't quite get that. Try asking about cyber security topics.",
                    "Hmm, I'm not sure how to respond to that. Can you ask something else?",
                    "I couldn't find an answer for that. Please ask about programming, security, or technology.",
                    "My apologies, I don't have information on that topic yet."
                };//end of array

                Random random = new Random();

                string fallbackMessage =
                    fallbackMessages[random.Next(fallbackMessages.Length)];

                error_method("CyberBot", fallbackMessage);
            }// end of else statement

            // Clear the textbox
            question.Clear();

        }// end of ai_chat method

        //method to remove special characters
        private string RemoveSpecialCharacters(string input)
        {//start of method
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            StringBuilder sanitized = new StringBuilder();

            foreach (char c in input)
            {//start of foreach loop    
                if (char.IsLetterOrDigit(c)
                    || char.IsWhiteSpace(c)
                    || c == '\''
                    || c == '-')
                {//start of if statement
                    sanitized.Append(c);
                }// end of if statement
                else
                {// Replace special characters with space
                    sanitized.Append(' ');
                }// end of else statement
            }// end of foreach loop

            string result = sanitized.ToString();

            result = Regex.Replace(result, @"\s+", " ").Trim();

            return result;
        }// end of method

        //method count to show interests randomly
        private void auto_show_interest()
        {//start of method
            if (counting == 3)
            {//start of if statement
                string filename = "interested_topic.txt";

                if (File.Exists(filename))
                {//start of if statement
                    string[] lines = File.ReadAllLines(filename);

                    foreach (string line in lines)
                    {//start of foreach loop
                        if (line.StartsWith(username))
                        {//start of if statement
                            int colonIndex =
                                line.IndexOf("interested in:");

                            if (colonIndex >= 0)
                            {//start of if statement
                                string interests =
                                    line.Substring(colonIndex + 14).Trim();
                                  error_method(
                                              "CyberBot",
                                              "As someone interested in "
                                              + interests +
                                              ", you should regularly review your account security settings and stay alert online."
                                              );

                                ai_check(interests);

                                break;
                            }// end of if statement
                        }// end of if statement
                    }// end of foreach loop
                }// end of if statement

                counting = 0;
            }// end of if statement
            else
            {//start of else statement
                counting += 1;
            }// end of else statement
        }// end of method

        // Updated error method
        private void error_method(string name, string message)
        {//start of method
            Border messageBorder = new Border
            {//start of object initializer
                Margin = new Thickness(0, 2, 0, 2),
                Padding = new Thickness(5, 3, 5, 3),
                CornerRadius = new CornerRadius(5)
            };// end of object initializer

            // chatbot color
            if (name.ToLower().Contains("CyberBot")
                || name.ToLower().Contains("chat"))
            {//start of if statement
                messageBorder.Background =
                    new SolidColorBrush(Color.FromRgb(240, 248, 255));

                messageBorder.BorderBrush =
                    new SolidColorBrush(Color.FromRgb(173, 216, 230));
            }// user color
            else
            {// start of else statement
                // user color
                messageBorder.Background =
                    new SolidColorBrush(Color.FromRgb(245, 245, 245));

                messageBorder.BorderBrush =
                    new SolidColorBrush(Color.FromRgb(211, 211, 211));
            }// end of else statement

            messageBorder.BorderThickness = new Thickness(1);

            TextBlock messageText = new TextBlock
            {//start of object initializer
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(2)
            };// end of object initializer

            Brush nameColor =
                (name.ToLower().Contains("CyberBot")
                || name.ToLower().Contains("chat"))
                ? Brushes.DarkBlue
                : Brushes.DarkGreen;

            Brush messageColor = Brushes.Black;

            messageText.Inlines.Add(new Run
            {//start of object initializer
                Text = name + ": ",
                Foreground = nameColor,
                FontWeight = FontWeights.Bold
            }// end of object initializer
            )
            ;


            messageText.Inlines.Add(new Run
            {//start of object initializer
                Text = message,
                Foreground = messageColor
            }// end of object initializer
            );

            messageBorder.Child = messageText;

            chats.Items.Add(messageBorder);

            chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
        }// end of method

    }//end of class
}//end of namespace