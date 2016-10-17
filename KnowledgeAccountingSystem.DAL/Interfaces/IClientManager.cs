using System;
using KnowledgeAccountingSystem.DAL.Models.Identity;

namespace KnowledgeAccountingSystem.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}