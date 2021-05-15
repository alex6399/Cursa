using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cursa.Interfaces.GenericRepository;
using DataLayer.Entities;

namespace Cursa.Interfaces
{
    public interface IProjectRepository:IGenericRepository<Project>
    {
        //Task<IEnumerable<Project>> GetProject();
        //Task<IEnumerable<Project>> Details(int? id);

    }
}
