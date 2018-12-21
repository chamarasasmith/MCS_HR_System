using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MCS_HR_System
{
    public partial class Staff_Movements : UserControl
    {
        public Staff_Movements()
        {
            InitializeComponent();
            textBox3.Text = "17";
            textBox4.Text = "30";
            textBox1.Text = "08";
            textBox2.Text = "30";
            loadEmployees();
            LoadGridView();
        }


        public void LoadGridView()
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    dataGridView1.Rows.Clear();

                    DateTime d1 = dateTimePicker1.Value;

                    dataGridView1.ColumnCount = 7;
                    dataGridView1.Columns[0].Name = "ID";
                    dataGridView1.Columns[1].Name = "Date";
                    dataGridView1.Columns[2].Name = "Employee Name";
                    dataGridView1.Columns[3].Name = "Start Time";
                    dataGridView1.Columns[4].Name = "End Time";
                    dataGridView1.Columns[5].Name = "Place";
                    dataGridView1.Columns[6].Name = "Reason";

                    List<Staff_Movements1> em = db.Staff_Movements1.Where(x => x.Status1.Trim() == "1" && x.SM_Date.Value.Day == d1.Day && x.SM_Date.Value.Month == d1.Month && x.SM_Date.Value.Year == d1.Year).ToList();

                    string[] row = new string[] { "", "EMP20171023001", "Chamara", "Sasmith" };

                    foreach (var item in em)
                    {
                        row = new string[] { item.SM_ID.ToString().Trim(), item.SM_Date.Value.ToString("yyyy-MM-dd"), item.Employee.First_Name.Trim() + " " + item.Employee.Last_Name.Trim(), item.S_Time.Value.ToString("HH:mm:ss"), item.E_Time.Value.ToString("HH:mm:ss"),item.Place.Trim(),item.Reason1.Trim() };
                        dataGridView1.Rows.Add(row);
                    
                    }
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadEmployees()
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    comboBox1.Items.Clear();

                    List<Employee> l1 = db.Employees.ToList();

                    foreach (var item in l1)
                    {
                        comboBox1.Items.Add(item.Emp_ID.Trim() + ":" + item.First_Name.Trim() + " " + item.Last_Name.Trim());

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            int i = textBox1.Text.Length;

            int n;
            bool isNumeric = int.TryParse(textBox1.Text, out n);

            if (isNumeric)
            {
                if (i >= 2)
                {
                    textBox2.Focus();
                    textBox2.SelectAll();
                }
            }
            else
            {
                textBox1.Text = "";
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            int i = textBox2.Text.Length;

            int n;
            bool isNumeric = int.TryParse(textBox2.Text, out n);

            if (isNumeric)
            {
                if (i >= 2)
                {
                    textBox3.Focus();
                    textBox3.SelectAll();
                }
            }
            else
            {
                textBox2.Text = "";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            int i = textBox3.Text.Length;

            int n;
            bool isNumeric = int.TryParse(textBox3.Text, out n);

            if (isNumeric)
            {
                if (i >= 2)
                {
                    textBox4.Focus();
                    textBox4.SelectAll();
                }
            }
            else
            {
                textBox3.Text = "";
            }
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            int i = textBox4.Text.Length;

            int n;
            bool isNumeric = int.TryParse(textBox4.Text, out n);

            if (isNumeric)
            {
                if (i >= 2)
                {
                    button6.Focus();
                }
            }
            else
            {
                textBox4.Text = "";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                   int i1= comboBox1.SelectedIndex;

                    if (i1>=0)
                    {
                        string emp= comboBox1.SelectedItem.ToString().Split(':')[0];

                        DateTime d1= dateTimePicker1.Value;
                        int s1 = int.Parse(textBox1.Text);
                        int s2 = int.Parse(textBox2.Text);
                        int e1 = int.Parse(textBox3.Text);
                        int e2 = int.Parse(textBox4.Text);

                        DateTime st= new DateTime(d1.Year,d1.Month,d1.Day,s1,s2,0);
                        DateTime et = new DateTime(d1.Year, d1.Month, d1.Day, e1, e2, 0);

                        

                        Staff_Movements1 sm = new Staff_Movements1();
                        sm.Emp_ID = emp;
                        sm.SM_Date = d1;
                        sm.Place = textBox5.Text.Trim();
                        sm.Reason1 = textBox6.Text.Trim();
                        sm.S_Time = st;
                        sm.E_Time = et;
                        sm.Status1 = "1";

                        db.Staff_Movements1.Add(sm);
                        db.SaveChanges();
                        textBox5.Text = "";
                        textBox6.Text = "";
                        MessageBox.Show("Success....!");
                        LoadGridView();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : "+ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                   int i1= comboBox1.SelectedIndex;
                    if (i1>=0)
                    {

                try
                {
                    using (MCSHRMEntities db = new MCSHRMEntities())
                    {
                        dataGridView1.Rows.Clear();

                        DateTime d1 = dateTimePicker1.Value;

                        dataGridView1.ColumnCount = 7;
                        dataGridView1.Columns[0].Name = "ID";
                        dataGridView1.Columns[1].Name = "Date";
                        dataGridView1.Columns[2].Name = "Employee Name";
                        dataGridView1.Columns[3].Name = "Start Time";
                        dataGridView1.Columns[4].Name = "End Time";
                        dataGridView1.Columns[5].Name = "Place";
                        dataGridView1.Columns[6].Name = "Reason";

                        string emp = comboBox1.SelectedItem.ToString().Split(':')[0];

                        List<Staff_Movements1> em = db.Staff_Movements1.Where(x => x.Emp_ID.Trim() == emp && x.SM_Date.Value.Year == d1.Year && x.SM_Date.Value.Month==d1.Month).ToList();


                        string[] row = new string[] { "", "EMP20171023001", "Chamara", "Sasmith" };

                        foreach (var item in em)
                        {
                            row = new string[] { item.SM_ID.ToString().Trim(), item.SM_Date.Value.ToString("yyyy-MM-dd"), item.Employee.First_Name.Trim() + " " + item.Employee.Last_Name.Trim(), item.S_Time.Value.ToString("HH:mm:ss"), item.E_Time.Value.ToString("HH:mm:ss"), item.Place.Trim(), item.Reason1.Trim() };
                            dataGridView1.Rows.Add(row);

                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
             
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        int ri = dataGridView1.SelectedRows[0].Index;

                        string s = dataGridView1.Rows[ri].Cells["ID"].FormattedValue.ToString();

                        int lid = int.Parse(s);

                        Staff_Movements1 l = db.Staff_Movements1.First(x => x.SM_ID == lid);

                        db.Staff_Movements1.Remove(l);
                        db.SaveChanges();

                        MessageBox.Show("Success..!");
                        LoadGridView();
                    }
                    else
                    {
                        MessageBox.Show("Please Select One");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog open = new SaveFileDialog()
                {
                    Filter = "Comma Separated Values(*.csv)|*.csv"
                };

                if (open.ShowDialog() == DialogResult.OK)
                {
                    string path= open.FileName;
                    using (var w = new StreamWriter(path))
                    {

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            var c1 = row.Cells[0].Value.ToString();
                            var c2 = row.Cells[1].Value.ToString();
                            var c3 = row.Cells[2].Value.ToString();
                            var c4 = row.Cells[3].Value.ToString();
                            var c5 = row.Cells[4].Value.ToString();
                            var c6 = row.Cells[5].Value.ToString();
                            var c7 = row.Cells[6].Value.ToString();
                            var line = string.Format("{0},{1},{2},{3},{4},{5},{6}", c1, c2, c3, c4, c5, c6, c7);
                            w.WriteLine(line);
                            w.Flush();
                        }
                        MessageBox.Show("Success..!!!");
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : "+ex.Message);
            }
            
        }
    }
}
