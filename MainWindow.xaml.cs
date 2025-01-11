using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace chat_app
{
    public partial class MainWindow : Window
    {
        private const string MessagesFilePath = @"messages.txt"; // مسیر فایل
        List<string> profaneWords = LoadProfaneWords("profaneWords.json");
        public MainWindow()
        {
            InitializeComponent();
            LoadMessages(); // بارگذاری پیام‌ها از فایل
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = MessageInput.Text.Trim();

            if (!string.IsNullOrEmpty(userMessage))
            {
                // نمایش پیام کاربر در صفحه
                DisplayMessage("User", userMessage);

                // ذخیره پیام کاربر در فایل
                SaveMessageToFile("User", userMessage);


                // پیش‌پردازش متن
                string processedInput = PreprocessText(userMessage);

                // بررسی وجود کلمات توهین‌آمیز
                bool containsProfanity = ContainsProfanity(processedInput, profaneWords);

                // فیلتر کردن پیام (مثلاً برای کلمات توهین‌آمیز)
                string filteredMessage = FilterProfanity(userMessage, profaneWords);

                // نمایش پیام ربات در صفحه
                DisplayMessage("Bot", filteredMessage);

                // ذخیره پیام ربات در فایل
                SaveMessageToFile("Bot", filteredMessage);

                // پاک کردن فیلد ورودی
                MessageInput.Clear();
            }
        }

        private void DisplayMessage(string sender, string message)
        {
            // ایجاد یک StackPanel برای نمایش هر پیام
            StackPanel messagePanel = new StackPanel
            {
                Orientation = sender == "User" ? Orientation.Horizontal : Orientation.Horizontal,
                HorizontalAlignment = sender == "User" ? HorizontalAlignment.Right : HorizontalAlignment.Left
            };

            // اضافه کردن یک کادر (Border) برای ظاهر پیام‌ها
            Border messageBorder = new Border
            {
                Background = sender == "User" ? System.Windows.Media.Brushes.LightBlue : System.Windows.Media.Brushes.LightGray,
                CornerRadius = new System.Windows.CornerRadius(10),
                Margin = new System.Windows.Thickness(5),
                Padding = new System.Windows.Thickness(10)
            };

            // اضافه کردن متن پیام به کادر
            TextBlock messageText = new TextBlock
            {
                Text = $"{message}",
                Foreground = System.Windows.Media.Brushes.Black,
                TextWrapping = TextWrapping.Wrap
            };

            // افزودن متن به کادر
            messageBorder.Child = messageText;

            // اضافه کردن کادر به StackPanel
            messagePanel.Children.Add(messageBorder);

            // اضافه کردن StackPanel به پنل پیام‌ها
            MessagesPanel.Children.Add(messagePanel);
        }


        private void SaveMessageToFile(string sender, string message)
        {
            try
            {
                // ثبت پیام در فایل
                using (StreamWriter writer = new StreamWriter(MessagesFilePath, true))
                {
                    writer.WriteLine($"{sender}: {message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving message: {ex.Message}");
            }
        }

        private void LoadMessages()
        {
            try
            {
                if (File.Exists(MessagesFilePath))
                {
                    // خواندن پیام‌ها از فایل
                    var messages = File.ReadAllLines(MessagesFilePath);
                    foreach (var message in messages)
                    {
                        var messageParts = message.Split(':');
                        if (messageParts.Length == 2)
                        {
                            DisplayMessage(messageParts[0].Trim(), messageParts[1].Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading messages: {ex.Message}");
            }
        }

        private string FilterProfanity(string input)
        {
            // مثلاً برای فیلتر کردن کلمات رکیک از فایل
            string[] profaneWords = File.ReadAllLines(@"C:\Users\kouri\Desktop\ProjectFolder\SharedFiles\profaneWords.txt");
            foreach (var word in profaneWords)
            {
                if (input.Contains(word))
                {
                    input = input.Replace(word, new string('*', word.Length)); // جایگزین کلمات رکیک با ستاره
                }
            }
            return input;
        }


        // تابع بارگذاری کلمات توهین‌آمیز از فایل JSON
        static List<string> LoadProfaneWords(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonContent);
                return data["profaneWords"];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading profane words: {ex.Message}");
                return new List<string>();
            }
        }

        // تابع پیش‌پردازش برای حذف سیمبول‌ها و فاصله‌ها
        static string PreprocessText(string input)
        {
            // حذف کاراکترهای غیرالفبایی و فاصله
            return Regex.Replace(input, @"[^\w]", "").ToLower();
        }

        // تابع بررسی وجود کلمات توهین‌آمیز
        static bool ContainsProfanity(string input, List<string> profaneWords)
        {
            foreach (var word in profaneWords)
            {
                string pattern = string.Join(@"\W*", word.ToCharArray()); // ایجاد الگو برای تطابق با فاصله یا سیمبول
                if (Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        // فیلتر کردن کلمات توهین‌آمیز
        static string FilterProfanity(string input, List<string> profaneWords)
        {
            foreach (var word in profaneWords)
            {
                string pattern = string.Join(@"\W*", word.ToCharArray());
                input = Regex.Replace(input, pattern, new string('*', word.Length), RegexOptions.IgnoreCase);
            }
            return input;
        }
    }
}
