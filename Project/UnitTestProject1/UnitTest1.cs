using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project;
using System.Windows.Forms;

namespace UnitTestProject1
{
    [TestClass]
    public class LaboratoryStaffTest
    {
        [TestMethod]
        public void TestConnect()
        {
            LaboratoryStaff Lb = new LaboratoryStaff();
            Lb.Show();
            Assert.IsTrue(Lb.dataGridView1.Rows.Count > 0);
        }

        [TestMethod]
        public void TestButtonAdd()
        {
            LaboratoryStaff Lb = new LaboratoryStaff();
            Lb.Show();
            try
            {
                Lb.buttonAdd.PerformClick();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }

    [TestClass]
    public class AddStaffTest
    {
        [TestMethod]
        public void TestAddStaff()
        {
            AddStaff Adds = new AddStaff();
            Adds.Show();
            Adds.family.Text = "Иванов";
            Adds.name.Text = "Иван";
            Adds.surname.Text = "Иванович";
            Adds.gender.SelectedItem = "Мужской(M)";
            Adds.dateTimePicker1.Value = DateTime.Now;
            Adds.id_marital_status.SelectedItem = "Женат";
            Adds.having_child.SelectedItem = "Нет";
            Adds.id_position.SelectedItem = "Научный сотрудник";
            Adds.id_academic_degree.SelectedItem = "Без ученой степени";

            Adds.buttonSave.PerformClick();

            LaboratoryStaff Lb = new LaboratoryStaff();
            Lb.Show();
            foreach (DataGridViewRow item in Lb.dataGridView1.Rows)
            {
                if (item.Cells["Фамилия"].Value.ToString().Equals("Иванов") && item.Cells["Имя"].Value.ToString().Equals("Иван") && item.Cells["Отчество"].Value.ToString().Equals("Иванович") && item.Cells["Должность"].Value.ToString().Equals("Научный сотрудник") && item.Cells["Ученая степень"].Value.ToString().Equals("б/с"))
                {
                    return;
                }
            }
            Assert.Fail();
        }
        [TestMethod]
        public void TestButtonBack()
        {
            AddStaff Adds = new AddStaff();
            Adds.Show();
            try
            {
                Adds.buttonBack.PerformClick();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
    [TestClass]
    public class EmployeeCardTest
    {
        int id;

        [TestMethod]
        public void TestButtonRed()
        {
            EmployeeСard Emp = new EmployeeСard(id);
            Emp.Show();
            try
            {
                Emp.red.PerformClick();
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestButtonBack()
        {
            EmployeeСard Emp = new EmployeeСard(id);
            Emp.Show();
            try
            {
                Emp.buttonBack.PerformClick();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
    [TestClass]
    public class RedTest
    {

        [TestMethod]
        public void TestRedStaff()
        {
            LaboratoryStaff Lb = new LaboratoryStaff();
            Lb.Show();
            DataGridViewRow item = Lb.dataGridView1.Rows[0];
            
            int id = int.Parse(item.Cells["id"].Value.ToString());
            Lb.Hide();

            EditingData red = new EditingData(id);
            red.Show();
            Assert.AreEqual(red.family.Text, item.Cells["Фамилия"].Value.ToString());
            Assert.AreEqual(red.name.Text, item.Cells["Имя"].Value.ToString());
            Assert.AreEqual(red.surname.Text, item.Cells["Отчество"].Value.ToString());
            Assert.AreEqual(red.position.Text, item.Cells["Должность"].Value.ToString());
            

            red.family.Text = "Петров";
            red.name.Text = "Игорь";

            red.buttonSave.PerformClick();

            Lb.Dispose();
            Lb = new LaboratoryStaff();
            Lb.Show();
            foreach (DataGridViewRow item1 in Lb.dataGridView1.Rows)
            {
                if (item1.Cells["Фамилия"].Value.ToString().Equals("Петров"))
                { 
                    return;
                }
            }
            Assert.Fail();


        }
        [TestMethod]
        public void TestButtonDel()
        {
            LaboratoryStaff Lb = new LaboratoryStaff();
            Lb.Show();
            DataGridViewRow item = Lb.dataGridView1.Rows[0];

            int id = int.Parse(item.Cells["id"].Value.ToString());
            Lb.Hide();

            EditingData red = new EditingData(id);
            red.Show();

            red.button1.PerformClick();

            Lb.Dispose();
            Lb = new LaboratoryStaff();
            Lb.Show();
            foreach (DataGridViewRow item1 in Lb.dataGridView1.Rows)
            {
                if (item1.Cells["id"].Value.ToString().Equals(id.ToString()))
                {
                    Assert.Fail();
                }
            }
        }
    }
}