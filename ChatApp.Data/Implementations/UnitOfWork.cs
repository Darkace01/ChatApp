using ChatApp.Data.Contracts;
using ChatApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ChatRepo ChatRepo { get; private set; }
        public MessageRepo MessageRepo { get; private set; }
        public UserRepo UserRepo {get; private set;}
        public ChatUserRepo ChatUserRepo {get; private set;}


        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
            this.ChatRepo = new ChatRepo(this._context);
            this.MessageRepo = new MessageRepo(this._context);
            this.UserRepo = new UserRepo(this._context);
            this.ChatUserRepo = new ChatUserRepo(this._context);

        }

        public async Task Save()
        {
            await this._context.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
