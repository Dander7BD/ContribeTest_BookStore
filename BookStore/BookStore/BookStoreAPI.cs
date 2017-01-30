using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore
{
    public class BookStoreAPI
    {
        public static IBookstoreService GetService()
        {
            return new BookDataLayer();
        }
    }
}