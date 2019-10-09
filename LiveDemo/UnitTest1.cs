using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace LiveDemo
{
    [TestClass]
    public class DemoFeature
    {
        public IWebDriver _driver;

        public TestContext TestContext { get; set; }
        [TestInitialize]
        public void Setup()
        {
            _driver = GetDriver();
        }
        public void Cleanup()
        {
            var passed = TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;
            if (_driver != null)
                ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            _driver?.Quit();
        }
        [TestMethod]
        public void ShouldBeAbleToLoad()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.Open();
            loginPage.IsLoaded().Should().BeTrue();
        }

        [TestMethod]
        public void ShouldBeAbleToLogin()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.Open();
            loginPage.IsLoaded().Should().BeTrue();

            loginPage.Login("standard_user", "secret_sauce");
            new InventoryPage(_driver).IsLoaded().Should().BeTrue();
        }


        private IWebDriver GetDriver()
        {
            //TODO please supply your Sauce Labs user name in an environment variable
            var sauceUserName = Environment.GetEnvironmentVariable(
                "SAUCE_USERNAME", EnvironmentVariableTarget.User);
            //TODO please supply your own Sauce Labs access Key in an environment variable
            var sauceAccessKey = Environment.GetEnvironmentVariable(
                "SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);

            ChromeOptions options = new ChromeOptions();
            options.AddAdditionalCapability(CapabilityType.Version, "latest", true);
            options.AddAdditionalCapability(CapabilityType.Platform, "Windows 10", true);
            options.AddAdditionalCapability("username", sauceUserName, true);
            options.AddAdditionalCapability("accessKey", sauceAccessKey, true);
            options.AddAdditionalCapability("name", MethodBase.GetCurrentMethod().Name, true);

            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"), options.ToCapabilities(),
                TimeSpan.FromSeconds(600));
            return _driver;
        }
    }
}
