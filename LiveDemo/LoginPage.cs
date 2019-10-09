using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LiveDemo
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }


        internal void Open()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com");
        }

        internal bool IsLoaded()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login_button_container")));
            return element.Displayed;
        }

        internal void Login(string userName, string password)
        {
            driver.FindElement(By.Id("user-name")).SendKeys(userName);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.ClassName("btn_action")).Click();
        }
    }
}