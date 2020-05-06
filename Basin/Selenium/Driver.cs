using System;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Basin.Selenium
{
    public static class Driver
    {
        [ThreadStatic] private static IWebDriver _driver;

        [ThreadStatic] public static Wait Wait;

        [ThreadStatic] public static Window Window;

        public static IWebDriver Current => _driver ?? throw new NullReferenceException("_driver is null.");

        public static string Title => Current.Title;

        public static void Init([Optional] IWebDriver driver)
        {
            _driver = driver ?? DriverFactory.Build(BSN.Config.Driver.Browser);
            FinishSetup();
        }

        public static void Goto(string url)
        {
            Current.Navigate().GoToUrl(url);
        }

        //TODO: Implement screenshots

        public static void Quit()
        {
            Current.Quit();
        }

        private static void FinishSetup()
        {
            Wait = new Wait(BSN.Config.Driver.Timeout);
            Window = new Window();
            Window.Maximize();
        }
    }
}