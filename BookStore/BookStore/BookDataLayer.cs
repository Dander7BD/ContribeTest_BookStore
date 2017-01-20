using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace BookStore
{
    public class BookDataLayer : IBookstoreService
    {
        private class DataSchema
        {
            public class BookSchema : IBook
            {
                public string Author { get { return this.author; } }

                public decimal Price { get { return this.price; } }

                public string Title { get { return this.title; } }

                public bool InStock { get { return this.inStock > 0; } }

                public bool IsInStock(int num)
                {
                    return num <= this.inStock;
                }

                public int CompareTo(IBook other)
                {
                    if( other is BookSchema )
                    {
                        int result = (other as BookSchema).lastMatchingRelevance.CompareTo(this.lastMatchingRelevance);
                        if( result != 0 ) return result;
                    }
                    {
                        int result = this.title.CompareTo( other.Title );
                        return result != 0 ? result : this.author.CompareTo( other.Author );
                    }
                }

                public string title { private get; set; }
                public string author { private get; set; }
                public decimal price { private get; set; }
                public int inStock { private get; set; }
                private int lastMatchingRelevance = 0;

                internal bool Matches(string searchString)
                {
                    string[] filter = searchString.ToLower().Split( ' ', '\t', '\n' );
                    foreach( string f in filter )
                    {
                        if( this.title.ToLower().Contains( f ) ) ++this.lastMatchingRelevance;
                        if( this.author.ToLower().Contains( f ) ) ++this.lastMatchingRelevance;
                    }
                    return this.lastMatchingRelevance > 0;
                }
            }

            public List<BookSchema> books = new List<BookSchema>();

            public List<IBook> GetAsIBooks()
            {
                List<IBook> result = new List<IBook>(this.books.Count);

                foreach( BookSchema book in this.books )
                    result.Add( book );

                return result;
            }

            public List<IBook> GetAsIBooks(string filter)
            {
                List<IBook> result = new List<IBook>(this.books.Count);

                foreach( BookSchema book in this.books )
                    if(book.Matches(filter)) result.Add( book );

                return result;
            }
        }

        public Task<IEnumerable<IBook>> GetBooksAsync()
        {
            Task < IEnumerable < IBook >> task = new Task<IEnumerable<IBook>>( delegate
            {
                try
                {
                    DataSchema data;
                    HttpWebRequest request = WebRequest.Create( "http://www.contribe.se/arbetsprov-net/books.json" ) as HttpWebRequest;

                    using( WebResponse response = request.GetResponse() )
                    {
                        Stream stream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        data = JsonConvert.DeserializeObject<DataSchema>( reader.ReadToEnd() );
                    }
                    List<IBook> result = data.GetAsIBooks();
                    result.Sort();

                    return result;
                }
                catch( HttpException )
                {
                    return new List<IBook>();
                }
            } );
            task.Start();
            return task;
        }

        public Task<IEnumerable<IBook>> GetBooksAsync(string searchString)
        {
            Task < IEnumerable < IBook >> task = new Task<IEnumerable<IBook>>( delegate
            {
                try
                {
                    DataSchema data;
                    HttpWebRequest request = WebRequest.Create( "http://www.contribe.se/arbetsprov-net/books.json" ) as HttpWebRequest;

                    using( WebResponse response = request.GetResponse() )
                    {
                        Stream stream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        data = JsonConvert.DeserializeObject<DataSchema>( reader.ReadToEnd() );
                    }
                    List<IBook> result = data.GetAsIBooks(searchString);
                    result.Sort();
                    return result;
                }
                catch(HttpException)
                {
                    return new List<IBook>();
                }                
            } );
            task.Start();
            return task;
        }
    }
}