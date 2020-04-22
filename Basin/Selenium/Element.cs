using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Basin.Selenium
{
    public class Element : IWebElement
    {
        private readonly IWebElement _element;
        private readonly IWebElement _parentElement;
        private readonly int _timeout;
        
        public Element(IWebElement element)
        {
            _element = element;
        }

        public Element(By by, int timeout = 2)
        {
            FoundBy = by;
            _timeout = timeout;
        }

        public Element(By by, By parentBy, int timeout = 2)
        {
            FoundBy = by;
            ParentFoundBy = parentBy;
            _parentElement = new Element(parentBy, timeout);
            _timeout = timeout;

        }

        public By FoundBy { get; set; }

        public By ParentFoundBy { get; set; }

        private DefaultWait<IWebDriver> Wait
        {
            get
            {
                var wait = new DefaultWait<IWebDriver>(Driver.Current)
                {
                    Timeout = TimeSpan.FromSeconds(_timeout)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

                return wait;
            }
        }
        
        /// <summary>
        /// Locates and returns an <see cref="IWebElement"/>, will return null if its
        /// </summary>
        private IWebElement Locate
        {
            get
            {
                return Wait.Until(driver =>
                {
                    var element = ParentFoundBy != null
                        ? CurrentParent.FindElement(FoundBy)
                        : driver.FindElement(FoundBy);

                    return element.Displayed
                        ? element 
                        : null;
                });
            }
        }

        private IWebElement Current => Locate ?? throw new NullReferenceException("Element could not located because it was null");

        private IWebElement CurrentParent => _parentElement ?? throw new NullReferenceException("Parent element could not be located because it was null");

        public Func<IWebDriver, bool> IsDisplaying => WaitConditions.ElementDisplayed(_element);

        public Func<IWebDriver, bool> IsNotDisplaying => WaitConditions.ElementNotDisplayed(_element);

        public string TagName => Current.TagName;

        public string Text => Current.Text;

        public bool Enabled => Current.Enabled;

        public bool Selected => Current.Selected;

        public Point Location => Current.Location;

        public Size Size => Current.Size;

        public bool Displayed => Current.Displayed;

        public void Clear()
        {
            Current.Clear();
        }

        public void Click()
        {
            Current.Click();
        }

        public IWebElement FindElement(By by)
        {
            return Current.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Current.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            return Current.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return Current.GetCssValue(propertyName);
        }

        public string GetProperty(string propertyName)
        {
            return Current.GetProperty(propertyName);
        }

        public void SendKeys(string text)
        {
            Current.SendKeys(text);
        }

        public void Submit()
        {
            Current.Submit();
        }

        public void Hover()
        {
            var actions = new Actions(Driver.Current);
            actions.MoveToElement(Current).Perform();
        }
    }
}