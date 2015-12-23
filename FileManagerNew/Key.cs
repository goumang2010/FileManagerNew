using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagerNew
{
    public partial class Key : Form
    {
        public MainPage f1;
        bool isExist = false;
        public string ftpURI;
        //显示的状态
        //public int viewState;
        int i=0;
        List<string> url=new List<string>();
        List<string> username = new List<string>();
        List<string> password = new List<string>();
        public Key()
        {
            InitializeComponent();
            if(Properties.Settings.Default.ftpname!="")
            { 
           url = Properties.Settings.Default.ftpname.Split(new Char[] { '\t' }).ToList();
            username = Properties.Settings.Default.ftpuser.Split(new Char[] { '\t' }).ToList();
             password = Properties.Settings.Default.ftppassword.Split(new Char[] { '\t' }).ToList();

               
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
           // string ftpURI = "ftp://218.61.139.21/";
            
            //string ftpUserID = "saciftp";
            //string ftpPassword = "saciftp_C1107";
            string ftpUserID = textBox1.Text;
            string ftpPassword = textBox2.Text;
           
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
              //  string line = reader.ReadLine();
                string line = reader.ReadToEnd();
                /*while (line != null)
                {
                    result.Append(line);
                    result.Append("\r\n");
                    line = reader.ReadLine();
                }
                 * */
                //result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
               // return result.ToString().Split('\n');
                //MainPage f = new MainPage();
                f1.Form2Value = line;
                f1.Show(); //显示Form2
                this.Hide(); //隐藏Form1
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        
         if(isExist==true)
            {
                username[i] = textBox1.Text;
                password[i] = textBox2.Text;
            }
            else
            {

               // url.Insert(i, ftpURI);
                username.Insert(i, textBox1.Text);
                password.Insert(i, textBox2.Text);

             Properties.Settings.Default.ftpname += ftpURI + "\t";
            }

         int n = 0;
         Properties.Settings.Default.ftpuser = "";
         Properties.Settings.Default.ftppassword = "";
         while (n < username.Count())
         {
             Properties.Settings.Default.ftpuser += username[n] + "\t";
             Properties.Settings.Default.ftppassword += password[n] + "\t";
             n = n + 1;
         }

            Properties.Settings.Default.Save();
        }

        private void Key_Activated(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void Key_Load(object sender, EventArgs e)
        {
            i = 0;
            while (i < url.Count())
            {
                if (ifContain(url[i]))
                {
                    isExist = true;
                    textBox1.Text = username[i];
                    textBox2.Text = password[i];
                    break;
                }
                i = i + 1;

            }
        }

        private bool ifContain(string p)
        {
            if (ftpURI.Length < p.Length)
                return p.Contains(ftpURI);
            else
                return ftpURI.Contains(p);
        }
    }
}
