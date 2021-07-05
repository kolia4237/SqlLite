using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SqlLiteTest.readers
{

    public static class FoxCookierReader
    {
        //string audioSpectrumProgram = "WindowsService1.exe";
        //private string audioSpectrumBatchProgram;
        //private Process pVizualizer;
        public static List<string> FindCookie(string host)
        {
            string path = null;
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string directory = Path.Combine(appdata, @"Mozilla\Firefox\Profiles\");

            if (Directory.Exists(directory))
            {
                var sub = Directory.GetDirectories(directory)[0]; 
                if (sub != null)
                    path = Path.Combine(sub, @"cookies.sqlite");
            }

            var cookieLists = new List<string>();

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return cookieLists;

            string totalCOnnection = @"data source=""" + path + @"""";
            using (var connect = new SQLiteConnection(totalCOnnection))
            {

                connect.Open();

                using (var cmd = connect.CreateCommand())
                {
                    

                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText =
                        string.Format(
                            @"select name || '=' || value as result 
                                                    from cookies 
                                                    where host_key like('%{0}%')",
                            host);


                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var result = reader["result"];

                            if (result != null)
                                cookieLists.Add(result.ToString());
                        }
                    }
                }
            }

            return cookieLists;
        }
    }

}