using System; //alexa davis chapter 14 cpt 206
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace alexadavis_chapter14
{
    public partial class Form1 : Form
    {
        private alexdatabaseDataContext db;
        public Form1()
        {
            InitializeComponent();
            db = new alexdatabaseDataContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var results = from Product in db.Products
                          orderby Product.Units_On_Hand ascending
                          select Product;
          
            productsDataGridView.DataSource = results.ToList();


        }

        private void ProNum_Click(object sender, EventArgs e)
        {
            var proNum = proNumT.Text;
            var results = from Product in db.Products
                          where Product.Product_Number.Contains(proNum)
                          select Product;
            //Covert result to data grid to find specefic number
            productsDataGridView.DataSource = results.ToList();

            if (productsDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Error: No results found in Database");
            }
          
        }

        private void proDesc_Click(object sender, EventArgs e)
        {
            var proDesc = proDescT.Text;
            var results = from Product in db.Products
                          where Product.Description.Contains(proDesc)
                          select Product;
            //Convert results to data grid to find variad product description user input phrases
            productsDataGridView.DataSource = results.ToList();

            //Error message for no item found

            if (productsDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Error: No Product found containing:     " + (proDesc));
            }
        }
        //Search by units on HANDDDDDDDD
        private void units_Click(object sender, EventArgs e)
        {
            int units;
            if (int.TryParse(unitT1.Text, out units))
            {
                var results = from Product in db.Products
                              where Product.Units_On_Hand < units
                              orderby Product.Units_On_Hand descending
                              select Product;
                productsDataGridView.DataSource = results.ToList();
            }
            else if (int.TryParse(unitT2.Text, out units))
            {
                var results = from Product in db.Products
                              where Product.Units_On_Hand > units
                              orderby Product.Units_On_Hand ascending
                              select Product;
                productsDataGridView.DataSource = results.ToList();

            }
            else
                MessageBox.Show("Error: No products meet this criteria");
                          
        }

        private void newForm_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
