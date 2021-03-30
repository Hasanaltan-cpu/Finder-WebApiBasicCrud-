using Finder_Business.Abstract;
using Finder_DataAccess.Abstract;
using Finder_DataAccess.Repositories.Concrete;
using Finder_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finder_Business.Concrete
{
    public class FinderManager : IFinderService
    {

        private IFinderRepository _finderRepository;

        public FinderManager(IFinderRepository finderRepository)
        {
            _finderRepository = finderRepository;
        }
        public Pharmacy CreatePharmacy(Pharmacy pharmacy)
        {
            return _finderRepository.CreatePharmacy(pharmacy);
        }

        public void DeletePharmacy(int id)
        {
          _finderRepository.DeletePharmacy(id);
        }

        public List<Pharmacy> GetAllPharmacies()
        {
          
           return _finderRepository.GetAllPharmacy();
        }

        public Pharmacy GetPharmacyById(int id)
        {
            if (id > 0)
            {
                return _finderRepository.GetPharmacyById(id);

            }
            throw new Exception("id can not be less than 1");
        }

        public Pharmacy UpdatePharmacy(Pharmacy pharmacy)
        {
            return _finderRepository.UpdatePharmacy(pharmacy);
        }
    }
}
