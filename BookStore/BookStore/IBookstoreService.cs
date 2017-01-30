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
        Task<IBook> GetBookAsync(string id, int quantity);
        Task<IEnumerable<IBook>> GetBookAsync(IDictionary<string, int> idQuantity);
    }
}
