using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBooksRecommender._Queries
{
    public class AppQueries
    {
        public static string IsEmailAvailable = @"Begin
            SELECT CASE WHEN EXISTS (
            SELECT *
            FROM dbo.Users U
            WHERE (U.EmailAddress = @Email AND U.Password = @SecurePassword)
            )
            THEN (SELECT UserID FROM dbo.Users U WHERE U.EmailAddress = @Email AND U.Password = @SecurePassword)
            ELSE 0 END
            End";


        public static string GetUserDetails = @"SELECT UserID,FirstName,LastName FROM dbo.Users where UserID = @UserID ";
    }
}
