using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Util;

namespace KnowledgeAccountingSystem.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// Registers new user to Identity database
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        Task<OperationDetails> Create(UserIdentityDTO userDto);
        /// <summary>
        /// Performs the autentication of user based on creditnails
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        Task<ClaimsIdentity> Authenticate(UserIdentityDTO userDto);
        /// <summary>
        /// Sets Admin account and roles on database drop
        /// </summary>
        /// <param name="adminDto"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        Task SetInitialData(UserIdentityDTO adminDto, List<string> roles);
        /// <summary>
        /// Gets all users from Identity database
        /// </summary>
        /// <returns></returns>
        List<UserIdentityDTO> GetAll();
        /// <summary>
        /// Gets all roles from identity database
        /// </summary>
        /// <returns></returns>
        List<RoleDTO> GetRoles();
        /// <summary>
        /// Reassigns user to a new role, attached to it
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        Task<UserIdentityDTO> Assign(UserIdentityDTO map);
    }
}
