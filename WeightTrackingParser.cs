using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Globalization;

/*
 * This script should do the following
 * 1. Import CSV file data into a list or dictionary, most likely a dictionary
 * 2. Print sorted data to console
 *      2a. Sort by multiple criteria
 *      2b. Sort only a slice of the data (i.e. only 2024 or only November 2018 data etc.)
 * 3. Provide statstics based on the data
 *      3a. Average
 *      3b. Max / Min
 * 4. Provide some sort of logging of user actions
 */

namespace WeightTrackingParser
{
    internal class WeightTrackingParser
    {
        static void Main(string[] args)
        {

            List<string> weight = new List<string>()
            {
                "156.2", "162.8", "155.5", null, "162.8"
            };

            List<string> date = new List<string>()
            {
                "11/5/2021", "5/8/2019", "9/23/2020", "7/11/2016", "1/26/2024"
            };
            List<string> time = new List<string>()
            {
                "6:45", "5:45", "6:15", "6:25", null
            };

            List<DayData> list = new List<DayData>();

            for (int i = 0; i < weight.Count; i++)
            {
                // do something about empty cell values

                DayData day = new DayData();
                day.Date = date[i];
                day.Weight = Convert.ToDouble(weight[i]);
                day.Time = time[i];
                list.Add(day);
            }

            double maximum = ReturnMaxWeight(list); 
            Console.WriteLine(maximum);

            List<string> datemax = ReturnDateAtWeight(list, maximum);
            foreach (string dates in datemax)
            {
                Console.Write($"{dates} ");
            }

            List<string> emptyDates = new List<string>();
            Console.WriteLine();
            Console.WriteLine(ReturnUnweighedDaysCount(list, out emptyDates));
            emptyDates.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine();
            List<DayData> filteredList = FilterByDateRange(list, "1/1/2019", "12/31/2020");
            foreach (DayData day in filteredList)
            {
                Console.WriteLine($"{day.Weight}\t{day.Date}\t{day.Time}");
            }
            Console.WriteLine();
            List<DayData> filteredList2 = FilterByMonth(list,9);
            foreach (DayData day in filteredList2)
            {
                Console.WriteLine($"{day.Weight}\t{day.Date}\t{day.Time}");
            }
            Console.WriteLine();
            List<DayData> filteredList3 = FilterByMonth(list, "November");
            if ( filteredList3 != null )
            {

                foreach (DayData day in filteredList3)
                {
                    Console.WriteLine($"{day.Weight}\t{day.Date}\t{day.Time}");
                }
            }
            else
            {
                Console.WriteLine($"The month you were searching for contains no data");
            }
            Console.WriteLine();
            List<DayData> filteredList4 = FilterByYear(list, 2020);
            foreach (DayData day in filteredList4)
            {
                Console.WriteLine($"{day.Weight}\t{day.Date}\t{day.Time}");
            }
            Console.WriteLine();

            Console.ReadLine();
        }
















        static List<DayData> FilterByYear(List<DayData> list, int year)
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
        static List<DayData> FilterByMonth(List<DayData> list, int monthNum)
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
        static List<DayData> FilterByMonth(List<DayData> list, string monthStr)
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
        static List<DayData> FilterByDateRange(List<DayData> list, string startDate, string endDate)
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
        
        static double ReturnMaxWeight(List<DayData> list)
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
        static List<string> ReturnDateAtWeight(List<DayData> list, double weight)
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
        static int ReturnUnweighedDaysCount(List<DayData> list, out List<string> emptyDates)
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
       
    }

    struct DayData
    {
        private string date;
        private string time;
        private double weight;

        public double Weight
        {
            get
            { return weight; }
            set
            { weight = value; }
        }
        public string Time
        {
            get
            { return time; }
            set
            { time = value; }
        }
        public string Date
        {
            get
            { return date; }
            set
            { date = value; }
        }




    }


}


