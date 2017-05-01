using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Drawor.Financeiro.Models;
using Drawor.Financeiro.ViewModels;

namespace Drawor.Financeiro.Mapper
{
    public class MapperFinanceiro
    {
        Tools.ToolsDataTImecs toolDateTime = null;
        private string StringConnection = string.Empty;
        private string InsertTipoDespesa = "INSERT INTO TiposDespesa (Nome,Cor,CreateTime,CreateBy,Obsoleto) VALUES(@Nome,@Cor,@CreateTime,@CreateBy,@Obsoleto)";
        private string InsertConta = "INSERT INTO Contas (Nome,Saldo,CreateTime,CreateBy,Obsoleto) VALUES(@Nome,@Saldo,@CreateTime,@CreateBy,@Obsoleto)";
        private string InsertDespesas = "INSERT INTO Despesas (EstaPago,Vencimento,TipoDespesaId,ContaId,Valor,CreateTime,CreateBy,Obsoleto) VALUES(@EstaPago,@Vencimento,@TipoDespesaId,@ContaId,@Valor,@CreateTime,@CreateBy,@Obsoleto)";
        private string SelectContasAtivas = "SELECT * Contas where obsoleto = false";
        private string SelectContasAtivasDropDownList = "SELECT Id,Nome from Contas where Obsoleto = 'FALSE'";
        private string SelectTipoDespesaAtivasDropDownList = "SELECT Id,Nome from TiposDespesa where Obsoleto = 'FALSE'";

        public MapperFinanceiro()
        {
            StringConnection = Mappers.MapperConfig.connetionStringWEB;
            toolDateTime = new Tools.ToolsDataTImecs();
        }
        internal void CriarConta(Conta dto, string currentUserId)
        {
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(StringConnection))
                    using (SqlCommand cmd = new SqlCommand(InsertConta, cn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Nome", dto.Nome));
                        cmd.Parameters.Add(new SqlParameter("@Saldo", (decimal)dto.SaldoAtual));
                        cmd.Parameters.Add(new SqlParameter("@CreateTime", DateTime.Now));
                        cmd.Parameters.Add(new SqlParameter("@CreateBy", currentUserId));
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
        internal void CriarNovaDespesa(DespesaViewModel novaDespesa,string currentUserId)
        {
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(StringConnection))
                    using (SqlCommand cmd = new SqlCommand(InsertDespesas, cn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@EstaPago", true));
                        cmd.Parameters.Add(new SqlParameter("@Vencimento", novaDespesa.Vencimento));
                        cmd.Parameters.Add(new SqlParameter("@TipoDespesaId", Convert.ToInt32(novaDespesa.TipoDespesa)));
                        cmd.Parameters.Add(new SqlParameter("@ContaId", Convert.ToInt32(novaDespesa.Conta)));
                        cmd.Parameters.Add(new SqlParameter("@Valor", Convert.ToDecimal(novaDespesa.Valor)));
                        cmd.Parameters.Add(new SqlParameter("@CreateTime",DateTime.Now));
                        cmd.Parameters.Add(new SqlParameter("@CreateBy", currentUserId));
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

        internal List<Despesa> PegarTodasDespesas()
        {
            List<Despesa> despesas = new List<Despesa>();
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    //   private string InsertConta = "INSERT INTO Contas (Nome,Saldo,CreateTime,CreateBy,Obsoleto) VALUES(@Nome,@Saldo,@CreateTime,@CreateBy,@Obsoleto)";
                    connection.Open();
                    var command = new SqlCommand(SelectTipoDespesaAtivasDropDownList, connection);
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Despesa Despesa = new Despesa();
                        ////string ID = dataReader["ID"].ToString();
                        //tipoDespesa.Id = Convert.ToInt32(dataReader["Id"]);
                        //tipoDespesa.Nome = dataReader["Nome"].ToString();

                        despesas.Add(Despesa);

                    }
                    dataReader.Close();
                    command.Dispose();
                    connection.Close();
                }
                catch (Exception ex)
                {

                }
            }

            return despesas;
        }

        internal List<TipoDespesa> PegarTodosTiposDeDespesaAtivosDropDownList()
        {
            List<TipoDespesa> tipoDespesas = new List<TipoDespesa>();
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    //   private string InsertConta = "INSERT INTO Contas (Nome,Saldo,CreateTime,CreateBy,Obsoleto) VALUES(@Nome,@Saldo,@CreateTime,@CreateBy,@Obsoleto)";
                    connection.Open();
                    var command = new SqlCommand(SelectTipoDespesaAtivasDropDownList, connection);
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        TipoDespesa tipoDespesa = new TipoDespesa();
                        //string ID = dataReader["ID"].ToString();
                        tipoDespesa.Id = Convert.ToInt32(dataReader["Id"]);
                        tipoDespesa.Nome = dataReader["Nome"].ToString();

                        tipoDespesas.Add(tipoDespesa);

                    }
                    dataReader.Close();
                    command.Dispose();
                    connection.Close();
                }
                catch (Exception ex)
                {

                }
            }

            return tipoDespesas;
        }

        internal List<Conta> PegarTodasContasAtivas()
        {
            List<Conta> contas = new List<Conta>();
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                   //   private string InsertConta = "INSERT INTO Contas (Nome,Saldo,CreateTime,CreateBy,Obsoleto) VALUES(@Nome,@Saldo,@CreateTime,@CreateBy,@Obsoleto)";
                    connection.Open();
                    var command = new SqlCommand(SelectContasAtivas, connection);
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Conta conta = new Conta();
                        //string ID = dataReader["ID"].ToString();
                        conta.Id = dataReader["Id"];
                        conta.Nome = dataReader["Nome"].ToString();
                        conta.SaldoAtual = Convert.ToDouble(dataReader["Saldo"].ToString());
                        conta.CreateTime = dataReader["CreateTime"].ToString();
                        conta.CreateBy = dataReader["CreateBy"].ToString();
                        conta.Obsoleto = dataReader["Obsoleto"].ToString();

                        contas.Add(conta);

                    }
                    dataReader.Close();
                    command.Dispose();
                    connection.Close();
                }
                catch (Exception ex)
                {

                }
            }

            return contas;
        }
        public void CriarTipoDespesa(Financeiro.Models.TipoDespesa tipoDespesa,string currentUserID)
        {
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {   
                    using (SqlConnection cn = new SqlConnection(StringConnection))
                    using (SqlCommand cmd = new SqlCommand(InsertTipoDespesa, cn))
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
        internal List<Conta> PegarTodasContasAtivasDropDownList()
        {
            List<Conta> contas = new List<Conta>();
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    //   private string InsertConta = "INSERT INTO Contas (Nome,Saldo,CreateTime,CreateBy,Obsoleto) VALUES(@Nome,@Saldo,@CreateTime,@CreateBy,@Obsoleto)";
                    connection.Open();
                    var command = new SqlCommand(SelectContasAtivasDropDownList, connection);
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Conta conta = new Conta();
                        //string ID = dataReader["ID"].ToString();
                        conta.Id = dataReader["Id"];
                        conta.Nome = dataReader["Nome"].ToString();
                       
                        contas.Add(conta);

                    }
                    dataReader.Close();
                    command.Dispose();
                    connection.Close();
                }
                catch (Exception ex)
                {

                }
            }

            return contas;
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