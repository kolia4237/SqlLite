using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace SqlLiteTest.readers
{
    public static class OperaCookieReader
    {    
        private class Record
        {
            public byte TagId;  
            public ushort Length;
            public byte[] Payload;
        }

        public static List<string> FindCookie(string host)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string filePath = Path.Combine(appdata, @"C:\Users\Admin\AppData\Roaming\Opera Software\Opera Stable"); 
          
            var cookies = new List<string>();

            if (!File.Exists(filePath))
                return cookies;   

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    int file_version_number = /*IPAddress.NetworkToHostOrder*/ (reader.ReadInt32());
                    int app_version_number = /*IPAddress.NetworkToHostOrder*/ (reader.ReadInt32());

                    short idtag_length = IPAddress.NetworkToHostOrder(reader.ReadInt16());
                    short length_length = IPAddress.NetworkToHostOrder(reader.ReadInt16());


                    var path = new Stack<string>();

                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        int tag = reader.ReadByte();
                        
                        switch (tag)
                        {
                            case 133: 
                                continue;  
                            case 132:
                                path.Pop();
                                continue;   
                        }
                        
                        int length = (IPAddress.NetworkToHostOrder(reader.ReadInt16()));

                        long endRecord = length + reader.BaseStream.Position;

                        switch (tag)
                        {
                            case 0x01:    
                            case 0x02:      

                                var record = ReadRecord(reader);
                                string text = Encoding.ASCII.GetString(record.Payload);
                               
                                path.Push(text);

                                break;

                            case 0x03:
                                
                                if (path.Contains(host))
                                {
                                    var nameRecord = ReadRecord(reader);
                                    string name = Encoding.ASCII.GetString(nameRecord.Payload);

                                    var valueRecord = ReadRecord(reader);
                                    string value = Encoding.ASCII.GetString(valueRecord.Payload);

                                    cookies.Add(name + "=" + value);
                                }

                                break;

                            default:
                                Debugger.Break();
                                break;
                        }

                        reader.BaseStream.Position = endRecord;
                    }
                }
            }

            return cookies;
        }

        private static Record ReadRecord(BinaryReader reader)
        {
            var r = new Record();

            r.TagId = reader.ReadByte();

            r.Length = (ushort)(IPAddress.NetworkToHostOrder(reader.ReadInt16()));

            r.Payload = reader.ReadBytes(r.Length);

            return r;
        }

    }
}