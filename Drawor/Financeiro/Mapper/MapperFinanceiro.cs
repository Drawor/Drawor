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
        private string SelectMaxArquivoId = "SELECT MAX(Id) as Id FROM Arquivos";
        private string InsertTipoDespesa = "INSERT INTO TiposDespesa (Nome,Cor,CreateTime,CreateBy,Obsoleto) VALUES(@Nome,@Cor,@CreateTime,@CreateBy,@Obsoleto)";
        private string InsertConta = "INSERT INTO Contas (Nome,Saldo,CreateTime,CreateBy,Obsoleto) VALUES(@Nome,@Saldo,@CreateTime,@CreateBy,@Obsoleto)";
        private string InsertComprovante = "INSERT INTO Arquivos (Nome,bytes,TipoArquivo,CreateTime,CreateBy) VALUES(@Nome,@bytes,@TipoArquivo,@CreateTime,@CreateBy)";
        private string SelectContaPorId = "Select * from Contas where Id = @Id";
        private string SelectTipoDespesaId = "Select * from TiposDespesa where Id = @Id";
        private string InsertDespesas = "INSERT INTO Despesas (EstaPago,Vencimento,TipoDespesaId,ContaId,Valor,CreateTime,CreateBy,Obsoleto,Descricao,Comprovante) VALUES(@EstaPago,@Vencimento,@TipoDespesaId,@ContaId,@Valor,@CreateTime,@CreateBy,@Obsoleto,@Descricao,@Comprovante)";
        private string SelectContasAtivas = "SELECT * Contas where obsoleto = false";
        private string SelectContasAtivasDropDownList = "SELECT Id,Nome from Contas where Obsoleto = 'FALSE'";
        private string SelectTipoDespesaAtivasDropDownList = "SELECT Id,Nome from TiposDespesa where Obsoleto = 'FALSE'";
        private string SelectTodasDespesasViewModel = @"select D.Id as DespesaId,D.EstaPago,D.Vencimento,T.Nome as TipoDespesaNome, D.Valor from Despesas as D
                                                        join TiposDespesa as T on T.Id = D.TipoDespesaId
                                                        join Contas as C on C.Id = D.ContaId";
        private string SelectDespesaViewModelPorId = @"select * from Despesas where Id = @Id";

        private string UpdateDespesa = @"UPDATE Despesas SET EstaPago = @EstaPago,SET Vencimento =@Vencimento, SET TipoDespesaId =@TipoDespesaId, SET ContaId = @ContaId, SET Valor = @Valor, SET Descricao =@Descricao, SET Atualizacao =@Atualizacao, where Despesas.Id = @Id";
        private string SelectComprovanteById = @"select TipoArquivo.Nome as Tipo, Arquivos.bytes as Bytes from Arquivos
                                                  join TipoArquivo on TipoArquivo.Id = TipoArquivo
                                                  where Arquivos.Id = @Id";

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

        internal void UpdateDespesas(DespesaViewModel despesa)
        {
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(StringConnection))
                    using (SqlCommand cmd = new SqlCommand(UpdateDespesa, cn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@EstaPago", true));
                        cmd.Parameters.Add(new SqlParameter("@Vencimento", despesa.Vencimento));
                        cmd.Parameters.Add(new SqlParameter("@TipoDespesaId", Convert.ToInt32(despesa.TipoDespesa)));
                        cmd.Parameters.Add(new SqlParameter("@ContaId", Convert.ToInt32(despesa.Conta)));
                        cmd.Parameters.Add(new SqlParameter("@Valor", Convert.ToDecimal(despesa.Valor)));
                        cmd.Parameters.Add(new SqlParameter("@Atualizacao", DateTime.Now));
                        cmd.Parameters.Add(new SqlParameter("@Id", despesa.Id));

                        //  cmd.Parameters.Add(new SqlParameter("@CreateTime", DateTime.Now));
                        //   cmd.Parameters.Add(new SqlParameter("@CreateBy", despesa));
                        //cmd.Parameters.Add(new SqlParameter("@Obsoleto", false));
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

        internal void InserirComprovante(Comprovante comprovante, string currentUserId)
        {
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(StringConnection))
                    using (SqlCommand cmd = new SqlCommand(InsertComprovante, cn))
                    {
                        //  @Nome,@String64,@TipoArquivo,@CreateTime,@CreateBy
                        cmd.Parameters.Add(new SqlParameter("@Nome", comprovante.NomeArquito));
                        cmd.Parameters.Add(new SqlParameter("@bytes", comprovante.Bytes));
                        cmd.Parameters.Add(new SqlParameter("@TipoArquivo",Convert.ToInt32(comprovante.Type)));
                        cmd.Parameters.Add(new SqlParameter("@CreateTime", DateTime.Now));
                        cmd.Parameters.Add(new SqlParameter("@CreateBy", currentUserId));
                       
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

        internal DespesaViewModel PegarDespesaViewModelPorId(int id)
        {
            List<DespesaViewModel> despesas = new List<DespesaViewModel>();
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    //   private string InsertConta = "INSERT INTO Contas (Nome,Saldo,CreateTime,CreateBy,Obsoleto) VALUES(@Nome,@Saldo,@CreateTime,@CreateBy,@Obsoleto)";
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(SelectDespesaViewModelPorId, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", id));

                        //   cn.Open();
                        var dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            Conta conta = new Conta();

                            Financeiro.ViewModels.DespesaViewModel Despesa = new Financeiro.ViewModels.DespesaViewModel();

                            Despesa.Id = Convert.ToInt32(dataReader["Id"]);
                            Despesa.EstaPago = dataReader["EstaPago"].ToString() == "TRUE" ? true : false;
                            Despesa.Vencimento = DateTime.Parse(dataReader["Vencimento"].ToString());
                            Despesa.TipoDespesa = dataReader["TipoDespesaId"].ToString();
                            Despesa.Descricao = dataReader["Descricao"].ToString();
                            Despesa.Valor = Convert.ToDouble(dataReader["Valor"]);
                            Despesa.Conta = dataReader["ContaId"].ToString();

                            despesas.Add(Despesa);

                        }

                        dataReader.Close();
                        cmd.Dispose();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return despesas.FirstOrDefault();
        }

        internal void CriarNovaDespesa(DespesaViewModel novaDespesa, string currentUserId)
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
                        cmd.Parameters.Add(new SqlParameter("@Descricao", novaDespesa.Descricao));
                        cmd.Parameters.Add(new SqlParameter("@ContaId", Convert.ToInt32(novaDespesa.Conta)));
                        cmd.Parameters.Add(new SqlParameter("@Valor", Convert.ToDecimal(novaDespesa.Valor)));
                        cmd.Parameters.Add(new SqlParameter("@CreateTime", DateTime.Now));
                        cmd.Parameters.Add(new SqlParameter("@CreateBy", currentUserId));
                        cmd.Parameters.Add(new SqlParameter("@Obsoleto", false));
                        cmd.Parameters.Add(new SqlParameter("@Comprovante", Convert.ToInt32(novaDespesa.ComprovanteId)));
                       
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

        public List<TipoDespesa> PegarTipoDespesaPorId(int id)
        {
            List<TipoDespesa> tipoDespesas = new List<TipoDespesa>();

            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(StringConnection))
                    using (SqlCommand cmd = new SqlCommand(SelectTipoDespesaId, cn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", id));

                        cn.Open();
                        var dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            TipoDespesa tipoDespesa = new TipoDespesa();

                            tipoDespesa.Id = Convert.ToInt32(dataReader["Id"]);
                            tipoDespesa.Nome = dataReader["Nome"].ToString();
                            tipoDespesas.Add(tipoDespesa);


                        }

                        cn.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return tipoDespesas;

        }
        public List<Conta> PegarContaPorId(int id)
        {
            List<Conta> contas = new List<Conta>();
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(StringConnection))
                    using (SqlCommand cmd = new SqlCommand(SelectContaPorId, cn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", id));

                        cn.Open();
                        var dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            Conta conta = new Conta();

                            conta.Id = Convert.ToInt32(dataReader["Id"]);
                            conta.Nome = dataReader["Nome"].ToString();
                            conta.SaldoAtual = Convert.ToDouble(dataReader["Saldo"]);
                            contas.Add(conta);

                        }

                        cn.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return contas;

        }
        internal List<Financeiro.ViewModels.DespesaViewModel> PegarTodasDespesasPartialView()
        {
            List<Financeiro.ViewModels.DespesaViewModel> despesas = new List<Financeiro.ViewModels.DespesaViewModel>();
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand(SelectTodasDespesasViewModel, connection);
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Financeiro.ViewModels.DespesaViewModel Despesa = new Financeiro.ViewModels.DespesaViewModel();

                        Despesa.Id = Convert.ToInt32(dataReader["DespesaId"]);
                        Despesa.EstaPago = dataReader["EstaPago"].ToString() =="TRUE" ? true : false;
                        Despesa.Vencimento = DateTime.Parse(dataReader["Vencimento"].ToString());
                        Despesa.TipoDespesa = dataReader["TipoDespesaNome"].ToString();
                        Despesa.Valor = Convert.ToDouble(dataReader["Valor"]);

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
        private List<Conta> PegarContaPorId(SqlConnection cn, int id)
        {
            List<Conta> contas = new List<Conta>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(SelectContaPorId, cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Id", id));

                 //   cn.Open();
                    var dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Conta conta = new Conta();

                        conta.Id = Convert.ToInt32(dataReader["Id"]);
                        conta.Nome = dataReader["Nome"].ToString();
                        conta.SaldoAtual = Convert.ToDouble(dataReader["Saldo"]);
                        contas.Add(conta);

                    }

                   // cn.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return contas;
        }

        private List<TipoDespesa> PegarTipoDespesaPorId(SqlConnection cn, int id)
        {
            List<TipoDespesa> tipoDespesas = new List<TipoDespesa>();

                try
                {

                    using (SqlCommand cmd = new SqlCommand(SelectTipoDespesaId, cn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", id));

                      //  cn.Open();
                        var dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            TipoDespesa tipoDespesa = new TipoDespesa();

                            tipoDespesa.Id = Convert.ToInt32(dataReader["Id"]);
                            tipoDespesa.Nome = dataReader["Nome"].ToString();
                            tipoDespesas.Add(tipoDespesa);
                        
                        }

                       // cn.Close();
                    }
                }
                catch (Exception ex)
                {

                }
           
            return tipoDespesas;
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
        public void CriarTipoDespesa(Financeiro.Models.TipoDespesa tipoDespesa, string currentUserID)
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
        public List<string> PegarUltimoIdArquivos()
        {
            List<string> ids = new List<string>();
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(StringConnection))
                    using (SqlCommand cmd = new SqlCommand(SelectMaxArquivoId, cn))
                    {
                        cn.Open();
                        var dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            var Id = dataReader["Id"];

                            ids.Add(Id.ToString());
                        }

                        cn.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return ids;
          
        }
        internal Comprovante PegarComprovanteporId(int id)
        {
            List<Comprovante> comprovantes = new List<Comprovante>();
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                try
                {
                    //   private string InsertConta = "INSERT INTO Contas (Nome,Saldo,CreateTime,CreateBy,Obsoleto) VALUES(@Nome,@Saldo,@CreateTime,@CreateBy,@Obsoleto)";
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(SelectComprovanteById, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", id));

                        //   cn.Open();
                        var dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            Comprovante comprovante = new Comprovante();
                            
                            comprovante.ContentType = dataReader["Tipo"].ToString();
                            comprovante.Bytes = (byte[])dataReader["Bytes"];
                            
                            comprovantes.Add(comprovante);

                        }

                        dataReader.Close();
                        cmd.Dispose();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return comprovantes.FirstOrDefault();
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