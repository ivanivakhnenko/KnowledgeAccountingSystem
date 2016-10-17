using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Util;
using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public class UserService : IUserService
    {
        IIdentityUnitOfWork Database { get; }

        public UserService(IIdentityUnitOfWork uow)
        {
            Database = uow;
        }
        
        public async Task<OperationDetails> Create(UserIdentityDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role.Name);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
        }

        public async Task<ClaimsIdentity> Authenticate(UserIdentityDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserIdentityDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public List<UserIdentityDTO> GetAll()
        {
            return Mapper.Map(Database.UserManager.Users.ToList(), Database.RoleManager.Roles.ToList());
        }

        public List<RoleDTO> GetRoles()
        {
            return Mapper.Map(Database.RoleManager.Roles.ToList());
        }

        public async Task<UserIdentityDTO> Assign(UserIdentityDTO user)
        {
            var dbuser = Database.UserManager.Users.First(x => x.Id == user.IdentityId);

            dbuser.Roles.Clear();
            dbuser.Roles.Add(new IdentityUserRole { RoleId = user.Role.Id });

            await Database.SaveAsync();

            return Mapper.Map(dbuser, Database.RoleManager.Roles.ToList());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}