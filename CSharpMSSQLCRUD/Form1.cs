using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSharpMSSQLCRUD
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=ALLMANKIND\\MSSQLSERVER8; Database=db_cscrud; Integrated Security=True;");
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string currentid = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetRecords();
        }

        public void GetRecords()
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("select * from tbl_names", conn);
            adapter.Fill(ds, "tbl_names");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tbl_names";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("insert into tbl_names (firstname, lastname) VALUES " + 
             "('" + textBox1.Text + "','" + textBox2.Text + "')",conn);
            adapter.Fill(ds, "tbl_names");
            GetRecords();
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            currentid = dataGridView1[0, i].Value.ToString();

            ds = new DataSet();
            adapter = new SqlDataAdapter("delete from tbl_names where id = " + currentid, conn);
            adapter.Fill(ds, "tbl_names");
            GetRecords();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            currentid = dataGridView1[0, i].Value.ToString();
            textBox1.Text = dataGridView1[1, i].Value.ToString();
            textBox2.Text = dataGridView1[2, i].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("update tbl_names set firstname = '" + textBox1.Text + 
                "', lastname = '" + textBox2.Text + 
                "' where id = " + currentid, conn);
            adapter.Fill(ds, "tbl_names");
            textBox1.Clear();
            textBox2.Clear();
            GetRecords();
        }
    }
}
