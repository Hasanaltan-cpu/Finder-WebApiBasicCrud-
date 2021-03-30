using Finder_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finder_Business.Abstract
{
    public interface IFinderService
    {

        List<Pharmacy> GetAllPharmacies();

        Pharmacy GetPharmacyById(int id);

        Pharmacy CreatePharmacy(Pharmacy pharmacy);

        Pharmacy UpdatePharmacy(Pharmacy pharmacy);

        void DeletePharmacy(int id);


    }
}
