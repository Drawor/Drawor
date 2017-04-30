using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Drawor.Mappers
{
    public static class MapperConfig
    {
        public static string connetionStringLocal = @"Server=DESKTOP-CBQSQL3\SQLEXPRESS; Database= AiacosDB; Integrated Security=True;";
        public static string connetionStringWEB = @"Server=tcp:aiacosdb.database.windows.net,1433;Initial Catalog=AiacosDB;Persist Security Info=False;User ID=drawor;Password=Lcf#251088;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        //public string  GetConnectionString()
        //{
        //    return connetionStringLocal;
        //}
        //public void TestConection()
        //{
        //    SqlCommand command;
        //    string sql = null;
        //    SqlDataReader dataReader;

        //    sql = "select * from Carriers";

        //    using (SqlConnection connection = new SqlConnection(connetionStringLocal))
        //    {
        //        try
        //        {
                    //connection.Open();
                    //command = new SqlCommand(sql, connection);
                    //dataReader = command.ExecuteReader();
                    //while (dataReader.Read())
                    //{

                    //    //string ID = dataReader["ID"].ToString();
                    //    string Name = dataReader["internal_name"].ToString();

                    //}
                    //dataReader.Close();
                    //command.Dispose();
                    //connection.Close();
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }

        //}
    }
}