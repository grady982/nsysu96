using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice_3
{
    class Program
    {
        /// <summary>
        /// 運算子大小
        /// </summary>
        private static int m = 3;

        /// <summary>
        /// 影像大小
        /// </summary>
        private static int n = 7;

        /// <summary>
        /// 座標物件
        /// </summary>
        private class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Value { get; set; }
        }

        /// <summary>
        /// 結果物件
        /// </summary>
        private class Result
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Difference { get; set; }
        }

        /// <summary>
        /// 運算子資料
        /// </summary>
        private static int[] mDataList = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 0 };

        /// <summary>
        /// 影像資料
        /// </summary>
        private static int[] nDataList = new int[] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0};

        /// <summary>
        /// 運算子座標資料
        /// </summary>
        private static List<Coordinate> listCoordinateM = new List<Coordinate>();

        /// <summary>
        /// 影像座標資料
        /// </summary>
        private static List<Coordinate> listCoordinateN = new List<Coordinate>();
        
        /// <summary>
        /// 結果
        /// </summary>
        private static Result result;

        static void Main(string[] args)
        {
            // 運算子資料轉座標
            listCoordinateM = TranslateData(m, mDataList);
            // 影像資料轉座標
            listCoordinateN = TranslateData(n, nDataList);

            // 影像查詢
            foreach (Coordinate cor in listCoordinateN)
            {
                // 1.取得座標影像清單
                List<Coordinate>listCorA = GetCoordinateList(cor);

                // 2. 計算差異值
                if (listCorA != null)
                {
                    int dValue = CalcDifferenceValue(listCorA, listCoordinateM);
                    Result _result = new Result();
                    _result.X = cor.X;
                    _result.Y = cor.Y;
                    _result.Difference = dValue;

                    if (result == null)
                    {
                        result = _result;
                    }
                    else
                    {
                        if (result.Difference > dValue)
                        {
                            result = _result;
                        }
                    }
                    // 如果差異值為0結束查詢
                    if (dValue == 0)
                    {
                        Console.WriteLine(result.X + " " + result.Y);
                    }
                }

            }

            Console.WriteLine(result.X + " " + result.Y);
        }

        /// <summary>
        /// 一維陣列資料轉座標資料
        /// </summary>
        private static List<Coordinate> TranslateData(int size,int[] dataList)
        {
            List<Coordinate> resultList = new List<Coordinate>();

            for (int i = 0; i < size; i++)
            {
                int y = 1;
                for (int index = 0; index < (size * size); index++)
                {
                    if (index < (size + i * size) && index >= (i * size))
                    {
                        Coordinate cor = new Coordinate();
                        cor.X = i + 1;
                        cor.Y = y;
                        cor.Value = dataList[index];

                        resultList.Add(cor);
                        y++;
                    }
                }
            }

            return resultList;
        }

        /// <summary>
        /// 取得座標影像清單
        /// </summary>
        private static List<Coordinate> GetCoordinateList(Coordinate cor)
        {
            if ((cor.X + m - 1) > n || (cor.Y + m - 1) > n)
            {
                return null;
            }
            else
            {
                var find = from data in listCoordinateN
                           where (data.X >= cor.X && data.X < (cor.X + m)) && (data.Y >= cor.Y && data.Y < (cor.Y + m))
                           select data;

                List<Coordinate> listResult = new List<Coordinate>();

                foreach (Coordinate cc in find)
                {
                    listResult.Add(cc);
                }

                return listResult;
            }
        }

        /// <summary>
        /// 計算差異值
        /// </summary>
        private static int CalcDifferenceValue(List<Coordinate>listCorA,List<Coordinate>listCorB)
        {
            int i = 1;
            // 差異等級
            int level = 1;
            int deferenceValue = 0;
            do
            {
                // 1.取得該圈座標清單
                var findA = from data in listCorA
                            where (data.X == i || data.X == (m - i + 1)) && (data.Y == i || data.Y == (m - i + 1))
                            select data;

                var findB = from data in listCorB
                            where (data.X == i || data.X == (m - i + 1)) && (data.Y == i || data.Y == (m - i + 1))
                            select data;

                // 2.座標清單整理 
                Dictionary<string, int> dicCorA = new Dictionary<string, int>();
                Dictionary<string, int> dicCorB = new Dictionary<string, int>();

                // 重新定義座標
                int x = 1;
                int y = 1;
                foreach (Coordinate cor in listCorA)
                {
                    if (x > m)
                    {
                        x = 1;
                        y++;
                    }
                    string key = string.Format("{0}_{1}", x, y);
                    int value = cor.Value;
                    x++;

                    dicCorA.Add(key,value);
                }

                foreach (Coordinate cor in listCorB)
                {
                    string key = string.Format("{0}_{1}", cor.X, cor.Y);
                    int value = cor.Value;

                    dicCorB.Add(key, value);
                }

                // 計算該圈差異值
                foreach (string key in dicCorA.Keys)
                {
                    if (dicCorA[key] != dicCorB[key])
                    {
                        deferenceValue += level;
                    }
                }

                i++;
                level++;
            } while (i < (m - 1));

            return deferenceValue;
        }
    }
}
