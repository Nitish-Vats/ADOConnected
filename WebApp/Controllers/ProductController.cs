using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WebApp.Models;
using WebApp.DAO;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        IConfiguration Configration;
        public ProductDataAccessLayer productDAO;
        public ProductController(IConfiguration configuration)
        {
            Configration = configuration;
            productDAO = new ProductDataAccessLayer(Configration);
        }
        public IActionResult Index()
        {
            return View(productDAO.GetProducts());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product model)
        {
            productDAO.AddProduct(model);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(productDAO.GetProduct(id));
        }

        public IActionResult Edit(int id)
        {
            return View(productDAO.GetProduct(id));
        }

        [HttpPost]
        public IActionResult Edit(Product model,int id)
        {

            //string connectionString = "Data Source=tserver;Initial Catalog=Demo1;User Id=sa;Password=kpl!@!83";
            productDAO.EditProduct(id,model);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            return View(productDAO.GetProduct(id));
        }

        [HttpPost]
        public IActionResult Delete(Product model, int id)
        {
            productDAO.DeleteProduct(id, model);
            return RedirectToAction("Index");
        }
    }
}
