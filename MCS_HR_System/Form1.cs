using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCS_HR_System
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
            MaterialSkin.MaterialSkinManager sm = MaterialSkin.MaterialSkinManager.Instance;
            sm.AddFormToManage(this);
            sm.Theme = MaterialSkin.MaterialSkinManager.Themes.DARK;
            sm.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Blue600,
                MaterialSkin.Primary.BlueGrey900,MaterialSkin.Primary.Blue300,
                MaterialSkin.Accent.Orange700,MaterialSkin.TextShade.WHITE);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {

            try
            {
                using (MCSHRMEntities db = new MCSHRMEntities())
                {
                    User1 u = db.User1.Where(x => x.Username.Trim() == materialSingleLineTextField1.Text.Trim() && x.Password.Trim() == materialSingleLineTextField2.Text.Trim() && x.St == "1").SingleOrDefault();
                    if (u != null)
                    {
                        Main2 m = new Main2();
                        m.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password");
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
