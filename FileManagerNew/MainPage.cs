using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Word;
using GoumangToolKit;
namespace FileManagerNew
{



    public partial class MainPage : Form
    {


      
        public List<FileInfo> AllFiles = new List<FileInfo>();
        public List<DirectoryInfo> AllFolers = new List<DirectoryInfo>();
        public List<FileInfo> Filterfile = new List<FileInfo>();
        public MainPage()
       {
            //每次启动 都是默认显示全部文件
           Properties.Settings.Default.combo = "All";
            InitializeComponent();
            
        }
    



        private void button1_Click(object sender, EventArgs e)
        {

           /* switch(comboBox1.Text)
            {
                case "FTP":
                richTextBox1.Text = ftpGet();
                break;
                case "本地目录":
                WalkDirectoryTree(inputPath.Text);
                break;
                    
            }
            * */
        }

        private void WalkDirectoryTree(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            AllFolers.Add(dir);
            //var files;
           //System.IO.DirectoryInfo[] subDirs = null;
           IOrderedEnumerable<FileInfo> files = null;
           
            string display = "";
            // First, process all the files directly under this folder
          
            if(comboBox1.SelectedIndex!=1)
                { 
            try
            {
                if(Properties.Settings.Default.sort==true)
                {
                    files = dir.GetFiles().OrderBy(i => i.Name);
                }
                else
                {
                    if (Properties.Settings.Default.sort2 == true)
                    { 
                    files = dir.GetFiles().OrderBy(i => i.LastWriteTime);
                    }
                    else
                    {
                        files = dir.GetFiles().OrderBy(i => i.CreationTime);
                    }
                }
             //   files = dir.GetFiles().OrderBy(i => i.LastWriteTime);
              //  
              //  
               // files.AsQueryable();


            }
                // This is thrown if even one of the files requires permissions greater
                // than the application provides.
                catch 
           // catch (UnauthorizedAccessException e)
            {
                MessageBox.Show("Wrong Path!");
            }


            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {


                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    display = display + fi.Name + "\r\n";
                        AllFiles.Add(fi);
                    //Allfile.add(path, fi.FullName, fi.Name, fi.Extension);
                    if (isExtension.Checked == false && fi.Extension != "")
                    {


                        display = display.Replace(fi.Extension, "");




                    }

                    //  Console.WriteLine(fi.FullName);
                }
            }
            }
                    // Now find all the subdirectories under this directory.
                if(comboBox1.SelectedIndex!=2)
                { 
                    try
                    { 
                var subDirs = dir.GetDirectories();
                   // display = display + "\r\n";
                    foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                    {
                        display = display + dirInfo.Name + "\r\n";
                        AllFolers.Add(dirInfo);
                        if (SubDictionary.Checked == true)
                        {
                           // dirInfo.FullName
                         WalkDirectoryTree(dirInfo.FullName);
                        }
                    }
                        }
                    catch
                    {

                    }



                }
                
            
            richTextBox1.Text += display;
        }


        public string Form2Value
        {
            get
            {
                return this.richTextBox1.Text;
            }
            set
            {
                if(comboBox1.SelectedIndex!=0)
                {

                    if (comboBox1.SelectedIndex == 1)
                    {
                        value = Regex.Replace(value, "\r\n.*\\..*\r\n", "\r\n");
                    }
                    else
                    {
                        string value1 = "";
                        foreach (Match match in Regex.Matches(value, "\r\n.*\\..*\r\n"))
                        {
                            value1 += match.Value.Substring(2, match.Value.Length-2);
                        }

                        value = value1;
}
                    
                }
                this.richTextBox1.Text =value;
            }
        }


        public string inputValue
        {
            get
            {
                return this.inputPath.Text;
            }
            set
            {

                this.inputPath.Text = value;
                choose();
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void MainPage_Load(object sender, EventArgs e)
        {
          /*  List<string> test = new List<string>();
            test.Add("");
            MessageBox.Show(test.Count().ToString());
           * **/
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            settingPage f = new settingPage();
            f.Show();

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            choose();
        }
        private void choose()
        {
            richTextBox1.Text = "";
           // Allfile.removeall();
            AllFiles.Clear();
            AllFolers.Clear();
            if (Regex.Match(inputPath.Text, "^[A-Za-z]:\\\\").Success)
            {
                label1.Text = "目录";
                WalkDirectoryTree(inputPath.Text);
            }
            else
            {
                if (Regex.Match(inputPath.Text, "^ftp://").Success)
                {
                    label1.Text = "FTP";
                    Key f = new Key();
                    f.ftpURI = inputPath.Text;
                    f.f1 = this;
                    //f.viewState = comboBox1.SelectedIndex;
                    f.Show(); //显示Form2
                    //this.Hide();//隐藏Form1
                }
                else
                {
                    if((Regex.Match(inputPath.Text, "^\\\\").Success))
                    {
                        label1.Text = "远程目录";
                        WalkDirectoryTree(inputPath.Text);
                    }
                    else
                    {
                        MessageBox.Show("输入错误，请重新输入！");

                    }

                }

            }
            saveLabel();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @inputPath.Text;// 设置默认路径
            DialogResult ret = fbd.ShowDialog();
            inputPath.Text = fbd.SelectedPath;
            choose();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            History h = new History();
            h.f1 = this;
            h.Show();

        }

        private void inputPath_KeyDown(object sender, KeyEventArgs e)
        {
            
            //13为Enter的键值
          if (e.KeyValue == 13)
            {
                toolStripButton4.Select();
                choose();
            }
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(richTextBox1.SelectedText);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            richTextBox1.SelectAll();
            //richTextBox1.HideSelection = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            inputPath.Text = "";
            richTextBox1.Text = "";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (Regex.Match(inputPath.Text, "^[A-Za-z]:\\\\").Success)
            {
                saveLabel();

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd";
                p.StartInfo.WorkingDirectory = inputPath.Text;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = false;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.RedirectStandardError = false;
                p.StartInfo.CreateNoWindow = false;
                //true表示不显示黑框，false表示显示dos界面
                p.Start();
                //p.StandardInput.WriteLine("cd f:\\");
                ///   p.StandardInput.WriteLine("dir");
                //   string s = p.StandardOutput.ReadToEnd();
                //  MessageBox.Show(s);


                // p.Close(); 
            }
            else
                MessageBox.Show("错误，请重新输入");
        }
        private void saveLabel()
        {
           // if (Properties.Settings.Default.saveIndex < 5)
          //  {
            if (inputPath.Text != "" && inputPath.Text != label2.Text)
            { 
                Label[] myLabel = new Label[5];
                myLabel[0] = label2;
                myLabel[1] = label3;
                myLabel[2] = label4;
                myLabel[3] = label5;
                myLabel[4] = label6;
                int i = 3;
                Properties.Settings.Default.history += myLabel[i + 1].Text + "\t";
                 
                while (i >= 0)
                {
                    myLabel[i + 1].Text = myLabel[i].Text;
                    i = i - 1;

                }
                myLabel[0].Text = inputPath.Text;
              //  Properties.Settings.Default.saveIndex = Properties.Settings.Default.saveIndex + 1;

                Properties.Settings.Default.Save();
         //   }
          /*  else
            {
                Properties.Settings.Default.saveIndex = Properties.Settings.Default.saveIndex - 5;
                saveLabel();
                
            }
           * */
            }
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            labelClick(sender);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            labelClick(sender);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            labelClick(sender);
        }

        private void label2_Click(object sender, EventArgs e)
        {
labelClick(sender);
        }
        private void labelClick(object sender)
        {
            Label abc = (Label)sender;
            inputPath.Text = abc.Text;
            choose();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            labelClick(sender);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            choose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.goumang.net");  
        }

        private void isExtension_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Filterfile.Clear();
            if(AllFiles.Count!=0)
            {
                if(checkBox1.Checked==true)
                {
                    if(checkBox2.Checked==true)
                    {
                        Filterfile = AllFiles.extfilter("doc").namefilter("","~$").ToList();
                    }
                    else
                    {
                        Filterfile = AllFiles.extfilter("doc", "docx").namefilter("", "~$").ToList();
                    }
                }
                else
                {
                    if (checkBox2.Checked == true)
                    {
                        Filterfile = AllFiles.extfilter("docx").namefilter("", "~$").ToList();
                    }
                    else
                    {
                        MessageBox.Show("Please select filter string!");

                    }

                }


                if(textBox3.Text!="")
                {
                    Filterfile=Filterfile.namefilter(textBox3.Text).ToList();
                }


                if (textBox4.Text != "")
                {
                    Filterfile = Filterfile.namefilter("",textBox4.Text).ToList();
                }


                listBox1.DataSource = Filterfile.Select(p=>p.FullName).ToList();
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    listBox1.SelectedIndex = i;  
                }

                listBox1.Refresh();
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedDoc = this.listBox1.SelectedItems;
            List<string> opDocs = new List<string>();
            foreach (string docname in selectedDoc)
            {
                opDocs.Add(docname);

            }
           
            if (MessageBox.Show("更改将不可恢复，请先备份！是否继续？","备份警告",MessageBoxButtons.YesNoCancel)==DialogResult.Yes)
            {
               
          
            object missing = Type.Missing;
            foreach (string pp in opDocs)
            {
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document wordDoc = wordApp.Documents.Open(pp);
              wordMethod.SearchReplace(wordApp, wordDoc, textBox1.Text, textBox2.Text);
                //object missing = Type.Missing;
                wordApp.Quit();
            }

        
            

            MessageBox.Show("完成");

            }

        }






        /// <summary>
        /// 查找并替换文本
        /// </summary>
        /// <param name="wordApp"></param>
        /// <param name="oldStr"></param>
        /// <param name="newStr"></param>




        #region localFolder



        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime dt = dateTimePicker1.Value;


            AllFolers.ForEach(
                delegate(DirectoryInfo x)
                {
                    try
                    {

                  
                    x.CreationTime = dt;
                    x.LastAccessTime = dt;
                    x.LastWriteTime = dt;
                    }
                    catch
                    {

                    }
                }
                
                );

            AllFiles.ForEach(
              delegate (FileInfo x)
              {
                  try
                  {

                 
                  x.CreationTime = dt;
                  x.LastAccessTime = dt;
                  x.LastWriteTime = dt;
                  }
                  catch
                  {

                  }
              }

              );

        }
    }
}
