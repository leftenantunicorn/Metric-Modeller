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
            /* This is an example of calling the script
             The record is the values for all the features - the first line in data.csv (but not months and cost)
             I looked over the assignment and it looks like he didn't want us to do capstone predictions so I used
             the fields/features from http://sunset.usc.edu/research/COCOMOII/expert_cocomo/drivers.html because he mentioned COCOMO in the slides and I found a data set for it
             The values COCOMO uses (vl, l, n, h, vh, xh) I converted to 0-5 to make numeric
             Any values we don't want to ask the user for (since there are way more than necessary) can be filled with zeroes and the script will use the mean instead
             (or we could delete them)
             I only put the example call here because it was easy so it can be deleted/moved
             Also he mentioned a database alot in the assignment, I was using a csv file instead because it was faster but if you guys want a database we can make one
             Lastly if you want the script to run change the pythonInstallationLocation to your python.exe path and install the python packages for numpy, pandas, sklearn and simple json
             Oh and cost is in thousands, I think that's everything
            */
            var record = "3,3,3,4,0,1,4,1,2,2,2,3,2,2,2,2,3,3,3,0,3,2,293,1600,25229";
            var result = ExecutePythonScript("predictor.py","data.csv", record);
            var exampleResult = "{ \"cost\": 419.178, \"months\": 43.09000000000001}";

            ViewData["json"] = exampleResult ;
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
