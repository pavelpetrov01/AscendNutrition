using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendNutrition.Services.Data.Interfaces;

namespace AscendNutrition.Services.Data
{
    public class BaseService : IBaseInterface
    {
        public bool IsGuidValid(string? id, ref Guid validGuid)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            bool isGuidValid = Guid.TryParse(id, out validGuid);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }
    }
}
