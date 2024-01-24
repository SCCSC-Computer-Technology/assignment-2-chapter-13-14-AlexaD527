using System;
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
    public partial class Form2 : Form
    {
        private alexdatabaseDataContext db;
        public Form2()
        {
            InitializeComponent();
            db = new alexdatabaseDataContext();
           
        }
        //sort by price
        private void price_Click(object sender, EventArgs e)
        {
            int price;
            if (int.TryParse(priceT1.Text, out price))
            {
                var results = from Product in db.Products
                              where Product.Price < price
                              orderby Product.Price descending
                              select Product;
                productsDataGridView.DataSource = results.ToList();
            }
            else if (int.TryParse(priceT2.Text, out price))
            {
                var results = from Product in db.Products
                              where Product.Price > price
                              orderby Product.Price ascending
                              select Product;
                productsDataGridView.DataSource = results.ToList();

            }
            else
                MessageBox.Show("Error: No products meet this criteria");

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            db = new alexdatabaseDataContext();
           

            var results = from Product in db.Products
                          orderby Product.Price ascending
                          select Product;

            productsDataGridView.DataSource = results.ToList();
            // Clear existing columns
            productsDataGridView.Columns.Clear();

            // Add columns manually because Visual Studio aggravates me
            DataGridViewTextBoxColumn colProductNumber = new DataGridViewTextBoxColumn();
            colProductNumber.HeaderText = "Product Number";
            colProductNumber.DataPropertyName = "Product_Number";
            productsDataGridView.Columns.Add(colProductNumber);

            DataGridViewTextBoxColumn colDescription = new DataGridViewTextBoxColumn();
            colDescription.HeaderText = "Description";
            colDescription.DataPropertyName = "Description";
            productsDataGridView.Columns.Add(colDescription);

            DataGridViewTextBoxColumn colUnitsOnHand = new DataGridViewTextBoxColumn();
            colUnitsOnHand.HeaderText = "Units On Hand";
            colUnitsOnHand.DataPropertyName = "Units_On_Hand";
            productsDataGridView.Columns.Add(colUnitsOnHand);

            DataGridViewTextBoxColumn colPrice = new DataGridViewTextBoxColumn();
            colPrice.HeaderText = "Price";
            colPrice.DataPropertyName = "Price";
            colPrice.DefaultCellStyle.Format = "N2";
            colPrice.DefaultCellStyle.Format = "C";
            productsDataGridView.Columns.Add(colPrice);



            // Bind data to the DataGridView
            productsDataGridView.DataSource = results.ToList();

        }
    }
}
