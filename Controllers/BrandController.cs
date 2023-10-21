using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using testproje.Data;
using testproje.Models;

namespace testproje.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        //Brands butonu icin dusundum.
        [HttpGet]
        public IActionResult GetAllBrands()
        {
            try
            {
                var Brands = AplicationContext.Brands;
                return Ok(Brands);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatasi: " + ex.Message);
            }
        }

        // Search kismi için
        [HttpGet("Search/{title}")]
        public IActionResult GetOneBrand(string title)
        {
            try
            {
                var brand = AplicationContext
                .Brands
                .FirstOrDefault(b => string.Equals(b.Title, title, StringComparison.OrdinalIgnoreCase));
                // Buyuk kucuk harf uyumsuzlugundan kurtulmak icin sonuna StringComparison.OrdinalIgnoreCase ekeldim.

                if (brand == null)
                {
                    return NotFound("ilgili marka bulunamadi.");
                }

                return Ok(brand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Bir hata oluştu: " + ex.Message);
            }
        }

        // urun eklemek icin
        [HttpPost]
        public IActionResult CreateBrand([FromBody] Brand brand)
        {
            try
            {
                if (brand == null)
                {
                    return BadRequest("ilgili marka bulunamadi.");
                }

                AplicationContext.Brands.Add(brand);
                //AplicationContext.SaveChanges(); // Veritabanına kaydetmek icin

                return CreatedAtRoute("GetBrand", new { id = brand.Title }, brand);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatasi: " + ex.Message);
                
            }
        }
        
        // guncelleme kısmı, sag ustteki logo icin
        [HttpPut("{title}")]
        public IActionResult UpdateBrand(string title, [FromBody] Brand brand)
        {
            try
            {
                //Böyle bir marka var mı diye bir logic işleterek kontrol ediyoruz.
                var entity = AplicationContext
                .Brands
                .SingleOrDefault(b => string.Equals(b.Title, title, StringComparison.OrdinalIgnoreCase));

                if(entity is null)
                    return NotFound();

                return Ok(brand);
            }
            catch (Exception ex)
            {               
                return StatusCode(500, "Sunucu hatasi: " + ex.Message);
            }
        }
        
        [HttpDelete("{title}")]
        public IActionResult DeleteBrand(string title)
        {
            try
            {
                var entity = AplicationContext.Brands.SingleOrDefault(b => string.Equals(b.Title, title, StringComparison.OrdinalIgnoreCase));
    
                if(entity is null)
                    return NotFound();
    
                AplicationContext.Brands.Remove(entity);
                //AplicationContext.SaveChanges(); // Degisiklikleri veritabanına kaydetmek icin

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatasi: " + ex.Message);
            }
        }

        /* patch edilecek birsey olmadigi icin yorum satirina aldim
        [HttpPatch("{title}")]
        public IActionResult PatchBrand(string title, [FromBody] JsonPatchDocument<Brand> patchDocument)
        {
            try
            {
                
                var entity = AplicationContext.Brands.SingleOrDefault(b => string.Equals(b.Title, title, StringComparison.OrdinalIgnoreCase));

                if (entity is null)
                {
                    return NotFound(); // 404 Notfound 
                }

                patchDocument.ApplyTo(entity, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); 
                }

                AplicationContext.SaveChanges(); 

                return NoContent(); 
            }
            catch (Exception ex)
            { 
                return StatusCode(500, "Sunucu hatasi: " + ex.Message);
            }
        }

        */
    }
}