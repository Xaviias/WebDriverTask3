using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverTask3;

namespace WebDriverTask3.Tests
{
    [TestFixture]
    public class GoogleCloudPricingTests
    {
        private IWebDriver _driver;
        private GoogleCloudProductPricingPage _pricingPage;
        private Dictionary<string, string> expectedValues;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _pricingPage = new GoogleCloudProductPricingPage(_driver);
            _pricingPage.NavigateToPage();
            expectedValues = new Dictionary<string, string>
            {
                { "numInstances", "4" },
                { "operatingSystem", "Free: Debian, CentOS, CoreOS, Ubuntu or BYOL (Bring Your Own License)" },
                { "provisioningModel", "Regular" },
                { "machineType", "n1-standard-8, vCPUs: 8, RAM: 30 GB" },
                { "gpuModel", "NVIDIA V100" },
                { "numOfGPUs", "1" },
                { "storage", "2x375 GB" },
                { "region", "Netherlands (europe-west4)" }
            };
        }

        [Test]
        public void VerifyCostEstimateSummary()
        {
            _pricingPage.FillForm();

            var summaryValues = _pricingPage.GetSummaryValues();

            foreach (var key in expectedValues.Keys)
            {
                Assert.That(summaryValues[key], Is.EqualTo(expectedValues[key]), $"Mismatch in {key}");
            }
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

    }
}
