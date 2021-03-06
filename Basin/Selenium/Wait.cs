using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Basin.Selenium
{
    public class Wait
    {
        private readonly WebDriverWait _wait;

        public Wait(int waitSeconds)
        {
            _wait = new WebDriverWait(BrowserSession.Current, TimeSpan.FromSeconds(waitSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };

            _wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException),
                typeof(StaleElementReferenceException),
                typeof(WebDriverTimeoutException)
            );
        }

        public bool Until(Func<IWebDriver, bool> condition)
        {
            return _wait.Until(condition);
        }

        public IWebElement Until(Func<IWebDriver, IWebElement> condition)
        {
            return _wait.Until(condition);
        }

        public Elements Until(Func<IWebDriver, Elements> condition)
        {
            return _wait.Until(condition);
        }

        public Element Until(Func<IWebDriver, Element> condition)
        {
            return _wait.Until(condition);
        }
    }
}