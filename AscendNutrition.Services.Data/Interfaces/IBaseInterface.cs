using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AscendNutrition.Services.Data.Interfaces
{
    public interface IBaseInterface
    {
        bool IsGuidValid(string? id, ref Guid validGuid);
    }
}
