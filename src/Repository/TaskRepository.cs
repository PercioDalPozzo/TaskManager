﻿using Domain.Entity;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TaskRepository : ITaskRepository
    {

        IEnumerable<Domain.Entity.Task> ITaskRepository.GetAll()
        {
            //Mock
            return new List<Domain.Entity.Task>()
            {
                new Domain.Entity.Task(Guid.NewGuid(), "", "", DateTime.Now),
                new Domain.Entity.Task(Guid.NewGuid(), "", "", DateTime.Now),
                new Domain.Entity.Task(Guid.NewGuid(), "", "", DateTime.Now),
                new Domain.Entity.Task(Guid.NewGuid(), "", "", DateTime.Now)
            };
        }

        public void Add(Domain.Entity.Task task)
        {
            
        }

        public void Delete(Guid id)
        {
            
        }


        public Domain.Entity.Task GetById(Guid id)
        {
            //Mock
            return new Domain.Entity.Task(Guid.NewGuid(), "", "", DateTime.Now);
        }

        public void Update(Domain.Entity.Task task)
        {
            
        }
    }
}
