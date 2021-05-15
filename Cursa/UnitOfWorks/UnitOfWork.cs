using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cursa.Interfaces;
using Cursa.Interfaces.UnitOfWorks;
using Cursa.Repositories;
using DataLayer;

namespace Cursa.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
        {
            private readonly EfDbContext _context;
            public UnitOfWork(EfDbContext context)
            {
                _context = context;
                Projects = new ProjectRepository(_context);
                SubProjects = new SubProjectRepository(_context);
            }
            public ISubProjectRepository SubProjects { get; private set; }
            public IProjectRepository Projects { get; private set; }


            public int Complete()
            {
                return _context.SaveChanges();
            }
            public void Dispose()
            {
                _context.Dispose();
            }
        }
    }