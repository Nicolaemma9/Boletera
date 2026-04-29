using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Boletera.Model
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);

        void Add(UserModel userModel);

        void Update(UserModel userModel);

        void Delete(UserModel userModel);

        UserModel GetByUername(string username);

        IEnumerable<UserModel> GetAllUsers();

        bool EmailExiste(string email);

    }
}
