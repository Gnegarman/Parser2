using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        private static List<string> GetValueInNode(HtmlDocument doc, string div, string table)
        {
            List<string> List = new List<string>();
            var list = doc.DocumentNode.SelectSingleNode(div);
            foreach (var title in list.SelectNodes(table))
            {
               List.Add(title.InnerText);
            }
            return List;
        }
        public static void GetValue(string url)
        {
            List<string> PRODUCT = new List<string>();
            List<string> REGION = new List<string>();
            IWebDriver browser = new ChromeDriver(Environment.CurrentDirectory);
            browser.Navigate().GoToUrl(url);
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var doc = new HtmlDocument();
            doc.LoadHtml(browser.PageSource);
            PRODUCT = GetValueInNode(doc, "//div[@class='k-grid-content-locked']", ".//td[@id='cell-0-0-0']");
            REGION = GetValueInNode(doc, "//div[@class='k-grid-content-locked']", ".//td[@id='cell-0-0-1']");
            foreach(string l in PRODUCT)
            {
                Console.WriteLine(l);
            }
            foreach (string l in REGION)
            {
                Console.WriteLine(l);
            }
            //var list = doc.DocumentNode.SelectSingleNode("//div[@class='k-grid-content-locked']");
            //foreach (var title in list.SelectNodes(".//td[@id='cell-0-0-0']"))
            //{
            //    //switch (title.InnerText)
            //    //{
            //    //    case "Средние потребительские цены (тарифы) на товары и услуги":
            //    //        Hrefs.Add(title.InnerText, url += title.Attributes["href"].Value);
            //    //        break;
            //    //    default:
            //    //        break;
            //    //}
            //    Console.WriteLine(title.InnerText);
            //}
            //list = doc.DocumentNode.SelectSingleNode("//div[@class='k-grid-content-locked']");
            //foreach (var title in list.SelectNodes(".//td[@id='cell-0-0-1']"))
            //{
            //    //switch (title.InnerText)
            //    //{
            //    //    case "Средние потребительские цены (тарифы) на товары и услуги":
            //    //        Hrefs.Add(title.InnerText, url += title.Attributes["href"].Value);
            //    //        break;
            //    //    default:
            //    //        break;
            //    //}
            //    Console.WriteLine(title.InnerText);
            //}
            //list = doc.DocumentNode.SelectSingleNode("//div[@class='k-grid-content-locked']");
            //foreach (var title in list.SelectNodes(".//td[@id='cell-0-0-2']"))
            //{
            //    //switch (title.InnerText)
            //    //{
            //    //    case "Средние потребительские цены (тарифы) на товары и услуги":
            //    //        Hrefs.Add(title.InnerText, url += title.Attributes["href"].Value);
            //    //        break;
            //    //    default:
            //    //        break;
            //    //}
            //    Console.WriteLine(title.InnerText);
            //}
            browser.Quit();
            //return Hrefs;
        }
        public static Dictionary<string, string> GetHref()
        {
            Dictionary<string, string> Hrefs = new Dictionary<string, string>();
            string url = "https://www.fedstat.ru";
            IWebDriver browser = new ChromeDriver(Environment.CurrentDirectory);
            browser.Navigate().GoToUrl(url);
            IWebElement search = browser.FindElement(By.XPath(@".//div[@class='gt_l']//input[@placeholder='Поиск...']"));
            search.SendKeys("Средние потребительские цены");
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            search.Submit();
            Thread.Sleep(3000);

            var doc = new HtmlDocument();
            doc.LoadHtml(browser.PageSource);
            var list = doc.DocumentNode.SelectSingleNode("//div[@class='publ_cont']");
            foreach (var title in list.SelectNodes(".//a[@href]"))
            {
                switch (title.InnerText)
                {
                    case "Средние потребительские цены (тарифы) на товары и услуги":
                        Hrefs.Add(title.InnerText, url += title.Attributes["href"].Value);
                        break;
                    default:
                        break;
                }
            }
            browser.Quit();
            return Hrefs;
        }
        static void Main(string[] args)
        {
            Dictionary<string, string> Hrefs = new Dictionary<string, string>();
            Hrefs = GetHref();
            GetValue(Hrefs["Средние потребительские цены (тарифы) на товары и услуги"]);
            //string url = "https://www.fedstat.ru/";
            //IWebDriver browser = new ChromeDriver(Environment.CurrentDirectory);

            //browser.Navigate().GoToUrl(url);
            //browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            ////var results = browser.FindElementByClassName("ss-results");
            //var doc = new HtmlDocument();
            //doc.LoadHtml(browser.PageSource);

            //// Show results
            //var list = doc.DocumentNode.SelectSingleNode("//div[@id='popularIndicator0']");
            //foreach (var title in list.SelectNodes(".//a[@href]"))
            //{
            //    Console.WriteLine(title.Attributes["href"].Value);
            //}
            //Console.ReadLine();
            foreach (var h in Hrefs)
            {
                Console.WriteLine($"key: {h.Key}  value: {h.Value}");
            }
            
        }


    }
}
