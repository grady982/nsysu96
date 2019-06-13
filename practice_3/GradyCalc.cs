using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice_3
{
    class GradyCalc : ICalculator
    {
        int _targetSize, _imageSize;
        int[,] _targetList, _imageList;

        // 實作介面屬性
        public int targetSize
        {
            get { return _targetSize; }
            set { _targetSize = value; }
        }
        public int imageSize
        {
            get { return _imageSize; }
            set { _imageSize = value; }
        }
        public int[,] targetList
        {
            get { return _targetList; }
            set { _targetList = value; }
        }
        public int[,] imageList
        {
            get { return _imageList; }
            set { _imageList = value; }
        }

        /// <summary>
        /// 運算子座標資料
        /// </summary>
        private List<Coordinate> listCoordinateTarget = new List<Coordinate>();
        /// <summary>
        /// 影像座標資料
        /// </summary>
        private List<Coordinate> listCoordinateImage = new List<Coordinate>();

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
        /// 結果
        /// </summary>
        private Result result;

        public GradyCalc(int m, int n, int[,] mDatatList, int[,] nDataList)
        {
            targetSize = m;
            imageSize = n;
            targetList = mDatatList;
            imageList = nDataList;
        }

        // 實作介面方法
        public Coordinate Caculate()
        {
            // 運算子資料轉座標
            listCoordinateTarget = TranslateData(targetSize, targetList);
            // 影像資料轉座標
            listCoordinateImage = TranslateData(imageSize, imageList);

            // 影像查詢
            foreach (Coordinate cor in listCoordinateImage)
            {
                // 1.取得座標影像清單
                List<Coordinate> listCorA = GetCoordinateList(cor);

                // 2. 計算差異值
                if (listCorA != null)
                {
                    int dValue = CalcDifferenceValue(listCorA, listCoordinateTarget);
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

            Coordinate answer = new Coordinate();
            answer.X = result.X;
            answer.Y = result.Y;
            answer.Value = imageList[result.X, result.Y];

            return answer;
        }

        /// <summary>
        /// 二維陣列資料轉座標資料
        /// </summary>
        private List<Coordinate> TranslateData(int size, int[,] dataList)
        {
            List<Coordinate> resultList = new List<Coordinate>();

            for (int i = 0; i < dataList.GetLength(0); i++)
            {
                for (int j = 0; j < dataList.GetLength(1); j++)
                {
                    Coordinate cor = new Coordinate();
                    cor.X = i + 1;
                    cor.Y = j + 1;
                    cor.Value = dataList[i, j];
                    resultList.Add(cor);
                }
            }

            return resultList;
        }

        /// <summary>
        /// 取得座標影像清單
        /// </summary>
        private List<Coordinate> GetCoordinateList(Coordinate cor)
        {
            if ((cor.X + targetSize - 1) > imageSize || (cor.Y + targetSize - 1) > imageSize)
            {
                return null;
            }
            else
            {
                var find = from data in listCoordinateImage
                           where (data.X >= cor.X && data.X < (cor.X + targetSize)) && (data.Y >= cor.Y && data.Y < (cor.Y + targetSize))
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
        private int CalcDifferenceValue(List<Coordinate> listCorA, List<Coordinate> listCorB)
        {
            int i = 1;
            // 差異等級
            int level = 1;
            int deferenceValue = 0;
            do
            {
                // 1.取得該圈座標清單
                var findA = from data in listCorA
                            where (data.X == i || data.X == (targetSize - i + 1)) && (data.Y == i || data.Y == (targetSize - i + 1))
                            select data;

                var findB = from data in listCorB
                            where (data.X == i || data.X == (targetSize - i + 1)) && (data.Y == i || data.Y == (targetSize - i + 1))
                            select data;

                // 2.座標清單整理 
                Dictionary<string, int> dicCorA = new Dictionary<string, int>();
                Dictionary<string, int> dicCorB = new Dictionary<string, int>();

                // 重新定義座標
                int x = 1;
                int y = 1;
                foreach (Coordinate cor in listCorA)
                {
                    if (x > targetSize)
                    {
                        x = 1;
                        y++;
                    }
                    string key = string.Format("{0}_{1}", x, y);
                    int value = cor.Value;
                    x++;

                    dicCorA.Add(key, value);
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
            } while (i < (targetSize - 1));

            return deferenceValue;
        }
    }
}
