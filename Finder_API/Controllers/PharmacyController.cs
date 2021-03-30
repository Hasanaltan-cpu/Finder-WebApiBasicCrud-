using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finder_Business.Abstract;
using Finder_Business.Concrete;
using Finder_Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finder_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private IFinderService _finderService;

        public PharmacyController(IFinderService finderService)
        {
            _finderService = finderService;
        }

        [HttpGet]

        public List<Pharmacy> Get()
        {
            return _finderService.GetAllPharmacies();
        }

        [HttpGet("id")]

        public Pharmacy Get(int id)

        {
            return _finderService.GetPharmacyById(id);
        }


        [HttpPost]

        public Pharmacy Post([FromBody] Pharmacy pharmacy)
        {
            return _finderService.UpdatePharmacy(pharmacy);
        }

        [HttpDelete("id")]

        public void Delete(int id)

        {
            _finderService.DeletePharmacy(id);
        }
    }
}
