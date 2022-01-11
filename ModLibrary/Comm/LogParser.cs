using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ModLibrary.Comm
{
    public static class LogParser
    {
        private static readonly Log log = new Log();

        public static List<LogModel> GetLog(DateTime startdate, DateTime enddate, LogLevel loglevel)
        {
            List<LogModel> LM = new List<LogModel>();

            for (int i = 0; i <= enddate.Subtract(startdate).Days; i++)
            {
                LM.AddRange(GetDayLog(startdate.AddDays(i), loglevel));
            }

            return LM;
        }

        private static List<LogModel> GetDayLog(DateTime logdate, LogLevel loglevel)
        {
            string filepath = $"log\\{logdate.Year}\\{logdate.Month:D2}\\{logdate:yyyy_MM_dd}_Log.log";
            FileInfo fi = new FileInfo(filepath);

            try
            {
                if (fi.Exists)
                {
                    using (FileStream fs = fi.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (TextReader tr = new StreamReader(fs, Encoding.Default))
                        {
                            string jtext = "[" + tr.ReadToEnd() + "]";

                            List<LogModel> LM = JsonConvert.DeserializeObject<List<LogModel>>(jtext);

                            if (LM != null)
                            {
                                return LM.Where(o => (int)o.Level >= (int)loglevel).ToList();
                            }
                        }
                    }
                }
                else
                {
                    log.Warn("파일 없음 FileName = " + fi.Name);
                }
            }
            catch (Exception ex)
            {
                log.Error("로그 파일 불러오기 오류 Exception=" + ex.Message);
                return new List<LogModel>();
            }

            return new List<LogModel>();
        }
    }
}
