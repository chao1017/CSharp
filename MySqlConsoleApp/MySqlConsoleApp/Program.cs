using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.IO;

namespace MySqlConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Console.WriteLine("Hello World!");

            IConfiguration _config = config();

            string conn_str = _config.GetConnectionString("1");
            MySqlConnection conn = new MySqlConnection(conn_str);

            string sql_str = string.Empty;

            try
            {
                conn.Open();

                //check if table exists
                sql_str = @"SELECT * FROM information_schema.tables WHERE table_schema = '1' 
                                    AND table_name like 'fwdetect_" + DateTime.Now.ToString("yyyy") +
                                    "_" + DateTime.Now.ToString("MM") + "%' order by table_name desc;";

                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql_str, conn);
                    MySqlDataReader myData = cmd.ExecuteReader();

                    if (!myData.HasRows)
                    {
                        // 如果沒有資料,顯示沒有資料的訊息
                        Console.WriteLine("沒有資料");
                    }
                    else {
                        while (myData.Read())
                        {
                            Console.WriteLine("table catalog={0}, table schema={1}, table name={2}, table type={3}", 
                                myData.GetString(0) + ", ", myData.GetString(1) + ", ",
                                myData.GetString(2) + ", ", myData.GetString(3));
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 1042:
                        Console.WriteLine("無法連線到資料庫.");
                        break;

                    case 1045:
                        Console.WriteLine("使用者帳號或密碼錯誤,請再試一次.");
                        break;

                    default:
                        break;
                }
            }
            finally {
                conn.Close();
            }

            
            Console.WriteLine(sql_str);
            Console.ReadLine();
        }

        public static IConfiguration config()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        //public static IConfigurationRoot ReadFromAppSettings()
        //{
        //    return new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", false)
        //        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT")}.json", optional: true)
        //        .AddEnvironmentVariables()
        //        .Build();
        //}

        //public static void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:Default"]));
        //}
    }

       
}
