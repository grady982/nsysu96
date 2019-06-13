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
        /// 運算子資料
        /// </summary>
        private static int[,] targetDataList = new int[,] {
            { 1, 1, 1}, 
            { 1, 1, 1}, 
            { 1, 1, 0}};

        /// <summary>
        /// 影像資料
        /// </summary>
        private static int[,] imageDataList = new int[,] {
            { 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1, 0},
            { 0, 0, 0, 1, 1, 1, 1},
            { 1, 1, 0, 1, 1, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0},
            { 1, 1, 0, 1, 1, 0, 0},
            { 1, 1, 0, 1, 1, 0, 0}
        };

        static void Main(string[] args)
        {
            GradyCalc gc = new GradyCalc(m,n,targetDataList,imageDataList);
            Coordinate cor = gc.Caculate();
        }
    }
}
