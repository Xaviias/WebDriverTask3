using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace WebDriverTask3
{
    public class GoogleCloudProductPricingPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private const string Url = "https://cloud.google.com/products/calculator";

        private readonly Dictionary<string, By> locators = new Dictionary<string, By>()
        {
            { "addToEstimateButton", By.XPath("//button[@class='UywwFc-LgbsSe UywwFc-LgbsSe-OWXEXe-Bz112c-M1Soyc UywwFc-LgbsSe-OWXEXe-dgl2Hf xhASFc']") },
            { "computeEngineButton", By.XPath("(//div[@role='button'])[2]") },
            { "numOfInstances", By.XPath("//input[@value='1']") },
            { "operatingSystemDropdown", By.XPath("//div[@data-field-input-type='2']") },
            { "operatingSystemOption", By.XPath("//li[@data-value='free-debian-centos-coreos-ubuntu-or-byol-bring-your-own-license']") },
            { "provisioningRadioButton", By.XPath("//div[@class='xJ0wqe']") },
            { "machineFamilyDropdown", By.XPath("(//div[@class='VfPpkd-aPP78e'])[5]") },
            { "machineFamilyOption", By.XPath("//li[@data-value='general-purpose']") },
            { "seriesDropdown", By.XPath("(//div[@class='VfPpkd-aPP78e'])[6]") },
            { "seriesOption", By.XPath("//li[@data-value='n1']") },
            { "machineTypeDropdown", By.XPath("(//div[@class='VfPpkd-aPP78e'])[7]") },
            { "machineTypeOption", By.XPath("//li[@data-value='n1-standard-8']") },
            { "addGPUsButton", By.XPath("//button[@aria-label='Add GPUs']") },
            { "gpuModelDropdown", By.XPath("/html/body/c-wiz[1]/div/div/div/div[1]/div/div/div/div/div/div/div/div[1]/div/div[2]/div[3]/div[23]/div/div[1]/div/div/div/div[1]/div") },
            { "gpuModelOption", By.XPath("//li[@data-value='nvidia-tesla-v100']") },
            { "numOfGPUsDropDown", By.XPath("(//div[@class='VfPpkd-aPP78e'])[9]") },
            { "numOfGPUsOption", By.XPath("//li[@data-value='1']") },
            { "storageDropdown", By.XPath("(//div[@class='VfPpkd-aPP78e'])[10]") },
            { "storageOption", By.XPath("/html/body/c-wiz[1]/div/div/div/div[1]/div/div/div/div/div/div/div/div[1]/div/div[2]/div[3]/div[27]/div/div[1]/div/div/div/div[2]/ul/li[3]") },
            { "regionDropdown", By.XPath("(//div[@class='VfPpkd-aPP78e'])[11]") },
            { "regionOption", By.XPath("//li[@data-value='europe-west4']") },
            { "shareButton", By.XPath("//button[@aria-label='Open Share Estimate dialog']") },
            { "openEstimateSummary", By.XPath("//a[@track-name='open estimate summary']") },
            { "closeClick", By.XPath("//button[@aria-label='Close dialog']") }
        };

        public GoogleCloudProductPricingPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateToPage()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.Manage().Window.Maximize();
            ClickElement("addToEstimateButton");
            ClickElement("computeEngineButton");
        }

        public void FillForm()
        {
            ChangeNumberOfInstances("4");
            SelectOption("operatingSystemDropdown", "operatingSystemOption");
            ClickElement("provisioningRadioButton");
            SelectOption("machineFamilyDropdown", "machineFamilyOption");
            SelectOption("seriesDropdown", "seriesOption");
            SelectOption("machineTypeDropdown", "machineTypeOption");
            ClickElement("addGPUsButton");
            SelectOption("gpuModelDropdown", "gpuModelOption");
            SelectOption("numOfGPUsDropDown", "numOfGPUsOption");
            SelectOption("storageDropdown", "storageOption");
            SelectOption("regionDropdown", "regionOption");

            ClickElement("shareButton");
            ClickElement("openEstimateSummary");
            CloseSecondTab();
            ClickElement("shareButton");
            ClickElement("openEstimateSummary");
            SwitchToEstimateSummaryTab();
        }

        private void ChangeNumberOfInstances(string numInstances)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var element = _driver.FindElement(locators["numOfInstances"]);
            element.Clear();
            element.SendKeys(numInstances);
        }

        private void ClickElement(string key)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.FindElement(locators[key]).Click();
        }

        private void SelectOption(string dropdownKey, string optionKey)
        {
            ClickElement(dropdownKey);
            ClickElement(optionKey);
        }

        public void SwitchToEstimateSummaryTab()
        {
            var handles = _driver.WindowHandles;
            if (handles.Count > 1)
            {
                _driver.SwitchTo().Window(handles[1]);
            }
            System.Threading.Thread.Sleep(5000);
        }

        public void CloseSecondTab()
        {
            var handles = _driver.WindowHandles;
            if (handles.Count == 2)
            {
                _driver.SwitchTo().Window(handles[1]);
                _driver.Close();
                _driver.SwitchTo().Window(handles[0]);
            }
            System.Threading.Thread.Sleep(5000);
        }

        public Dictionary<string, string> GetSummaryValues()
        {
            return new Dictionary<string, string>
            {
                { "numInstances", _driver.FindElement(By.XPath("(//span[@class='Kfvdz'])[10]")).Text },
                { "operatingSystem", _driver.FindElement(By.XPath("(//span[@class='Kfvdz'])[11]")).Text },
                { "provisioningModel", _driver.FindElement(By.XPath("(//span[@class='Kfvdz'])[12]")).Text },
                { "machineType", _driver.FindElement(By.XPath("(//span[@class='Kfvdz'])[3]")).Text },
                { "gpuModel", _driver.FindElement(By.XPath("(//span[@class='Kfvdz'])[5]")).Text },
                { "numOfGPUs", _driver.FindElement(By.XPath("(//span[@class='Kfvdz'])[6]")).Text },
                { "storage", _driver.FindElement(By.XPath("(//span[@class='Kfvdz'])[7]")).Text },
                { "region", _driver.FindElement(By.XPath("(//span[@class='Kfvdz'])[18]")).Text }
            };
        }
    }
}
