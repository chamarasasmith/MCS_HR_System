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
using System.Drawing.Imaging;

namespace MCS_HR_System
{
    public partial class Employee1 : UserControl
    {
        
        public Employee1()
        {
            InitializeComponent();
            egen.SelectedIndex = 0;



            LoadGridView();

        }


        public void LoadGridView() {

            
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    dataGridView1.Rows.Clear();

                    dataGridView1.ColumnCount = 3;
                    dataGridView1.Columns[0].Name = "Employee ID";
                    dataGridView1.Columns[1].Name = "Employee Name";
                    dataGridView1.Columns[2].Name = "Company Number";

                    List<Employee> em = db.Employees.Where(x => x.Status.Trim() == "1").ToList();

                    string[] row = new string[] { "EMP20171023001", "Chamara", "Sasmith" };

                    foreach (var item in em)
                    {
                        row = new string[] { item.Emp_ID.Trim(), item.First_Name.Trim() + " " + item.Last_Name.Trim(), item.Company_No.Trim() };
                        dataGridView1.Rows.Add(row);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }



        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        public byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MCSHRMEntities db=new MCSHRMEntities())
                {
                    if (!string.IsNullOrEmpty(eid.Text))
                    {
                        Employee em = new Employee();
                        em.Emp_ID = eid.Text;
                        em.First_Name = efn.Text;
                        em.Last_Name = eln.Text;
                        em.Personal_No = epn.Text;
                        em.Company_No = ecn.Text;
                        em.Email = eemail.Text;
                        em.Street = estreet.Text;
                        em.City = ecity.Text;
                        em.Status = "1";
                        em.Gender = egen.Text;
                        em.BOD = ebod.Value;
                        em.Joined_Date = ejd.Value;
                        em.Img = imageToByteArray(pictureBox1.Image);
                        em.Salary = esal.Text;
                        em.NIC = enic.Text;
                        em.Nationality = enat.Text;

                        db.Employees.Add(em);
                        db.SaveChanges();
                        MessageBox.Show("Success..!");
                    }
                    else
                    {
                        MessageBox.Show("Employee ID is Empty or Invalid");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unsuccess..! (Error: " + ex.Message + ")");
            }
            finally
            {
                LoadGridView();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            using (MCSHRMEntities db = new MCSHRMEntities())
            {
                if (e.RowIndex >= 0)
                {
                    string s = dataGridView1.Rows[e.RowIndex].Cells["Employee ID"].FormattedValue.ToString();

                    Employee em = db.Employees.Where(x => x.Emp_ID.Trim() == s).SingleOrDefault();

                    if (em != null)
                    {
                        eid.Text = em.Emp_ID.ToString().Trim();
                        efn.Text = em.First_Name.ToString().Trim();
                        eln.Text = em.Last_Name.ToString().Trim();
                        epn.Text = em.Personal_No.ToString().Trim();
                        ecn.Text = em.Company_No.ToString().Trim();
                        eemail.Text = em.Email.ToString().Trim();
                        estreet.Text = em.Street.ToString().Trim();
                        ecity.Text = em.City.ToString().Trim();
                        egen.SelectedItem = em.Gender.ToString().Trim();
                        enat.Text = em.Nationality.ToString().Trim();
                        ebod.Value = em.BOD.GetValueOrDefault();
                        ejd.Value = em.Joined_Date.GetValueOrDefault();
                        esal.Text = em.Salary.ToString().Trim();
                        enic.Text = em.NIC.ToString().Trim();
                        if (em.Img != null)
                        {
                            pictureBox1.Image = byteArrayToImage(em.Img);
                        }
                        else
                        {
                            pictureBox1.Image = Properties.Resources.user_male2_512;
                        }

                    }
                    else
                    {
                        ClearAll();
                    }

                }
            }

            
        }

        private void ClearAll()
        {
            eid.Text = "";
            efn.Text = "";
            eln.Text = "";
            epn.Text = "";
            ecn.Text = "";
            eemail.Text = "";
            estreet.Text = "";
            ecity.Text = "";
            egen.SelectedIndex = 0;
            enat.Text = "";
            ebod.Value = DateTime.Now;
            ejd.Value = DateTime.Now;
            esal.Text = "";
            enic.Text = "";
            pictureBox1.Image = Properties.Resources.user_male2_512;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadGridView();
            ClearAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    if (!string.IsNullOrEmpty(eid.Text))
                    {
                        Employee em = db.Employees.Where(x => x.Emp_ID.Trim() == eid.Text.ToString()).SingleOrDefault();

                        em.First_Name = efn.Text;
                        em.Last_Name = eln.Text;
                        em.Personal_No = epn.Text;
                        em.Company_No = ecn.Text;
                        em.Email = eemail.Text;
                        em.Street = estreet.Text;
                        em.City = ecity.Text;
                        em.Status = "1";
                        em.Gender = egen.Text;
                        em.BOD = ebod.Value;
                        em.Joined_Date = ejd.Value;
                        em.Img = imageToByteArray(pictureBox1.Image);
                        em.Salary = esal.Text;
                        em.NIC = enic.Text;
                        em.Nationality = enat.Text;

                        db.SaveChanges();
                        MessageBox.Show("Success..!");
                    }
                    else
                    {
                        MessageBox.Show("Employee ID is Empty Or Invalid");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unsuccess..! (Error: " + ex.Message + ")");
            }
            finally
            {
                LoadGridView();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    if (!string.IsNullOrEmpty(eid.Text))
                    {
                        Employee em = db.Employees.Where(x => x.Emp_ID.Trim() == eid.Text.ToString()).SingleOrDefault();

                        em.Status = "0";

                        db.SaveChanges();
                        MessageBox.Show("Success..!");
                    }
                    else
                    {
                        MessageBox.Show("Employee ID is Empty Or Invalid");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unsuccess..! (Error: " + ex.Message + ")");
            }
            finally
            {
                LoadGridView();
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    ClearAll();
                    dataGridView1.Rows.Clear();

                    dataGridView1.ColumnCount = 3;
                    dataGridView1.Columns[0].Name = "Employee ID";
                    dataGridView1.Columns[1].Name = "Employee Name";
                    dataGridView1.Columns[2].Name = "Company Number";



                    List<Employee> em = db.Employees.Where(x => x.Emp_ID.Contains(textBox11.Text) || x.First_Name.Contains(textBox11.Text) || x.Last_Name.Contains(textBox11.Text) || x.Company_No.Contains(textBox11.Text)).ToList();

                    string[] row = new string[] { "EMP20171023001", "Chamara", "Sasmith" };

                    foreach (var item in em)
                    {
                        row = new string[] { item.Emp_ID.Trim(), item.First_Name.Trim() + " " + item.Last_Name.Trim(), item.Company_No.Trim() };
                        dataGridView1.Rows.Add(row);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid ("+ex.Message+")");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog()
            {
                Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg"
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open.FileName);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (MCSHRMEntities db = new MCSHRMEntities())
            {
                try
                {

                    SaveFileDialog open = new SaveFileDialog()
                    {
                        Filter = "Comma Separated Values(*.csv)|*.csv"
                    };

                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        string path = open.FileName;
                        using (var w = new StreamWriter(path))
                        {

                            foreach (Employee emp in db.Employees.ToList())
                            {
                                var c1 = emp.Emp_ID.ToString().Trim().Replace(',',' ');
                                var c2 = emp.First_Name.ToString().Trim().Replace(',', ' ');
                                var c3 = emp.Last_Name.ToString().Trim().Replace(',', ' ');
                                var c4 = emp.Personal_No.ToString().Trim().Replace(',', ' ');
                                var c5 = emp.Company_No.ToString().Trim().Replace(',', ' ');
                                var c6 = emp.Email.ToString().Trim().Replace(',', ' ');
                                var c7 = emp.Street.ToString().Trim().Replace(',', ' ');
                                var c8 = emp.City.ToString().Trim().Replace(',', ' ');
                                var c9 = emp.Gender.ToString().Trim().Replace(',', ' ');
                                var c10 = emp.BOD.ToString().Trim().Replace(',', ' ');
                                var c11= emp.Joined_Date.ToString().Trim().Replace(',', ' ');
                                var c12= emp.Resigned_Date.ToString().Trim().Replace(',', ' ');
                                var c13= emp.Salary.ToString().Trim().Replace(',', ' ');
                                var c14= emp.NIC.ToString().Trim().Replace(',', ' ');
                                var c15= emp.Nationality.ToString().Trim().Replace(',', ' ');
                                var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", c1, c2, c3, c4, c5, c6, c7,c8,c9,c10,c11,c12,c13,c14,c15);
                                w.WriteLine(line);
                                w.Flush();
                            }
                            MessageBox.Show("Success..!!!");
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
            }
        }
    }
}
