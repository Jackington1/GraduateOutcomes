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

        //public static void SetLink(string link)
        //{
        //    FileUpload.filelink = link;
        //}

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
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(/*AppContext.BaseDirectory*/webroot, $"{Guid.NewGuid().ToString()}.txt"); /*Path.GetTempPath()+ Guid.NewGuid().ToString()+".txt";*/
                    filePaths.Add(filePath);
                    TempData["filepath"] = filePath;

                    using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        await formFile.CopyToAsync(stream);

                    }
                }
            }
            return RedirectToAction("Index");//Ok(/*new { count = files.Count, size, filePaths }*/);
        }

        [HttpPost]
        public IActionResult Log()
        {
            try
            {


                var webRoot = _env.WebRootPath;
                var filepath = TempData["filepath"].ToString();
                var file = Path.Combine(webRoot, filepath);
                //var lines = System.IO.File.ReadAllText()
                var text = System.IO.File.ReadAllText(file);
                TempData["filepath"] = filepath;

                return Content(text);
            }
            catch
            {
                return Redirect("~/");
            }
        }

        public IActionResult FormatFile()
        {
            try
            {


                var webroot = _env.WebRootPath;
                var filepath = TempData["filepath"].ToString();
                //string[] reader = System.IO.File.ReadAllLines(Path.Combine(webroot, filepath));
                //foreach (var line in reader)
                //{            

                //        System.IO.File.WriteAllLines(filepath, line.Replace("   ", ","));

                //}
                var text = System.IO.File.ReadAllText(filepath);
                text = text.Replace("\t", ",");
                System.IO.File.WriteAllText(filepath, text);
                TempData["filepath"] = filepath;


                return Content(System.IO.File.ReadAllText(filepath));
            }
            catch
            {
                return Redirect("~/");
            }
        }

        public IActionResult Convert()
        {
            try
            {
                var webroot = _env.WebRootPath;
                var filepath = TempData["filepath"].ToString();

                string[] reader = System.IO.File.ReadAllLines(Path.Combine(webroot, filepath));
                XElement Record = new XElement("Records",
                    from str in reader
                    let fields = str.Split(',')
                    select new XElement("GraduateOutcomeRecord",
                    new XElement("Provider",
                    new XElement("RECID", "17071"),
                    new XElement("UKPRN", "10007164"),
                    new XElement("CENSUS", fields[0])),
                    new XElement("GRADUATE",
                    new XElement("HUSID", fields[1]),
                    new XElement("OWNSTU", fields[2]),
                    new XElement("COUNTRY", fields[3]),
                    new XElement("EMAIL", fields[4]),
                    new XElement("EMAIL2", fields[5]),
                    new XElement("EMAIL3", fields[6]),
                    new XElement("EMAIL4", fields[7]),
                    new XElement("EMAIL5", fields[8]),
                    new XElement("FNAMES", fields[9]),
                    new XElement("FNMECHNGE", fields[10]),
                    new XElement("GRADSTATUS", fields[11]),
                    new XElement("INTTEL", fields[12]),
                    new XElement("INTTEL2", fields[13]),
                    new XElement("INTTEL3", fields[14]),
                    new XElement("INTTEL4", fields[15]),
                    new XElement("INTTEL5", fields[16]),
                    new XElement("SNAMECHANGE", fields[17]),
                    new XElement("SURNAME", fields[18]),
                    new XElement("UKTEL", fields[19]),
                    new XElement("UKTEL2", fields[20]),
                    new XElement("UKTEL3", fields[21]),
                    new XElement("UKTEL4", fields[22]),
                    new XElement("UKTEL5", fields[23]),
                    new XElement("UKMOB", fields[24]),
                    new XElement("UKMOB2", fields[25]),
                    new XElement("UKMOB3", fields[26]),
                    new XElement("UKMOB4", fields[27]),
                    new XElement("UKMOB5", fields[28]))
                    )
                    );

                return Content(Record.ToString());
            }
            catch (Exception)
            {
                return Redirect("~/");
            }
        }
        /*Old*/
        //public IActionResult Convert()
        //{
        //    var webroot = _env.WebRootPath;
        //    var filepath = TempData["filepath"].ToString();

        //    string[] reader = System.IO.File.ReadAllLines(Path.Combine(webroot, filepath));
        //    XElement cust = new XElement("Root",
        //        from str in reader
        //        let fields = str.Split(',')
        //        select new XElement("Student",
        //        new XAttribute("HUSID", fields[0]),
        //        new XElement("OWNSTU", fields[1]),
        //        new XElement("Country", fields[2]),
        //        new XElement("GOForenames", fields[3]),
        //        new XElement("GOSurname", fields[4]),
        //        new XElement("IsisForename", fields[5]),
        //        new XElement("IsisSurname", fields[6]),
        //        new XElement("FNameChange", fields[7]),
        //        new XElement("SNameChange", fields[8]),
        //        new XElement("GradStatus", fields[9]),
        //        new XElement("UKMob", fields[10]),
        //        new XElement("UKMob2", fields[11]),
        //        new XElement("UKTel", fields[12]),
        //        new XElement("UKTel2", fields[13]),
        //        new XElement("IntTel", fields[14]),
        //        new XElement("InteTel2", fields[15]),
        //        new XElement("Email", fields[16]),
        //        new XElement("Email2", fields[17]),
        //        new XElement("PersonUrn", fields[18]),
        //        new XElement("TblMasterFacultyCode", fields[19]),
        //        new XElement("PrimaryDept", fields[20]),
        //        new XElement("StudentNumber", fields[21]),
        //        new XElement("TblAddStuDetailsCode", fields[22]),
        //        new XElement("PrimaryTargetCode", fields[23]),
        //        new XElement("PrimaryTargetName", fields[24]),
        //        new XElement("IncomeStatusCode", fields[25]),
        //        new XElement("EnrolmentStatusCode", fields[26]),
        //        new XElement("MobileNumber", fields[27]),
        //        new XElement("PersonalEmail", fields[28]),
        //        new XElement("EmailAddress", fields[29]),
        //        new XElement("TypeCode", fields[30]),
        //        new XElement("TelephoneNumber", fields[31]),
        //        new XElement("HesaReasonForLeavingCode", fields[32]),
        //        new XElement("DateOfWithdrawal", fields[33]),
        //        new XElement("HFormattedTelNumber", fields[34]),
        //        new XElement("MatchMobileAndTelNo", fields[35])

        //            )
        //        );

        //        return Content(cust.ToString());
        //}

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
