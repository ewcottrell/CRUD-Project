﻿using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
            
namespace CRUD_Operationsdemo
{
    public class productrepository
    {
        public productrepository(string connectionstring)
        {
            ConnectionString = connectionstring;
        }

        public string ConnectionString { get; set; }


        public void DeleteProduct(int productid)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "delete from products where Productid = @productid";
                cmd.Parameters.AddWithValue("productid", productid);
                cmd.ExecuteNonQuery();
            }
        }

        public void CreateNewProduct(string name, decimal price)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO products (Name, Price) VALUES (@name, @price);";
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("price", price);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> GetProductNames(int categoryID)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Products WHERE categoryid = @categoryID;";
                cmd.Parameters.AddWithValue("categoryID", categoryID);
                
                MySqlDataReader dataReader = cmd.ExecuteReader();
                List<Product> productNames = new List<Product>();

                while (dataReader.Read())
                {
                    string Name = dataReader["Name"].ToString();
                    Product prod = new Product();
                    prod.name = Name;
                    productNames.Add(prod);
                }
                foreach (Product item in productNames)
                {
                    Console.WriteLine(item.name);
                }
                return productNames;
            }

        }

        public void UpdateTable(int ProductID, string newName)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Products Set Name = @newName WHERE ProductID = @ProductID;";
                cmd.Parameters.AddWithValue("newname", newName);
                cmd.Parameters.AddWithValue("ProductID", ProductID);
                cmd.ExecuteNonQuery();
            }    
        }
    }
}
