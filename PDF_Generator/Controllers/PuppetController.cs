using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PuppeteerSharp;

namespace PDF_Generator.Controllers
{
    [Route("api/puppet")]
    [ApiController]
    public class PuppetController : Controller
    {
        
        public async Task<IActionResult> IndexAsync()  // async returns Task
        {
            Console.WriteLine("Enter URL to generate PDF file:");

            //string url = Console.ReadLine();
            string url = "https://jwt.io/introduction";
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });

            await using var page = await browser.NewPageAsync();
            await page.GoToAsync(url);
            //await page.PdfAsync($"C:/temp/{DateTime.Today.ToShortDateString().Replace("/", "-")}.pdf");
            var BYTE = await page.PdfDataAsync();
            Console.WriteLine("PDF Puppet File generated successfully.");
            // Console.ReadLine();
            // return Content("it works");
            return File(BYTE, "application/pdf"); //mime type

        }
    }
}
