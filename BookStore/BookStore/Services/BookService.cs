namespace BookStore.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BookStore.Models;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Core;

    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("Bookstore"));
            var database = client.GetDatabase("mongotask-bookstore");
            _books = database.GetCollection<Book>("Books");
        }

        public List<string> GetMoreOne()
        {
            var projection = Builders<Book>.Projection.Expression(p => p.Name);

            return _books
                .Find(book => true)
                .SortBy(book => book.Name)
                .Project(projection)
                .Limit(3)
                .ToList();
        }

        public List<Book> Get()
        {
            return _books.Find(book => true).ToList();
        }

        public Book Get(string id)
        {
            return _books
                .Find<Book>(book => book.Id == id)
                .FirstOrDefault();
        }

        public Book GetWithMax()
        {
            return _books
                .Find<Book>(book => true)
                .SortByDescending(val => val.Count)
                .Limit(1)
                .FirstOrDefault();
        }

        public Book GetWithMin()
        {
            return _books
                .Find<Book>(book => true)
                .SortBy(val => val.Count)
                .Limit(1)
                .FirstOrDefault();
        }

        public List<Book> GetWithoutAuthor()
        {
            return _books
                .Find<Book>(book => string.IsNullOrEmpty(book.Author))
                .ToList();
        }

        public List<string> GetAuthors()
        {
            var projection = Builders<Book>.Projection.Expression(p => p.Author);

            return _books
                .Distinct<string>("Author", Builders<Book>.Filter.Empty)
                .ToList();
        }

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public List<Book> CreateMany(List<Book> books)
        {
            _books.InsertMany(books);
            return books;
        }

        public void IncrementAll()
        {
            var update = new UpdateDefinitionBuilder<Book>().Inc(n => n.Count, 1);

            _books.UpdateMany<Book>(book => true, update);
        }

        public void PushFavorityIfNotExists()
        {
            var update = new UpdateDefinitionBuilder<Book>().AddToSet(b => b.Genre, "Favority");

            _books.UpdateMany<Book>(book => book.Genre.Any(g => g == "Fantasy"), update);
        }

        public void Update(string id, Book bookIn)
        {
            _books.ReplaceOne(book => book.Id == id, bookIn);
        }

        public void Remove(Book bookIn)
        {
            _books.DeleteOne(book => book.Id == bookIn.Id);
        }

        public void Remove(string id)
        {
            _books.DeleteOne(book => book.Id == id);
        }

        public void RemoveAllLessThree()
        {
            _books.DeleteMany(book => book.Count < 3);
        }

        public void RemoveAll()
        {
            _books.DeleteMany(book => true);
        }
    }
}
