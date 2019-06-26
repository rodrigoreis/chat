﻿using Chat.Models;
using Chat.Repositories.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Repositories
{
    public class ConnectionsRepository : IConnectionsRepository
    {
        private readonly Dictionary<string, User> connections
            = new Dictionary<string, User>();

        public void Add(string uniqueID, User user)
        {
            if (!connections.ContainsKey(uniqueID))
                connections.Add(uniqueID, user);
        }

        public string GetUserId(long id)
        {
            return (from con in connections
                    where con.Value.key == id
                    select con.Key).FirstOrDefault();
        }

        public List<User> GetAllUser()
        {
            return (from con in connections
                    select con.Value).ToList();
        }
    }
}
