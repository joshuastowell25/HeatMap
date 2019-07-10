using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemTools;

namespace SystemDisplayApp
{
    public partial class HeatMapForm : Form
    {
        private string nestPattern = @"([\s*\w+\s*]+)*([\s*\d+\s*]+)*(\((\([^()]+\)|\d+|\w)\s*(conf|vs|accm|accw|\*)*\s*(\([^()]+\)|\d+|\w)\))*([\s*\d+\s*]+)*([\s*\w+\s*]+)*";
        private static int systemCount = 26;
        bool initialized = false;
        private DataTable heatmapData = new DataTable();
        private DataTable systemData = new DataTable();
        private List<dateBucket> dateBuckets;
        private List<inc> data;
        private List<inc> systemA_incData = new List<inc>();
        private List<inc> systemB_incData = new List<inc>();
        private List<inc> systemC_incData = new List<inc>();
        private List<inc> systemD_incData = new List<inc>();
        private List<inc> systemE_incData = new List<inc>();
        private List<inc> systemF_incData = new List<inc>();
        private List<inc> systemG_incData = new List<inc>();
        private List<inc> systemH_incData = new List<inc>();
        private List<inc> systemI_incData = new List<inc>();
        private List<inc> systemJ_incData = new List<inc>();
        private List<inc> systemK_incData = new List<inc>();
        private List<inc> systemL_incData = new List<inc>();
        private List<inc> systemM_incData = new List<inc>();
        private List<inc> systemN_incData = new List<inc>();
        private List<inc> systemO_incData = new List<inc>();
        private List<inc> systemP_incData = new List<inc>();
        private List<inc> systemQ_incData = new List<inc>();
        private List<inc> systemR_incData = new List<inc>();
        private List<inc> systemS_incData = new List<inc>();
        private List<inc> systemT_incData = new List<inc>();
        private List<inc> systemU_incData = new List<inc>();
        private List<inc> systemV_incData = new List<inc>();
        private List<inc> systemW_incData = new List<inc>();
        private List<inc> systemX_incData = new List<inc>();
        private List<inc> systemY_incData = new List<inc>();
        private List<inc> systemZ_incData = new List<inc>();
        private List<List<inc>> systems_incData = new List<List<inc>>(); //a collection of the above 26 system inc lists
        private bool[] validationArray = new bool[systemCount];

        public HeatMapForm()
        {
            InitializeComponent();

        }

        private void granularity_or_date_changed(object sender, EventArgs e)
        {
            if (initialized)
            {
                recalcRedisplayGridBuckets();
                putIncDataIntoHeatMapColumns();
            }
        }

        public void initialize()
        {
            systems_incData.Add(systemA_incData);
            systems_incData.Add(systemB_incData);
            systems_incData.Add(systemC_incData);
            systems_incData.Add(systemD_incData);
            systems_incData.Add(systemE_incData);
            systems_incData.Add(systemF_incData);
            systems_incData.Add(systemG_incData);
            systems_incData.Add(systemH_incData);
            systems_incData.Add(systemI_incData);
            systems_incData.Add(systemJ_incData);
            systems_incData.Add(systemK_incData);
            systems_incData.Add(systemL_incData);
            systems_incData.Add(systemM_incData);
            systems_incData.Add(systemN_incData);
            systems_incData.Add(systemO_incData);
            systems_incData.Add(systemP_incData);
            systems_incData.Add(systemQ_incData);
            systems_incData.Add(systemR_incData);
            systems_incData.Add(systemS_incData);
            systems_incData.Add(systemT_incData);
            systems_incData.Add(systemU_incData);
            systems_incData.Add(systemV_incData);
            systems_incData.Add(systemW_incData);
            systems_incData.Add(systemX_incData);
            systems_incData.Add(systemY_incData);
            systems_incData.Add(systemZ_incData);

            systemData.Columns.Add("Sys", typeof(string));
            systemData.Columns.Add("Divisors", typeof(string));
            
            for(int i = 0; i<systemCount; i++)
            {
                systemData.Rows.Add(Convert.ToChar(i+65).ToString(), "");
            }

            systemDataGrid.DataSource = systemData;

            heatmapData.Columns.Add("Date Range", typeof(string));
            heatmapData.Columns.Add("A", typeof(double));
            heatmapData.Columns.Add("B", typeof(double));
            heatmapData.Columns.Add("C", typeof(double));
            heatmapData.Columns.Add("D", typeof(double));
            heatmapData.Columns.Add("E", typeof(double));
            heatmapData.Columns.Add("F", typeof(double));
            heatmapData.Columns.Add("G", typeof(double));
            heatmapData.Columns.Add("H", typeof(double));
            heatmapData.Columns.Add("I", typeof(double));
            heatmapData.Columns.Add("J", typeof(double));
            heatmapData.Columns.Add("K", typeof(double));
            heatmapData.Columns.Add("L", typeof(double));
            heatmapData.Columns.Add("M", typeof(double));
            heatmapData.Columns.Add("N", typeof(double));
            heatmapData.Columns.Add("O", typeof(double));
            heatmapData.Columns.Add("P", typeof(double));
            heatmapData.Columns.Add("Q", typeof(double));
            heatmapData.Columns.Add("R", typeof(double));
            heatmapData.Columns.Add("S", typeof(double));
            heatmapData.Columns.Add("T", typeof(double));
            heatmapData.Columns.Add("U", typeof(double));
            heatmapData.Columns.Add("V", typeof(double));
            heatmapData.Columns.Add("W", typeof(double));
            heatmapData.Columns.Add("X", typeof(double));
            heatmapData.Columns.Add("Y", typeof(double));
            heatmapData.Columns.Add("Z", typeof(double));
            
            granularityListBox.Items.AddRange(typeof(bucketGranularity).GetEnumNames());
            granularityListBox.SelectedIndex = (int)bucketGranularity.YEARLY;

            maTypeListBox.Items.AddRange(typeof(maType).GetEnumNames());
            maTypeListBox.SelectedIndex = 0;

            initialized = true;
            recalcRedisplayGridBuckets();

            updateFont(15.0F);

            string examples = "10, 10 20, 10 vs 20, 10 conf 20, a b, a vs b, a 30, 10*5, etc.";
            for(int i = 0; i< systemCount; i++)
            {
                systemDataGrid.Rows[i].Cells[1].ToolTipText = examples;
            }

            systemDataGrid.Columns[0].Width = 30;
        }

        /// <summary>
        /// Changes the bucket size of the HeatMapDataGridView
        /// </summary>
        public void recalcRedisplayGridBuckets()
        {
            heatmapData.Clear();
            dateBuckets = getDateBuckets(startDateTimePicker.Value, endDateTimePicker.Value, (bucketGranularity)granularityListBox.SelectedIndex);

            foreach (dateBucket bucket in dateBuckets)
            {
                string label = bucket.start.ToLongDateString() + "-" + bucket.end.ToLongDateString();
                heatmapData.Rows.Add(label);
            }

            heatMapDataGrid.DataSource = heatmapData;
            colorizeHeatmap();
        }


        /// <summary>
        /// given buckets and increment data, returns a list of bucket stats
        /// </summary>
        public List<bucketStat> calculateBuckets(List<dateBucket> buckets, List<inc> incData)
        {
            //look over datas: date, isTradePoint, and WinLoss
            bucketStat[] bucketList = new bucketStat[buckets.Count];

            foreach(inc i in incData)
            {
                foreach(dateBucket bucket in buckets)
                {
                    if (bucket.inBucket(i.date))
                    {
                        if (i.isTradePoint)
                        {
                            bucketList[buckets.IndexOf(bucket)].tradeCount++;
                            bucketList[buckets.IndexOf(bucket)].winLossTotal += i.winLoss;
                            if (i.winLoss > 0)
                                bucketList[buckets.IndexOf(bucket)].winCount++;
                            if (i.winLoss < 0)
                                bucketList[buckets.IndexOf(bucket)].lossCount++;
                            if (i.winLoss == 0)
                                bucketList[buckets.IndexOf(bucket)].tieCount++;
                        }
                    }
                }
            }

            return bucketList.ToList<bucketStat>();
        }

        /// <summary>
        /// Given a column Index and the increment data, calculates the bucket stats and places the stats in the HeatMap
        /// </summary>
        public void putIncDataIntoHeatMapColumn(int columnIndex, List<inc> incData)
        {
            List<bucketStat> bucketList = calculateBuckets(dateBuckets, incData);

            for(int i = 0; i< heatmapData.Rows.Count; i++)
            {
                heatmapData.Rows[i][columnIndex] = Math.Round(bucketList[i].winLossTotal, 2);
            }

            heatMapDataGrid.DataSource = heatmapData;
            colorizeHeatmap();
        }

        /// <summary>
        /// Given a column Index and the increment data, calculates the bucket stats and places the stats in the HeatMap
        /// </summary>
        public void clearHeatMapColumn(int columnIndex)
        {
            for (int i = 0; i < heatmapData.Rows.Count; i++)
            {
                heatmapData.Rows[i][columnIndex] = DBNull.Value;
                heatMapDataGrid.Rows[i].Cells[columnIndex].Style.BackColor = Color.White;
            }
        }

        /// <summary>
        /// puts the calculated column data into the heatmap columns
        /// </summary>
        public void putIncDataIntoHeatMapColumns()
        {
            for(int i = 0; i< 26; i++)
            {
                if (validationArray[i] == true)
                {
                    putIncDataIntoHeatMapColumn(i + 1, systems_incData[i]);
                }
            }
        }

        /// <summary>
        /// Makes cells in the heatmap green, red, or black
        /// </summary>
        public void colorizeHeatmap()
        {
            float value = 0;
            for(int i= 0; i< heatMapDataGrid.Rows.Count; i++)
            {
                for(int j = 0; j<heatMapDataGrid.Columns.Count; j++)
                {
                    if(heatMapDataGrid.Rows[i].Cells[j].Value!=null && float.TryParse(heatMapDataGrid.Rows[i].Cells[j].Value.ToString(), out value) && value > 0)
                    {
                        heatMapDataGrid.Rows[i].Cells[j].Style.BackColor = Color.Green;
                    }
                    else if(heatMapDataGrid.Rows[i].Cells[j].Value != null && float.TryParse(heatMapDataGrid.Rows[i].Cells[j].Value.ToString(), out value) && value < 0)
                    {
                        heatMapDataGrid.Rows[i].Cells[j].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //a year that made 0.00
                        heatMapDataGrid.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                }
            }
        }

        private void HeatMapForm_Load(object sender, EventArgs e)
        {
            initialize(); //initialize the data sources on load
        }

        private List<inc> testData = new List<inc> {
            new inc(995.00, 995.00, 995.00, 995.00,  1000.00, DateTime.Parse("2018-11-26"), false, position.LONG,  900.00, 0, 0),
            new inc(995.00, 995.00, 995.00, 996.00,  1000.00, DateTime.Parse("2018-11-27"), false, position.LONG,  900.00, 0, 0),
            new inc(995.00, 995.00, 995.00, 997.00,  1000.00, DateTime.Parse("2018-11-28"), false, position.LONG,  900.00, 0, 0),
            new inc(995.00, 995.00, 995.00, 998.00,  1000.00, DateTime.Parse("2018-11-29"), false, position.LONG,  900.00, 0, 0),
            new inc(995.00, 995.00, 995.00, 999.00,  1000.00, DateTime.Parse("2018-11-30"), false, position.LONG,  900.00, 0, 0),

            new inc(995.00, 995.00, 995.00, 1000.00,  1000.00, DateTime.Parse("2018-12-03"), false, position.LONG,  900.00, 0, 0),
            new inc(995.00, 995.00, 995.00, 1001.00,   500.00, DateTime.Parse("2018-12-04"), false, position.LONG,  900.00, 0, 0),
            new inc(995.00, 995.00, 995.00, 1002.00,    -1.00, DateTime.Parse("2018-12-05"),  true, position.SHORT, 1002.00, 102.00, 0),
            new inc(995.00, 995.00, 995.00, 1003.00,  -500.00, DateTime.Parse("2018-12-06"), false, position.SHORT, 1002.00, 0, 0),
            new inc(995.00, 995.00, 995.00, 1004.00, -1000.00, DateTime.Parse("2018-12-07"), false, position.SHORT, 1002.00, 0, 0)
        };


        /// <summary>
        /// Breaks a span of dates into buckets of a given granularity: day, week, month, year
        /// </summary>
        /// <returns>list of buckets</returns>
        public List<dateBucket> getDateBuckets(DateTime start, DateTime end, bucketGranularity bucket_granularity)
        {
            List<dateBucket> buckets = new List<dateBucket>();

            DateTime currentDate = start;
            DateTime bucketEnd = start;

            while (currentDate.CompareTo(end) <= 0) //while the date is earlier or equal to end
            {
                switch (bucket_granularity)
                {
                    case bucketGranularity.DAILY:
                        buckets.Add(new dateBucket(currentDate, currentDate));
                        currentDate = currentDate.AddDays(1);
                        break;

                    case bucketGranularity.WEEKLY:
                        bucketEnd = currentDate.AddDays(7).AddDays(-1);
                        bucketEnd = (end.CompareTo(bucketEnd) > 0 ? bucketEnd : end);
                        buckets.Add(new dateBucket(currentDate, bucketEnd));
                        currentDate = currentDate.AddDays(7);
                        break;

                    case bucketGranularity.MONTHLY:
                        bucketEnd = currentDate.AddMonths(1).AddDays(-1);
                        bucketEnd = (end.CompareTo(bucketEnd) > 0 ? bucketEnd : end);
                        buckets.Add(new dateBucket(currentDate, bucketEnd));
                        currentDate = currentDate.AddMonths(1);
                        break;

                    case bucketGranularity.YEARLY:
                        bucketEnd = currentDate.AddYears(1).AddDays(-1);
                        bucketEnd = (end.CompareTo(bucketEnd) > 0 ? bucketEnd : end);
                        buckets.Add(new dateBucket(currentDate, bucketEnd));
                        currentDate = currentDate.AddYears(1);
                        break;

                    default:
                        break;
                }
            }

            //add one for the overall view
            buckets.Add(new dateBucket(start, end));
            return buckets;
        }

        private void updateFont(float fontsize)
        {
            //Change cell font
            foreach (DataGridViewColumn c in heatMapDataGrid.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", fontsize, GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// Handler function for when the input file changes
        /// </summary>
        /// <param name="filepath"></param>
        private void handleFileChange(string filepath)
        {
            if (File.Exists(filepath))
            {
                dataFileBox.ForeColor = Color.Black;
                data = ToolClass.getData(filepath);
                for(int i = 0; i< 26; i++)
                {
                    systems_incData[i] = data;
                }

                setDateRanges(data);
                recalculateSystems();
                putIncDataIntoHeatMapColumns();
                
                //set the dataFileBox.Text here
                string[] tokens = filepath.Split('\\');
                if(tokens.Length >= 1)
                {
                    dataFileBox.Text = tokens[tokens.Length - 1];
                }
            }
            else
            {
                dataFileBox.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// sets the start and end date on the start and end date calendars to the beginning date 
        /// and end date of the data file you have opened
        /// </summary>
        /// <param name="data"></param>
        private void setDateRanges(List<inc> data)
        {
            int firstIndex = 0;
            int lastIndex = data.Count - 1;

            startDateTimePicker.Value = data[firstIndex].date;
            endDateTimePicker.Value = data[lastIndex].date;

            recalcRedisplayGridBuckets();
        }

        /// <summary>
        /// tells if the user entered a valid vs system
        /// </summary>
        /// <param name="sysEntry"></param>
        /// <returns></returns>
        private bool validateEntry(string entry)
        {
            //                        systemDataGrid.Rows[row].Cells[col].Style.ForeColor = Color.Red;
            int index1 = 0;
            int index2 = 0;
            bool isValid = false;
            entry = entry.ToLower();

            if(entry == "")
            {
                return false;
            }

            MatchCollection nestmatches = match(entry, nestPattern);

            if (nestmatches.Count >= 1)
            {
                return true;
            }

            int token1 = 0;
            int token2 = 0;
            string entry1 = "";
            string entry2 = "";

            if (entry.Replace(" ","").All(char.IsDigit)) //all numeric
            {
                string[] tokens = entry.Split(' ');
                isValid = true;
                for(int i = 0; i< tokens.Length; i++)
                {
                    int val = 0;
                    isValid = isValid && int.TryParse(tokens[i], out val) && val > -1;
                }

            }
            else
            {
                string[] confirmTokens = entry.Replace(" ","").Split(new string[] {splitTypes.confirm}, StringSplitOptions.None);
                if (confirmTokens.Length == 2)
                {
                    if (confirmTokens[0].Length == 1 && Convert.ToChar(confirmTokens[0]) >= 'a' && Convert.ToChar(confirmTokens[0]) <= 'z'
                    && confirmTokens[1].Length == 1 && Convert.ToChar(confirmTokens[1]) >= 'a' && Convert.ToChar(confirmTokens[1]) <= 'z')
                    {
                        //if this system depends on other cols then its validity does too
                        index1 = Convert.ToChar(confirmTokens[0]) - 'a';
                        index2 = Convert.ToChar(confirmTokens[1]) - 'a';
                        entry1 = systemDataGrid.Rows[index1].Cells[1].EditedFormattedValue.ToString();
                        entry2 = systemDataGrid.Rows[index2].Cells[1].EditedFormattedValue.ToString();
                        isValid = validateEntry(entry1) && validateEntry(entry2);
                    }
                    else if (int.TryParse(confirmTokens[0], out token1) && int.TryParse(confirmTokens[1], out token2))
                    {
                        isValid = true;
                    }
                    return isValid;
                }

                string[] vsTokens = entry.Replace(" ", "").Split(new string[] {splitTypes.versus}, StringSplitOptions.None);
                if (vsTokens.Length == 2)
                {
                    if (vsTokens[0].Length == 1 && Convert.ToChar(vsTokens[0]) >= 'a' && Convert.ToChar(vsTokens[0]) <= 'z'
                    && vsTokens[1].Length == 1 && Convert.ToChar(vsTokens[1]) >= 'a' && Convert.ToChar(vsTokens[1]) <= 'z')
                    {
                        //if this system depends on other cols then its validity does too
                        index1 = Convert.ToChar(vsTokens[0]) - 'a';
                        index2 = Convert.ToChar(vsTokens[1]) - 'a';
                        entry1 = systemDataGrid.Rows[index1].Cells[1].EditedFormattedValue.ToString();
                        entry2 = systemDataGrid.Rows[index2].Cells[1].EditedFormattedValue.ToString();
                        isValid = validateEntry(entry1) && validateEntry(entry2);
                    }
                    else if (int.TryParse(vsTokens[0], out token1) && int.TryParse(vsTokens[1], out token2))
                    {
                        isValid = true;
                    }
                    return isValid;
                }

                string[] multTokens = entry.Replace(" ", "").Split(new string[] { splitTypes.multiplier }, StringSplitOptions.None);
                if (multTokens.Length == 2)
                {
                    if (multTokens[0].Length == 1 && Convert.ToChar(multTokens[0]) >= 'a' && int.TryParse(multTokens[1], out token2))
                    {
                        //if this system depends on other cols then its validity does too
                        index1 = Convert.ToChar(multTokens[0]) - 'a';
                        entry1 = systemDataGrid.Rows[index1].Cells[1].EditedFormattedValue.ToString();
                        isValid = validateEntry(entry1);
                    }
                    else if (int.TryParse(multTokens[0], out token1) && int.TryParse(multTokens[1], out token2))
                    {
                        isValid = true;
                    }
                    return isValid;
                }

                //in this case the user has a 'system a' and a 'system b' and wants to enter 'system c' as 'a b' (or elsewhere, 'a b c d')
                string[] combineSystemTokens = entry.Split(' ');
                if(combineSystemTokens.Length >= 2)
                {
                    bool allLongEnough = true;
                    for(int j = 0; j<combineSystemTokens.Length; j++)
                    {
                        allLongEnough = allLongEnough && (combineSystemTokens[j].Length >= 1);
                    }
                    if (allLongEnough)
                    {
                        isValid = true;
                        for(int i = 0; i<combineSystemTokens.Length; i++)
                        {
                            string e = combineSystemTokens[i];

                            if(e.Length == 1 && Convert.ToChar(e) >= 'a' && Convert.ToChar(e) <= 'z')
                            {
                                index1 = Convert.ToChar(e) - 'a';
                                entry1 = systemDataGrid.Rows[index1].Cells[1].EditedFormattedValue.ToString();
                                isValid = isValid && validateEntry(entry1);
                            }
                            else if(int.TryParse(e, out token1))
                            {
                                isValid = true;
                            }
                            else
                            {
                                 isValid = false;
                            }
                        }
                    }
                    return isValid;
                }
            }

            return isValid;
        }

        /// <summary>
        /// Recalculates ALL of the systems
        /// usually called after the data or maType has changed
        /// changing of one system would call recalculateSystem, singular
        /// </summary>
        private void recalculateSystems()
        {
            //parse what system numbers they wrote
            //recalculate the system
            //redisplay that systems column
            for (int i = 0; i< 26; i++)
            {
                string entry = systemDataGrid.Rows[i].Cells[1].EditedFormattedValue.ToString();
                if (validationArray[i] == true)
                {
                    recalculateSystem(i, (maType)maTypeListBox.SelectedIndex);
                }
            }
        }

        private enum sysType{
            NONE,
            PLAIN_NUMBERS,
            TIMES_KEY, //a*5, or 10*5
            VERSUS,
            CONFIRM,
            COMBINE_SYSTEMS //f = a b c
        }

        private static MatchCollection match(string text, string expr)
        {
            MatchCollection mc = Regex.Matches(text, expr);
            return mc;
        }

        private void recalculateSystem(int sysIndex, maType matype)
        {
            string entry = systemDataGrid.Rows[sysIndex].Cells[1].EditedFormattedValue.ToString();
            entry = entry.ToLower();

            if (data != null && entry != "")
            {
                inc[] calctemp = data.ToArray();

                calctemp = calculateNestedSystem(entry, matype);

                systems_incData[sysIndex] = calctemp.ToList();
            

                //redraw
                putIncDataIntoHeatMapColumn(sysIndex + 1, systems_incData[sysIndex]); //the column is offset by 1 due to the date column at index 0

                //recalculate and redisplay the dependent systems, if there are any
                List<int> dependentSystems = getDependentSystems(sysIndex);
                foreach (int i in dependentSystems)
                {
                    recalculateSystem(i, matype);
                }
            }
        }

        //after the higher level has drilled down to an item a vs b for example, this system 
        private inc[] calculateNestedSystem(string entry, maType matype)
        {
            string gvg = ""; //group vs group like: (10*6)vs(6*2)
            inc[] result = data.ToArray();
            inc[] temp = data.ToArray();
            List<inc[]> temps = new List<inc[]>();

            MatchCollection matches = match(entry, nestPattern);
            if (matches.Count > 0)
            {
                string firstPart = "";
                string splitType = "";
                string secondPart = "";
                if(matches.Count >5 && (matches[5].Value == "vs" || matches[5].Value == "conf"))
                {
                    firstPart += matches[0].Value + matches[1].Value + matches[2].Value + matches[3].Value + matches[4].Value;

                    for (int i = 5; i<matches.Count; i++)
                    {
                        secondPart += matches[i].Value;
                    }

                    if (matches[5].Value == "vs")
                    {
                        ToolClass.calcVsCol(calculateNestedSystem(firstPart , matype).ToList<inc>(),
                                                        calculateNestedSystem(secondPart, matype).ToList<inc>(),
                                                        ref result);
                        return result;
                    }
                    else if(matches[5].Value == "conf")
                    {
                        ToolClass.calcConfCol(calculateNestedSystem(firstPart, matype).ToList<inc>(),
                                                        calculateNestedSystem(secondPart, matype).ToList<inc>(),
                                                        ref result);
                        return result;
                    }
                }

                foreach (Match m in matches)
                {
                    if(m.Value != "") {
                        var matchgroups = m.Groups;
                        if (matchgroups.Count >= 7)
                        {
                            if(matchgroups[8].Value == "vs" || matchgroups[8].Value == "conf")
                            {
                                gvg = matchgroups[8].Value;
                                if(gvg == "vs" && matches.Count > 2)
                                {
                                    ToolClass.calcVsCol(calculateNestedSystem(matches[0].Value.Replace("vs",""),matype).ToList<inc>(),
                                                        calculateNestedSystem(matches[1].Value.Replace("vs", ""), matype).ToList<inc>(),
                                                        ref result);
                                    return result;
                                }
                                else if(gvg == "conf" && matches.Count > 2)
                                {
                                    ToolClass.calcConfCol(calculateNestedSystem(matches[0].Value.Replace("conf", ""), matype).ToList<inc>(),
                                                        calculateNestedSystem(matches[1].Value.Replace("conf", ""), matype).ToList<inc>(),
                                                        ref result);
                                    return result;
                                }
                            }
                            firstPart = matchgroups[4].Value;
                            splitType = matchgroups[5].Value;
                            secondPart = matchgroups[6].Value;
                            inc[] temp1;
                            inc[] temp2;
                            inc[] temp3;
                            if (firstPart != "")
                            {
                                switch (splitType)
                                {
                                    case "conf":
                                        temp1 = calculateNestedSystem(firstPart, matype);
                                        temp2 = calculateNestedSystem(secondPart, matype);
                                        ToolClass.calcConfCol(temp1.ToList(), temp2.ToList(), ref temp);
                                        temps.Add((inc[])temp.Clone());
                                        break;
                                    case "vs":
                                        temp1 = calculateNestedSystem(firstPart, matype);
                                        temp2 = calculateNestedSystem(secondPart, matype);
                                        ToolClass.calcVsCol(temp1.ToList(), temp2.ToList(), ref temp);
                                        temps.Add((inc[])temp.Clone());
                                        break;
                                    case "*":
                                        temp1 = calculateNestedSystem(firstPart, matype);
                                        ToolClass.calcMultCol(temp1, int.Parse(secondPart), ref temp);
                                        temps.Add((inc[])temp.Clone());
                                        break;
                                    case "": //they entered something like: a vs (b 2)
                                        temp1 = calculateNestedSystem(firstPart+" "+secondPart, matype);
                                        string otherParts = "";
                                        if(matchgroups[1].Length > 0)
                                        {
                                            otherParts = matchgroups[1].Value;
                                            if (otherParts.Contains("conf"))
                                            {
                                                temp2 = calculateNestedSystem(otherParts.Replace("conf",""), matype);
                                                ToolClass.calcConfCol(temp2.ToList<inc>(), temp1.ToList<inc>(), ref temp);
                                            }else if (otherParts.Contains("vs"))
                                            {
                                                temp2 = calculateNestedSystem(otherParts.Replace("vs", ""), matype);
                                                ToolClass.calcVsCol(temp2.ToList<inc>(), temp1.ToList<inc>(), ref temp);
                                            }
                                        }else if (matchgroups[7].Length > 0)
                                        {
                                            otherParts = matchgroups[7].Value;
                                            if (otherParts.Contains("conf"))
                                            {
                                                temp2 = calculateNestedSystem(otherParts.Replace("conf", ""), matype);
                                                ToolClass.calcConfCol(temp1.ToList<inc>(), temp2.ToList<inc>(), ref temp);
                                            }
                                            else if (otherParts.Contains("vs"))
                                            {
                                                temp2 = calculateNestedSystem(otherParts.Replace("vs", ""), matype);
                                                ToolClass.calcVsCol(temp1.ToList<inc>(), temp2.ToList<inc>(), ref temp);
                                            }
                                        }

                                        temps.Add((inc[])temp.Clone());
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        
                        if (firstPart == "" && splitType == "" && secondPart == "")
                        {
                            temp = calculateBaseSystem(entry, matype);
                            temps.Add(temp);
                        }
                        else
                        {
                            if (matchgroups[1].Length > 0)
                            {
                                temps.Add(calculateNestedSystem(matchgroups[1].Value, matype));
                            }
                            if (matchgroups[7].Length > 0)
                            {
                                temps.Add(calculateNestedSystem(matchgroups[7].Value, matype));
                            }
                        }
                    }
                }

                ToolClass.calcCombinedSystems(temps, ref result);
            }
            else
            {
                result = calculateBaseSystem(entry, matype);
            }

            return result;
        }

        private inc[] calculateBaseSystem(string entry, maType matype)
        {
            entry = entry.Replace("(", "");
            entry = entry.Replace(")", "");
            inc[] calctemp = data.ToArray();
            sysType type = sysType.NONE;

            //you're either separated by 'vs' or 'conf' or '*' or ' '
            string[] confirmTokens = entry.Replace(" ","").Split(new string[] { splitTypes.confirm }, StringSplitOptions.None);
            string[] vsTokens = entry.Replace(" ", "").Split(new string[] { splitTypes.versus }, StringSplitOptions.None);
            string[] multiplyTokens = entry.Replace(" ", "").Split(new string[] { splitTypes.multiplier}, StringSplitOptions.None);
            string[] combineSystemsTokens = entry.Split(new string[] {" "}, StringSplitOptions.None);

            List<int> sysNumbers = new List<int>();
            List<int> indexes = new List<int>();
            List<List<inc>> sysCols = new List<List<inc>>();
            List<inc[]> temps = new List<inc[]>();
            inc[] intemp1 = data.ToArray();
            inc[] intemp2 = data.ToArray();

            int alphaindex1 = 0;
            int alphaindex2 = 0;
            int token1 = 0;
            int token2 = 0;

            if (entry != "" && entry.Replace(" ", "").All(char.IsDigit)) //all numeric
            {
                string[] tokens = entry.Split(' ');
                foreach (string token in tokens)
                {
                    if (token != "")
                    {
                        sysNumbers.Add(int.Parse(token));
                    }
                }
                type = sysType.PLAIN_NUMBERS;
            }
            else // alpha or alphanumeric
            {
                if (confirmTokens.Length == 2)
                {
                    if (int.TryParse(confirmTokens[0], out token1))
                    {
                        ToolClass.calcSysCol(new List<int> { token1 }, ref intemp1, matype);
                    }
                    else if (confirmTokens[0].Length == 1 && Convert.ToChar(confirmTokens[0]) >= 'a' && Convert.ToChar(confirmTokens[0]) <= 'z')
                    {
                        alphaindex1 = Convert.ToChar(confirmTokens[0]) - 97;
                        intemp1 = systems_incData[alphaindex1].ToArray();
                    }

                    if (int.TryParse(confirmTokens[1], out token2))
                    {
                        ToolClass.calcSysCol(new List<int> { token2 }, ref intemp2, matype);
                    }
                    else if (confirmTokens[1].Length == 1 && Convert.ToChar(confirmTokens[1]) >= 'a' && Convert.ToChar(confirmTokens[1]) <= 'z')
                    {
                        alphaindex2 = Convert.ToChar(confirmTokens[1]) - 97;
                        intemp2 = systems_incData[alphaindex2].ToArray();
                    }

                    type = sysType.CONFIRM;
                }else if (vsTokens.Length == 2)
                {
                    if(int.TryParse(vsTokens[0], out token1))
                    {
                        ToolClass.calcSysCol(new List<int> { token1 }, ref intemp1, matype);
                    }else if(vsTokens[0].Length == 1 && Convert.ToChar(vsTokens[0]) >= 'a' && Convert.ToChar(vsTokens[0]) <= 'z')
                    {
                        alphaindex1 = Convert.ToChar(vsTokens[0]) - 97;
                        intemp1 = systems_incData[alphaindex1].ToArray();
                    }

                    if (int.TryParse(vsTokens[1], out token2))
                    {
                        ToolClass.calcSysCol(new List<int> { token2 }, ref intemp2, matype);
                    }
                    else if (vsTokens[1].Length == 1 && Convert.ToChar(vsTokens[1]) >= 'a' && Convert.ToChar(vsTokens[1]) <= 'z')
                    {
                        alphaindex2 = Convert.ToChar(vsTokens[1]) - 97;
                        intemp2 = systems_incData[alphaindex2].ToArray();
                    }

                    type = sysType.VERSUS;

                }
                else if (multiplyTokens.Length == 2)
                {
                    if (int.TryParse(multiplyTokens[1], out token1) && multiplyTokens[0].Length == 1 && Convert.ToChar(multiplyTokens[0]) >= 'a' && Convert.ToChar(multiplyTokens[0]) <= 'z')
                    {
                        alphaindex1 = Convert.ToChar(multiplyTokens[0]) - 97;
                        intemp1 = systems_incData[alphaindex1].ToArray();
                        type = sysType.TIMES_KEY;
                    }else if (int.TryParse(multiplyTokens[0], out token1) && int.TryParse(multiplyTokens[1], out token2))
                    {
                        intemp1 = calculateBaseSystem(multiplyTokens[0], matype);
                    }
                    type = sysType.TIMES_KEY;
                }
                else if (type == sysType.NONE) //a last chance catch for alpha combination systems: a b c d... or a 20
                {
                    string[] combineSystemTokens = entry.Split(' ');
                    if (combineSystemTokens.Length >= 2)
                    {
                        for (int i = 0; i < combineSystemTokens.Length; i++)
                        {
                            string e = combineSystemTokens[i];
                            if (e != "")
                            {
                                if (int.TryParse(e, out token1))
                                {
                                    intemp1 = calculateBaseSystem(e, matype);
                                    temps.Add((inc[])intemp1.ToArray().Clone());
                                }
                                else if (Convert.ToChar(e) >= 'a' && Convert.ToChar(e) <= 'z')
                                {
                                    int index = Convert.ToChar(e) - 97;
                                    temps.Add((inc[])systems_incData[index].ToArray().Clone());
                                }
                            }
                        }
                        type = sysType.COMBINE_SYSTEMS;  //it appears only one system val i getting filled in for this type of system
                    }
                }
            }

            if (data != null)
            {

                switch (type)
                {
                    case sysType.PLAIN_NUMBERS:
                        ToolClass.calcSysCol(sysNumbers, ref calctemp, matype);
                        break;
                    case sysType.TIMES_KEY:
                        ToolClass.calcMultCol(intemp1, token1, ref calctemp);
                        break;
                    case sysType.VERSUS:
                        ToolClass.calcVsCol(intemp1.ToList(), intemp2.ToList(), ref calctemp);
                        break;
                    case sysType.CONFIRM:
                        ToolClass.calcConfCol(intemp1.ToList(), intemp2.ToList(), ref calctemp);
                        break;
                    case sysType.COMBINE_SYSTEMS:
                        ToolClass.calcCombinedSystems(temps, ref calctemp);
                        break;
                    default:
                        break;
                }
            }
            return calctemp;
        }

        /// <summary>
        /// Returns a list of the indices of the systems that depend on the given system index
        /// </summary>
        private List<int> getDependentSystems(int index)
        {
            List<int> dependentSystems = new List<int>();
            char sysChar = Convert.ToChar(index + 97);

            for(int i = 0; i < 26; i++)
            {
                string entry = systemDataGrid.Rows[i].Cells[1].EditedFormattedValue.ToString();
                if (entry != "" && !entry.Replace(" ", "").All(char.IsDigit)) //not empty and not all numeric
                {
                    string[] confTokens = entry.Replace(" ","").Split(new string[] {splitTypes.confirm}, StringSplitOptions.None);
                    string[] vsTokens = entry.Replace(" ", "").Split(new string[] {splitTypes.versus}, StringSplitOptions.None);
                    string[] multTokens = entry.Replace(" ", "").Split(new string[] { splitTypes.multiplier }, StringSplitOptions.None);
                    string[] combineSystemsTokens = entry.Split(' ');

                    if (confTokens.Length >= 2 && entry.Replace("conf", "").Replace("vs", "").Contains(sysChar))
                    {
                        dependentSystems.Add(i);
                    }
                    else if (vsTokens.Length >= 2 && entry.Replace("conf", "").Replace("vs", "").Contains(sysChar))
                    {
                        dependentSystems.Add(i);
                    }
                    else if (multTokens.Length >=1 && entry.Replace("conf","").Replace("vs","").Contains(sysChar))
                    {
                        dependentSystems.Add(i);
                    }
                    else if((entry.Split(' ')).ToList().Contains(sysChar.ToString()))
                    {
                        dependentSystems.Add(i);
                    }
                }
            }

            foreach(int i in dependentSystems)
            {
                dependentSystems.Concat(getDependentSystems(i));
            }
            
            return dependentSystems;
        }

        private void maTypeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                recalculateSystems();
                putIncDataIntoHeatMapColumns();
            }
        }

        private void systemDataGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (initialized)
            {
                string entry = systemDataGrid.Rows[e.RowIndex].Cells[1].EditedFormattedValue.ToString();
                if (entry != "" && validateEntry(entry))
                {
                    validationArray[e.RowIndex] = true;
                    recalculateSystem(e.RowIndex, (maType)maTypeListBox.SelectedIndex);
                }
                else
                {
                    validationArray[e.RowIndex] = false;
                    clearHeatMapColumn(e.RowIndex+1);
                }
            }
        }

        //clears out the entered systems
        private void clearSystemsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < systemData.Rows.Count; i++)
            {
                systemData.Rows[i][1] = DBNull.Value;
                validationArray[i] = false;
            }

            for (int i = 0; i < systemData.Rows.Count; i++)
            {
                clearHeatMapColumn(i + 1); //+1 due to offset for the date column
            }
        }

        /// <summary>
        /// Event handler for when the checkbox gets checked or unchecked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPlayOvernight_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            ToolClass.playOvernight = cb.Checked;
            recalculateSystems();
            putIncDataIntoHeatMapColumns();
        }

        private void filebutton_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }

                    handleFileChange(filePath);
                }
            }
        }
    }
}