using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightTrackingParser
{
    internal class csvToList
    {
        public FileStream File;

        private FileStream file { get; set; }

        public List<DayData> importCSV()

        {
            StreamReader sr = new StreamReader(File);
            List<string> date = new List<string>();
            List<string> weight = new List<string>();
            List<string> time = new List<string>();
            
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var values = line.Split(',');

                date.Add(values[0]);
                time.Add(values[1]);
                weight.Add(values[2]);
            }
           
            sr.Close();

            List<DayData> weightData = new List<DayData>();

            for (int i = 1; i < weight.Count; i++)
            {

                DayData day = new DayData();
                day.Date = date[i];
                day.Weight = Convert.ToDouble(weight[i]);
                day.Time = time[i];
                weightData.Add(day);
            }
            return weightData;
        }
    }
}
