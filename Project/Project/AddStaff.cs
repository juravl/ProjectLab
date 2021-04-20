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
        public AddStaff()
        {
            InitializeComponent();
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
            if (birthday.Text == "")
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
            command.Parameters.Add("@birthday", MySqlDbType.Date).Value = birthday.Text;
            command.Parameters.Add("@id_marital_status", MySqlDbType.Int32).Value = id_marital_status.Text;
            command.Parameters.Add("@having_child", MySqlDbType.Byte).Value = having_child.Text;
            command.Parameters.Add("@id_position", MySqlDbType.Int32).Value = id_position.Text;
            command.Parameters.Add("@id_academic_degree", MySqlDbType.Int32).Value = id_academic_degree.Text;

            if (gender.GetItemText(gender.SelectedItem) == "Мужской(M)")
            {
                command.Parameters.Add("@gender", MySqlDbType.Text).Value = "M";
            }

            if (gender.GetItemText(gender.SelectedItem) == "Женский(F)")
            {
                command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = "F";
            }

            dataBase.openConnetion();

           if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Сотрудник успешно добавлен");
           else
                MessageBox.Show("Сотрудник не добавлен");

            dataBase.closeConnetion();
        }

        private void AddStaff_Load(object sender, EventArgs e)
        {

        }

        private void gender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}