using System;
using System.Threading.Tasks;
using KnowledgeAccountingSystem.DAL.Identity;

namespace KnowledgeAccountingSystem.DAL.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}