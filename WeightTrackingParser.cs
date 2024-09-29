using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using static WeightTrackingParser.WeightParsingMethods;

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
            List<double> average = ReturnAverageBlocks(list, 2);
            foreach (double value in average)
            {
                Console.WriteLine(value);
            }
            double minWeight = ReturnMinWeight(list);
            List<string> minWeightDates = ReturnDateAtWeight(list, minWeight);
            Console.WriteLine($"Minimum weight = {minWeight}");
            Console.Write($"On date(s)");
            foreach (string day in minWeightDates)
            {
                Console.Write($"{day} ");
            }

            Console.ReadLine();
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


