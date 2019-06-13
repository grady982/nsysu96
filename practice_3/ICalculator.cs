using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice_3
{
    public interface ICalculator
    {
        /// <summary>
        /// 運算維度大小
        /// </summary>
        int targetSize { get; set; }
        /// <summary>
        /// 影像維度大小
        /// </summary>
        int imageSize { get; set; }
        /// <summary>
        /// 運算資料
        /// </summary>
        int[,] targetList { get; set; }
        /// <summary>
        /// 影像資料
        /// </summary>
        int[,] imageList { get; set; }
        /// <summary>
        /// 計算結果並回傳座標物件
        /// </summary>
        Coordinate Caculate();
    }
}
