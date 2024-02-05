using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeriController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public VeriController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetVeriler()
        {
            var veriler = _dbContext.NemSicaklikVerileri.ToList();

            return Ok(veriler);
        }

        [HttpGet("{id}")]
        public IActionResult GetVeriById(int id)
        {
            var veri = _dbContext.NemSicaklikVerileri.FirstOrDefault(v => v.Id == id);
            if (veri == null)
            {
                return NotFound();
            }
            return Ok(veri);
        }

        [HttpPost]
        public IActionResult PostVeri()
        {
            // Veri eklemek istediğiniz öğeyi oluşturun
            var yeniVeri = new NemSicaklikVerisi
            {
                Saat = DateTime.Now,
                Sicaklik = 25.5,
                Nem = 50.0
            };

            // DbSet kullanarak veriyi ekleyin ve kaydedin
            _dbContext.NemSicaklikVerileri.Add(yeniVeri);
            _dbContext.SaveChanges();

            return Ok("Veri başarıyla eklendi.");
        }
    }
}

