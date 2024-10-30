using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Helpers
{
    public static class PhotoManager
    {
        //upload
        public static string _uploadPhoto(this IWebHostEnvironment webHost,IFormFile file,string pathName)
        {
            string src ="";
            string root = "wwwroot/";
            if (!Directory.Exists(root+ $"Image/{pathName}"))
            {
                Directory.CreateDirectory(root + $"Image/{pathName}");
            }
            if (file is not null)
            {
                src=$"Image/{pathName}/"+Guid.NewGuid()+file.FileName;
                string path=Path.Combine(webHost.ContentRootPath,root,src);

                using(var fileStrem =new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStrem);
                }
            }
            return src;
        }

        //remove
        public static void _removePhoto(this IWebHostEnvironment webHost, string oldFileName)
        {
            if (!String.IsNullOrEmpty(oldFileName))
            {
                string root = "wwwroot/";
                string oldPath = Path.Combine(webHost.ContentRootPath, root, oldFileName);
                File.Delete(oldPath);
            }
        }
    }
}
