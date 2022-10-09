using RandomDice_Login.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

using MySql.Data.MySqlClient;
using System;

namespace RandomDice_Login.Services
{
    public class BookService_Maira
    {
        private string m_connectionString;
        private BookService_Maira() { }
        public BookService_Maira(string connectionString)
        {
            m_connectionString = connectionString;
        }

        public Book_Maria GetBook(string id)
        {
            Book_Maria rt = new Book_Maria();
            string SQL = "SELECT * FROM books WHERE id='" + id + "'";
            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(SQL, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rt.id = Convert.ToString(reader["id"]);
                    rt.bookName = Convert.ToString(reader["bookName"]);
                    rt.price = Convert.ToInt32(reader["price"]);
                    rt.category = Convert.ToString(reader["category"]);
                    rt.author = Convert.ToString(reader["id"]);

                }
                //using (var reader = )
                //{
                //    reader.Read();
                //    str = reader["id"];

                //}
                reader.Close();
                conn.Close();
            }
            return rt;
        }

        public bool InsertBook(Book_Maria book)
        {
            bool rt = true;
            string SQL = string
                .Format("INSERT INTO Books VALUES ('{0}', '{1}', {2}, '{3}', '{4}')",
                book.id, book.bookName, book.price, book.category, book.author);
            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(SQL, conn);
                    
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("Insert success");
                    }
                    else
                    {
                        Console.WriteLine("Insert fail");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("DB connection Fial");
                    Console.WriteLine(e.ToString());
                    rt = false;
                }
                finally
                {
                    conn.Close();
                }
                return rt;
            }
        }
    }
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}
