using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoLimpioCore.Helpers
{
    public enum Folders
    {
        Documents = 0, Images = 1
    }

    public class PathProvider
    {
        IWebHostEnvironment Environment;

        public PathProvider(IWebHostEnvironment environment)
        {
            this.Environment = environment;
        }

        public String MapPath(String filename, Folders folder)
        {
            String carpeta = "";
            if (folder == Folders.Documents)
            {
                carpeta = "docs";
            }else if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            String path = Path.Combine(this.Environment.WebRootPath, carpeta, filename);
            return path;
        }
    }
}
