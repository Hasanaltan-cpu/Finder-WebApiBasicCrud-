using Finder_DataAccess.Abstract;
using Finder_DataAccess.ApplicationDbContext;
using Finder_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finder_DataAccess.Repositories.Concrete
{
    public class FinderRepository : IFinderRepository
    {
        public Pharmacy CreatePharmacy(Pharmacy pharmacy)
        {
            using (var finderDbContext = new FinderDbContext())
            {
                finderDbContext.Pharmacies.Add(pharmacy);
                finderDbContext.SaveChanges();
                return pharmacy;
            };
        }

        public void DeletePharmacy(int id)
        {
            using (var finderDbContext = new FinderDbContext())
            {
                var deletedPharmacy=GetPharmacyById(id);
                finderDbContext.Pharmacies.Remove(deletedPharmacy);
                finderDbContext.SaveChanges();


            };
        }

        public List<Pharmacy> GetAllPharmacy()
        {
            using (var finderDbContext = new FinderDbContext())
            {
                return finderDbContext.Pharmacies.ToList();
            };
        }

        public Pharmacy GetPharmacyById(int id)
        {
            using (var finderDbContext = new FinderDbContext())
            {
                return finderDbContext.Pharmacies.Find(id);
            }
            
        }

        public Pharmacy UpdatePharmacy(Pharmacy pharmacy)
        {
            using (var finderDbContext = new FinderDbContext())
            {
               finderDbContext.Pharmacies.Update(pharmacy);
                finderDbContext.SaveChanges();
                return pharmacy;
            };
        }
    }
}
