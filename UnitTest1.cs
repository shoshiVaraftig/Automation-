using Docker.DotNet.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AlertProject
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]

    public class GoogleSearchTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;


        [OneTimeSetUp]
        public void SetUp()
        {
            string path = "T:\\����� �����\\��� �\\����� �\\�������\\���� �������\\AlertProject\\AlertProject\\drivers";


            driver = new ChromeDriver(path);

        }

        [Test]
        public void TestHandleAlert()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/alerts");

            // ����� ������ ����� alert ���� 5 �����
            var alertButton = driver.FindElement(By.Id("timerAlertButton"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", alertButton);

            // ����� �� ������
            alertButton.Click();

            // ����� �-alert ������� ��
            IAlert alert = WaitForAlert(driver, TimeSpan.FromSeconds(10));
            Assert.IsNotNull(alert, "Alert was not displayed.");

            alert.Accept();
        }
        private IAlert WaitForAlert(IWebDriver driver, TimeSpan timeout)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, timeout);
                return wait.Until(ExpectedConditions.AlertIsPresent());
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
