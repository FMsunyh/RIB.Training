using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Osacr
{
    public partial class Form1 : Form
    {

        List<Dictionary<string, string>> _voteList = new List<Dictionary<string, string>>();
        ArrayList _movie = new ArrayList();
        ArrayList _music = new ArrayList();
        public Form1()
        {
            InitializeComponent();
            InitUI();
            InitSetup();
        }

        private void InitSetup()
        {
            using (FileStream fs = new FileStream(@"D:\Firmin Sun\project\Osacr\Osacr\bin\Debug\movie\item.txt", FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string s = null;

                    while ((s = sr.ReadLine()) != null)
                    {
                        comboBox2.Items.Add(s);
                    }
                    sr.Close();
                    fs.Close();
                }
            }

            using (FileStream fs = new FileStream(@"D:\Firmin Sun\project\Osacr\Osacr\bin\Debug\movie\movie.txt", FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string s = null;

                    while ((s = sr.ReadLine()) != null)
                    {
                        _movie.Add(s);
                    }
                    sr.Close();
                    fs.Close();
                }
            }

            using (FileStream fs = new FileStream(@"D:\Firmin Sun\project\Osacr\Osacr\bin\Debug\movie\music.txt", FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string s = null;

                    while ((s = sr.ReadLine()) != null)
                    {
                        _music.Add(s);
                    }
                    sr.Close();
                    fs.Close();
                }
            }
        }

        private void InitUI()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 8;
            //ataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.Columns[0].Name = "voter";
            dataGridView1.Columns[1].Name = "The best movie";
            dataGridView1.Columns[2].Name = "The best music";
            dataGridView1.Columns[3].Name = "The best director";
            dataGridView1.Columns[4].Name = "The best actor";
            dataGridView1.Columns[5].Name = "The best actress";
            dataGridView1.Columns[6].Name = "The best walking gentleman";
            dataGridView1.Columns[7].Name = "The best walking lady";
            dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, string> vateMsgShow = _voteList[comboBox1.SelectedIndex];
            dataGridView1.Rows.Clear();

            string[] row = { vateMsgShow["voter"],
            vateMsgShow["The best movie"], vateMsgShow["The best music"], 
            vateMsgShow["The best director"], vateMsgShow["The best actor"],vateMsgShow["The best actress"],vateMsgShow["The best walking gentleman"],vateMsgShow["The best walking lady"] };
            dataGridView1.Rows.Add(row);
        }

        private void ShowAllData(List<Dictionary<string, string>> voteList)
        {
            dataGridView1.Rows.Clear();

            foreach (Dictionary<string, string> voteMsgShow in voteList)
            {

                string[] row = { voteMsgShow["voter"],
            voteMsgShow["The best movie"], voteMsgShow["The best music"], 
            voteMsgShow["The best director"], voteMsgShow["The best actor"],
            voteMsgShow["The best actress"],voteMsgShow["The best walking gentleman"],
            voteMsgShow["The best walking lady"] };

                dataGridView1.Rows.Add(row);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] fileEntries = Directory.GetFiles(@"D:\Firmin Sun\project\Osacr\Osacr\bin\Debug\votes");
            _voteList.Clear();
            foreach (string fileName in fileEntries)
            {
                comboBox1.Items.Add(fileName);

                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string s = null;
                        string item = null;
                        string role = null;
                        Dictionary<string, string> vote = new Dictionary<string, string>();
                        while ((s = sr.ReadLine()) != null)
                        {
                            string[] itemAndrole = s.Split(':');
                            item = itemAndrole[0];
                            role = itemAndrole[1];
                            vote.Add(item, role);
                        }
                        _voteList.Add(vote);

                        sr.Close();
                        fs.Close();
                    }
                }
            }

            ShowAllData(_voteList);
        }

        private void statistic(ArrayList item, string key)
        {
            Dictionary<string, int> statistcItem = new Dictionary<string, int>();
            foreach (string initItem in item)
            {
                statistcItem.Add(initItem, 0);
            }

            foreach (Dictionary<string, string> vote in _voteList)
            {
                if (vote[key] != null && vote[key] != "")
                {
                    statistcItem[vote[key]] = statistcItem[vote[key]] + 1;
                }
            }

            foreach (string itemName in item)
            {
                string[] row = { itemName, statistcItem[itemName].ToString() };
                dataGridView1.Rows.Add(row);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            dataGridView1.ColumnCount = 2;
            string headerName = "The best " + comboBox2.Text;
            dataGridView1.Columns[0].Name = headerName;
            dataGridView1.Columns[1].Name = "votes";

            switch (comboBox2.Text)
            {
                case "movie":
                    statistic(_movie, headerName);
                    break;
                case "music":
                    statistic(_music, headerName);
                    break;
                default:
                    break;
            }
        }
    }
}
