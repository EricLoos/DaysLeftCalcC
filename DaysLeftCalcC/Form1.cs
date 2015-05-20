using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DaysLeftCalcC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fn = openFileDialog1.FileName;
                if (File.Exists(fn))
                {
                    int lineCount = 0;
                    byte DaysLeft = 180;
                    byte[] daylist = new byte[289];
                    using (StreamReader rdr = new StreamReader(fn))
                    {
                        string line;
                        DateTime FirstDayOfSchool, LastDayOfSchool, dt;
                        string lastdates = "";
                        FirstDayOfSchool = DateTime.Now;
                        while ((line = rdr.ReadLine()) != null)
                        {

                            string[] fields = line.Split(',');
                            lastdates = fields[0];
                            if (lineCount == 0)
                                FirstDayOfSchool = DateTime.Parse(fields[0]);
                            dt = DateTime.Parse(lastdates);
                            daylist[lineCount] = DaysLeft;
                            if (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday && fields[2].Trim() == "")
                                DaysLeft--;
                            lineCount++;
                        }
                        LastDayOfSchool = DateTime.Parse(lastdates);
                        string s = string.Format("First day of school: {0}\r\n", FirstDayOfSchool);
                        s += string.Format("Last day of school: {0}\r\n", LastDayOfSchool);
                        for (int i = 0; i < 289; i++)
                            s += string.Format("{0},", daylist[i]);
                        label1.Text = s;
                    }
                    MessageBox.Show(string.Format("Read {0} lines.", lineCount));
                }
            }
        }
    }
}
