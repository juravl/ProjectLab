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
    public partial class EditingData : Form
    {
        //Подключение к базе данных
        MySqlConnection connection = new MySqlConnection("server = localhost; port = 3306; username = root; password = root; database = project");

        int id;

        //Создание пары ключ-атрибут
        private Dictionary<string, string> _famaliStatis = new Dictionary<string, string>();
        private Dictionary<string, string> _positionName = new Dictionary<string, string>();
        private Dictionary<string, string> _degreeName = new Dictionary<string, string>();

        public EditingData(int id)
        {
            this.id = id;
            InitializeComponent();

            //Класс представляет таблицу в памяти
            DataBase dataBase = new DataBase();
            DataTable linkcat = new DataTable("linkcat");
            DataTable linkcat1 = new DataTable("linkcat1");
            DataTable linkcat2 = new DataTable("linkcat2");
            dataBase.openConnetion();

            //Запрос на извлечение семейного положения из базы данных
            using (MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM marital_sratus", connection))
            {
                da.Fill(linkcat);
            }
            foreach (DataRow da in linkcat.Rows)
            {
                _famaliStatis.Add(da[1].ToString(), da[0].ToString());
                status.Items.Add(da[1].ToString());
            }

            //Запрос на извлечение списка должностей из базы данных
            using (MySqlDataAdapter da1 = new MySqlDataAdapter("SELECT * FROM position", connection))
            {
                da1.Fill(linkcat1);
            }
            foreach (DataRow da1 in linkcat1.Rows)
            {
                _positionName.Add(da1[1].ToString(), da1[0].ToString());
                position.Items.Add(da1[1].ToString());
            }

            //Запрос на извлечение списка ученых степеней
            using (MySqlDataAdapter da2 = new MySqlDataAdapter("SELECT * FROM academic_degree", connection))
            {
                da2.Fill(linkcat2);
            }
            foreach (DataRow da2 in linkcat2.Rows)
            {
                _degreeName.Add(da2[1].ToString(), da2[0].ToString());
                academic_degree.Items.Add(da2[1].ToString());
            }
            dataBase.closeConnetion();

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            //Переход на форму личной карточки сотрудника
            this.Hide();
            EmployeeСard EmpC = new EmployeeСard(id);
            EmpC.Show();
        }

        private void EditingData_Load(object sender, EventArgs e)
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
                having_child.Text = "Есть";
            }

            else
            {
                having_child.Text = "Нет";
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //При нажатии на кнопку сохранение, происходит проверка на заполненность полей и на корректность
            if (family.Text == "")
            {
                MessageBox.Show("Введите фамилию");
                return;
            }
            if (!Regex.Match(family.Text, @"[А-яёЁ]").Success)
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
            if (status.Text == "")
            {
                MessageBox.Show("Укажите семейное положение");
                return;
            }
            if (having_child.Text == "")
            {
                MessageBox.Show("Укажите наличие детей");
                return;
            }
            if (position.Text == "")
            {
                MessageBox.Show("Укажите должность сотрудника");
                return;
            }
            if (academic_degree.Text == "")
            {
                MessageBox.Show("Укажите учёную степень сотрудника");
                return;
            }

            DataBase dataBase = new DataBase();

            //Запрос на обновление данных в базе по id
            string queryUp = String.Format("UPDATE `employees` SET `family` = @family,`name` = @name, `surname` = @surname, `gender` = @gender, `birthday` = @birthday, `id_marital_status` = @id_marital_status, `having_child` = @having_child, `id_position` = @id_position, `id_academic_degree`= @id_academic_degree WHERE id={0} limit 1", id);
            MySqlCommand commandUp = new MySqlCommand(queryUp, dataBase.getConnection());

            //Добавление данных с формы
            commandUp.Parameters.Add("@family", MySqlDbType.VarChar).Value = family.Text;
            commandUp.Parameters.Add("@name", MySqlDbType.VarChar).Value = name.Text;
            commandUp.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname.Text;
            commandUp.Parameters.Add("@birthday", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;
            commandUp.Parameters.Add("@id_marital_status", MySqlDbType.Int32).Value = _famaliStatis[status.SelectedItem.ToString()];
            commandUp.Parameters.Add("@id_position", MySqlDbType.Int32).Value = _positionName[position.SelectedItem.ToString()];
            commandUp.Parameters.Add("@id_academic_degree", MySqlDbType.Int32).Value = _degreeName[academic_degree.SelectedItem.ToString()];

            //Обработка вводимых данных
            if (gender.GetItemText(gender.SelectedItem) == "Мужской(M)")
            {
                commandUp.Parameters.Add("@gender", MySqlDbType.Text).Value = "M";
            }

            if (gender.GetItemText(gender.SelectedItem) == "Женский(F)")
            {
                commandUp.Parameters.Add("@gender", MySqlDbType.VarChar).Value = "F";
            }

            if (having_child.GetItemText(having_child.SelectedItem) == "Есть")
            {
                commandUp.Parameters.Add("@having_child", MySqlDbType.Byte).Value = "1";
            }
            if (having_child.GetItemText(having_child.SelectedItem) == "Нет")
            {
                commandUp.Parameters.Add("@having_child", MySqlDbType.Byte).Value = "0";
            }

            dataBase.openConnetion();

            if (commandUp.ExecuteNonQuery() == 1)
                MessageBox.Show("Данные успешно изменены");
            else
                MessageBox.Show("Данные не изменены");

            dataBase.closeConnetion();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Удаление сотрудника из базы данных
            DataBase dataBase = new DataBase();

            dataBase.openConnetion();
            string query3 = String.Format("DELETE FROM employees WHERE id={0} limit 1", id);
            MySqlCommand command2 = new MySqlCommand(query3,dataBase.getConnection());

            if (command2.ExecuteNonQuery() == 1)
                MessageBox.Show("Данные удалены");

            dataBase.closeConnetion();

            //После удаления переход на форму списка сотрудников
            this.Hide();
            LaboratoryStaff labStaff = new LaboratoryStaff();
            labStaff.Show();
        }
    }
}
