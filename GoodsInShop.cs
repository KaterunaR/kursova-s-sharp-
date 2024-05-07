using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace kursova_c_sharp
{
    internal class GoodsInShop : GoodsInStock
    {
        public double Markup { get; set; }
        public DateTime ExpirationDate { get; set; }


        public GoodsInShop(string name, double price, DateTime ManufactureDate, string certificateOfQuality, double markup, DateTime expirationDate)
            : base(name, price, ManufactureDate, certificateOfQuality)
        {
            Markup = markup;
            ExpirationDate = expirationDate;
        }

        // setters
        public void SetMarkup(double markup) { Markup = markup; }
        public void SetExpirationDate(DateTime expirationDate) { ExpirationDate = expirationDate; }


        // Override ToString method to include new fields
        public override string ToString()
        {
            double finalPrice = Price * (1 + Markup);
            return $"Name: {Name}, Price: {finalPrice}";
        }

        // Destructor
        ~GoodsInShop() { }
    }
}
