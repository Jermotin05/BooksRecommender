using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBooksRecommender._Objects
{    
    public class Book
    {

        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("bookID")]
        public string BookID { get; set; } //Contains the unique ID for each book/series
        [BsonElement("title")]
        public string Title{ get; set; } //contains the titles of the books
        [BsonElement("authors")]
        public string Authors { get; set; } //contains the author of the particular book
        [BsonElement("average_rating")]
        public string AverageRating { get; set; } //the average rating of the books, as decided by the users
        [BsonElement("isbn")]
        public string ISBN { get; set; } //ISBN(10) number, tells the information about a book - such as edition and publisher
        [BsonElement("isbn13")]
        public string ISBN13 { get; set; } //The new format for ISBN, implemented in 2007. 13 digits
        [BsonElement("language_code")]
        public string Language { get; set; } //Tells the language for the books
        [BsonElement("  num_pages")]//**Needs the 2 spaces in front**
        public string NumPages { get; set; } // Contains the number of pages for the book
        [BsonElement("ratings_count")]
        public string Ratings_Count { get; set; } // Contains the number of ratings given for the book
        [BsonElement("text_reviews_count")]
        public string TextReviews_count { get; set; } // Has the count of reviews left by users
        [BsonElement("publication_date")]
        public string PublicationDate { get; set; } //The data of the publication
        [BsonElement("publisher")]
        public string Publisher { get; set; } // Tells who the publisher was
    }
}
