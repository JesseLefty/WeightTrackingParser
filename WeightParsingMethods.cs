using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightTrackingParser
{
    class WeightParsingMethods
    {
        public static List<DayData> FilterByYear(List<DayData> list, int year)
        {
            List<DayData> filteredList = new List<DayData>();

            foreach (var item in list)
            {
                DateOnly date = DateOnly.Parse(item.Date);
                if (date.Year == year)
                {
                    filteredList.Add(item);
                }
            }

            return filteredList;
        }
        public static List<DayData> FilterByMonth(List<DayData> list, int monthNum)
        {
            List<DayData> filteredList = new List<DayData>();

            foreach (var item in list)
            {
                DateOnly date = DateOnly.Parse(item.Date);
                if (date.Month == monthNum)
                {
                    filteredList.Add(item);
                }
            }

            return filteredList;
        }
        public static List<DayData> FilterByMonth(List<DayData> list, string monthStr)
        {
            Dictionary<string, int> months = new Dictionary<string, int>()
            {
                {"january", 01},
                {"febuary", 02},
                {"march", 03},
                {"april", 04},
                {"may", 05},
                {"june", 06},
                {"july", 07},
                {"august", 08},
                {"september", 09},
                {"october", 10},
                {"november", 11},
                {"december", 12},

            };
            int month = months[monthStr.ToLower()];
            List<DayData> filteredList = FilterByMonth(list, month);

            return filteredList;
        }
        public static List<DayData> FilterByDateRange(List<DayData> list, string startDate, string endDate)
        {
            List<DayData> filteredList = new List<DayData>();
            DateOnly start = DateOnly.Parse(startDate);
            DateOnly end = DateOnly.Parse(endDate);
            foreach (var item in list)
            {
                if (DateOnly.Parse(item.Date) >= start && DateOnly.Parse(item.Date) <= end)
                {
                    filteredList.Add(item);
                }
            }
            return filteredList;
        }

        public static double ReturnMaxWeight(List<DayData> list)
        {
            /**
             * This function takes in a list of DayData objects and returns the maximum 
             * weight in that set of DayData
             * 
             * 
             **/
            var tempList = new List<double>();
            foreach (var item in list)
            {
                tempList.Add(item.Weight);
            }
            return tempList.Max();
        }
        public static double ReturnMinWeight(List<DayData> list)
        {
            var tempList = new List<double>();
            foreach (var item in list)
            {
                tempList.Add(item.Weight);
            }
            return tempList.Where(w => w != 0).Min();
        }
        public static List<string> ReturnDateAtWeight(List<DayData> list, double weight)
        {
            var tempList = new List<string>();

            foreach (var item in list)
            {
                if (item.Weight == weight)
                {
                    tempList.Add(item.Date);
                }
            }

            return tempList;
        }
        public static int ReturnUnweighedDaysCount(List<DayData> list, out List<string> emptyDates)
        {
            int total = 0;
            emptyDates = new List<string>();
            foreach (var item in list)
            {
                if (item.Weight == 0 || string.IsNullOrEmpty(item.Time))
                {
                    total++;
                    emptyDates.Add(item.Date);
                }
            }
            return total;
        }
        public static List<double> ReturnAverageBlocks(List<DayData> list, int blocksize)
        {

            List<double> averageList = new List<double>();
            List<double> weightList = new List<double>();
            for (int i = 0; i < list.Count; i = i + blocksize)
            {
                var blockList = list.Skip(i).Take(blocksize);
                foreach (var item in blockList)
                {
                    weightList.Add(item.Weight);
                }
                averageList.Add(weightList.Where(w => w != 0).Average());
            }

            return averageList;
        }
        public static double ReturnAverage(List<DayData> list)
        {
            List<double> averageList = new List<double>();
            foreach (var item in list)
            {
                averageList.Add(item.Weight);
            }
            return averageList.Average();

        }
    }
}
