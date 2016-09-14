using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var cnstr = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;

            using (var cn = new SqlConnection(cnstr))
            {
                cn.Open();
                
                #region 使用泛型版本的 Query 方法
                //string sql = string.Empty;
                //sql = @"select * from Customers where City like 'A%'";
                //var customers = cn.Query<TestDapper.Customers>(sql);
                #endregion

                #region 參數式查詢
                //var sql = "select * from Customers where City like @City or Country=@Country";
                //var parameters = new
                //{
                //    City = "E%",
                //    Country = "UK"
                //};
                //
                //var customers = cn.Query<TestDapper.Customers>(sql, parameters);
                #endregion

                //foreach (var cust in customers)
                //{
                //    Console.WriteLine(cust.CompanyName);
                //}

                #region 執行無結果集的 SQL 命令
                //var sql = "insert into Customers (CustomerID, CompanyName, Address, City, Phone, Fax)" +
                //            "values(@CustomerID, @CompanyName, @Address, @City, @Phone, @Fax)";
                //
                //var newCust = new TestDapper.Customers()
                //{
                //    CustomerID = "Z001",
                //    CompanyName = "MikeSoft",
                //    Address = "大馬路",
                //    City = "台北",
                //    Phone = "12345",
                //    Fax = "67890"
                //};
                //
                //int rowChanged = cn.Execute(sql, newCust);
                //
                //Console.WriteLine("rowChangeed = " + rowChanged);
                #endregion

                #region Delete
                var sql = "delete from Customers where CustomerID = @CustomerID";

                var _params = new TestDapper.Customers() {

                    CustomerID = "Z001"
                };

                int rowChanged = cn.Execute(sql, _params);

                Console.WriteLine("rowChangeed = " + rowChanged);
                #endregion

                #region 呼叫預儲程序
                //var spParams = new DynamicParameters();
                //
                //spParams.Add("CustomerID", "ALFKI", DbType.String, ParameterDirection.Input);
                //var custHistories = cn.Query("CustOrderHist", spParams, commandType: CommandType.StoredProcedure);
                //foreach (var custHist in custHistories)
                //{
                //    Console.WriteLine(custHist.ProductName);
                //}

                #endregion

                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }
    }
}
