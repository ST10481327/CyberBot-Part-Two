using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CyberBot_Part_Two
{
    public class user_name
    {
        public string submit_name(TextBox user_name, ListView chats)
        {
            string filename = "user_names.txt";

            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
            }

            string name = user_name.Text.Trim();

            bool found = check_name(name);

            if (!found)
            {
                File.AppendAllText(filename, name + Environment.NewLine);

                error_method(
                    "CyberBot",
                    "Hi " + name + ", welcome to CyberBot world of innovation.",
                    chats
                );
            }
            else
            {
                error_method(
                    "CyberBot",
                    "Hi " + name + ", nice to have back again.",
                    chats
                );
            }

            return name;
        }

        // check username
        private Boolean check_name(string name)
        {
            string filename = "user_names.txt";

            string[] names = File.ReadAllLines(filename);

            foreach (string name_found in names)
            {
                if (name_found.ToLower().Trim()
                    == name.ToLower().Trim())
                {
                    return true;
                }
            }

            return false;
        }

        // chat display method
        private void error_method(
            string name,
            string message,
            ListView chats)
        {
            Border messageBorder = new Border
            {
                Margin = new Thickness(0, 2, 0, 2),
                Padding = new Thickness(5, 3, 5, 3),
                CornerRadius = new CornerRadius(5)
            };

            messageBorder.Background =
                new SolidColorBrush(Color.FromRgb(240, 248, 255));

            messageBorder.BorderBrush =
                new SolidColorBrush(Color.FromRgb(173, 216, 230));

            messageBorder.BorderThickness = new Thickness(1);

            TextBlock messageText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(2)
            };

            messageText.Inlines.Add(new Run
            {
                Text = name + ": ",
                Foreground = Brushes.DarkBlue,
                FontWeight = FontWeights.Bold
            });

            messageText.Inlines.Add(new Run
            {
                Text = message,
                Foreground = Brushes.Black
            });

            messageBorder.Child = messageText;

            chats.Items.Add(messageBorder);
        }
    }
}