using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice_2
{
    class Program
    {
        private class Activity
        {
            public string Name { get; set; }
            public int StartTime { get; set; }
            public int EndTime { get; set; }
            public int Bonus { get; set; }
            public int AverageBonus { get; set; }
        }

        private static List<Activity> listHistory = new List<Activity>();
        private static List<Activity> listActivity = new List<Activity>();

        //private static Dictionary<int, List<Activity>> dicActivityByStartTime = new Dictionary<int, List<Activity>>();
        private static Dictionary<int, List<Activity>> dicResults = new Dictionary<int, List<Activity>>();
        private static int Index = 0;

        static void Main(string[] args)
        {
            int N;
            
            Dictionary<string, Activity> dicActivityByName = new Dictionary<string, Activity>();
            

            Console.WriteLine("請輸入活動個數:");
            N = int.Parse(Console.ReadLine());

            Console.WriteLine("請輸入活動:");
            {
                #region 方法1
                {
                    //for (int i = 0; i < N; i++)
                    //{
                    //    string data = Console.ReadLine();
                    //    string[] dataList = data.Split(' ');

                    //    Activity ac = new Activity();
                    //    ac.Name = dataList[0];
                    //    ac.StartTime = int.Parse(dataList[1]);
                    //    ac.EndTime = int.Parse(dataList[2]);
                    //    ac.Bonus = int.Parse(dataList[3]);
                    //    ac.AverageBonus = 100 * ac.Bonus / (ac.EndTime - ac.StartTime);

                    //    if (!dicActivityByName.ContainsKey(ac.Name))
                    //    {
                    //        dicActivityByName.Add(ac.Name, ac);
                    //    }

                    //    if (!dicActivityByStartTime.ContainsKey(ac.StartTime))
                    //    {
                    //        dicActivityByStartTime.Add(ac.StartTime, new List<Activity>());
                    //    }
                    //    dicActivityByStartTime[ac.StartTime].Add(ac);
                    //}
                }
                #endregion
                for (int i = 0; i < N; i++)
                {
                    string data = Console.ReadLine();
                    string[] dataList = data.Split(' ');
                    Activity ac = new Activity();
                    ac.Name = dataList[0];
                    ac.StartTime = int.Parse(dataList[1]);
                    ac.EndTime = int.Parse(dataList[2]);
                    ac.Bonus = int.Parse(dataList[3]);
                    ac.AverageBonus = 100 * ac.Bonus / (ac.EndTime - ac.StartTime);

                    listActivity.Add(ac);
                }
            }
            Console.WriteLine("資料計算中...");

            #region 方法1
            {
                //for (int i = 700; i <= 1700;)
                //{
                //    if (dicActivityByStartTime.ContainsKey(i))
                //    {
                //        // 透過Linq取得同一開始時間平均收穫最高的活動
                //        var _result = from data in dicActivityByStartTime[i]
                //                      where data.AverageBonus == dicActivityByStartTime[i].Max(x => x.AverageBonus)
                //                      select data;

                //        //_result.First()

                //        listResult.Add((Activity)_result.First());
                //        i = ((Activity)_result.First()).EndTime;
                //    }
                //    else
                //    {
                //        i += 100;
                //    }
                //}

                //Console.WriteLine("結果:");
                //int total = 0;
                //string result = "";
                //foreach (Activity ac in listResult)
                //{
                //    result += ac.Name + " ";
                //    total += ac.Bonus;
                //}
                //Console.WriteLine(result + " " + total);
            }
            #endregion

            // 方法2: 活動7點開始
            GetMaxActivity(700);

            Dictionary<int, int> dicTotalBonusByKey = new Dictionary<int, int>();
            
            foreach (int key in dicResults.Keys)
            {
                int totalBonus = dicResults[key].Sum(x => x.Bonus);
                dicTotalBonusByKey.Add(key,totalBonus);
            }

            IEnumerable<KeyValuePair<int,int>> result = from data in dicTotalBonusByKey
                         where data.Value == dicTotalBonusByKey.Max(x => x.Value)
                         select data;

            int maxTotal = result.ToArray()[0].Value;
            int targetKey = result.ToArray()[0].Key;

            List<Activity> listTarget = dicResults[targetKey];
            string sr = "";
            foreach (Activity ac in listTarget)
            {
                sr += " " + ac.Name;
            }

            Console.WriteLine(dicResults.Keys.Count);
            Console.WriteLine(sr + " " + maxTotal);
        }


        private static void GetMaxActivity(int time)
        {
            // 透過Linq取得該時段可選擇的活動
            IEnumerable<Activity> activitys = from data in listActivity
                          where data.StartTime >= time
                          select data;
 
            // 當時間找不到對應的活動時結束遞迴
            if (activitys.ToArray().Length == 0)
            {
                List<Activity> listAc = listHistory.ToList();
                dicResults[Index] = listAc;
                listHistory.RemoveAt(listHistory.Count - 1);
                Index++;
                //dicResults[Index] = listHistory;
            }
            else
            {
                foreach (Activity ac in activitys)
                {
                    listHistory.Add(ac);

                    //if (dicResults.ContainsKey(Index))
                    //{
                    //    dicResults[Index].Add(ac);
                    //}
                    //else
                    //{
                    //    dicResults.Add(Index, new List<Activity>());

                    //    dicResults[Index].Add(ac);
                    //}

                    GetMaxActivity(ac.EndTime);
                }
                if (listHistory.Count > 0)
                {
                    listHistory.RemoveAt(listHistory.Count - 1);
                }
            }
        }
    }
}
