using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SpecflowTutorial.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SpecflowTutorial.Base
{
    public class BasePage
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        IWebDriver driver;
        WebDriverWait wait;
        IReadOnlyList<IWebElement> result;
        IJavaScriptExecutor scriptExecutor;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));   
        }


        public IWebElement FindElement(By by)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
            HighLightElement(by);
            log.Info("Aranılan element: " + by);
            return driver.FindElement(by);
        }

        public void ClickElement(By by)
        {
           
            FindElement(by).Click();
           
        }

        public SelectElement SelectOption(IWebElement element)
        {
            return new SelectElement(element);
        }

        public void SelectElementByText(By by, String visibleText)
        {
            SelectOption(FindElement(by)).SelectByText(visibleText);
        }

        public void SelectElementByValue(By by, String value)
        {
            SelectOption(FindElement(by)).SelectByValue(value);
        }

        public void HoverElement(By by)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(FindElement(by)).Build().Perform();
        }

        public void SendKeys(By by, string s)
        {
            FindElement(by).SendKeys(s);
            FindElement(by).SendKeys(Keys.Enter);
        }

        public void SendKeysById(By by, string s)
        {
            FindElement(by).SendKeys(s);
        }

        public void DateSelect(string s, string[] arr)
        {
            if (s == "Gidiş Bileti")
            {
                
                YearSelect(s, arr[2], "//*[@id='search-flight-datepicker-departure']/div/div[1]/div/div/span[2]");
                MonthSelect(s, arr[1], "//*[@id='search-flight-datepicker-departure']/div/div[1]/div/div/span[1]");
                DaySelect(s, arr[0], "//*[@id='search-flight-datepicker-departure']/div/div[1]//tbody//a");
            }
            else if (s == "Dönüş Bileti")
            {
                YearSelect(s, arr[2], "//*[@id='search-flight-datepicker-arrival']/div/div[2]/div/div/span[2]");
                MonthSelect(s, arr[1], "//*[@id='search-flight-datepicker-arrival']/div/div[2]/div/div/span[1]");
                DaySelect(s, arr[0], "//*[@id='search-flight-datepicker-arrival']/div/div[2]//tbody//a");
            }

        }

        public void YearSelect(string rightclick, string year, string xpath)
        {
            if (rightclick == "Gidiş Bileti")
                DateYear(By.XPath(xpath), year, "//*[@id='search-flight-datepicker-departure']/div/div[2]/div/a");
            else if (rightclick == "Dönüş Bileti")
                DateYear(By.XPath(xpath), year, "//*[@id='search-flight-datepicker-arrival']/div/div[2]/div/a");
        }

        public void MonthSelect(string rightclick, string mounth, string xpath)
        {
            if (rightclick == "Gidiş Bileti")
                DateMounth(By.XPath(xpath), mounth, "//*[@id='search-flight-datepicker-departure']/div/div[2]/div/a");
            else if (rightclick == "Dönüş Bileti")
                DateMounth(By.XPath(xpath), mounth, "//*[@id='search-flight-datepicker-arrival']/div/div[2]/div/a");

        }

        public void DaySelect(string clickday, string day, string xpath)
        {
            DateDay(xpath, day);
        }



        public void DateYear(By by, string yearStr, string RightClick)
        {
            string s;
            while (true)
            {
                s = FindElement(by).Text;
                if (s.Equals(yearStr))
                    break;
                ClickElement(By.XPath(RightClick));
            }
        }

        public void DateMounth(By by, string mounthStr, string RightClick)
        {
            string s;
            while (true)
            {
                s = FindElement(by).Text;
                if (s.Equals(mounthStr))
                    break;
                ClickElement(By.XPath(RightClick));
                //Thread.Sleep(5);
            }
        }

        public void DateDay(string links, string day)
        {
            result = driver.FindElements(By.XPath(links));

            foreach (var item in result)
            {
                if (item.Text.Equals(day)) 
                {
                    item.Click();
                    break;
                }
            }
        }

        public string GetText(By by)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            string elementText = FindElement(by).Text;
            Console.WriteLine("Element Text :" + elementText);
            return elementText;
        }

        public void HighLightElement(By by)
        {
            scriptExecutor = (IJavaScriptExecutor)driver;
            scriptExecutor.ExecuteScript("arguments[0].setAttribute('style', 'background: yellow; border: 2px solid red;');", driver.FindElement(by));
            Thread.Sleep(TimeSpan.FromMilliseconds(700));
        }
    }
}
