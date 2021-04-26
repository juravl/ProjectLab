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
    public partial class EmployeeСard : Form
    {
        //Подключение к базе данных
        MySqlConnection connection = new MySqlConnection("server = localhost; port = 3306; username = root; password = root; database = project");

        int id;

        public EmployeeСard(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void EmployeeСard_Load(object sender, EventArgs e)
        {
            //Запрос на извлечение данных по id
            string query1 = String.Format("SELECT id, family , " +
                    "name , surname , gender, birthday, ms.name_marital_sratus, having_child, p.name_position , " +
                    "ad.name_academic_degree " +
                    "FROM employees " +
                    "JOIN marital_sratus ms ON employees.id_marital_status = ms.id_status " +
                    "JOIN position p ON employees.id_position = p.id_position " +
                    "JOIN academic_degree ad ON employees.id_academic_degree = ad.id_academic_degree where id={0} limit 1", id);

            MySqlDataAdapter adapter = new MySqlDataAdapter(query1, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);
            DataRow item = data.Tables[0].Rows[0];

            //Заполнение данными полей
            family.Text = item["family"].ToString();
            name.Text = item["name"].ToString();
            surname.Text = item["surname"].ToString();
            gender.Text = item["gender"].ToString();
            dateTimePicker1.Text = item["birthday"].ToString();
            status.Text = item["name_marital_sratus"].ToString();
            having_child.Text = item["having_child"].ToString();
            position.Text = item["name_position"].ToString();
            academic_degree.Text = item["name_academic_degree"].ToString();

            //Обработка данных
            if (gender.Text == "M")
            {
                gender.Text = "Mужской(M)";
            }

            else 
            {
                gender.Text = "Женский(F)";
            }

            if (having_child.Text == "True")
            {
                having_child.Text = "Есть дети";
            }

            else
            {
                having_child.Text = "Детей нет";
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            //Переход на форму списка сотрудников
            this.Hide();
            LaboratoryStaff labStaff = new LaboratoryStaff();
            labStaff.Show();
        }

        private void red_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditingData eData = new EditingData(id);
            eData.Show();
        }
    }
}
