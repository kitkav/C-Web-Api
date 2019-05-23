using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using System.Data.Common;
using System.Configuration;
using System.Net;
using System.IO;
using System.Collections;

namespace WebAPIDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Web API Call and Storing JSON into SQL Database.");
            Console.WriteLine("Calling : https://jsonplaceholder.typicode.com/posts");

            // Establish Database Connection.
            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
            
            using (DbConnection connection = factory.CreateConnection())
            {
                if(connection == null)
                {
                    Console.WriteLine("Connection Error");
                    Console.ReadLine();
                    return;
                }
                connection.ConnectionString = connectionString;
                connection.Open();
                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    Console.WriteLine("Command Error");
                    Console.ReadLine();
                    return;
                }
                command.Connection = connection;
                
                // Calling Api 
                string strurltest = String.Format("https://jsonplaceholder.typicode.com/posts");
                WebRequest requestObjGet = WebRequest.Create(strurltest);
                requestObjGet.Method = "GET";
                HttpWebResponse responseObjGet = null;
                responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();

                string apistrresult = null;
                using (Stream stream = responseObjGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    apistrresult = sr.ReadToEnd();

                    // Parsing the JSON payload
                    var jsondata = JsonConvert.DeserializeObject<dynamic>(apistrresult);
                    
                    Console.WriteLine("Total Json obj's: " + jsondata.Count);
                    // Storing that data in a SQL database
                    for (int i = 0; i < jsondata.Count; i++)
                    {
                        command.CommandText = "INSERT INTO apiData(userId, title, body) VALUES ('" 
                            + jsondata[i].userId + "', '" + jsondata[i].title + "', '" + jsondata[i].body + "')";
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine("Data Stored in SQL Database.");
                    //Console.WriteLine(jsondata[1].userId);
                    //Console.WriteLine(jsondata[1].id);
                    //Console.WriteLine(jsondata[1].title);
                    //Console.WriteLine(jsondata[1].body);
                    sr.Close();
                }
                connection.Close();
                Console.ReadKey();
            }
        }
    }
}
