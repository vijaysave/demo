using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ProductsForm : Form
    {
        private string connectionString = "Server=DESKTOP-VNU7T8C; Database=Mukane2024;Integrated Security=True;";

        public ProductsForm()
        {
            InitializeComponent();
            LoadProducts();
        }

        // Метод для загрузки продуктов с возможностью фильтрации и сортировки  
        private void LoadProducts(string filter = "", bool sortByPrice = false, bool sortByAsc = false)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Products", connection);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Фильтрация по названию продукта  
                if (!string.IsNullOrEmpty(filter))
                {
                    table.DefaultView.RowFilter = $"ProductName LIKE '%{filter}%'";
                }

                // Сортировка по цене  
                if (sortByPrice)
                {
                    if (sortByAsc)
                    {
                        table.DefaultView.Sort = "Price ASC"; // Сортировка по возрастанию  
                    }
                    else
                    {
                        table.DefaultView.Sort = "Price DESC"; // Сортировка по убыванию  
                    }
                }

                // Подключаем таблицу к DataGridView  
                dataGridView1.DataSource = table;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Products", connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                DataTable table = (DataTable)dataGridView1.DataSource;

                adapter.Update(table);
                MessageBox.Show("Изменения сохранены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void поиск_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();
            LoadProducts(searchText);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Этот метод можно использовать для фильтрации во время ввода текста, если необходимо  
        }

        private void сортировка_Click(object sender, EventArgs e)  // Кнопка для сортировки по убыванию  
        {
            string searchText = textBox1.Text.Trim();
            bool sortByPrice = true; // Сортировать по цене  
            bool sortByAsc = false; // По убыванию  

            LoadProducts(searchText, sortByPrice, sortByAsc);
        }

        private void button2_Click(object sender, EventArgs e)  // Кнопка для сортировки по возрастанию  
        {
            string searchText = textBox1.Text.Trim();
            bool sortByPrice = true; // Сортировать по цене  
            bool sortByAsc = true; // По возрастанию  

            LoadProducts(searchText, sortByPrice, sortByAsc);
        }
    }
}