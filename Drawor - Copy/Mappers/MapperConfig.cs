using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Drawor.Mappers
{
    public class MapperConfig
    {
        private string connetionStringLocal = @"Server=DESKTOP-CBQSQL3\SQLEXPRESS; Database= AiacosDB; Integrated Security=True;";
      
        public void TestConection()
        {
            SqlCommand command;
            string sql = null;
            SqlDataReader dataReader;

            sql = "select * from Carriers";

            using (SqlConnection connection = new SqlConnection(connetionStringLocal))
            {
                try
                {
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {

                        //string ID = dataReader["ID"].ToString();
                        string Name = dataReader["internal_name"].ToString();

                    }
                    dataReader.Close();
                    command.Dispose();
                    connection.Close();
                }
                catch (Exception ex)
                {

                }
            }

        }
    }
}