﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Service.Interface
{
    public interface IUserService
    {
        List<TAUser> GetAllTAUsers();
        TAUser GetDetailsForTAUser(string id);
        void CreateNewTAUser(TAUser p);
        void UpdateExistingTAUser(TAUser p);
        void DeleteTAUser(string id);
    }
}