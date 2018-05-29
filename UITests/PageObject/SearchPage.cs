﻿using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UITests.PageObject
{
    public class SearchPage : BasePage
    {
        private WindowsElement SearchLocationEnter => DriverSession.FindElementByAccessibilityId("SearchValueEntry");
        private WindowsElement SearchLocationButton => DriverSession.FindElementByAccessibilityId("GetLocationBtn");
        private IReadOnlyCollection<WindowsElement> SearchResultsContainer => DriverSession.FindElementsByAccessibilityId("SearchResultItem");

        public SearchPage(WindowsDriver<WindowsElement> session) : base(session)
        {
        }

        public SearchPage SetValueToSearch(string valueToSearch)
        {
            SearchLocationEnter.SendKeys(valueToSearch);
            return this;
        }

        public SearchPage ClickSearchButton()
        {
            SearchLocationButton.Click();
            return this;
        }

        public IEnumerable<string> GetSearchResults()
        {
            WaitFoSearchApplied();
            return SearchResultsContainer.Select(item => item.Text);
        }

        public string SelectFirstResultItem()
        {
            WaitFoSearchApplied();

            var firstItem = SearchResultsContainer.First();
            var textResult = firstItem.Text;

            DriverSession.Mouse.Click(firstItem.Coordinates);

            return textResult;
        }

        private void WaitFoSearchApplied()
        {
            for (int retryNumber = 0; retryNumber < 10; retryNumber++)
            {
                if (SearchLocationButton.Text == "Search Again") break;
                Task.Delay(1000);
            }
        }

    }
}