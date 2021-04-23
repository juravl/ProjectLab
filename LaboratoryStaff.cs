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

        Dictionary<string, int> position = new Dictionary<string, int>();

        Dictionary<string, int> academic_degree = new Dictionary<string, int>();

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

            DataBase dataBase = new DataBase();

            dataBase.openConnetion();

            string query = "SELECT family AS 'Фамилия', name AS 'Имя', surname AS 'Отчество', p.name_position AS 'Должность', ad.small_name AS 'Ученая степень' FROM employees join position p ON employees.id_position = p.id_position JOIN academic_degree ad ON employees.id_academic_degree = ad.id_academic_degree;";
            MySqlDataAdapter adpt = new MySqlDataAdapter(query,connection);

            DataSet dset = new DataSet();

            adpt.Fill(dset);

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Фамилия";
            dataGridView1.Columns[1].Name = "Имя";
            dataGridView1.Columns[2].Name = "Отчество";
            dataGridView1.Columns[3].Name = "Должность";
            dataGridView1.Columns[4].Name = "Ученая степень";
            foreach (DataRow item in dset.Tables[0].Rows)
            {
                string[] buf = {item["Фамилия"].ToString(), item["Имя"].ToString(), item["Отчество"].ToString(), item["Должность"].ToString(), item["Ученая степень"].ToString() };
                dataGridView1.Rows.Add(buf);
            }

            string query1 = "SELECT name_position, id_position FROM position ORDER BY id_position";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query1, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);
            foreach (DataRow item in data.Tables[0].Rows)
            {
                this.position.Add(item["name_position"].ToString(), int.Parse(item["id_position"].ToString()));
            }

            string query2 = "SELECT small_name, id_academic_degree FROM academic_degree ORDER BY id_academic_degree";
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(query2, connection);
            DataSet data2 = new DataSet();
            adapter2.Fill(data2);
            foreach (DataRow item in data2.Tables[0].Rows)
            {
                this.academic_degree.Add(item["small_name"].ToString(), int.Parse(item["id_academic_degree"].ToString()));
            }
            dataBase.closeConnetion();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddStaff addStaff = new AddStaff();
            addStaff.Show();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Hide();
            EmployeeСard EmpCard = new EmployeeСard();
            EmpCard.Show();
        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {

            if (e.Column.Name == "Должность")
            {
                int a = position[e.CellValue1.ToString()];
                int b = position[e.CellValue2.ToString()];
                e.SortResult = a.CompareTo(b);
            }
            else
            {
                e.SortResult = System.String.Compare(
                e.CellValue1.ToString(), e.CellValue2.ToString());
            }

            if (e.Column.Name == "Ученая степень")
            {
                int c = academic_degree[e.CellValue1.ToString()];
                int d = academic_degree[e.CellValue2.ToString()];
                e.SortResult = c.CompareTo(d);
            }
            else
            {
                e.SortResult = System.String.Compare(
                e.CellValue1.ToString(), e.CellValue2.ToString());
            }
            e.Handled = true;
        }
    }
}
