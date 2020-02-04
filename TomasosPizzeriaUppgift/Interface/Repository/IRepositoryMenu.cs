﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Interface
{
    public interface IRepositoryMenu
    {
        MenuPage CheckMatrattsValidation(MenuPage model);
        MenuPage GetMenuInfo();
        List<MatrattTyp> GetMatrattTyper();
        void SaveBestallningMatratter(List<Matratt> matratter);
        void SaveOrder(List<Matratt> matratter, int userid);
        Matratt GetMatratterToCustomerbasket(int id);
        int GetTotalPayment(List<Matratt> matratter);
        Kund GetById(int id);
    }
}
