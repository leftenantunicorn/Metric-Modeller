using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MetricModeller.Models;
using System.IO;
using System.Text;

namespace MetricModeller.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var result = ExecutePythonScript("predictor.py","data.csv", "3,3,3,4,0,1,4,1,2,2,2,3,2,2,2,2,3,3,3,0,3,2,293,1600,25229");
            ViewData["json"] = "{ \"cost\": 419.178, \"months\": 43.09000000000001}";
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string ExecutePythonScript(string fileName, string dataName, params string[] args)
        {
            var pythonInstallationLocation = @"C:\Users\Erin\Anaconda3\python.exe";

            string result;
            string pathPy = Path.Combine(Directory.GetCurrentDirectory(), @"python\", fileName);
            string pathCsv = Path.Combine(Directory.GetCurrentDirectory(), @"python\", dataName);

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonInstallationLocation;
            var argumentsBuilder = new StringBuilder().AppendFormat("{0} {1}", pathPy, pathCsv);
            foreach (string arg in args)
            {
                argumentsBuilder.AppendFormat(" {0}", arg);
            }
            start.Arguments = argumentsBuilder.ToString();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {

                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
    }
}
