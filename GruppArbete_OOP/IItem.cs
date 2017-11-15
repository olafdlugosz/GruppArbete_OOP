using System;

namespace GruppArbete_OOP
{
    interface IItem
    {
        Guid Identifier { get; set; }
        int Price { get; set; }
        int Quantity { get; set; }
        string Title { get; set; }
        string Type { get; set; }

        void ChangePrice(double Price);
        string LineUpClassPropertiesForStreamReader();
        void Restock(int Quantity);
        void Sell(int Quantity, int Price);
        string ToString();
    }
}