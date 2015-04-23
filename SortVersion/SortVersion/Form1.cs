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

namespace SortVersion
{

    public partial class Form1 : Form
    {
        /// <summary>
        /// data buffer
        /// </summary>
        ArrayList _dataList = new ArrayList();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream("d://version.txt", FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string s = null;
                    richTextBox1.Text = null;
                                      
                    while ((s = sr.ReadLine()) != null)
                    {
                        _dataList.Add(s);

                        //show data in richTextBox1
                        richTextBox1.Text += s + Environment.NewLine; 
                    }
                     

                    sr.Close();
                    fs.Close();
                }
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = null;
            IComparer myComparer = new myReverserClass();
            _dataList.Sort(myComparer);

            foreach(string str in _dataList)
            {
                richTextBox2.Text += str + Environment.NewLine; 
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream("d://SortVersion.txt", FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (string str in _dataList)
                    {
                        sw.Write(str + Environment.NewLine);
                    }
                    
                    sw.Close();
                    fs.Close();
                }
            }
            MessageBox.Show("output data ok");
        }
    }

    public class myReverserClass : IComparer
    {
        int IComparer.Compare(Object x, Object y)
        {
            string sx = x.ToString();
            string sy = y.ToString();

            //first Sqlit
            string[] SplitX =  sx.Split('.');
            string[] SplitY = sy.Split('.');

            int i = 0,j = 0;
            
            while (i < SplitX.Length && j < SplitY.Length)
            {
                string xSubFront = "";
                string xSubBehind = "";

                string ySubFront = "";
                string ySubBehind = "";

                SubString(SplitX[i], ref xSubFront, ref xSubBehind);
                SubString(SplitY[j], ref ySubFront, ref ySubBehind);

                if (IsNunber(xSubFront) && IsNunber(ySubFront))
                {
                    int flag = Convert.ToInt64(xSubFront).CompareTo(Convert.ToInt64(ySubFront));
                    if (flag == 0)
                    {
                        flag = String.Compare(xSubBehind, ySubBehind, true);
                        if (flag == 0)
                        {
                            i++;
                            j++;
                            continue;
                        }
                        else
                        {
                            return flag;
                        }
                    }
                    else
                    {
                        return flag;
                    }
                }
                else
                {
                    int flagLitterAll = String.Compare(xSubFront, ySubFront, true);
                    if (flagLitterAll == 0)
                    {
                        i++;
                        j++;
                        continue;
                    }
                    else
                    {
                        return flagLitterAll;
                    }
                }
            }

            return 0;
        }

        bool IsNunber(string s)
        {
            char ch;
            for (int i = 0; i < s.Length; i++)
            {
                ch = s[i];
                if (ch >= '0' && ch <= '9')
                {
                    continue;
                }
                else
                    return false;
            }

            return true;
        }                                                                                      

        bool IsNunberOfFirstLitter(string s)
        {
            return (s[0] >= '0' && s[0] <= '9');           
        }

        void SubString(string s, ref string subFront, ref string subBehind)
        {
            if (s == "" || s == null)
            {
                return;
            }
            if (IsNunberOfFirstLitter(s))
            {
                int index;
                for (index = 0; index < s.Length; index++)
                {
                    if (s[index] >= '0' && s[index] <= '9')
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                subFront = s.Substring(0, index);
                subBehind = s.Substring(index);
            }
            else
            {
                subFront = s;
                subBehind = "";
            }
        }
    }

}
