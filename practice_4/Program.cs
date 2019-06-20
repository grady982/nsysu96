using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice_4
{
    class Program
    {
        class Word
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }

        static string[] listSubject = new string[] { "I", "They", "He", "She", "Mary", "John"};
        static string[] listVerb = new string[] { "love", "like", "see", "find"};
        static string[] listObject = new string[] { "me", "him", "her", "them", "Mary", "John" };

        static string[] listData = new string[] {
            "I her love",
            "I love her",
            "Mary love John",
            "them see I",
            "love I me"
        };

        static List<string> listResult = new List<string>();

        /// <summary>
        /// 當主詞是He, She, Mary, John時，動詞要加s
        /// </summary>
        public static string SwithVerb(string subject, string verb)
        {
            switch (subject)
            {
                case "He":
                    verb += "s";
                    break;
                case "She":
                    verb += "s";
                    break;
                case "Mary":
                    verb += "s";
                    break;
                case "John":
                    verb += "s";
                    break;
                default:
                    break;
            }

            return verb;
        }

        /// <summary>
        /// 當主詞和受詞是同一類人稱代名詞時，受詞要改成反身代名詞
        /// </summary>
        public static string SwitchObject(string subject,string ob)
        {
            string key = string.Format("{0}_{1}",subject,ob);
            switch (key)
            {
                case "I_me":
                    ob = "myself";
                    break;
                case "He_him":
                    ob = "himself";
                    break;
                case "John_him":
                    ob = "himself";
                    break;
                case "She_her":
                    ob = "herself";
                    break;
                case "Mary_her":
                    ob = "herself";
                    break;
                case "They_them":
                    ob = "themselves";
                    break;
                default:
                    break;
            }
            return ob;
        }

        /// <summary>
        /// 重整句子結構
        /// </summary>
        /// <param name="data"></param>
        public static string[] ParseSentence(string data)
        {
            string[] listWord = data.Split(' ');
            string subject = "";
            string verb = "";
            string obj = "";

            foreach (string word in listWord)
            {
                subject = Array.Find(listSubject, sub => sub == word) ?? subject;
                verb = Array.Find(listVerb, ver => ver == word) ?? verb;
                obj = Array.Find(listObject, ob => ob == word) ?? obj;
            }

            return new string[] { subject, verb, obj};
        }

        static void Main(string[] args)
        {

            foreach (string data in listData)
            {
                // 1. 重整句子結構
                string[] listWord = ParseSentence(data);
                // 2. 動詞轉換
                listWord[1] = SwithVerb(listWord[0], listWord[1]);
                // 3. 受詞轉換
                listWord[2] = SwitchObject(listWord[0], listWord[2]);

                listResult.Add(string.Format("{0} {1} {2}",listWord[0],listWord[1],listWord[2]));

                Console.WriteLine(string.Format("{0} {1} {2}", listWord[0], listWord[1], listWord[2]));
            }


        }
    }
}
