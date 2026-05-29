
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
            {
                error_method("CyberBot", "Please enter a question.");
                return;
            }

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
            {
                error_method("CyberBot", feelingReply);

                // continue cybersecurity topic automatically
                ai_check(questions);

                return;
            }

            // ai response
            auto_show_interest();
            ai_check(questions);

        }// end of send event handler

        //start of ai_chat method
        private void ai_check(string questions)
        {// Remove special characters and clean the question

            // Check if user entered anything meaningful
            if (string.IsNullOrWhiteSpace(questions))
            {
                error_method("CyberBot", "Please enter a valid question.");
                question.Clear();
                return;
            }

            // Variables for processing
            string[] words = questions.ToLower().Split(
                new char[] { ' ', ',', '.', '?', '!', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries);

            // follow up conversation support
            if (questions.ToLower().Contains("more")
                || questions.ToLower().Contains("another")
                || questions.ToLower().Contains("explain"))
            {
                questions = pre_question;
            }

            bool found = false;
            string message = string.Empty;

            Random indexer = new Random();

            List<string> per_word = new List<string>();
            List<string> answers_found = new List<string>();

            // Process each word
            foreach (string word in words)
            {
                // Skip ignored words
                if (word.Length < 3 || ignore.Contains(word.ToLower()))
                    continue;

                per_word.Clear();

                // INTEREST MEMORY
                if (word.Contains("interested"))
                {
                    string store_interests = string.Empty;
                    bool found_interest = false;

                    HashSet<string> currentInterests = new HashSet<string>();

                    foreach (string interest in words)
                    {
                        string clean = interest.ToLower().Trim();

                        clean = Regex.Replace(clean, @"[^a-zA-Z0-9\s]", "");

                        if (!ignore.Contains(clean)
                            && clean != "interested"
                            && clean != "and"
                            && clean != "in"
                            && clean.Length >= 3)
                        {
                            found_interest = true;
                            currentInterests.Add(clean);
                        }
                    }

                    store_interests = string.Join(", ", currentInterests);

                    if (found_interest && !string.IsNullOrWhiteSpace(store_interests))
                    {
                        string filename = "interested_topic.txt";

                        bool userFound = false;

                        if (File.Exists(filename))
                        {
                            string[] lines = File.ReadAllLines(filename);

                            for (int i = 0; i < lines.Length; i++)
                            {
                                if (lines[i].StartsWith(username))
                                {
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
                                    {
                                        existingSet.Add(item);
                                    }

                                    string finalList = string.Join(", ", existingSet);

                                    lines[i] = username + " interested in: " + finalList;

                                    File.WriteAllLines(filename, lines);

                                    message += "Great! I added "
                                        + store_interests
                                        + " to your interests.\n";

                                    break;
                                }
                            }
                        }

                        if (!userFound)
                        {
                            File.AppendAllText(
                                filename,
                                username + " interested in: "
                                + store_interests + "\n"
                            );

                            message += "Great! I will remember that you are interested in "
                                + store_interests + ".\n";
                        }
                    }
                    else
                    {
                        message += "Please specify your interests clearly.\n";
                    }
                }

                // Search for matching answers
                bool wordFound = false;

                foreach (string answer in reply)
                {
                    if (answer.ToLower().Contains(word))
                    {
                        wordFound = true;
                        per_word.Add(answer);
                    }
                }

                // RANDOM RESPONSE
                if (wordFound && per_word.Count > 0)
                {
                    found = true;

                    int indexing = indexer.Next(0, per_word.Count);

                    answers_found.Add(per_word[indexing]);
                }
            }

            // Show responses
            if (found && answers_found.Count > 0)
            {
                answers_found = answers_found.Distinct().ToList();

                foreach (string per_answer in answers_found)
                {
                    message += per_answer + "\n";
                }

                error_method("CyberBot", message.TrimEnd('\n'));

                chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
            }
            else
            {
                // fallback messages
                string[] fallbackMessages =
                {
                    "I'm sorry, I don't understand that. Could you rephrase your question?",
                    "I didn't quite get that. Try asking about cyber security topics.",
                    "Hmm, I'm not sure how to respond to that. Can you ask something else?",
                    "I couldn't find an answer for that. Please ask about programming, security, or technology.",
                    "My apologies, I don't have information on that topic yet."
                };

                Random random = new Random();

                string fallbackMessage =
                    fallbackMessages[random.Next(fallbackMessages.Length)];

                error_method("CyberBot", fallbackMessage);
            }

            // Clear the textbox
            question.Clear();

        }// end of ai_chat method

        //method to remove special characters
        private string RemoveSpecialCharacters(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            StringBuilder sanitized = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsLetterOrDigit(c)
                    || char.IsWhiteSpace(c)
                    || c == '\''
                    || c == '-')
                {
                    sanitized.Append(c);
                }
                else
                {
                    sanitized.Append(' ');
                }
            }

            string result = sanitized.ToString();

            result = Regex.Replace(result, @"\s+", " ").Trim();

            return result;
        }

        //method count to show interests randomly
        private void auto_show_interest()
        {
            if (counting == 3)
            {
                string filename = "interested_topic.txt";

                if (File.Exists(filename))
                {
                    string[] lines = File.ReadAllLines(filename);

                    foreach (string line in lines)
                    {
                        if (line.StartsWith(username))
                        {
                            int colonIndex =
                                line.IndexOf("interested in:");

                            if (colonIndex >= 0)
                            {
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
                            }
                        }
                    }
                }

                counting = 0;
            }
            else
            {
                counting += 1;
            }
        }

        // Updated error method
        private void error_method(string name, string message)
        {
            Border messageBorder = new Border
            {
                Margin = new Thickness(0, 2, 0, 2),
                Padding = new Thickness(5, 3, 5, 3),
                CornerRadius = new CornerRadius(5)
            };

            // chatbot color
            if (name.ToLower().Contains("CyberBot")
                || name.ToLower().Contains("chat"))
            {
                messageBorder.Background =
                    new SolidColorBrush(Color.FromRgb(240, 248, 255));

                messageBorder.BorderBrush =
                    new SolidColorBrush(Color.FromRgb(173, 216, 230));
            }
            else
            {
                // user color
                messageBorder.Background =
                    new SolidColorBrush(Color.FromRgb(245, 245, 245));

                messageBorder.BorderBrush =
                    new SolidColorBrush(Color.FromRgb(211, 211, 211));
            }

            messageBorder.BorderThickness = new Thickness(1);

            TextBlock messageText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(2)
            };

            Brush nameColor =
                (name.ToLower().Contains("CyberBot")
                || name.ToLower().Contains("chat"))
                ? Brushes.DarkBlue
                : Brushes.DarkGreen;

            Brush messageColor = Brushes.Black;

            messageText.Inlines.Add(new Run
            {
                Text = name + ": ",
                Foreground = nameColor,
                FontWeight = FontWeights.Bold
            });

            messageText.Inlines.Add(new Run
            {
                Text = message,
                Foreground = messageColor
            });

            messageBorder.Child = messageText;

            chats.Items.Add(messageBorder);

            chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
        }

    }//end of class
}//end of namespace