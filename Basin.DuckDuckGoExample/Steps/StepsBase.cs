﻿using System;
using Basin.Selenium;
using Basin.Selenium.Builders;
using Basin.Selenium.Drivers;
using Basin.Selenium.Interfaces;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Basin.DuckDuckGoExample.Steps
{
    [Binding]
    public class StepsBase
    {
        private static readonly string ConfigPath = AppDomain.CurrentDomain.BaseDirectory.Replace(
            "/bin/Debug/netcoreapp3.1/",
            "");

        [BeforeFeature]
        public static void BeforeFeatureHook()
        {
            BSN.SetConfig($"{ConfigPath}/DuckDuckGo.json");
        }
        
        [BeforeScenario]
        public static void BeforeScenarioHook()
        {
            Driver.Init();
            Pages.Init();
        }

        [AfterScenario]
        public static void AfterScenarioHook()
        {
            Driver.Current?.Quit();
        }
    }
}
