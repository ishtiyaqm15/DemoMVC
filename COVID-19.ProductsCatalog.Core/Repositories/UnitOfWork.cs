using COVID_19.ProductsCatalog.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVID_19.ProductsCatalog.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }



        // Add other Iframes here
        // This will be called from controller default constructor
        public UnitOfWork()
        {
            ProductsRepository = new ProductsRepository();
            LoginRepository = new LoginRepository();
        }


        public UnitOfWork(List<Repositories> repositories)
        {

            if (repositories.Contains(Repositories.ProductsRepo))
                ProductsRepository = new ProductsRepository();
            if (repositories.Contains(Repositories.LoginRepo))
                LoginRepository = new LoginRepository();
        }


        // This will be created from test project and passed on to the
        // controllers parameterized constructors

        public IProductsRepository ProductsRepository
        {
            get;
            private set;
        }

        public ILoginRepository LoginRepository
        {
            get;
            private set;
        }

    }
}
