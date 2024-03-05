using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.DAO
{
    public class ProductDataAccessLayer : IProductDataAccessLayer
    {
        public IConfiguration Configuration { get; }
        public ProductDataAccessLayer(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void AddProduct(Product product)
        {
            try
            {
                string connectionString = Configuration["ConnectionStrings:DbConnection"];
                //string connectionString = "Data Source=tserver;Initial Catalog=Demo1;User Id=sa;Password=kpl!@!83";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query = "Insert into Product values(@Name,@Description,@unitprice,@Catid);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@unitprice", product.UnitPrice);
                command.Parameters.AddWithValue("@Catid", product.CategoryId);
                int result = command.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
            }
        }

        public void DeleteProduct(int id,Product product)
        {
            //string connectionString = "Data Source=tserver;Initial Catalog=Demo1;User Id=sa;Password=kpl!@!83";
            string connectionString = Configuration["ConnectionStrings:DbConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "delete from product where ProductId=@id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@id", id);
                        int result = command.ExecuteNonQuery();
                        
                    }
                    catch (Exception ex)
                    {

                      
                    }
                }
                connection.Close();
            }
        }

        public void EditProduct(int id, Product model)
        {
            string connectionString = Configuration["ConnectionStrings:DbConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "Update Product set Name=@Name,Description=@Description,UnitPrice=@unitprice,CategoryId=@Catid where ProductId=@id;";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@Description", model.Description);
                    command.Parameters.AddWithValue("@unitprice", model.UnitPrice);
                    command.Parameters.AddWithValue("@Catid", model.CategoryId);
                    command.Parameters.AddWithValue("@id", id);
                    int result = command.ExecuteNonQuery();
                  
                }
                catch (Exception ex)
                {
                  

                }
            }
        }

        public Product GetProduct(int id)
        {
            Product product = new Product();
            string connectionString = Configuration["ConnectionStrings:DbConnection"];
            //string connectionString = "Data Source=tserver;Initial Catalog=Demo1;User Id=sa;Password=kpl!@!83";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Select * from Product where ProductId=@id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            product.Name = dataReader["Name"].ToString();
                            product.Description = dataReader["Description"].ToString();
                            product.UnitPrice = (decimal)dataReader["UnitPrice"];
                            product.ProductId = (int)dataReader["ProductId"];
                            product.CategoryId = dataReader["CategoryId"].ToString();

                        }
                    }
                }
                connection.Close();
            }
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> product = new List<Product>();
            //string connectionString = "Data Source=tserver;Initial Catalog=Demo1;User Id=sa;Password=kpl!@!83";
            string connectionString = Configuration["ConnectionStrings:DbConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "select * from Product join Categories on Categories.CategoryId=Product.CategoryId;";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Product pro = new Product();
                    pro.ProductId = (int)dataReader["ProductId"];
                    // pro.category.CategoryId = (int)dataReader["CategoryId"];
                    pro.Name = dataReader["Name"].ToString();
                    pro.UnitPrice = (decimal)dataReader["UnitPrice"];
                    pro.CategoryName = dataReader[6].ToString();
                    pro.CategoryDescription = dataReader[7].ToString();
                    pro.Description = dataReader["Description"].ToString();


                    product.Add(pro);
                }
                connection.Close();
              
            }
            catch (Exception ex)
            {
               

            }
            return product;
        }
    }
}
