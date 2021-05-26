using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zedcrest.DocumentManager.Infrastructure.Utilities
{
    public static class ReferenceGenerator
    {
        public static string Generate()
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToUpper(); 
        }
    }
}
