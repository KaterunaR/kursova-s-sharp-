using store_c_sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursova_c_sharp
{
    public partial class Form1 : Form
    {
        private Store store;
        public Form1()
        {
            InitializeComponent();
            store = new Store();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name for the item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = txtName.Text;
            DateTime manufactureDate = DateTime.Parse(dtManufacturingDate.Text);
            string qualityCertificate = txtCertificate.Text;

            double price;
            if (!double.TryParse(txtPrice.Text, out price) || price < 0)
            {
                MessageBox.Show("Invalid price format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double markup;
            if (!double.TryParse(textBox1.Text, out markup) || price < 0)
            {
                MessageBox.Show("Invalid markup format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime expiryDate = DateTime.Parse(dateTimePicker1.Text);

            // Create GoodsInStore object
            GoodsInStore item = new GoodsInStore(name, manufactureDate, qualityCertificate, price, markup, expiryDate);

            store.AddItem(item);

            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            dataGridView.DataSource = null;
            dataGridView.AutoGenerateColumns = true;
            dataGridView.DataSource = store.GetAllItems();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string itemNameToDelete = txtDelete.Text;

            // Find the item in input to delete
            GoodsInStore itemToDelete = store.GetAllItems().Find(item => item.name == itemNameToDelete);

            if (itemToDelete != null)
            {
                store.DeleteItem(itemToDelete);
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("Item not found in the store.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double profit = store.CalculateProfit();
            MessageBox.Show($"Total Profit: {profit:C}", "Profit", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fileName = txtFile.Text.Trim();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                MessageBox.Show("Please enter a valid file name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                store.WriteToFile(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnLoad_Click(object sender, EventArgs e)
        {
            string fileName = txtFile.Text.Trim();
            try
            {
                store.AddItemsFromFile(fileName);
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
