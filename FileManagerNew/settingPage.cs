using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagerNew
{
    public partial class settingPage : Form
    {
        public settingPage()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        /*    Properties.Settings.Default.Background = checkBox1.Checked;
            Properties.Settings.Default.sort = radioButton1.Checked;
            Properties.Settings.Default.sort2 = radioButton2.Checked;
            Properties.Settings.Default.sort3 = radioButton3.Checked;
            Properties.Settings.Default.combo = comboBox1.Text;
           */
            Properties.Settings.Default.Save();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Properties.Settings.Default.Reset();
        }


    }
}
