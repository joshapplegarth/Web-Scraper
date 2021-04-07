using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TysMASTER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IWebDriver driver;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
             
        }
        private void Find_Data()
        {
            IList<IWebElement> searchElements = driver.FindElements(By.TagName("tbody"));
            foreach (IWebElement i in searchElements)
            {
                HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                var text = i.GetAttribute("innerHTML");

                htmlDocument.LoadHtml(text);
                var inputs = htmlDocument.DocumentNode.Descendants("tr").ToList();
                foreach (var items in inputs)
                {
                    HtmlAgilityPack.HtmlDocument htmlDocument1 = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument1.LoadHtml(items.InnerHtml);
                    var tds = htmlDocument1.DocumentNode.Descendants("td").ToList();
                    if (tds.Count != 0)
                        Text_Results.AppendText(tds[0].InnerText + "   " + tds[1].InnerText + "   " + tds[2].InnerText +  "   " + "\t\r");
                }
                Text_Results.AppendText("\t\r");
            }
        }
        public void Open_Browser()
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            //var options = new ChromeOptions();
            //options.AddArgument("headless");
            driver = new ChromeDriver(driverService);
            try
            {
                driver.Navigate().GoToUrl("https://www.scoresandodds.com/ncaab/odds");
            }
            catch
            {
                throw;
            }
        }

        private void BTN_Open_Browser_Click(object sender, RoutedEventArgs e)
        {
            Open_Browser();
        }

        private void BTN_EXIT_Click(object sender, RoutedEventArgs e)
        {
            driver.Quit();
        }

        private void BTN_Scrape_Click(object sender, RoutedEventArgs e)
        {
            Find_Data();
        }
    }
}
