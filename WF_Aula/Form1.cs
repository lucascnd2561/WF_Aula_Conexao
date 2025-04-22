using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WF_Aula
{
    public partial class Form1: Form
    {
        MySqlConnection Conexao; // abre a conexão com o banco de dados
        private string data_source = "datasource=localhost;username=root;password=;database=AULAS_UC3";         // string  de 
        public Form1()
        {
            InitializeComponent();

            // configurações
            lstContatos.View = View.Details;
            lstContatos.LabelEdit = true;
            lstContatos.AllowColumnReorder = true;
            lstContatos.FullRowSelect = true;
            lstContatos.GridLines = true;


            // Posição dos Cabeçalhos a serem exibidos na tela
            lstContatos.Columns.Add("ID", 30, HorizontalAlignment.Left);
            lstContatos.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            lstContatos.Columns.Add("Email", 150, HorizontalAlignment.Left);
            lstContatos.Columns.Add("Telefone", 150, HorizontalAlignment.Left);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);  // Usa  a string de conexão para acessar com os parametros
                Conexao.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                // SQL que desejamos executar - marcadores
                cmd.CommandText =
                    "INSERT INTO contato " +
                    "(nome, email, telefone) " +
                    "VALUES" +
                    "(@nome, @email, @telefone)";

                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);

                cmd.ExecuteNonQuery();
                // Insere os valores de formulario dentro do banco de dados
                //string sql = "INSERT INTO contato (nome, email, telefone)" + "VALUES" + "(' " + txtNome.Text + " ' , ' " + txtEmail.Text + " ' , ' " + txtTelefone.Text + " ')";


                //MySqlCommand INSERT = new MySqlCommand(sql, Conexao);
                //Conexao.Open(); // Abre a Conexão

                //INSERT.ExecuteReader();

                MessageBox.Show("Dados inseridos com Sucesso !!!");
            } catch(Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                string q = "%" + txtBuscar.Text + "%";

                // Criar a conexão com o MySQL
                Conexao = new MySqlConnection(data_source);

                string sql = "SELECT * FROM contato WHERE nome LIKE @q OR email LIKE @q";


                Conexao.Open();

                // Buscar as informações
                MySqlCommand buscar = new MySqlCommand(sql, Conexao);
                buscar.Parameters.AddWithValue("@q", q);

                // armazena as informacoes que temos na busca para mostrar na tela
                MySqlDataReader reader = buscar.ExecuteReader();

                // lista vazia 
                lstContatos.Items.Clear();


                while (reader.Read())
                {
                    // vetor
                    string[] linha =
                    {
                        // obtendo as informações do banco de dados (vetor de strings)
                        reader.GetInt32(0).ToString(), // id
                        reader.GetString(1),           // nome
                        reader.GetString(2),           // email
                        reader.GetString(3),           // telefone
                    };

                    var linha_list_view = new ListViewItem(linha);
                    lstContatos.Items.Add(linha_list_view);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void lstContatos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    
}
