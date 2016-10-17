namespace KnowledgeAccountingSystem.BLL.Interfaces
{
    public interface IServiceCreator
    {
        /// <summary>
        /// Creates an instance of IUserService implementation
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        IUserService CreateUserService(string connection);
    }
}