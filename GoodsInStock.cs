using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursova_c_sharp
{
    internal class GoodsInStock
    {
        public string name { get; set; }
        public double price { get; set; }
        public DateTime manufactureDate { get; set; }
        public string certifycateOfQuality { get; set; }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime ManufactureDate
        {
            get { return ManufactureDate; }
            set { ManufactureDate = value; }
        }

        public string CertifycateOfQuality
        {
            get { return certifycateOfQuality; }
            set { certifycateOfQuality = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        //constructor
        public GoodsInStock(string name, double price, DateTime ManufactureDate, string certificateOfQuality)
        {
            Name = name;
            Price = price;
            ManufactureDate = manufactureDate;
            CertifycateOfQuality = certificateOfQuality;
        }

        //copy constructor
        public GoodsInStock(GoodsInStock other)
        {
            Name = other.Name;
            Price = other.Price;
            ManufactureDate = other.ManufactureDate;
            CertifycateOfQuality = other.CertifycateOfQuality;
        }

        //overload
        public static GoodsInStock operator +(GoodsInStock good, double amount)
        {
            good.Price += amount;
            return good;
        }

        public static GoodsInStock operator -(GoodsInStock good, double amount)
        {
            good.Price -= amount;
            return good;
        }

        //getters and setters
        //public void SetName(string name) { Name = name; }
        //public void SetManufacturingDate(DateTime manufacturingDate) { ManufacturingDate = manufacturingDate; }
        //public void SetCertifycateOfQuality(string c) { CertifycateOfQuality = c; }
        //public void SetPrice(double price) { Price = price; }

        // ToString method for displaying the object
        public override string ToString()
        {
            return $"Name: {Name}, Price: {Price}, Manufacturing Date: {ManufactureDate}, Certificate of Quality: {CertifycateOfQuality}";
        }

        // Validation method
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name) && Price >= 0;
        }

        // virtual
        public virtual double CalculatePrice()
        {
            return price;
        }

        ~GoodsInStock() { }
    }
}
