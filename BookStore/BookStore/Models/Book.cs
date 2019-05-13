namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Author")]
        public string Author { get; set; }

        [BsonElement("Count")]
        public int Count { get; set; }

        [BsonElement("Genre")]
        public List<string> Genre { get; set; }

        [BsonElement("Year")]
        public int Year { get; set; }
    }
}
