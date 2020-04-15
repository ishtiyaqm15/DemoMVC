using COVID_19.ProductsCatalog.Core.Common;
using COVID_19.ProductsCatalog.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVID_19.ProductsCatalog.Core.Repositories
{
    public class BaseRepository : ConnectionManager
    {
        #region Constructors
        public BaseRepository()
            : base()
        {
        }

        public BaseRepository(string ConnectionString)
            : base(ConnectionString)
        {
        }
        #endregion Constructors

    }
}
