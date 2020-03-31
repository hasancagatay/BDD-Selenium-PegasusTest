using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecflowTutorial.Base;
using SpecflowTutorial.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace SpecflowTutorial
{
    [Binding]
    public sealed class Steps
    {

        public IWebDriver Driver { get; set; }
        public BasePage basePage;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ScenarioContext context;
        public static int years;
        public static int Years { get => years; set => years = value; }

        public Steps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [BeforeScenario]
        public void SetUp()
        {
            Logging.Logger();
            
            ChromeOptions options = new ChromeOptions();
            
            options.AddArgument("start-maximized");
            options.AddArgument("disable-popup-blocking");
            options.AddArgument("disable-notifications");
            options.AddArgument("test-type");
            
            Driver = new ChromeDriver(options);
            
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            
            Driver.Navigate().GoToUrl("https://www.flypgs.com/");
            log.Info("Driver ayağa kalktı...");
            basePage = new BasePage(Driver);
        }
        [BeforeStep]
        public void BeforeStep()
        {
            log.Info("Step : " + context.StepContext.StepInfo.Text);
        }

        [Given("'(.*)' objesine tıklanır..")]
        public void ClickFromWhere(String obje)
        {
            log.Info("Parametre: " + obje);
            basePage.ClickElement(By.CssSelector(obje));
        }

        [Given("'(.*)' id objesine tıklanır..")]
        public void ClickFromWhereById(String obje)
        {
            basePage.ClickElement(By.CssSelector(obje));
        }



        [Given("'(.*)' objesine '(.*)' yazılır.")]
        public void Send(string xpath, string value)
        {
            basePage.SendKeys(By.CssSelector(xpath), value);
        }

        [Given("'(.*)' id objesine '(.*)' yazılır.")]
        public void SendById(string id, string value)
        {
            basePage.SendKeys(By.CssSelector(id), value);
        }

        [Given("'(.*)' tarihi '(.*)' tarihine alınır.")]
        public void GetDate(string s, string date)
        {
            // features da girilen tarih aşağıda birbirinden ayrılıyor. Ve bir array e atanıyor...
            string[] arr = date.Split("/");

            // birbirinden ayrılan gün/ay/yıl arr parametresi ile DateSelect metoduna parametre olarak gönderiliyor...
            basePage.DateSelect(s, arr);
        }

        [Given("'(.*)' saniye süreyle beklenir.")]
        public void TimeSeconds(int seconds)
        {
            log.Info(seconds + "saniye bekleniyor...");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        [AfterScenario]
        public void TearDown()
        {
            Driver.Quit();
        }

        

        
    }
}
