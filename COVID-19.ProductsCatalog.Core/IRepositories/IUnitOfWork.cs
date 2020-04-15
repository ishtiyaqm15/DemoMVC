using System;
using System.Collections.Generic;
using System.Text;

namespace COVID_19.ProductsCatalog.Core.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductsRepository ProductsRepository { get; }

        ILoginRepository LoginRepository { get; }
    }
}
