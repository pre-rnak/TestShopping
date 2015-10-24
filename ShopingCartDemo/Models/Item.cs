using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopingCartDemo.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Offer { get; set; }
        public string ImagePath { get; set; }
    }

    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int ItemId { get; set; }
    }

    public class Login
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}