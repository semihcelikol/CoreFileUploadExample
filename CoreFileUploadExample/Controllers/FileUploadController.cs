using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CoreFileUploadExample.Controllers
{
    public class FileUploadController : Controller
    {
        public IActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> FileUpload(IFormFile formFile)
        {
            if (formFile != null)
            {
                var extent = Path.GetExtension(formFile.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", randomName);

                //Create file
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                return Json("Dosya yüklendi");
            }


            return Json("Dosya yüklenirken hata oluştu, lütfen daha sonra tekrar deneyiniz.");
        }
    }
}
