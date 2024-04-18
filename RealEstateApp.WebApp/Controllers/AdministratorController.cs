﻿using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums.Roles;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Administrator;
using RealEstateApp.Core.Application.ViewModels.Account;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Dtos.Account;
using Microsoft.AspNetCore.Authorization;

namespace RealEstateApp.WebApp.Controllers
{
    [Authorize]
    public class AdministratorController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRealEstatePropertyService _realEstatePropertyService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdministratorController(IUserService userService, IRealEstatePropertyService realEstatePropertyService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _realEstatePropertyService = realEstatePropertyService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> Index() => View(await _userService.GetAllByRoleViewModel(nameof(UserRoles.Admin)));


        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> AdminHome()
        {
            AdminHomeViewModel adminHome = new();

            adminHome.Username = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Username;
            adminHome.Properties = await _realEstatePropertyService.GetTotalProperties();
            adminHome.ActiveCustomers = await _userService.GetActiveUsers(nameof(UserRoles.Customer));
            adminHome.ActiveCustomers = await _userService.GetActiveUsers(nameof(UserRoles.Customer));
            adminHome.ActiveDevelopers = await _userService.GetActiveUsers(nameof(UserRoles.Developer));
            adminHome.InactiveCustomers = await _userService.GetInactiveUsers(nameof(UserRoles.Developer));
            adminHome.ActiveAgents = await _userService.GetActiveUsers(nameof(UserRoles.RealEstateAgent));
            adminHome.InactiveAgents = await _userService.GetInactiveUsers(nameof(UserRoles.RealEstateAgent));

            return View(adminHome);
        }

        //[Authorize(Roles = nameof(UserRoles.Admin))]
        public IActionResult Create()
        {
            SaveUserViewModel vm = new();
            return View("SaveAdmin", vm);
        }

        [HttpPost]
        //[Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveAdmin", vm);
            }
            var origin = Request.Headers["origin"];
            UserRegisterResponse response = await _userService.RegisterUserAsync(vm, origin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("SaveAdmin", vm);
            }
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> Edit(string Id)
        {
            SaveUserViewModel admin = await _userService.GetByIdSaveViewModelAsync(Id);
            return View("SaveAdmin", admin);
        }

        [HttpPost]
        //[Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveAdmin", vm);
            }
            vm.Role = nameof(UserRoles.Admin);
            var origin = Request.Headers["origin"];
            UserEditResponse response = await _userService.EditUserAsync(vm, origin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("SaveAdmin", vm);
            }
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> ActivateUser(string Id)
        {
            await _userService.ActivateUser(Id);
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> DeactivateUser(string Id)
        {
            await _userService.DeactivateUser(Id);
            return RedirectToAction("Index");
        }
    }
}
