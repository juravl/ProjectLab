using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class EmployeeСard : Form
    {
        public EmployeeСard()
        {
            InitializeComponent();
        }

        private void EmployeeСard_Load(object sender, EventArgs e)
        {

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            LaboratoryStaff labStaff = new LaboratoryStaff();
            labStaff.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void position_Click(object sender, EventArgs e)
        {

        }

        private void red_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditingData eData = new EditingData();
            eData.Show();
        }
    }
}
