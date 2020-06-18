using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBooksRecommender._Objects
{
    [BsonIgnoreExtraElements] // ignore fields not defined in this class
    public class UserRating
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("UserID")]
        public string UserID { get; set; }
        [BsonElement("BookID")]
        public string BookID { get; set; }
        [BsonElement("Rating")]
        public float Rating { get; set; }
    }
}
