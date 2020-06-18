using GoodBooksRecommender._Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBooksRecommender.Pages
{
    //Base class that every component has access to
    public class Base : BaseComponent
    {
        public static User UserDetails{ get; set; }
        public static string UserID { get; set; }
        public static string CurrentTab { get; set; }
        public static bool IsRegistering { get; set; }
        public static List<Book> Books { get; set; }
        public static List<UserBook> UserBooks { get; set; }
        public static List<Book> RecommendedBooks { get; set; }

        public bool CheckIfEmailIsAvailable(string email)
        {
            //Change this to look into the db and check for emails
            if (email != null && email != "")
                return true;
            else
                return false;
        }
        public bool IsEmailValidFormat(string email)
        {
            //Change this to look into the db and check for emails
            if (email != null && email.Contains("@"))
                return true;
            else
                return false;
        }

        public bool IsPasswordValidFormat(string password)
        {
            //Change this to look into the db and check for emails
            if (password != null && password != "")
                return true;
            else
                return false;
        }


        public bool IsPhoneValidFormat(string phone)
        {
            //Change this to look into the db and check for emails
            if (phone != null && phone != "")
                return true;
            else
                return false;
        }
    }
}
