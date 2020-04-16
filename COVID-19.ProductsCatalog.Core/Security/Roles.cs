using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID_19.ProductsCatalog.Core.Security
{
    public enum Roles
    {
        [Description("Admin")]
        Admin,
        [Description("Content Contributors")]
        ContentContributors,
        [Description("Viewers")]
        Viewers
    }
}
