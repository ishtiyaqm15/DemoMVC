using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID_19.ProductsCatalog.Core.Common
{
    public class AppSettings
    {
        #region local Constants
        private const string environment = "Environment";
        #endregion

        #region App Variables
        public static string Environment
        {
            get
            {
                return ConfigurationManager.AppSettings[environment];
            }
        }
        #endregion  
    }
}