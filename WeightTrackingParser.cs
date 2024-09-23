using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
            Console.WriteLine("This is the start of the project");

            List<double> weight = new List<double>()
            {
                156.2, 162.8, 155.5
            };

            List<string> date = new List<string>()
            {
                "11/5/2021", "5/8/2019", "9/23/2020"
            };

            List<DayData> list = new List<DayData>();

            for (int i = 0; i < weight.Count; i++)
            {
                // do something about empty cell values

                DayData day = new DayData();
                day.Date = date[i];
                day.Weight = weight[i];
                list.Add(day);
            }

            double maximum = ReturnMaxWeight(list); 
            Console.WriteLine(maximum);

            List<string> datemax = ReturnDateAtWeight(list, maximum);
            foreach (string dates in datemax)
            {
                Console.WriteLine(dates);
            }


            Console.ReadLine();
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


