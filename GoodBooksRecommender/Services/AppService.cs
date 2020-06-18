using Dapper;
using GoodBooksRecommender._Objects;
using GoodBooksRecommender._Queries;
using GoodBooksRecommender.Data;
using GoodBooksRecommender.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoodBooksRecommender.Services
{
    /// <summary>
    /// Service to handle database and data logic
    /// </summary>
    public class AppService:BaseService
    {
        public static string BaseURI => "http://localhost:8080/";

        private bool _isRegistering;
        //public event Action OnChange;
        public event Action OnRefresh;
        //private void NotifyStateChanged() => OnChange?.Invoke();
        private void NotifyStateChanged() => OnRefresh?.Invoke();

        public bool IsRegistering
        {
            get { return _isRegistering; }
            set
            {
                if (_isRegistering != value)
                {
                    _isRegistering = value;
                    NotifyStateChanged();
                }
            }
        }



        public User VerifyLogin(string username, string password)
        {
            password = Encryption.Encrypt(password);
            var collection = _db.GetCollection<User>("Users");
            var builder = Builders<User>.Filter;
            var filter = builder.Eq("UserName", username) & builder.Eq("Password",password);

            var user = collection.Find(filter).FirstOrDefault();

            return user;
        }

        public List<UserBook> GetUserBooks(string userID)
        {            
            var collection = _db.GetCollection<UserBook>("Users");
            var builder = Builders<UserBook>.Filter;
            var filter = builder.Eq("userID", userID) & builder.Exists("bookID");

            var _userbook = collection.Find(filter).ToList();

            return _userbook;
        }

        public Book GetBookByID(string bookID)
        {
            var collection = _db.GetCollection<Book>("Books");
            var builder = Builders<Book>.Filter;
            var filter = builder.Eq("bookID", bookID);

            var _book = collection.Find(filter).FirstOrDefault();

            return _book;
        }

        public void MarkBookRead(UserBook userBook)
        {
            var collection = _db.GetCollection<UserBook>("Users");
            
            collection.InsertOne(userBook);
        }


        public void RemoveUserBook(UserBook userBook)
        {
            var collection = _db.GetCollection<BsonDocument>("Users");            
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("bookID", userBook.BookID) & builder.Eq("userID", userBook.UserID);

            collection.DeleteOne(filter);
        }

        public void RegisterNewUser(User newUser)
        {
            //encrypt the password before storing
            newUser.Password = Encryption.Encrypt(newUser.Password);

            var collection = _db.GetCollection<User>("Users");

            try
            {
                    var document = new User()
                {
                    ID = newUser.ID,
                    Username = newUser.Username,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Phone = newUser.Phone,
                    Email = newUser.Email,
                    Password = newUser.Password                                           
                };

                    collection.InsertOne(document);
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        public List<Book> GetBooks()
        {            
            var collection = _db.GetCollection<Book>("Books");
            var builder = Builders<Book>.Filter;
            var filter = builder.Empty;

            var books = collection.Find(filter).ToList();

            return books;
        }


        public void SaveUserRating(UserRating newRating)
        {            
            var collection = _db.GetCollection<UserRating>("Users");
            var exist = GetBookRating(newRating.BookID, newRating.UserID);
            try
            {                
                if(exist == 0)
                {
                    collection.InsertOne(newRating);
                }
                else
                {
                    var builder = Builders<UserRating>.Filter;
                    var filter = builder.Eq("BookID", newRating.BookID) & builder.Eq("UserID", newRating.UserID) & builder.Exists("Rating");
                    var updateBuilder = Builders<UserRating>.Update;
                    var update = updateBuilder.Set("Rating",newRating.Rating);
                    collection.UpdateOne(filter,update);
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public float GetBookRating(string bookID, string userID)
        {
            var collection = _db.GetCollection<UserRating>("Users");
            var builder = Builders<UserRating>.Filter;
            var filter = builder.Eq("BookID", bookID)& builder.Eq("UserID", userID) & builder.Exists("Rating");

            var _book = collection.Find(filter).FirstOrDefault();
            if(_book == null)
            {
                return 0;
            }
            else
            {
                return _book.Rating;
            }            
        }


        public List<UserRating> GetUserRatings(long userID)
        {
            var collection = _db.GetCollection<UserRating>("Users");
            var builder = Builders<UserRating>.Filter;
            var filter = builder.Eq("UserID", userID) & builder.Exists("Rating");

            var _userRatings = collection.Find(filter).ToList();

            return _userRatings;
        }
        
        public List<Book> GetSimilarBooks(string bookID)
        {
            var uri = BaseURI + "getSimilarBooks/" + bookID;            

            var result = _httpClient.GetAsync(uri);            
            var res = JsonConvert.DeserializeObject<List<SimilarBook>>(result.Result.Content.ReadAsStringAsync().Result);            
            var response = GetBooksByID(res);

            return response;
        }

        public List<Book> GetUserRecommendations(string userID)
        {
            var uri = BaseURI + "getRecommendations/" + userID;

            var result = _httpClient.GetAsync(uri);
            var res = JsonConvert.DeserializeObject<List<SimilarBook>>(result.Result.Content.ReadAsStringAsync().Result);
            var response = GetBooksByID(res);

            return response;
        }


        public List<Book> GetBooksByID(List<SimilarBook> data)
        {
            var similarBooks = new List<Book>();
                 

            foreach (var sim in data)
            {
                var collection = _db.GetCollection<Book>("Books");
                var builder = Builders<Book>.Filter;
                var filter = builder.Eq("bookID", sim.BookID.ToString());
                var _book = collection.Find(filter).FirstOrDefault();
                similarBooks.Add(_book);
            }

            return similarBooks;
        }

    }
}
