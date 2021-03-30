![API](https://gblobscdn.gitbook.com/assets%2F-MRsSS_At9fCka5xk3SA%2F-MX3PAsNI7hXwzrLIWvU%2F-MX3PH7_BRyO_87M2ifm%2FAPI.jpg?alt=media&token=150823f3-a681-4ffa-a774-f47f32bbaf96)
![API2](https://gblobscdn.gitbook.com/assets%2F-MRsSS_At9fCka5xk3SA%2F-MX3PAsNI7hXwzrLIWvU%2F-MX3PH7ciW-QJjOf-768%2FAPIMETHODS.png?alt=media&token=88f0b5f9-caef-46b1-b736-1441a6c9c782)
<h6>In this project i use these packages & tools :<h6>

* Microsoft.AspNetCore.Identity(v9.5.3)
* Microsoft.EntityFrameworkCore(v5.0.4)
* Microsoft.AspNetCore.Identity.EntityFrameWorkCore(v3.1.6)
* Microsoft.EntityFrameworkCore.Design(v3.1.6)
* Microsoft.EntityFrameWorkCore.SqlServer(v5.0.4)
* Microsoft.EntityFrameWorkCore.Tools(v5.0.4)
* Postman v8.0.10 For API Testing methods

Generally in this project, i want to create basic crud in web api and then check these behaviour with Postman.That's why first of all;

*Open a Blank solution;

*Add a project => Core.Library => Finder_Entities
*Create an entity class.(It depends on ur project i ve mentioned just one class because i want to focus APİ.Layer
*Download EntityFramework.Core(v5.0.4) from Nuget.

 public class Pharmacy

    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)] => this will provide identity (id) otomatically from 1 and ++1
        public int Id { get; set; }

        [StringLength(50)] => this gives a rule to property.
        public string Name { get; set; }

        [StringLength(50)]
        public string City { get; set; }
    }
    
  *Add a project=> Core.Library=>Finder_DataAccess
  *Open a folder =>FinderDbContext
  
   public class FinderDbContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = Finder_Project; Trusted_Connection = True; ");
        }

        public DbSet<Pharmacy> Pharmacies { get; set; }
    }
    
    
    *Open new Folder which name is Repositories=>
    *Open an abstract folder for interfaces=>
    
     public interface IFinderRepository
    {
        List<Pharmacy> GetAllPharmacy();

        Pharmacy GetPharmacyById(int id);

        Pharmacy CreatePharmacy(Pharmacy pharmacy);

        Pharmacy UpdatePharmacy(Pharmacy pharmacy);

        void DeletePharmacy(int id);
    }
    
    *Open a concrete folder.
     public class FinderRepository : IFinderRepository => this inherited from interface !
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
    *Download EntityFrameworkCore,SqlServer,Tools (All of them v5.0.4)
    
    *Add a project=>Core.Library=>Finder_Business
    
    *Open an abstract folder=>IFinderService
     public interface IFinderService
    {

        List<Pharmacy> GetAllPharmacies();

        Pharmacy GetPharmacyById(int id);

        Pharmacy CreatePharmacy(Pharmacy pharmacy);

        Pharmacy UpdatePharmacy(Pharmacy pharmacy);

        void DeletePharmacy(int id);


    }
    *Open a concrete folder=>FinderManager
    
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
    
    *Add Project => Asp.NetCore.WebApplication => Empty (please check out Configure the Https if it is checked remove it)=> Finder_API
   
   *Open a Controllers folder.
   *Open a PharmacyController.
   
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
    
 *At the startup we should register services.
 public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSingleton<IFinderService, FinderManager>(); => This is our basic Dependency Injection with Singleton.It means when i call IFinderService on the constructor take me FinderManager.
            services.AddSingleton<IFinderRepository, FinderRepository>(); => Same
        }
