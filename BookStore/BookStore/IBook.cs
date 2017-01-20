using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public interface IBook : IComparable<IBook>
    {
        string Title { get; }
        string Author { get; }
        decimal Price { get; }
        bool InStock { get; }

        bool IsInStock(int num);
    }
}
