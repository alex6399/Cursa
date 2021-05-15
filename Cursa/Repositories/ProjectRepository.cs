using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cursa.Interfaces;
using DataLayer.Entities;
using Cursa.Repositories.Base;
using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace Cursa.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(EfDbContext context) : base(context)
        {
        }
        //public async Task<IEnumerable<Project>> GetProject()
        //{
        //    return await _context.Projects.Include(p => p.Owner).ToListAsync();
        //}

        //public Task<IEnumerable<Project>> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var project = await _context.Projects
        //        .Include(p => p.Owner)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(project);
        //}

    }
}
