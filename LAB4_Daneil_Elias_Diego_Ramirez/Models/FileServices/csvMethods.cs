using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAB4_Daneil_Elias_Diego_Ramirez.Models.Data;
using CsvHelper;
using System.IO;
using System.Text;

namespace LAB4_Daneil_Elias_Diego_Ramirez.Models.FileServices
{
    public class csvMethods
    {
        

        public void WriteCSVFile(string path, List<Developer> developers)
        {
            using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw))
            {
                cw.WriteHeader<Developer>();
                cw.NextRecord();
                foreach (Developer developer in developers)
                {
                    cw.WriteRecord<Developer>(developer);
                    cw.NextRecord();
                }
            }
        }
    }
}
