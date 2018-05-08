using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using tp1.Models;

namespace tp1.Repositories
{
    public class UnitOfWork
    {
        public UnitOfWork()
        {
            this.context = new MyDbContext();
        }
        private readonly MyDbContext context;

        private GenericRepository<Post> clientsRepository;
        public GenericRepository<Post> ClientsRepository
        {
            get
            {
                if (this.clientsRepository == null)
                {
                    this.clientsRepository = new GenericRepository<Post>(this.context);
                }
                return this.clientsRepository;
            }
        }

        private GenericRepository<User> salesRepository;
        public GenericRepository<User> SalesRepository
        {
            get
            {
                if (this.salesRepository == null)
                {
                    this.salesRepository = new GenericRepository<User>(this.context);
                }
                return this.salesRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}