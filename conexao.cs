using System;
using System.Data;
using System.Data.SqlClient;

namespace GestaoVendasApp
{
    public class Database
    {
        private string connectionString = "Data Source=.;Initial Catalog=GestaoVendas;Integrated Security=True";

        public void InserirProduto(string nome, decimal preco, int quantidade)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InserirProduto", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Preco", preco);
                cmd.Parameters.AddWithValue("@Quantidade", quantidade);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RegistrarVenda(int produtoId, int quantidadeVendida, DateTime data)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarVenda", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                cmd.Parameters.AddWithValue("@QuantidadeVendida", quantidadeVendida);
                cmd.Parameters.AddWithValue("@Data", data);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable RelatorioVendas()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_RelatorioVendas", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable RelatorioEstoque()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_RelatorioEstoque", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
