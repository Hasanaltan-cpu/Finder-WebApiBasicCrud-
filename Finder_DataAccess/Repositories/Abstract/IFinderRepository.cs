using Finder_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finder_DataAccess.Abstract
{
   public interface IFinderRepository
    {
        List<Pharmacy> GetAllPharmacy();

        Pharmacy GetPharmacyById(int id);

        Pharmacy CreatePharmacy(Pharmacy pharmacy);

        Pharmacy UpdatePharmacy(Pharmacy pharmacy);

        void DeletePharmacy(int id);
    }
}
