using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace LiveDemo
{
    internal class InventoryPage
    {
        private IWebDriver driver;

        public InventoryPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        internal bool IsLoaded()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inventory_filter_container")));
            return element.Displayed;
        }
    }
}