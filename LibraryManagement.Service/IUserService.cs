using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IUserService: IBaseService<User>
    {
        ResultModel<UserAuth> Authenticate(String userName, String password);
        Task<ResultModel<User>> Register(User user);
        Task<IEnumerable<User>> GetAllInclude();
    }

    public class UserAuth
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string YearOfBirth { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }

    }

}
