using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboPropertyTypes();

        IEnumerable<SelectListItem> GetComboAssistents();

        IEnumerable<SelectListItem> GetComboRoles();
        IEnumerable<SelectListItem> GetComboDevices();
        IEnumerable<SelectListItem> GetComboBrands();
        IEnumerable<SelectListItem> GetComboMovementTypes();
    }
}
