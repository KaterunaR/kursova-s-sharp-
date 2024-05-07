using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.AxHost;

namespace store_c_sharp
{
    internal class GoodsInStock
    {
        protected string Name { get; set; }
        protected DateTime ManufactureDate { get; set; }
        protected string QualityCertificate { get; set; }
        public double Price { get; set; }

        // Constructor
        public GoodsInStock(string name, DateTime manufactureDate, string qualityCertificate, double price)
        {
            Name = name;
            ManufactureDate = manufactureDate;
            QualityCertificate = qualityCertificate;
            Price = price;
        }

        // Copy constructor
        public GoodsInStock(GoodsInStock other)
        {
            Name = other.Name;
            ManufactureDate = other.ManufactureDate;
            QualityCertificate = other.QualityCertificate;
            Price = other.Price;
        }


        // Overloaded operators
        public static GoodsInStock operator +(GoodsInStock item, double amount)
        {
            item.Price += amount;
            return item;
        }

        public static GoodsInStock operator -(GoodsInStock item, double amount)
        {
            item.Price -= amount;
            return item;
        }

        // virtual method
        public virtual double GetPrice()
        {
            return Price;
        }

    }

    internal class GoodsInStore : GoodsInStock
    {
        public double Markup { get; set; }
        public DateTime ExpiryDate { get; set; }

        public GoodsInStore(string name, DateTime manufactureDate, string qualityCertificate, double price, double markup, DateTime expiryDate)
            : base(name, manufactureDate, qualityCertificate, price)
        {
            Markup = markup;
            Price = GetPrice();
            ExpiryDate = expiryDate;
        }

        // Properties for inherited fields
        public string name => Name;
        public DateTime manufactureDate => ManufactureDate;
        public string qualityCertificate => QualityCertificate;


        // override virtual method
        public override double GetPrice()
        {
            return Price + (Price * Markup / 100);
        }
    }

    internal class Store : IEnumerable<GoodsInStore>
    {
        private List<GoodsInStore> goodsInStore;

        public Store()
        {
            goodsInStore = new List<GoodsInStore>();
        }

        public void AddItem(GoodsInStore item)
        {
            goodsInStore.Add(item);
        }

        public void DeleteItem(GoodsInStore item)
        {
            goodsInStore.Remove(item);
        }

        public List<GoodsInStore> GetAllItems()
        {
            return goodsInStore;
        }


        public double CalculateProfit()
        {
            double profit = 0;
            foreach (var item in goodsInStore)
            {
                profit += item.Price - (item.Price / ((100 + item.Markup) / 100));
            }
            return profit;
        }


        public void WriteToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (GoodsInStore item in goodsInStore)
                    {
                        writer.WriteLine($"Name: {item.name}");
                        writer.WriteLine($"Manufacture Date: {item.manufactureDate}");
                        writer.WriteLine($"Quality Certificate: {item.qualityCertificate}");
                        writer.WriteLine($"Price: {item.Price}");
                        writer.WriteLine($"Markup: {item.Markup}");
                        writer.WriteLine($"Expiry Date: {item.ExpiryDate}");
                        writer.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void AddItemsFromFile(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    string[] separator = { ": " };
                    string[] fields = new string[2];

                    string name = "";
                    DateTime manufactureDate = DateTime.MinValue;
                    string qualityCertificate = "";
                    double price = 0;
                    double markup = 0;
                    DateTime expiryDate = DateTime.MinValue;

                    while ((line = reader.ReadLine()) != null)
                    {
                        fields = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                        if (fields.Length == 2)
                        {
                            string fieldName = fields[0].Trim();
                            string value = fields[1].Trim();

                            switch (fieldName)
                            {
                                case "Name":
                                    name = value;
                                    break;
                                case "Manufacture Date":
                                    manufactureDate = DateTime.Parse(value);
                                    break;
                                case "Quality Certificate":
                                    qualityCertificate = value;
                                    break;
                                case "Price":
                                    price = double.Parse(value);
                                    price -= (price / 3);
                                    break;
                                case "Markup":
                                    markup = double.Parse(value);
                                    break;
                                case "Expiry Date":
                                    expiryDate = DateTime.Parse(value);


                                    GoodsInStore newItem = new GoodsInStore(name, manufactureDate, qualityCertificate, price, markup, expiryDate);
                                    goodsInStore.Add(newItem);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //повертає об'єкт, який можна використовувати для послідовного перебору елементів
        public IEnumerator<GoodsInStore> GetEnumerator()
        {
            foreach (var item in goodsInStore)
            {
                yield return item;
            }
        }

        //просто повертає результат виклику методу GetEnumerator() для коректної роботи
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}