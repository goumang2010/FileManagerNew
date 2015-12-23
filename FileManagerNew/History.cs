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
    public partial class History : Form
    {
        string selectedHis="";
       public MainPage f1;
       List<string> lists = new List<string>();
        public History()
        {

            InitializeComponent();
           lists= Properties.Settings.Default.history.Split(new Char[] { '\t' }).ToList();
           listBox1.DataSource = lists;
         textBox1.Focus();
            
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            { 
            selectedHis=listBox1.SelectedItem.ToString();
        }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            f1.inputValue = selectedHis;
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.SelectedIndex=lists.FindIndex(delegate(string bk)
            {
                string abc=textBox1.Text;
                return bk.Contains(abc);
            });

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                f1.inputValue = selectedHis;
                this.Hide();
            }
        }
    }
}
