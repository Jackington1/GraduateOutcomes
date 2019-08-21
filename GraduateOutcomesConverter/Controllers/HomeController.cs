using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduateOutcomesConverter.Models;
using System.Web;
using Microsoft.AspNetCore.Hosting.Server;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Xml.Linq;

namespace GraduateOutcomesConverter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _env;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public void OutputData(List<Cohort> data)
        //{
        //    string path = new StreamWriter(HttpContext.Current)
        //}

        //[HttpPost("UploadFiles")]
        //public async Task<IActionResult> Post(IFormFile file)
        //{
        //    var result = new StringBuilder();
        //    using (var reader = new StreamReader(file.OpenReadStream()))
        //    {
        //        while (reader.Peek() >= 0)
        //        {
        //            result.AppendLine(await reader.ReadLineAsync());
        //        }
        //        StreamWriter sw = System.IO.File.CreateText("~/Home/Output.txt");
        //        sw.WriteLine(result);


        //    }
        //    return Ok();
        //}

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> FileUpload(List<IFormFile> files)
        {
            var webroot = _env.WebRootPath;
            long size = files.Sum(f => f.Length);
            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if(formFile.Length > 0)
                {
                    var filePath = Path.Combine(/*AppContext.BaseDirectory*/webroot, $"{Guid.NewGuid().ToString()}.csv"); /*Path.GetTempPath()+ Guid.NewGuid().ToString()+".txt";*/
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(new { count = files.Count, size, filePaths });
        }

        [HttpPost]
        public IActionResult Log()
        {
            var webRoot = _env.WebRootPath;
            var file = Path.Combine(webRoot, "Output.txt");
            //var lines = System.IO.File.ReadAllText()
            var text = System.IO.File.ReadAllText(file);

            return Content(text);
        }

        public IActionResult Convert()
        {
            var webroot = _env.WebRootPath;
            //using (var reader = new StreamReader("Output.txt"))
            //{

            //} //Cant find output file FIX
            string[] reader = System.IO.File.ReadAllLines(Path.Combine(webroot, "Output.txt"));
            XElement cust = new XElement("Root",
                from str in reader
                let fields = str.Split(',')
                select new XElement("Student",
                new XAttribute("HUSID", fields[0]),
                new XElement("OWNSTU", fields[1]),
                new XElement("Country", fields[2])
                    )
                );

                return Content(cust.ToString());
        }

        //private async Task WriteToFileAsAsync()
        //{
        //    string file = @"Johnson.txt";
        //    using (FileStream stream = new FileStream(file, FileMode.Create, FileAccess.ReadWrite))
        //    {
        //        using (StreamWriter sw = new StreamWriter(stream))
        //        {
        //            for (int i = 0; i < 1000; i++)
        //            {
        //                await sw.WriteLineAsync(i.ToString());
        //            }
        //        }
        //    }
        //}


        /*ORIGNAL*/
        //[HttpPost("UploadFiles")]
        //public async Task<IActionResult> FileUpload(List<IFormFile> files)
        //{
        //    long size = files.Sum(f => f.Length);
        //    var filePaths = new List<string>();
        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            var filePath = Path.GetTempFileName();
        //            filePaths.Add(filePath);

        //            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
        //            {
        //                await formFile.CopyToAsync(stream);
        //            }
        //        }
        //    }
        //    return Ok(new { count = files.Count, size, filePaths });
        //}

    }
}
