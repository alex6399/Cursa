using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cursa.Interfaces;
using Cursa.Repositories.Base;
using DataLayer;
using DataLayer.Entities;

namespace Cursa.Repositories
{
    public class SubProjectRepository: GenericRepository<SubProject>, ISubProjectRepository
    {
        public SubProjectRepository(EfDbContext context) : base(context)
        {
        }
    }
}
