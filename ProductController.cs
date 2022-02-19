using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationExam.Models;

namespace WebApplicationExam.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetExam;Integrated Security=True;Connect Timeout=30;";
            connection.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = connection;
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            cm.CommandText = "getproduct";
            SqlDataReader sdr = cm.ExecuteReader();
            List<product> productlist = new List<product>();
            while (sdr.Read())
            {
                product model = new product();
                model.ProductId = int.Parse(sdr["ProdctId"].ToString());
                model.ProductName = sdr["productname"].ToString();
                model.Rate = double.Parse(sdr["rate"].ToString());
                model.Description = sdr["description"].ToString();
                model.CategoryName = sdr["categoryname"].ToString();
                productlist.Add(model);
            }
            return View(productlist);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            product model = new product();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetExam;Integrated Security=True;Connect Timeout=30;";
            connection.Open();
            SqlCommand cm = new SqlCommand();
            cm.Connection = connection;
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            cm.CommandText = "getSingleproduct";
            cm.Parameters.AddWithValue("@productid", SqlDbType.Int).Value = id;
            SqlDataReader sdr = cm.ExecuteReader();
            while (sdr.Read())
            {
             
                model.ProductId = int.Parse(sdr["ProdctId"].ToString());
                model.ProductName = sdr["productname"].ToString();
                model.Rate = double.Parse(sdr["rate"].ToString());
                model.Description = sdr["description"].ToString();
                model.CategoryName = sdr["categoryname"].ToString();
                
            }
            return View(model);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetExam;Integrated Security=True;Connect Timeout=30;";
                connection.Open();
                SqlCommand cm = new SqlCommand();
                cm.Connection = connection;
                cm.CommandType = System.Data.CommandType.StoredProcedure;
                cm.CommandText = "editproduct";
                cm.Parameters.AddWithValue("@productid", SqlDbType.Int).Value = id;
                cm.Parameters.AddWithValue("@productname", SqlDbType.VarChar).Value = collection["ProductName"].ToString();
                cm.Parameters.AddWithValue("@description", SqlDbType.VarChar).Value = collection["Description"].ToString();
                cm.Parameters.AddWithValue("@rate", SqlDbType.Decimal).Value = collection["Rate"].ToString();
                cm.Parameters.AddWithValue("@categoryname", SqlDbType.VarChar).Value = collection["CategoryName"].ToString();


                cm.ExecuteNonQuery();
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
