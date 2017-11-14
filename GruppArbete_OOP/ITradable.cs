using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppArbete_OOP
{
    interface ITradable
    {
        int Price { get; set; }
        int Quantity { get; set; }
        void Sell(int Quantity, int Price);
        void Restock(int Quantity);
    }
}
