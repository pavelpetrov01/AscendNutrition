using Microsoft.AspNetCore.Mvc;

namespace AscendNutrition.Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsGuidValid(string? id, ref Guid parsedGuid)
        {

            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }


            bool isGuidValid = Guid.TryParse(id, out parsedGuid);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }
    }
}
