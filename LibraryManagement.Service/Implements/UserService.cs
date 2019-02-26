using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Config;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using LibraryManagement.Services.Implements;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IOptions<AppSettings> _config;
        private readonly byte DefaultRole = 3;
        public UserService(IUnitOfWork unitOfWork, IOptions<AppSettings> config) : base(unitOfWork)
        {
            this._config = config;
        }
        public async Task<IEnumerable<User>> GetAllInclude()
        {
            var includes = new List<string>(new string[] { "IdRoleNavigation" });
            return await _repository.GetAllAsync(includes);
        }
        public UserAuth Authenticate(string userName, string password)
        {
            //TODO : hash password here
            var passwordEncrypt = Auth.EncryptString(_config.Value.SecretKey, password);
            var includes = new List<string>(new string[] { "IdRoleNavigation" });
            var user = _repository.GetAsync(o => o.UserName == userName && o.Password == passwordEncrypt, includes);

            if (user.Result != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.Value.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,user.Result.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Result.IdRoleNavigation.RoleName),

                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return new UserAuth()
                {

                    UserName = user.Result.UserName,
                    Name = user.Result.Name,
                    Phone = user.Result.Phone,
                    YearOfBirth = user.Result.YearOfBirth,
                    Token = tokenString
                };
            }

            return null;

        }

        public async Task<ResultModel<User>> Register(User user)
        {
            user.Password = Auth.EncryptString(_config.Value.SecretKey, user.Password);
            user.IdRole = user.IdRole != null ? user.IdRole : DefaultRole;
            return await Insert(user);
        }
    }
}
