using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CyberBot_Part_Two
{//start of namespace
    public class user_name
    {//start of class
        public string submit_name(TextBox user_name, ListView chats)
        {//start of method
            string filename = "user_names.txt";

            if (!File.Exists(filename))
            {//start of it
                File.Create(filename).Close();
            }//end of it

            string name = user_name.Text.Trim();

            bool found = check_name(name);

            if (!found)
            {//start of if
                File.AppendAllText(filename, name + Environment.NewLine);

                error_method(
                    "CyberBot",
                    "Hi " + name + ", welcome to CyberBot world of innovation.",
                    chats
                );
            }//end of if
            else
            {//start of else
                error_method(
                    "CyberBot",
                    "Hi " + name + ", nice to have back again.",
                    chats
                );
            }//end of else

            return name;
        }//end of method

        // check username
        private Boolean check_name(string name)
        {//start of method
            string filename = "user_names.txt";

            string[] names = File.ReadAllLines(filename);

            foreach (string name_found in names)
            {//start of foreach
                if (name_found.ToLower().Trim()
                    == name.ToLower().Trim())
                {//start of if
                    return true;
                }//end of if
            }//end of foreach

            return false;
        }//end of method

        // chat display method
        private void error_method(
            string name,
            string message,
            ListView chats)
        {//start of method
            Border messageBorder = new Border
            {//start of object initializer
                Margin = new Thickness(0, 2, 0, 2),
                Padding = new Thickness(5, 3, 5, 3),
                CornerRadius = new CornerRadius(5)
            };//end of object initializer

            messageBorder.Background =
                new SolidColorBrush(Color.FromRgb(240, 248, 255));

            messageBorder.BorderBrush =
                new SolidColorBrush(Color.FromRgb(173, 216, 230));

            messageBorder.BorderThickness = new Thickness(1);

            TextBlock messageText = new TextBlock
            {//start of object initializer
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(2)
            };//end of object initializer

            messageText.Inlines.Add(new Run
            {//start of object initializer
                Text = name + ": ",
                Foreground = Brushes.DarkBlue,
                FontWeight = FontWeights.Bold
            });//end of object initializer

            messageText.Inlines.Add(new Run
            {//start of object initializer
                Text = message,
                Foreground = Brushes.Black
            }//end of object initializer
            );

            messageBorder.Child = messageText;

            chats.Items.Add(messageBorder);
        }//end of method
    }//end of class
}// end of namespace