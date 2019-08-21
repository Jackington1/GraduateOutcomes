using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduateOutcomesConverter.Models.ViewModels
{
    public class UploadModel
    {
        public IFormFile File { get; set; }
    }
}
