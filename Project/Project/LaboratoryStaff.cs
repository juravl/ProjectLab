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
        //Подключение к базе данных
        MySqlConnection connection = new MySqlConnection("server = localhost; port = 3306; username = root; password = root; database = project");

        //Создание пары ключ-атрибут
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

        private void LoadData()
        {

            DataBase dataBase = new DataBase();

            //Подключаемся к базе данных
            dataBase.openConnetion();

            //Запрос в базу данных на вывод сотрудников
            string query = "SELECT id, family AS 'Фамилия'," +
                " name AS 'Имя', surname AS 'Отчество', " +
                "p.name_position AS 'Должность', ad.small_name AS 'Ученая степень' " +
                "FROM employees join position p ON employees.id_position = p.id_position " +
                "JOIN academic_degree ad ON employees.id_academic_degree = ad.id_academic_degree;";

            //Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления базы данных
            MySqlDataAdapter adpt = new MySqlDataAdapter(query,connection);

            DataSet dset = new DataSet();

            adpt.Fill(dset);

            //Создание столбцов
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "id";
            dataGridView1.Columns[1].Name = "Фамилия";
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Name = "Имя";
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Name = "Отчество";
            dataGridView1.Columns[3].Width = 180;
            dataGridView1.Columns[4].Name = "Должность";
            dataGridView1.Columns[4].Width = 430;
            dataGridView1.Columns[5].Name = "Ученая степень";
            dataGridView1.Columns[5].Width = 150;
            //Заполнение столбцов
            foreach (DataRow item in dset.Tables[0].Rows)
            {
                string[] buf = {item ["id"].ToString(), item["Фамилия"].ToString(), item["Имя"].ToString(), item["Отчество"].ToString(), item["Должность"].ToString(), item["Ученая степень"].ToString() };
                dataGridView1.Rows.Add(buf);
            }
            
            //Создание запроса на сортировку по столбцу "Должность"
            string query1 = "SELECT name_position, id_position FROM position ORDER BY id_position";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query1, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);
            foreach (DataRow item in data.Tables[0].Rows)
            {
                this.position.Add(item["name_position"].ToString(), int.Parse(item["id_position"].ToString()));
            }

            //Создание запроса на сортировку по столбцу "Ученая степень"
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

        //Переход на форму Личной карточки сотрудника
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Hide();
            int id = int.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
            EmployeeСard EmpCard = new EmployeeСard(id);
            EmpCard.ShowDialog();
        }

        //Создание сортировки по столбцам
        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {

            if (e.Column.Name == "Должность")
            {
                int a = position[e.CellValue1.ToString()];
                int b = position[e.CellValue2.ToString()];
                e.SortResult = a.CompareTo(b);
            }
            else if (e.Column.Name == "Ученая степень")
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
