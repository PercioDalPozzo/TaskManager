﻿using Domain.Entity;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public void Add(User user)
        {
            
        }

        public User GetByLogin(string login)
        {
            //mock
            return new User("Pércio","");
        }
    }
}
