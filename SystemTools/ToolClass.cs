using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTools
{
    public class splitTypes
    {
        public const string confirm = "conf";
        public const string versus = "vs";
        public const string multiplier = "*";
    }

    public enum bucketGranularity
    {
        DAILY,
        WEEKLY,
        MONTHLY,
        YEARLY
    }

    public enum position
    {
        SHORT = -1,
        FLAT = 0,
        LONG = 1
    }

    /// <summary>
    /// Adding to the system types may be the key to getting a system that has no yearly losses
    /// </summary>
    public enum maType
    {
        NONE,
        NORMAL_MA,
        RALPHS_MA,
        NORMAL_MA_TIMES_VOLUME,
        RALPHS_MA_TIMES_VOLUME
    }

    /// <summary>
    /// An inc is a data point about the current stock. this struct is also used to hold additional infomation to aid in calculations for a system
    /// </summary>
    public struct inc
    {
        public int accurateYesterday;
        public int accuracyOver20TradingDays;
        public int accuracyOver5TradingDays;
        public int index;
        public double closingPrice;
        public double openingPrice;
        public double highPrice;
        public double lowPrice;
        public double systemVal;
        public DateTime date;
        public bool isTradePoint;
        public position position;
        public double priceAtPosition;
        public double winLoss;
        public long volume;
        public double runningGT;
        public int runningTradeCount;

        public inc(double open, double high, double low, double close, double sysval, DateTime date, bool hasTrade, position position, double positionPrice, double winloss, long volume) : this()
        {
            this.accurateYesterday = 0;
            this.accuracyOver20TradingDays = 0;
            this.accuracyOver5TradingDays = 0;
            this.index = 0;
            this.openingPrice = open;
            this.highPrice = high;
            this.lowPrice = low;
            this.closingPrice = close;
            this.systemVal = sysval;
            this.date = date;
            this.isTradePoint = hasTrade;
            this.position = position;
            this.priceAtPosition = positionPrice;
            this.winLoss = winloss;
            this.volume = volume;
            this.runningGT = 0;
            this.runningTradeCount = 0;
        }
    };

    public struct dateBucket
    {
        public DateTime start;
        public DateTime end;

        public dateBucket(DateTime s, DateTime e) : this()
        {
            this.start = s;
            this.end = e;
        }

        public bool inBucket(DateTime d)
        {
            return (d.CompareTo(end) <= 0) && (d.CompareTo(start) >= 0);
        }
    };

    public struct bucketStat
    {
        public double winLossTotal;
        public int tradeCount;
        public int winCount;
        public int lossCount;
        public int tieCount;
    };

    public class ToolClass
    {
        public static bool playOvernight = true;

        private enum columns
        {
            DATE,
            OPEN,
            HIGH,
            LOW,
            CLOSE,
            ADJCLOSE,
            VOLUME,
            COUNT
        }

        /// <summary>
        /// Accesses a csv historical data file as produced by yahoo finance.
        /// </summary>
        /// <returns>An inc struct list.</returns>
        public static List<inc> getData(string filepath)
        {
            List<inc> data = new List<inc>();

            //(double price, double sysval, DateTime date, bool hasTrade, position position, double positionPrice, double winloss, int volume)
            using (var reader = new StreamReader(filepath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    data.Add(new inc(parseDoubleString(values[(int)columns.OPEN]), parseDoubleString(values[(int)columns.HIGH]), parseDoubleString(values[(int)columns.LOW]), parseDoubleString(values[(int)columns.CLOSE]), 0, DateTime.Parse(values[(int)columns.DATE]), false, position.FLAT, 0,0,long.Parse(values[(int)columns.VOLUME])));
                }
            }

            return data;
        }

        /// <summary>
        /// Converts a string representation of a double floating point number such as '1000.1234'
        /// to a double with two decimal precision such as 1000.12
        /// </summary>
        /// <returns></returns>
        public static double parseDoubleString(string doubleString)
        {
            return Math.Round(double.Parse(doubleString), 2);
        }

        /// <summary>
        /// Calculates a result column for one system.
        /// The result is placed inside the inc structs' systemVal attribute
        /// </summary>
        public static void calcSysCol(List<int> system, ref inc[] data, maType systype)
        {
            double[] numcol = new double[data.Length];
            List<inc> dataList = data.ToList<inc>();
 
            for(int i = 0; i< system.Count; i++)
            {
                switch (systype)
                {
                    case maType.NORMAL_MA:
                        numcol = calcNumColNormalMA(system[i], dataList); //Future: run all possible calls to this and cache results
                        break;
                    case maType.RALPHS_MA:
                        numcol = calcNumColRalphMA(system[i], dataList);
                        break;
                    case maType.NORMAL_MA_TIMES_VOLUME:
                        numcol = calcNumColNormalMATimesVol(system[i], dataList);
                        break;
                    case maType.RALPHS_MA_TIMES_VOLUME:
                        numcol = calcNumColRalphMATimesVol(system[i], dataList);
                        break;
                }
                for(int j = 0; j< data.Length; j++)
                {
                    data[j].systemVal += numcol[j];
                }
            }
            calcStats(ref data); //fills in the win/loss, tradecount, and position data
        }


        /// <summary>
        /// calculates a column for a single system number
        /// </summary>
        public static double[] calcNumColNormalMA(int num, List<inc> data)
        {
            double[] numcol = new double[data.Count];

            double sum = 0;
            double backnum = 0;
            double frontnum = 0;

            for(int i = 0; i< num; i++)
            {
                sum += data[i].closingPrice;
            }

            for(int i = num; i< data.Count; i++)
            {
                backnum = data[i - num].closingPrice;
                sum -= backnum;

                frontnum = data[i].closingPrice;
                sum += frontnum;

                //There are two ways to run the calculation for 'normal moving average':
                //you can compare the moving average to todays close, or you can simply use that average to play (which will always be positive so you'd have to used it to play against some other average)

                //numcol[i] = data[i].closingPrice - (sum / num); //play todays close vs the MA
                numcol[i] = (sum / num);                      //just the MA
            }

            return numcol;
        }

        /// <summary>
        /// calculates a column for a single system number
        /// </summary>
        public static double[] calcNumColNormalMATimesVol(int num, List<inc> data)
        {
            double[] numcol = new double[data.Count];

            double sum = 0;
            double backnum = 0;
            double frontnum = 0;

            for (int i = 0; i < num; i++)
            {
                sum += data[i].closingPrice * data[i].volume;
            }

            for (int i = num; i < data.Count; i++)
            {
                backnum = data[i - num].closingPrice * data[i].volume;
                sum -= backnum;

                frontnum = data[i].closingPrice * data[i].volume;
                sum += frontnum;
                numcol[i] = (data[i].closingPrice * data[i].volume) - (sum / num); //play todays close vs the MA
                //numcol[i] = (sum / num);                        //just the MA
            }

            return numcol;
        }

        /// <summary>
        /// calculates a column for a single system number
        /// </summary>
        public static double[] calcNumColRalphMA(int num, List<inc> data)
        {
            double[] numcol = new double[data.Count];
            int part = num / 2;

            double frontsum = 0;
            double backsum = 0;
            double transfernum = 0;
            double backnum = 0;
            double frontnum = 0;

            for(int i = 0; i< part; i++)
            {
                backsum += data[i].closingPrice;
                frontsum += data[i + part].closingPrice;
            }

            numcol[num - 1] = frontsum - backsum;

            for(int i = num; i<data.Count; i++)
            {
                backnum = data[i - num].closingPrice;
                backsum -= backnum;
                transfernum = data[i - part].closingPrice;
                backsum += transfernum;
                frontsum -= transfernum;
                frontnum = data[i].closingPrice;
                frontsum += frontnum;
                numcol[i] = frontsum - backsum;
            }

            return numcol;
        }

        /// <summary>
        /// calculates a column for a single system number
        /// </summary>
        public static double[] calcNumColRalphMATimesVol(int num, List<inc> data)
        {
            double[] numcol = new double[data.Count];
            int part = num / 2;

            double frontsum = 0;
            double backsum = 0;
            double transfernum = 0;
            double backnum = 0;
            double frontnum = 0;

            for (int i = 0; i < part; i++)
            {
                backsum += data[i].closingPrice * data[i].volume;
                frontsum += data[i + part].closingPrice * data[i + part].volume;
            }

            numcol[num - 1] = frontsum - backsum;

            for (int i = num; i < data.Count; i++)
            {
                backnum = data[i - num].closingPrice * data[i - num].volume;
                backsum -= backnum;
                transfernum = data[i - part].closingPrice * data[i-part].volume;
                backsum += transfernum;
                frontsum -= transfernum;
                frontnum = data[i].closingPrice * data[i].volume;
                frontsum += frontnum;
                numcol[i] = frontsum - backsum;
            }

            return numcol;
        }

        /// <summary>
        /// calculates a vs column into the result inc structs' systemVal attribute
        /// </summary>
        public static void calcVsCol(List<inc> sys1, List<inc> sys2, ref inc[] result)
        {
            for(int i = 0; i< sys1.Count; i++)
            {
                result[i].systemVal = 0;
                result[i].systemVal += sys1[i].systemVal;
                result[i].systemVal -= sys2[i].systemVal;
            }

            calcStats(ref result); //fills in the win/loss, tradecount, and position data
        }

        /// <summary>
        /// Calculates many 'with' columns adding the system totals of many systems together
        /// </summary>
        public static void calcWithCol(List<int> indexes, List<List<inc>> systems, ref inc[] result)
        {
            for (int i = 0; i < systems[0].Count; i++)
            {
                double total = 0;

                for(int j = 0; j<indexes.Count; j++)
                {
                    total = total + systems[indexes[j]][i].systemVal;
                }

                result[i].systemVal = total;
            }
            calcStats(ref result);
        }

        /// <summary>
        /// Calculates adding the system totals of many systems together
        /// </summary>
        public static void calcCombinedSystems(List<inc[]> systems, ref inc[] result)
        {
            for (int i = 0; i < systems.Count; i++) {
                for (int j = 0; j < systems[0].Length; j++)
                {
                    result[j].systemVal += systems[i][j].systemVal;
                }
            }
            calcStats(ref result);
        }

        /// <summary>
        /// Calculates a 'multiplier system' meaning; if given 10*5 it is equivalent to 10 10 10 10 10
        /// </summary>
        /// <param name="system"></param>
        /// <param name="multiplier"></param>
        /// <param name="result"></param>
        public static void calcMultCol(inc[] system, int multiplier, ref inc[] result)
        {
            for(int i = 0; i < system.Length; i++)
            {
                result[i].systemVal = multiplier * system[i].systemVal;
            }
            calcStats(ref result);
        }

        /// <summary>
        /// calculates a confirmation column into the result inc structs' systemVal attribute
        /// </summary>
        public static void calcConfCol(List<inc> sys1, List<inc> sys2, ref inc[] result)
        {
            for (int i = 0; i < sys1.Count; i++)
            {
                if (sys1[i].systemVal > 0 && sys2[i].systemVal > 0)
                    result[i].systemVal = 1;
                else if(sys1[i].systemVal < 0 && sys2[i].systemVal < 0)
                    result[i].systemVal = -1;
                else
                    result[i].systemVal = 0;
            }
            calcStats(ref result); //fills in the win/loss, tradecount, and position data
        }

        /// <summary>
        /// Calculates the stats on the data after the inc structs have their systemVals filled in
        /// </summary>
        /// <param name="data"></param>
        public static void calcStats(ref inc[] data)
        {
            position lastposition = position.FLAT;
            position currentposition = position.FLAT;
            data[0].priceAtPosition = data[0].closingPrice;
            data[0].index = 0;

            for(int i = 1; i< data.Length; i++)
            {
                if((data[i - 1].position == position.LONG  && data[i].closingPrice > data[i - 1].closingPrice) ||
                    (data[i - 1].position == position.SHORT && data[i].closingPrice < data[i - 1].closingPrice))
                {
                    data[i].accurateYesterday = 1; //else it stays at zero
                }

                data[i].accuracyOver5TradingDays += data[i].accurateYesterday;
                data[i].accuracyOver20TradingDays += data[i].accurateYesterday;

                if(i >= 5)
                {
                    data[i].accuracyOver5TradingDays -= data[i - 5].accurateYesterday;
                }

                if (i >= 20)
                {
                    data[i].accuracyOver20TradingDays -= data[i - 20].accurateYesterday;
                }

                data[i].index = i;
                if (data[i].systemVal > 0)
                    currentposition = position.LONG;
                else if (data[i].systemVal < 0)
                    currentposition = position.SHORT;
                else
                    currentposition = position.FLAT;

                data[i].position = currentposition;
                if (!playOvernight)
                {
                    if(data[i-1].position == position.LONG)
                    {
                        data[i].winLoss = data[i].closingPrice - data[i].openingPrice;
                    }else if(data[i - 1].position == position.SHORT)
                    {
                        data[i].winLoss = data[i].openingPrice - data[i].closingPrice;
                    }
                    data[i].isTradePoint = true;
                }
                else
                {
                    if (currentposition != lastposition)
                    {
                        data[i].priceAtPosition = data[i].closingPrice;

                        if (lastposition == position.LONG)
                        {
                            data[i].winLoss = Math.Round(data[i].closingPrice - data[i - 1].priceAtPosition, 2);
                        }
                        else if (lastposition == position.SHORT)
                        {
                            data[i].winLoss = Math.Round(data[i - 1].priceAtPosition - data[i].closingPrice, 2);
                        }

                        if (currentposition == position.LONG || currentposition == position.SHORT)
                        {
                            data[i].runningGT = Math.Round(data[i - 1].runningGT + data[i].winLoss, 2);
                            data[i].runningTradeCount = data[i - 1].runningTradeCount + 1;
                        }
                        data[i].isTradePoint = true;
                    }
                    else
                    {
                        data[i].priceAtPosition = data[i - 1].priceAtPosition;
                    }
                }

                lastposition = currentposition;
            }
        }
    }
}
