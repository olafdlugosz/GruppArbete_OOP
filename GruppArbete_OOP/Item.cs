using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppArbete_OOP
{
    public abstract class Item : IComparable
    {
        private string _title { get; set; }
        private Guid _identifier { get; set; }

        private int _price { get; set; }
        private int _quantity { get; set; }
        private string _type { get; set; }

        public string Title { get => _title; set => _title = value; }
        public Guid Identifier { get => _identifier; set => _identifier = value; }
        public string Type { get => _type; set => _type = value; }
        public int Price { get => _price; set => _price = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }

        public Item(string Title, int Price, int Quantity,string Type) {

            Identifier = Guid.NewGuid();
            this.Title = Title;
            this.Price = Price;
            this.Quantity = Quantity;
            this.Type = Type;
        }
        public Item(string Title, int Price, int Quantity, string Type, Guid Identifier) {
            this.Identifier = Identifier;
            this.Title = Title;
            this.Price = Price;
            this.Quantity = Quantity;
            this.Type = Type;
        }

        public override string ToString() {
            return "Title: " + Title + " " +
                "GUID: " + Identifier + " " +
                "Price: " + Price + "kr" + " " +
                "Quantity: " + Quantity + " ";
        }
        public string LineUpClassPropertiesForStreamReader() {
            return String.Format($"{Title},{Price},{Quantity},{Type},{Identifier}");
        }

        public int CompareTo(object obj)
        {
            Item item = obj as Item;
            return this.Price - item.Price;
        }
    }
    public class Book : Item
    {
        public Book(string Title, int Price, int Quantity, string Type) : base(Title, Price, Quantity, Type) {
        }

        public Book(string Title, int Price, int Quantity, string Type, Guid Identifier) : base(Title, Price, Quantity, Type, Identifier) {
        }
    }
    public class Film : Item
    {
        public Film(string Title, int Price, int Quantity, string Type) : base(Title, Price, Quantity, Type) {
        }

        public Film(string Title, int Price, int Quantity, string Type, Guid Identifier) : base(Title, Price, Quantity, Type, Identifier) {
        }
    }
}
