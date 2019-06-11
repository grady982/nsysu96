using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pratice_2_1
{
    class Program
    {
        /// <summary>
        /// 活動物件
        /// </summary>
        public class Activity
        {
            public string Name { get; set; }
            public int StartTime { get; set; }
            public int EndTime { get; set; }
            public int Bonus { get; set; }
        }

        /// <summary>
        /// 活動開始時間表
        /// </summary>
        public static int[] arrStartTime = new int[] { 700, 800, 900, 1000, 1300 };
        /// <summary>
        /// 活動清單
        /// </summary>
        public static Dictionary<int, List<Activity>> dicActivityByStartTime = new Dictionary<int, List<Activity>>();

        static void Main(string[] args)
        {
            #region Init Target Data
            {

                Activity A1 = new Activity();
                A1.Name = "A1";
                A1.StartTime = 700;
                A1.EndTime = 800;
                A1.Bonus = 1;

                dicActivityByStartTime.Add(700,new List<Activity>());
                dicActivityByStartTime[700].Add(A1);

                Activity A2 = new Activity();
                A2.Name = "A2";
                A2.StartTime = 800;
                A2.EndTime = 1000;
                A2.Bonus = 6;

                dicActivityByStartTime.Add(800,new List<Activity>());
                dicActivityByStartTime[800].Add(A2);

                Activity A3 = new Activity();
                A3.Name = "A3";
                A3.StartTime = 800;
                A3.EndTime = 900;
                A3.Bonus = 5;

                dicActivityByStartTime[800].Add(A3);

                Activity A4 = new Activity();
                A4.Name = "A4";
                A4.StartTime = 900;
                A4.EndTime = 1200;
                A4.Bonus = 6;

                dicActivityByStartTime.Add(900, new List<Activity>());
                dicActivityByStartTime[900].Add(A4);

                Activity A5 = new Activity();
                A5.Name = "A5";
                A5.StartTime = 1000;
                A5.EndTime = 1600;
                A5.Bonus = 12;

                dicActivityByStartTime.Add(1000,new List<Activity>());
                dicActivityByStartTime[1000].Add(A5);

                Activity A6 = new Activity();
                A6.Name = "A6";
                A6.StartTime = 1300;
                A6.EndTime = 1700;
                A6.Bonus = 8;

                dicActivityByStartTime.Add(1300,new List<Activity>());
                dicActivityByStartTime[1300].Add(A6);

            }
            #endregion

            /*
             * 二分法 :
             * M = ( R - L ) / 2 + L
             * 1. 資料由小到大排序
             * 2. 搜尋 X 值 : 最高收穫量活動組合
             * 3. 二分法結束條件: 活動數 <= 3
             * 
             * 取中間值兩種結果: 參加或不參加
             * 參加: 中間的左邊 可參加的活動數 > 3 的話 再繼續往下切
             *        中間的左邊 可參加的活動數 <= 3 的話 直接做線性搜尋法 取得最高收穫量活動組合
             *        中間的右邊 可參加的活動數 > 3 的話 在繼續往下切
             *        中間的左邊 可參加的活動數 <= 3 的話 直接做線性搜尋法 取得最高收穫量活動組合
             * 不參加: 中間的左邊 可參加的活動數 > 3 的話 再繼續往下切
             *        中間的左邊 可參加的活動數 <= 3 的話 直接做線性搜尋法 取得最高收穫量活動組合
             *        中間的右邊 可參加的活動數 > 3 的話 在繼續往下切
             *        中間的左邊 可參加的活動數 <= 3 的話 直接做線性搜尋法 取得最高收穫量活動組合
             */
            BinarySearch(arrStartTime);
        }

        /// <summary>
        /// 透過活動開始時間表 做二分搜尋法
        /// </summary>
        public static void BinarySearch(int[] listStartTime)
        {
            int activityCount = 0;
            for (int i = 0; i < listStartTime.Length; i++)
            {
                activityCount += dicActivityByStartTime[i].Count;
            }


            /*
             * 可選活動數 <= 3 結束
             * 透過線性搜尋取最高收穫量活動組合
             */
            if (activityCount <= 3)
            {
                // 1. 計算最高收穫量活動組合
                return;
            }
            // 對切
            else
            {
                int first = 0;
                int end = arrStartTime.Length - 1;

                int M = (int)((first + end) / 2);

                // listStartTime 對切

                
                //dicActivityByStartTime[listStartTime[M]][0]; 
            }
        }
    }
}
