using System;

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
        /// <summary>
        /// Constructor for creating new items
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Price"></param>
        /// <param name="Quantity"></param>
        /// <param name="Type"></param>
        public Item(string Title, int Price, int Quantity,string Type) {

            Identifier = Guid.NewGuid(); // <= creates a new Guid.
            this.Title = Title;
            this.Price = Price;
            this.Quantity = Quantity;
            this.Type = Type;
        }
        /// <summary>
        /// 2nd Constructor to be used when loading from a saved .dat file
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Price"></param>
        /// <param name="Quantity"></param>
        /// <param name="Type"></param>
        /// <param name="Identifier">GUID</param>
        public Item(string Title, int Price, int Quantity, string Type, Guid Identifier) { // <= uses Guid from the saved .dat
            this.Identifier = Identifier;
            this.Title = Title;
            this.Price = Price;
            this.Quantity = Quantity;
            this.Type = Type;
        }
        /// <summary>
        /// Overrides base method to better suit out purposes.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return "Type: " + Type + " " + 
                "Title: " + Title + " " +
                "GUID: " + Identifier + " " +
                "Price: " + Price + " kr" + " " +
                "Quantity: " + Quantity + " ";
        }
        /// <summary>
        /// Formatted string to be used in our custom made Serializer.
        /// </summary>
        /// <returns></returns>
        public string LineUpClassPropertiesForStreamReader() {
            return String.Format($"{Title},{Price},{Quantity},{Type},{Identifier}");
        }
        /// <summary>
        /// Method from inherited interface to be used together with LINQ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            try
            {
            Item item = obj as Item; //TODO There might be a null exception if object is not an Item. HANDLE EXCEPTION by throwing a custom Exception!
            return this.Price - item.Price; 
            }
            catch
            {
                throw new InvalidCastException();
            }
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
