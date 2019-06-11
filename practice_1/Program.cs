using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"輸入範例:
    5
    NSYSU  NC  CT  NS  NM
    NTU  BC  NC  CT  NS
    NCCU  BC  NL  HL
    Providence    BC  NC
    NTHU      BC  NS
    2
    BC  NS + CT  HL
	NM  +  BC  NL  +  BC  NC
            ");
            SearchEngines search = new SearchEngines();

            #region 大學個數
            {
                Console.WriteLine("請輸入查詢學校條件:");
                bool result = false;
                do
                {
                    string data = Console.ReadLine();
                    result = search.SetNData(data);

                } while (!result);
            }
            #endregion

            #region 學校資料
            {
                Console.WriteLine(string.Format("請輸入{0}筆學校資料!", search.GetN()));

                for (int i = 0; i < search.GetN(); i++)
                {
                    string data = Console.ReadLine();
                    search.SetSchools(data);
                }
            }
            #endregion
            
            #region 查詢個數
            {
                Console.WriteLine("請輸入查詢個數!");
                bool result = false;
                do
                {
                    string data = Console.ReadLine();
                    result = search.SetMData(data);

                } while (!result);
            }
            #endregion

            #region 查詢條件
            {
                Console.WriteLine(string.Format("請輸入{0}筆查詢條件!", search.GetM()));
                for (int i = 0; i < search.GetM(); i++)
                {
                    string data = Console.ReadLine();
                    search.SetCondition(data);
                }
            }
            #endregion

            #region 結果
            {
                Dictionary<int,List<string>> dicResult = search.GetResult();
                
                foreach (int i in dicResult.Keys)
                {
                    string result = "";
                    foreach (string schoolName in dicResult[i])
                    {
                        result += " " + schoolName;
                    }
                    Console.WriteLine(result);
                }
            }
            #endregion

            Console.Read();
        }

        public class SearchEngines
        {
            private int N;
            private int M;
            private Dictionary<string, List<Attr>> dicAttrBySchoolName;
            private List<Condition> listCondition;
            private Dictionary<int, List<string>> dicResult;
            private List<string> listResult;

            public SearchEngines()
            {
                this.listCondition = new List<Condition>();
                this.listResult = new List<string>();
                this.dicAttrBySchoolName = new Dictionary<string, List<Attr>>();
                this.dicResult = new Dictionary<int, List<string>>();
            }

            public bool SetNData(string _data)
            {
                int x = 0;
                if (int.TryParse(_data, out x))
                {
                    if (int.Parse(_data) > 0 && int.Parse(_data) <= 10)
                    {
                        this.N = int.Parse(_data);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("請輸入小於等於10的正整數!");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("請輸入正整數!");
                    return false;
                }
            }

            public bool SetMData(string _data)
            {
                int x = 0;
                if (int.TryParse(_data, out x))
                {
                    if (int.Parse(_data) > 0 && int.Parse(_data) <= 20)
                    {
                        this.M = int.Parse(_data);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("請輸入小於等於20的正整數!");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("請輸入正整數!");
                    return false;
                }
            }

            public void SetSchools(string _data)
            {
                string[] listData = _data.Split(' ');
                string schoolName = listData[0];

                if (!this.dicAttrBySchoolName.ContainsKey(schoolName))
                {
                    this.dicAttrBySchoolName.Add(schoolName, new List<Attr>());
                }
                for (int i = 1; i < listData.Count(); i++)
                {
                    switch (listData[i])
                    {
                        case "BC":
                            this.dicAttrBySchoolName[schoolName].Add(Attr.BC);
                            break;
                        case "CT":
                            this.dicAttrBySchoolName[schoolName].Add(Attr.CT);
                            break;
                        case "HL":
                            this.dicAttrBySchoolName[schoolName].Add(Attr.HL);
                            break;
                        case "NC":
                            this.dicAttrBySchoolName[schoolName].Add(Attr.NC);
                            break;
                        case "NL":
                            this.dicAttrBySchoolName[schoolName].Add(Attr.NL);
                            break;
                        case "NM":
                            this.dicAttrBySchoolName[schoolName].Add(Attr.NM);
                            break;
                        case "NS":
                            this.dicAttrBySchoolName[schoolName].Add(Attr.NS);
                            break;
                        default:
                            break;
                    }
                }
            }

            public void SetCondition(string _data)
            {
                Condition condition = new Condition(_data);
                this.listCondition.Add(condition);
            }

            public int GetN()
            {
                return this.N;
            }

            public int GetM()
            {
                return this.M;
            }

            public Dictionary<int,List<string>> GetResult()
            {
                foreach (Condition cd in this.listCondition)
                {
                    int n = 0;
                    foreach (string key in cd.GetCondition().Keys)
                    {
                        foreach (string schoolName in dicAttrBySchoolName.Keys)
                        {
                            bool isTargetSchool = false;
                            foreach (Attr attr in cd.GetCondition()[key])
                            {

                                if (dicAttrBySchoolName[schoolName].Contains(attr))
                                {
                                    isTargetSchool = true;
                                }
                                else
                                {
                                    isTargetSchool = false;
                                }
                            }

                            if (isTargetSchool)
                            {
                                if (!this.dicResult.ContainsKey(n))
                                {
                                    this.dicResult.Add(n, new List<string>());
                                }
                                this.dicResult[n].Add(schoolName);
                            }
                        }

                    }
                    n++;
                }

                return this.dicResult;
            }
        }

        public enum Attr
        {
            /// <summary>
            /// 大校園
            /// </summary>
            BC,
            /// <summary>
            /// 鄰近有大城市
            /// </summary>
            NC,
            /// <summary>
            /// 交通方便
            /// </summary>
            CT,
            /// <summary>
            /// 靠海
            /// </summary>
            NS,
            /// <summary>
            /// 靠山
            /// </summary>
            NM,
            /// <summary>
            /// 校園有湖
            /// </summary>
            HL,
            /// <summary>
            /// 附近有風景區
            /// </summary>
            NL
        }

        /// <summary>
        /// 條件物件
        /// </summary>
        public class Condition
        {
            private Dictionary<string, List<Attr>> dicAttrByKey;

            public Condition(string data)
            {
                this.dicAttrByKey = new Dictionary<string, List<Attr>>();
                string[] listData = data.Split('+');
                foreach (string condition in listData)
                {
                    this.dicAttrByKey.Add(condition,new List<Attr>());

                    string[] listCondition = condition.Split(' ');
                    foreach (string attr in listCondition)
                    {
                        switch (attr)
                        {
                            case "BC":
                                this.dicAttrByKey[condition].Add(Attr.BC);
                                break;
                            case "CT":
                                this.dicAttrByKey[condition].Add(Attr.CT);
                                break;
                            case "HL":
                                this.dicAttrByKey[condition].Add(Attr.HL);
                                break;
                            case "NC":
                                this.dicAttrByKey[condition].Add(Attr.NC);
                                break;
                            case "NL":
                                this.dicAttrByKey[condition].Add(Attr.NL);
                                break;
                            case "NM":
                                this.dicAttrByKey[condition].Add(Attr.NM);
                                break;
                            case "NS":
                                this.dicAttrByKey[condition].Add(Attr.NS);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            public Dictionary<string, List<Attr>> GetCondition()
            {
                return this.dicAttrByKey;
            }
        }
    }
}
