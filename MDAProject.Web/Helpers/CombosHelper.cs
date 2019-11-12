using MDAProject.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDAProject.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboAssistents()
        {
            var list = _dataContext.Assistants.Select(l => new SelectListItem
            {
                Text = l.User.FullNameWithDocument,
                Value = $"{l.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a assistent...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboDevices()
        {
            var list = _dataContext.Devices.Select(d => new SelectListItem
            {
                Text = $"{d.CodeIntegral} - {d.DeviceType.DeviceTypeName}" ,
                Value = $"{d.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a device...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboBrands ()
        {
            var list = _dataContext.Brands.Select(b => new SelectListItem
            {
                Text = $"{b.Name}",
                Value = $"{b.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a brand...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboMovementTypes()
        {
            var list = _dataContext.MovementTypes.Select(b => new SelectListItem
            {
                Text = $"{b.MovementTypeName}",
                Value = $"{b.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Movement Type...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboPropertyTypes()
        {
            var list = _dataContext.DeviceTypes.Select(pt => new SelectListItem
            {
                Text = pt.DeviceTypeName,
                Value = $"{pt.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a property type...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "(Select a role...)" },
                new SelectListItem { Value = "1", Text = "Assistant" },
                new SelectListItem { Value = "2", Text = "Manaser" },
                new SelectListItem { Value = "2", Text = "WareHouseManager" }
            };

            return list;
        }
    }
}
