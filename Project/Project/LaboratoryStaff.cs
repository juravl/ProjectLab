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

namespace Project
{
    public partial class LaboratoryStaff : Form
    {
        MySqlConnection connection = new MySqlConnection("server = localhost; port = 3306; username = root; password = root; database = project");

        public void openConnetion()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnetion()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }

        public LaboratoryStaff()
        {
            InitializeComponent();
            LoadData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadData()
        {

            MySqlConnection connection = new MySqlConnection("server = localhost; port = 3306; username = root; password = root; database = project");

            openConnetion();

            string query = "SELECT id, family, name, surname FROM employees";

            MySqlDataAdapter adpt = new MySqlDataAdapter(query, connection);

            DataSet dset = new DataSet();

            adpt.Fill(dset);

            dataGridView1.DataSource = dset.Tables[0];

            closeConnetion();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddStaff addStaff = new AddStaff();
            addStaff.Show();
        }
    }
}
