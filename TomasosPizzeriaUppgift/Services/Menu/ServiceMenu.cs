using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.ViewModels;
using TomasosPizzeriaUppgift.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.Models.Repository;
using TomasosPizzeriaUppgift.Models.IdentityLogic;
using Microsoft.AspNetCore.Identity;
using TomasosPizzeriaUppgift.Models.Identity;

namespace TomasosPizzeriaUppgift.Services
{
    public class ServiceMenu
    {
        private static ServiceMenu instance = null;
        private static readonly Object padlock = new Object();
        private IRepositoryMenu _repository;
        private ICache _cache;
        private IIdentityUser _identityUser;
        private IIdentityRoles _identityRole;


        public static ServiceMenu Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceMenu();
                        instance._repository = new DBRepositoryMenu();
                        instance._cache = new CacheLogic();
                        instance._identityUser = new IdentityUserLogic();
                        instance._identityRole = new IdentityRoleLogic();

                    }
                    return instance;

                }
            }
        }

        public ServiceMenu()
        {
        }
    }
}
