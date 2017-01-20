using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public interface IBookstoreService
    {
        Task<IEnumerable<IBook>> GetBooksAsync();
        Task<IEnumerable<IBook>> GetBooksAsync(string searchString);
    }
}
