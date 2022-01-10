using System;
using Racing.DAL.Context;

namespace Racing.DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly DBRacingContext _context;

        public UnitOfWork(DBRacingContext racingContext)
        {
            _context = racingContext;
        }

        public UnitOfWork()
        {
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        // public object PilotRepository { get; set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}