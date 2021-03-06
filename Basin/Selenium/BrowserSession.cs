﻿using System;
using Basin.Core.Browsers;
using OpenQA.Selenium;

namespace Basin.Selenium
{
    public static class BrowserSession
    {
        [ThreadStatic] private static IWebDriver _driver;

        [ThreadStatic] public static Wait Wait;

        [ThreadStatic] public static Window Window;

        public static IWebDriver Current => _driver ?? throw new NullReferenceException("_driver is null. Call Init().");

        public static string Title => Current.Title;

        public static void Init()
        {
            _driver = ConfiguredBrowserFactory.Current;

            FinishSetup();
        }

        public static void Init(IWebDriver driver)
        {
            _driver = driver;
            Wait = new Wait(BasinEnv.Browser.Timeout);
        }

        public static void Goto(string url)
        {
            Current.Navigate().GoToUrl(url);
        }

        //TODO: #24 Implement screenshots

        public static void Quit()
        {
            Current.Close();
            Current.Quit();
        }

        private static void FinishSetup()
        {
            Wait = new Wait(BasinEnv.Browser.Timeout);
            Window = new Window();
            Window.Maximize();
        }
    }
}