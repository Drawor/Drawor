using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Drawor.Financeiro.Mapper
{
    public class MapperFinanceiro
    {
        Tools.ToolsDataTImecs toolDateTime = null;
        private string StringConnection = string.Empty;
        private string InsertDespesa = "INSERT INTO Despesa (Nome,Cor,CreateTime,CreateBy,Obsoleto) VALUES(@Nome,@Cor,@CreateTime,@CreateBy,@Obsoleto)";

        public MapperFinanceiro()
        {
            StringConnection = Mappers.MapperConfig.connetionStringWEB;
            toolDateTime = new Tools.ToolsDataTImecs();
        }
        public void CriarTipoDespesa(Financeiro.Models.TipoDespesa tipoDespesa,string currentUserID)
        {
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {   
                    using (SqlConnection cn = new SqlConnection(StringConnection))
                    using (SqlCommand cmd = new SqlCommand(InsertDespesa, cn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nome", tipoDespesa.Nome));
                        cmd.Parameters.Add(new SqlParameter("@Cor", tipoDespesa.Cor ?? DBNull.Value.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@CreateTime", DateTime.Now));
                        cmd.Parameters.Add(new SqlParameter("@CreateBy", currentUserID));
                        cmd.Parameters.Add(new SqlParameter("@Obsoleto", false));
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cn.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }

        }
    }
}











// using (SqlConnection connection = new SqlConnection(StringConnection))
//            {
//                try
//                {   
//                    string sql = $@"INSERT INTO Despesa (Nome,Cor) VALUES('{tipoDespesa.Nome}','{tipoDespesa.Cor}')";
//connection.Open();
//                    var command = new SqlCommand(sql, connection);
//var dataReader = command.ExecuteReader();
//                    while (dataReader.Read())
//                    {
//                        //string ID = dataReader["ID"].ToString();
//                        string Name = dataReader["internal_name"].ToString();

//                    }
//                    dataReader.Close();
//                    command.Dispose();
//                    connection.Close();
//                }
//                catch (Exception ex)
//                {

//                }
//            }