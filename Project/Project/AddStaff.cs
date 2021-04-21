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
using System.Text.RegularExpressions;

namespace Project
{
    public partial class AddStaff : Form
    {
        private Dictionary<string, string> _famaliStatis = new Dictionary<string, string>();
        private Dictionary<string, string> _positionName = new Dictionary<string, string>();
        private Dictionary<string, string> _degreeName = new Dictionary<string, string>();
        public AddStaff()
        {
            InitializeComponent();
            MySqlConnection connection = new MySqlConnection("server = localhost; port = 3306; username = root; password = root; database = project");
            DataBase dataBase = new DataBase();
            DataTable linkcat = new DataTable("linkcat");
            DataTable linkcat1 = new DataTable("linkcat1");
            DataTable linkcat2 = new DataTable("linkcat2");
            dataBase.openConnetion();

            using (MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM marital_sratus", connection))
            {
                da.Fill(linkcat);
            }
            foreach (DataRow da in linkcat.Rows)
            {
                _famaliStatis.Add(da[1].ToString(), da[0].ToString());
                id_marital_status.Items.Add(da[1].ToString());
            }

            using (MySqlDataAdapter da1 = new MySqlDataAdapter("SELECT * FROM position", connection))
            {
                da1.Fill(linkcat1);
            }
            foreach (DataRow da1 in linkcat1.Rows)
            {
                _positionName.Add(da1[1].ToString(), da1[0].ToString());
                id_position.Items.Add(da1[1].ToString());
            }

            using (MySqlDataAdapter da2 = new MySqlDataAdapter("SELECT * FROM academic_degree", connection))
            {
                da2.Fill(linkcat2);
            }
            foreach (DataRow da2 in linkcat2.Rows)
            {
                _degreeName.Add(da2[1].ToString(), da2[0].ToString());
                id_academic_degree.Items.Add(da2[1].ToString());
            }
            dataBase.closeConnetion();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            LaboratoryStaff labStaff = new LaboratoryStaff();
            labStaff.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(family.Text=="")
            {
                MessageBox.Show("Введите фамилию");
                return;
            }
            if(!Regex.Match(family.Text, @"[А-яёЁ]").Success)
            {
                MessageBox.Show("Допускаются только русские буквы");
                return;
            }
            if (name.Text == "")
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (!Regex.Match(name.Text, @"[А-яёЁ]").Success)
            {
                MessageBox.Show("Допускаются только русские буквы");
                return;
            }
            if (!Regex.Match(surname.Text, @"[А-яёЁ]").Success)
            {
                MessageBox.Show("Допускаются только русские буквы");
                return;
            }
            if (gender.Text == "")
            {
                MessageBox.Show("Выберите пол");
                return;
            }
            if (dateTimePicker1.Text == "")
            {
                MessageBox.Show("Укажите дату рождения");
                return;
            }
            if (id_marital_status.Text == "")
            {
                MessageBox.Show("Укажите семейное положение");
                return;
            }
            if (having_child.Text == "")
            {
                MessageBox.Show("Укажите наличие детей");
                return;
            }
            if (id_position.Text == "")
            {
                MessageBox.Show("Укажите должность сотрудника");
                return;
            }
            if (id_academic_degree.Text == "")
            {
                MessageBox.Show("Укажите учёную степень сотрудника");
                return;
            }

            DataBase dataBase = new DataBase();

            MySqlCommand command = new MySqlCommand("INSERT INTO `employees` (`family`, `name`, `surname`, `gender`, `birthday`, `id_marital_status`, `having_child`, `id_position`, `id_academic_degree`) VALUES (@family, @name, @surname, @gender, @birthday, @id_marital_status, @having_child, @id_position, @id_academic_degree)", dataBase.getConnection());

            command.Parameters.Add("@family", MySqlDbType.VarChar).Value = family.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname.Text;
            command.Parameters.Add("@birthday", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;
            command.Parameters.Add("@id_marital_status", MySqlDbType.Int32).Value = _famaliStatis[id_marital_status.SelectedItem.ToString()];
            command.Parameters.Add("@id_position", MySqlDbType.Int32).Value = _positionName[id_position.SelectedItem.ToString()];
            command.Parameters.Add("@id_academic_degree", MySqlDbType.Int32).Value = _degreeName[id_academic_degree.SelectedItem.ToString()];

            if (gender.GetItemText(gender.SelectedItem) == "Мужской(M)")
            {
                command.Parameters.Add("@gender", MySqlDbType.Text).Value = "M";
            }

            if (gender.GetItemText(gender.SelectedItem) == "Женский(F)")
            {
                command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = "F";
            }

            if (having_child.GetItemText(having_child.SelectedItem) == "Есть")
            {
                command.Parameters.Add("@having_child", MySqlDbType.Byte).Value = "1";
            }
            if (having_child.GetItemText(having_child.SelectedItem) == "Нет")
            {
                command.Parameters.Add("@having_child", MySqlDbType.Byte).Value = "0";
            }

            dataBase.openConnetion();

           if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Сотрудник успешно добавлен");
           else
                MessageBox.Show("Сотрудник не добавлен");

            dataBase.closeConnetion();

            family.Clear();
            name.Clear();
            surname.Clear();
            gender.SelectedItem = null;
            dateTimePicker1.Text = null;
            id_marital_status.SelectedItem = null;
            having_child.SelectedItem = null;
            id_position.SelectedItem = null;
            id_academic_degree.SelectedItem = null;

        }

        private void AddStaff_Load(object sender, EventArgs e)
        {

        }

        private void gender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}