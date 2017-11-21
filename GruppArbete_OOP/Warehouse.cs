using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Controls;

namespace GruppArbete_OOP
{

    public class Warehouse
    {
        public Dictionary<Guid, Item> WarehouseStorage;
        public List<Book> BookList;
        public List<Film> FilmList;
        string FilePath;

        public Warehouse()
        {
            WarehouseStorage = new Dictionary<Guid, Item>();
            BookList = new List<Book>();
            FilmList = new List<Film>();
            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //generic desktop filepath
        }
        /// <summary>
        /// Custom made Serializer. SaveData() is overloaded and can be used with different input parameters.
        /// </summary>
        /// <param name="WarehouseStorage">The main Warehouse Storage dictionary.</param>
        public void SaveData(Dictionary<Guid, Item> WarehouseStorage)
        {
            using (StreamWriter writer = new StreamWriter($"{FilePath}//WarehouseDatabase.dat", false))
            {
                foreach (var item in WarehouseStorage)
                {
                    writer.WriteLine(item.Value.LineUpClassPropertiesForStreamReader()); //<= writes a specifically formatted string for the Serializer.(See Item Class)
                }
            }
        }
        public void SaveData(List<Book> BookList)
        {
            using (StreamWriter writer = new StreamWriter($"{FilePath}//BookList.txt", false))
            {
                foreach (var item in BookList)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }
        public void SaveData(List<Film> FilmList)
        {
            using (StreamWriter writer = new StreamWriter($"{FilePath}//FilmList.txt", false))
            {
                foreach (var item in FilmList)
                {
                    writer.WriteLine(item.ToString());
                }
            }

        }
        /// <summary>
        /// Our custom made Serializer. Streamreads specifically formatted strings via en While Loop.
        /// The string is then divided at "," and the fields are then thrown into an array.
        /// The values in the fields correspond to class properties and can therefore be used to create instances
        /// of the corresponding objects. Objects are later added to their respective Lists and the main Dictionary.
        /// </summary>
        public void LoadData()
        {
            WarehouseStorage.Clear();
            BookList.Clear();
            FilmList.Clear();
            using (StreamReader reader = new StreamReader($"{FilePath}//WarehouseDatabase.dat"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var fields = line.Split(new[] { ',' });
                    if (fields[3] == "Book")
                    {
                        Book book = new Book(fields[0], int.Parse(fields[1]), int.Parse(fields[2]), fields[3], Guid.Parse(fields[4]));
                        WarehouseStorage.Add(Guid.Parse(fields[4]), book);
                        BookList.Add(book);
                    }
                    if (fields[3] == "Film")
                    {
                        Film film = new Film(fields[0], int.Parse(fields[1]), int.Parse(fields[2]), fields[3], Guid.Parse(fields[4]));
                        WarehouseStorage.Add(Guid.Parse(fields[4]), film);
                        FilmList.Add(film);

                    }
                }
            }
        }

        public List<Item> GetObjectType(ComboBox comboBox)
        {
            if (comboBox.SelectedItem.ToString() == "Book")
                return BookList.ToList<Item>();
            else if (comboBox.SelectedItem.ToString() == "Film")
                return FilmList.ToList<Item>();
            else
                return null;
        }

        public List<Item> PerformSearch(List<Item> searchList, object type, string name, string price, string quantity, string guid)
        {
            var resultList = searchList
                .Where(item => item.Type.ToLower().Contains(type.ToString().ToLower())
                && item.Title.ToLower().Contains(name.ToLower())
                && item.Price.ToString().Contains(price)
                && item.Quantity.ToString().Contains(quantity)
                && item.Identifier.ToString().ToLower().Contains(guid.ToLower()))
                .OrderBy(item => item)
                .Select(item => item).ToList();

            return resultList;
        }

        public void RemoveItems(Item itemToRemove)
        {

            WarehouseStorage.Remove(itemToRemove.Identifier);

            switch (itemToRemove.Type)
            {
                case "Book":
                    {
                        try
                        {
                            var item = BookList.SingleOrDefault(i => i.Identifier == itemToRemove.Identifier);
                            if (item != null)
                            {
                                BookList.Remove(item);
                            }
                        }
                        catch (InvalidOperationException exception)
                        {
                            Console.WriteLine(exception.Message);
                        }

                    }
                    break;

                case "Film":
                    {
                        try
                        {
                            var item = FilmList.SingleOrDefault(i => i.Identifier == itemToRemove.Identifier);
                            if (item != null)
                            {
                                FilmList.Remove(item);
                            }

                        }
                        catch (InvalidOperationException exception)
                        {
                            Console.WriteLine(exception.Message);
                        }

                    }
                    break;

                default:
                    break;
            }
        }

        public Item AddItems(string title, int price, int quantity, string type)
        {
            if (type == "Book")
            {
                Book book = new Book(title, price, quantity, type);
                WarehouseStorage.Add(book.Identifier, book);
                BookList.Add(book);
                return book;
            }
            else if (type == "Film")
            {
                Film film = new Film(title, price, quantity, type);
                WarehouseStorage.Add(film.Identifier, film);
                FilmList.Add(film);
                return film;
            }

            return null;
        }
        /// <summary>
        /// Overloaded methods to be used to print whole warehouse storage, a list of specific items or a single item.
        /// </summary>
        public void PrintToFile()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter writer = new StreamWriter($"{filePath}//WarehouseStorage.txt", false))
            {
                foreach (var item in WarehouseStorage)
                {
                    writer.WriteLine(item.ToString());
                }
            }

        }
        public void PrintToFile(List<Item> list)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter writer = new StreamWriter($"{filePath}\\{list}.txt", false))
            {
                foreach (var item in list)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }
        public void PrintToFile(Item item)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter writer = new StreamWriter($"{filePath}\\{item.Title}.txt", false))
                writer.WriteLine(item.ToString());
        }
        /// <summary>
        /// Overloaded methods mainly to be used as printlining functions to assist the developer.
        /// </summary>
        public void PrintToScreen()
        {
            foreach (var item in WarehouseStorage)
            {
                item.Value.ToString();
            }
        }
        public void PrintToScreen(List<Item> List)
        {
            List.ForEach(item => item.ToString());
        }
        public void PrintToScreen(Item item)
        {
            item.ToString();
        }
    }
}
