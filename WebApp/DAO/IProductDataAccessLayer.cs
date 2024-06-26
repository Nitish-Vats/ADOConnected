﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.DAO
{
    interface IProductDataAccessLayer
    {
        public IEnumerable<Product> GetProducts();
        public void AddProduct(Product product);
        public void EditProduct(int id, Product product);
        public void DeleteProduct(int id,Product product);
        public Product GetProduct(int id);
    }
}
