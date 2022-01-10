using System;

namespace Racing.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        public void Save();
    }
}