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
            string path = "T:\\הנדסת תוכנה\\שנה ב\\קבוצה ב\\תלמידות\\שושי ורהפטיג\\AlertProject\\AlertProject\\drivers";


            driver = new ChromeDriver(path);

        }

        [Test]
        public void TestHandleAlert()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/alerts");

            // גלילה לכפתור שמציג alert לאחר 5 שניות
            var alertButton = driver.FindElement(By.Id("timerAlertButton"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", alertButton);

            // לחיצה על הכפתור
            alertButton.Click();

            // המתנה ל-alert והטיפול בו
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
