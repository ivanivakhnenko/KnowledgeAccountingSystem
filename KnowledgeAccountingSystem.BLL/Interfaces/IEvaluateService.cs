using KnowledgeAccountingSystem.BLL.DTO;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public interface IEvaluateService
    {
        void AddUser(string identityId, string name, string email);
        CategorizedUserDTO GetCategorizedUser(string id);
        CategorizedUserDTO GetCategorizedUser(int? id);
        UserDTO GetUser(int? id);
        void SetUserSkills(UserDTO user);
    }
}