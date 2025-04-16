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
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);  // Usa  a string de conexão para acessar com os parametros
                
                // Insere os valores de formulario dentro do banco de dados
                string sql = "INSERT INTO contato (nome, email, telefone)" + "VALUES" + "(' " + txtNome.Text + " ' , ' " + txtEmail.Text + " ' , ' " + txtTelefone.Text + " ')";

               
                MySqlCommand INSERT = new MySqlCommand(sql, Conexao);
                Conexao.Open(); // Abre a Conexão

                INSERT.ExecuteReader();

                MessageBox.Show("Dados inseridos com Sucesso !!!");
            } catch(Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
        }
            

       

       
    }
}
