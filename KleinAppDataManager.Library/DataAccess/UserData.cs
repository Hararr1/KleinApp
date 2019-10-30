using KleinAppDataManager.Library.Internal.DataAccess;
using KleinAppDataManager.Library.Models;
using System.Collections.Generic;

namespace KleinAppDataManager.Library.DataAccess
{
    public class UserData
    {

        public List<UserModel> GetUserById(string UserId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { UserId = UserId };
            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "KleinAppData");
            return output;
        }

        public void SaveUser(string UserId, string FirstName, string LastName, string Email)
        {
            SqlDataAccess sql = new SqlDataAccess();
            var p = new { UserId = UserId, FirstName = FirstName, LastName = LastName, EmailAddress = Email };
            sql.SaveData<UserModel, dynamic>("dbo.spUserRegister", p, "KleinAppData");
        }


    }
}
