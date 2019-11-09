using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MDAProject.Web.Data;
using MDAProject.Web.Data.Entities;
using MDAProject.Web.Models;
using MDAProject.Web.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace MDAProject.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class WareHouseManagersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        //private readonly ICombosHelper _combosHelper;
        //private readonly IConverterHelper _converterHelper;
        //private readonly IImageHelper _imageHelper;
        private readonly IMailHelper _mailHelper;

        public WareHouseManagersController(
            DataContext dataContext,
            IUserHelper userHelper,
            //ICombosHelper combosHelper,
            //IConverterHelper converterHelper,
            //IImageHelper imageHelper,
            IMailHelper mailHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            //_combosHelper = combosHelper;
            //_converterHelper = converterHelper;
            //_imageHelper = imageHelper;
            _mailHelper = mailHelper;
        }

        // GET: WareHouseManagers
        public IActionResult Index()
        {
            return View(_dataContext.WareHouseManagers
                .Include(w => w.User)
                .Include(w => w.Inventories)
                .Include(w => w.Movements));
        }

        // GET: WareHouseManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wareHouseManager = await _dataContext.WareHouseManagers
                .Include(w => w.User)
                .Include(i => i.Inventories)
                .ThenInclude(d => d.Devices)
                .ThenInclude(dt => dt.DeviceType)
                .Include(m => m.Movements)
                .ThenInclude(mt => mt.MovementType)                
                .FirstOrDefaultAsync(w => w.Id == id);
            if (wareHouseManager == null)
            {
                return NotFound();
            }

            return View(wareHouseManager);
        }

        public async Task<IActionResult> DetailsMovement(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wareHouseManager = await _dataContext.WareHouseManagers
                .Include(w => w.User)
                .Include(i => i.Inventories)
                .ThenInclude(d => d.Devices)
                .ThenInclude(dt => dt.DeviceType)
                .Include(m => m.Movements)
                .ThenInclude(mt => mt.MovementType)
                .FirstOrDefaultAsync(w => w.Id == id);
            if (wareHouseManager == null)
            {
                return NotFound();
            }

            return View(wareHouseManager);
        }

        // GET: WareHouseManagers/Create
        public IActionResult Create()
        {
            var view = new AddUserViewModel { RoleId = 2 };
            return View(view);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await CreateUserAsync(model);
                if (user != null)
                {
                    var WareHouseManager = new WareHouseManager
                    {
                        Inventories = new List<Inventory>(),
                        Movements = new List<Movement>(),
                        User = user
                    };

                    _dataContext.WareHouseManagers.Add(WareHouseManager);
                    await _dataContext.SaveChangesAsync();

                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    var tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendMail(model.Username, "Email confirmation",
                        $"<table style = 'max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;'>" +
                        $"  <tr>" +
                        $"    <td style = 'background-color: #34495e; text-align: center; padding: 0'>" +
                        $"       <a href = 'https://www.facebook.com/NuskeCIV/' >" +
                        $"         <img width = '20%' style = 'display:block; margin: 1.5% 3%' src= 'https://veterinarianuske.com/wp-content/uploads/2016/10/line_separator.png'>" +
                        $"       </a>" +
                        $"  </td>" +
                        $"  </tr>" +
                        $"  <tr>" +
                        $"  <td style = 'padding: 0'>" +
                        $"     <img style = 'padding: 0; display: block' src = 'https://veterinarianuske.com/wp-content/uploads/2018/07/logo-nnske-blanck.jpg' width = '100%'>" +
                        $"  </td>" +
                        $"</tr>" +
                        $"<tr>" +
                        $" <td style = 'background-color: #ecf0f1'>" +
                        $"      <div style = 'color: #34495e; margin: 4% 10% 2%; text-align: justify;font-family: sans-serif'>" +
                        $"            <h1 style = 'color: #e67e22; margin: 0 0 7px' > Hola </h1>" +
                        $"                    <p style = 'margin: 2px; font-size: 15px'>" +
                        $"                      El mejor Hospital Veterinario Especializado de la Ciudad de Morelia enfocado a brindar servicios médicos y quirúrgicos<br>" +
                        $"                      aplicando las técnicas más actuales y equipo de vanguardia para diagnósticos precisos y tratamientos oportunos..<br>" +
                        $"                      Entre los servicios tenemos:</p>" +
                        $"      <ul style = 'font-size: 15px;  margin: 10px 0'>" +
                        $"        <li> Urgencias.</li>" +
                        $"        <li> Medicina Interna.</li>" +
                        $"        <li> Imagenologia.</li>" +
                        $"        <li> Pruebas de laboratorio y gabinete.</li>" +
                        $"        <li> Estetica canina.</li>" +
                        $"      </ul>" +
                        $"  <div style = 'width: 100%;margin:20px 0; display: inline-block;text-align: center'>" +
                        $"    <img style = 'padding: 0; width: 200px; margin: 5px' src = 'https://veterinarianuske.com/wp-content/uploads/2018/07/tarjetas.png'>" +
                        $"  </div>" +
                        $"  <div style = 'width: 100%; text-align: center'>" +
                        $"    <h2 style = 'color: #e67e22; margin: 0 0 7px' >Email Confirmation </h2>" +
                        $"    To allow the user,plase click in this link:</ br ></ br > " +
                        $"    <a style ='text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #3498db' href = \"{tokenLink}\">Confirm Email</a>" +
                        $"    <p style = 'color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0' > Nuskë Clinica Integral Veterinaria 2019 </p>" +
                        $"  </div>" +
                        $" </td >" +
                        $"</tr>" +
                        $"</table>");

                    ViewBag.Message = "The instructions to allow your user has been sent to email.";
                    return View(model);
                }

                ModelState.AddModelError(string.Empty, "User with this eamil already exists.");
            }

            return View(model);
        }

        private async Task<User> CreateUserAsync(AddUserViewModel model)
        {
            var user = new User
            {
                Address = model.Address,
                Document = model.Document,
                Email = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Username
            };

            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                user = await _userHelper.GetUserByEmailAsync(model.Username);
                await _userHelper.AddUserToRoleAsync(user, "WareHouseManager");
                return user;
            }

            return null;
        }

        // GET: WareHouseManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wareHouseManager = await _dataContext.WareHouseManagers
                .Include(w => w.User)
                .Include(w => w.Inventories)
                .Include(w => w.Movements)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (wareHouseManager == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = wareHouseManager.User.Address,
                Document = wareHouseManager.User.Document,
                FirstName = wareHouseManager.User.FirstName,
                Id = wareHouseManager.Id,
                LastName = wareHouseManager.User.LastName,
                PhoneNumber = wareHouseManager.User.PhoneNumber
            };
            return View(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var wareHouseManager = await _dataContext.WareHouseManagers
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                wareHouseManager.User.Document = model.Document;
                wareHouseManager.User.FirstName = model.FirstName;
                wareHouseManager.User.LastName = model.LastName;
                wareHouseManager.User.Address = model.Address;
                wareHouseManager.User.PhoneNumber = model.PhoneNumber;

                await _userHelper.UpdateUserAsync(wareHouseManager.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: WareHouseManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wareHouseManager = await _dataContext.WareHouseManagers
                .Include(w => w.User)
                .Include(w => w.Inventories)
                .Include(w => w.Movements)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wareHouseManager == null)
            {
                return NotFound();
            }

            if (wareHouseManager.Inventories.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "Warehouse Manager can't be delete because it has Inventories.");
                return RedirectToAction(nameof(Index));
            }
            if (wareHouseManager.Movements.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "Warehouse Manager can't be delete because it has Movements.");
                return RedirectToAction(nameof(Index));
            }
            _dataContext.WareHouseManagers.Remove(wareHouseManager);
            await _dataContext.SaveChangesAsync();
            await _userHelper.DeleteUserAsync(wareHouseManager.User.Email);
            return RedirectToAction(nameof(Index));
        }

        // POST: WareHouseManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wareHouseManager = await _dataContext.WareHouseManagers.FindAsync(id);
            _dataContext.WareHouseManagers.Remove(wareHouseManager);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WareHouseManagerExists(int id)
        {
            return _dataContext.WareHouseManagers.Any(e => e.Id == id);
        }
    }
}
