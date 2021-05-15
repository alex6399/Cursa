using System;

namespace Cursa.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }
        ISubProjectRepository SubProjects { get; }
        int Complete();
    }
}