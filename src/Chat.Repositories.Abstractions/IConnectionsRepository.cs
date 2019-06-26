using Chat.Models;
using System.Collections.Generic;

namespace Chat.Repositories.Abstractions
{
    public interface IConnectionsRepository
    {
        void Add(string uniqueID, User user);
        string GetUserId(long id);
        List<User> GetAllUser();
    }
}
