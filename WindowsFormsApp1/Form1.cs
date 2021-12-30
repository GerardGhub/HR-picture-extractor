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
using System.Data.SqlClient;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        String connetionString = @"Data Source=192.168.2.9\SQLEXPRESS;Initial Catalog=hr_bak;User ID=sa;Password=Nescafe3in1;MultipleActiveResultSets=true";
        //String connetionString1 = @"Data Source=192.168.2.9\SQLEXPRESS;Initial Catalog=mftg_db3;User ID=sa;Password=Nescafe3in1;MultipleActiveResultSets=true";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        int ctr = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = connetionString;
            con.Open();

            string q = "select employee_id,image_employee from employee where is_resigned = 0 and is_active = 1 and employment_status_id = 1";
            cmd = new SqlCommand(q, con);
            dr = cmd.ExecuteReader();

            if (!Directory.Exists("C:\\pics2019\\"))
                Directory.CreateDirectory("C:\\pics2019\\");
            while (dr.Read())
            {
                try
                {


                    await get(dr);


                }
                catch (Exception ex)
                {


                }


            }

            MessageBox.Show(ctr.ToString());
            con.Close();
        }
        public async Task<string> get(SqlDataReader dr)
        {
            byte[] pic = (byte[])dr[1];
            MemoryStream ms = new MemoryStream(pic);

            var qwe = new Bitmap(ms);
            pictureBox1.Image = resizeImage(qwe, new Size(400, 400));
            
            await Task.Delay(100);
            string filename = "C:\\pics2019\\" + dr[0].ToString() + ".jpg";
            pictureBox1.Image.Save(filename);
            ctr++;

            await Task.Delay(50);
            return "showemp";
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}
