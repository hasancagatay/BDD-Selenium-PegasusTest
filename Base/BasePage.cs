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
                //Gidiş bileti için Gün/Ay/Yıl seçimi...
                YearSelect(s, arr[2], "#search-flight-datepicker-departure .ui-datepicker-group-first .ui-datepicker-year");
                MonthSelect(s, arr[1], "#search-flight-datepicker-departure .ui-datepicker-group-first .ui-datepicker-month");
                DaySelect(s, arr[0], "#search-flight-datepicker-departure .ui-datepicker-group-first tbody a");
            }
            else if (s == "Dönüş Bileti")
            {
                //Dönüş bileti için Gün/Ay/Yıl seçimi...
                YearSelect(s, arr[2], "#search-flight-datepicker-arrival .ui-datepicker-group-first .ui-datepicker-year");
                MonthSelect(s, arr[1], "#search-flight-datepicker-arrival .ui-datepicker-group-first .ui-datepicker-month");
                DaySelect(s, arr[0], "#search-flight-datepicker-arrival .ui-datepicker-group-first tbody a");
            }

        }

        public void YearSelect(string rightclick, string year, string xpath)
        {
            if (rightclick == "Gidiş Bileti")
                DateYear(By.CssSelector(xpath), year, "#search-flight-datepicker-departure > div > div.ui-datepicker-group.ui-datepicker-group-last > div > a");
            else if (rightclick == "Dönüş Bileti")
                DateYear(By.CssSelector(xpath), year, "#search-flight-datepicker-arrival> div > div.ui-datepicker-group.ui-datepicker-group-last > div > a");
        }

        public void MonthSelect(string rightclick, string mounth, string xpath)
        {
            if (rightclick == "Gidiş Bileti")
                DateMounth(By.CssSelector(xpath), mounth, "#search-flight-datepicker-departure > div > div.ui-datepicker-group.ui-datepicker-group-last > div > a");
            else if (rightclick == "Dönüş Bileti")
                DateMounth(By.CssSelector(xpath), mounth, "#search-flight-datepicker-arrival> div > div.ui-datepicker-group.ui-datepicker-group-last > div > a");

        }

        public void DaySelect(string clickday, string day, string xpath)
        {
            DateDay(xpath, day);
        }



        public void DateYear(By by, string inputYear, string RightClick)
        {
            string s;
            while (true)
            {
                s = FindElement(by).Text;
                if (s.Equals(inputYear))
                    break;
                ClickElement(By.CssSelector(RightClick));
            }
        }

        public void DateMounth(By by, string inputMounth, string RightClick)
        {
            string s;
            while (true)
            {
                s = FindElement(by).Text;
                if (s.Equals(inputMounth))
                    break;
                ClickElement(By.CssSelector(RightClick));
            }
        }

        public void DateDay(string path, string day)
        {
            result = driver.FindElements(By.CssSelector(path));

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
