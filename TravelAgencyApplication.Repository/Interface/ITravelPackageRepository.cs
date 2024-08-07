using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.DTO;
using TravelAgencyApplication.Domain.Model;

namespace TravelAgencyApplication.Repository.Interface
{
    public interface ITravelPackageRepository
    {
        IEnumerable<TravelPackage> GetAll();
        TravelPackage Get(Guid? id);
        void Insert(TravelPackage entity);
        void Update(TravelPackage entity);
        void Delete(TravelPackage entity);
    }
}
