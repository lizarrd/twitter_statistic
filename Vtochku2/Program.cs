using System;
using System.Collections.Generic;
using Tweetinvi;
using Newtonsoft.Json;
using Tweetinvi.Models;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace Vtochku2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string AccessToken = "9UQHsE7azTEcoBlXivQQ6yLDu",
                AccessTokenSecret = "dWrenSFmZ6ehoaPISQAf6iK98rAqhwZGbDJgwT34Uijhq1aPis",
                ConsumerKey = "608382317-rMr4KqjbUBc4D5ma6K5fQJbpUgWQ3cn8lOfwKd05",
                ConsumerSecret = "Tqh9jb8kDg4iqnK1K9qTTIvjaSGFb8soQxc0y3OCLvQyn";


            Console.WriteLine("Введите логин пользователя: ");
            string UserName =Console.ReadLine();

            //авторизация и получение 5 твиттов юзера
            Auth.SetUserCredentials(AccessToken, AccessTokenSecret, ConsumerKey, ConsumerSecret);
            var userIdentifier = new UserIdentifier(UserName.Trim('@'));
            var tweets = Timeline.GetUserTimeline(userIdentifier, 5);


            //запись полученных твиттов в одну строку 
            string sum = "";
            foreach (var tweet in tweets)
            {
                sum += tweet;
                Console.WriteLine(tweet);
            }
                        
            
            //создание массивов из букв твиттов и букв алфавита
            Char [] letters = Regex.Replace(sum, "[-.?!)(,: ]", "").ToLower().ToCharArray();
            Char [] alphabet = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ь', 'ы', 'э', 'ю', 'я', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
            int count;
            double frequency;
            var dic = new Dictionary<char, double>();
            

            //подсчет совпадений букв алфавита в твиттах и рассчет частотности их повторения
            for (int i = 0; i < alphabet.Length; i++)
            {
                count = 0;
                for (int k = 0; k < letters.Length; k++)
                {
                    
                    if (alphabet[i] == letters[k])
                    {
                        count++;
                    }
                }
                frequency = Math.Round((double)count/(double)letters.Length, 2);
                dic.Add(alphabet[i], frequency);
            }

            //конвертация словаря (буква-частотность) в JSON
            var result = JsonConvert.SerializeObject(dic);            
            Console.WriteLine(result);


            Console.WriteLine("Запостить результаты в твиттер? 1 - да, 0 - нет");
            string post = Console.ReadLine();

            if (post == "1") {

                //создание изображения с текстом статистики
                Rectangle rect = new Rectangle(0, 0, 400, 150);
                Bitmap image = new Bitmap(400, 150, PixelFormat.Format24bppRgb);

                using (Graphics g = Graphics.FromImage(image))
                using (Font font = new Font("Arial", 10))
                {
                    g.FillRectangle(Brushes.White, rect);
                    g.DrawString(result, font, Brushes.Black, rect, StringFormat.GenericTypographic);
                }

                image.Save("stat.jpeg", ImageFormat.Jpeg);  
                
                //создание поста в твиттер             
                byte[] file = File.ReadAllBytes("stat.jpeg");
                var publish = Tweet.PublishTweetWithImage("@"+UserName + ", статистика для последних 5 твиттов: ", file);

                //удаление изображения 
                File.Delete("stat.jpeg");
            }

            Console.ReadKey();
        }
    }
}
