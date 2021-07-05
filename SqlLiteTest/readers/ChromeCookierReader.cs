using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SqlLiteTest.readers
{
    public static class ChromeCookierReader
    {
        public static List<string> FindCookie(string host)
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var path = Path.Combine(local, @"Google\Chrome\User Data\Default\Cookies");

            List<string> cookieLists = new List<string>();

            if (!File.Exists(path))
                return cookieLists;

            string totalCOnnection = @"data source=""" + path + @"""";
            using (var connect = new SQLiteConnection(totalCOnnection))
            {
                connect.Open();

                using (var cmd = connect.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = string.Format(@"select name || '=' || value as result 
                                                    from cookies 
                                                    where host_key like('%{0}%')", host);


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