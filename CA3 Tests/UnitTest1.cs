namespace CA3_Tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

public class Tests
{
    //private ChromeDriver chromeDriver;
    private FirefoxDriver firefoxDriver;
    private SafariDriver safariDriver;

    private IWebDriver[] drivers;

    [SetUp]
    public void Setup()
    {
        //chromeDriver = new ChromeDriver();
        firefoxDriver = new FirefoxDriver();
        safariDriver = new SafariDriver();

        drivers = new IWebDriver[] { firefoxDriver, safariDriver };
    }

    [TearDown]
    public void TearDown()
    {
        foreach (IWebDriver driver in drivers)
        {
            driver.Close();
        }
    }

    // Test which ensures a connection to the URL is established
    [Test]
    public void TestConnection()
    {
        var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
        Console.WriteLine("Running Test: " + methodName);

        foreach (IWebDriver driver in drivers)
        {
            // Navigate driver to the URL, and fullscreen the window.
            driver.Navigate().GoToUrl("http://localhost:5064");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            // Get the h1 element
            IWebElement element = driver.FindElement(By.TagName("h1"));

            if (element.Text == "Home")
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Fail");
            }
            Assert.True(element.Text == "Home");
        }
    }

    // Test which checks that no data is loaded, if the user has just loaded to the site
    [Test]
    public void TestInitialDataLoad()
    {
        var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
        Console.WriteLine("Running Test: " + methodName);

        foreach (IWebDriver driver in drivers)
        {
            // Navigate driver to the URL, and fullscreen the window.
            driver.Navigate().GoToUrl("http://localhost:5064/games");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            // Get the Text-input box element
            IWebElement element = driver.FindElement(By.Id("no-data"));

            // Get all the displayed Game cards into a var
            var cards = driver.FindElements(By.ClassName("card"));

            if (cards.Count == 0)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Fail");
            }
            Assert.True(cards.Count == 0);
        }
    }

    // Test which checks that Game cards appear when the user enters text input
    [Test]
    public void TestDataLoadWithInput()
    {
        var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
        Console.WriteLine("Running Test: " + methodName);

        foreach (IWebDriver driver in drivers)
        {
            // Navigate driver to the URL, and fullscreen the window.
            driver.Navigate().GoToUrl("http://localhost:5064/games");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            // Get the Text-input box element
            IWebElement element = driver.FindElement(By.Id("text-input-box"));

            // Click into the text box, and enter the prompt
            element.Click();
            element.SendKeys("Call of Duty");
            element.SendKeys(Keys.Enter);
            element.SendKeys(Keys.Space);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Get all the displayed Game cards into a var
            var cards = driver.FindElements(By.ClassName("card"));

            if (cards.Count > 0)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Fail");
            }
            Assert.True(cards.Count > 0);
        }
    }

    // Test which checks that when ASC button is pressed, games get sorted in ASC
    [Test]
    public void TestSortingAscending()
    {
        var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
        Console.WriteLine("Running Test: " + methodName);

        foreach (IWebDriver driver in drivers)
        {
            // Navigate driver to the URL, and fullscreen the window.
            driver.Navigate().GoToUrl("http://localhost:5064/games");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            // Get the Text-input box element
            IWebElement element = driver.FindElement(By.Id("text-input-box"));

            // Click into the text box, and enter the prompt
            element.Click();
            element.SendKeys("Call of Duty");
            element.SendKeys(Keys.Enter);
            element.SendKeys(Keys.Space);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Get the prices from the page
            var priceElements = driver.FindElements(By.ClassName("game-price"));

            // Convert the elements to a list of doubles
            var prices = priceElements.Select(x => double.Parse(x.Text)).ToList();

            // Sort the prices in ascending order
            var pricesSorted = prices.OrderBy(x => x).ToList();

            // Click the "Asc" button
            driver.FindElement(By.Id("option-asc")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Get the prices from the page again
            priceElements = driver.FindElements(By.ClassName("game-price"));

            // Convert the elements to a list of doubles
            prices = priceElements.Select(x => double.Parse(x.Text)).ToList();

            if(prices == pricesSorted)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Fail");
            }
            Assert.That(pricesSorted, Is.EqualTo(prices));
        }
    }

    // Test which checks that when DESC button is pressed, games get sorted in DESC
    [Test]
    public void TestSortingDescending()
    {
        var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
        Console.WriteLine("Running Test: " + methodName);

        foreach (IWebDriver driver in drivers)
        {
            // Navigate driver to the URL, and fullscreen the window.
            driver.Navigate().GoToUrl("http://localhost:5064/games");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            // Get the Text-input box element
            IWebElement element = driver.FindElement(By.Id("text-input-box"));

            // Click into the text box, and enter the prompt
            element.Click();
            element.SendKeys("Call of Duty");
            element.SendKeys(Keys.Enter);
            element.SendKeys(Keys.Space);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Get the prices from the page
            var priceElements = driver.FindElements(By.ClassName("game-price"));

            // Convert the elements to a list of doubles
            var prices = priceElements.Select(x => double.Parse(x.Text)).ToList();

            // Sort the prices in descending order
            var pricesSorted = prices.OrderByDescending(x => x).ToList();

            // Click the "Desc" button
            driver.FindElement(By.Id("option-desc")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Get the prices from the page again
            priceElements = driver.FindElements(By.ClassName("game-price"));

            // Convert the elements to a list of doubles
            prices = priceElements.Select(x => double.Parse(x.Text)).ToList();

            if (prices == pricesSorted)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Fail");
            }
            Assert.That(pricesSorted, Is.EqualTo(prices));
        }
    }

    [Test]
    public void TestPriceDiff()
    {
        var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
        Console.WriteLine("Running Test: " + methodName);

        foreach (IWebDriver driver in drivers)
        {
            // Navigate driver to the URL, and fullscreen the window.
            driver.Navigate().GoToUrl("http://localhost:5064/games");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            // Get the Text-input box element
            IWebElement element = driver.FindElement(By.Id("text-input-box"));

            // Click into the text box, and enter the prompt
            element.Click();
            element.SendKeys("Call of Duty");
            element.SendKeys(Keys.Enter);
            element.SendKeys(Keys.Space);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Get the calc-diff button, and click it
            driver.FindElement(By.Id("calc-diff")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Get the output from the Price diff function
            var priceDiffOutput = driver.FindElement(By.Id("price-diff-output")).Text;

            // Verify that the price difference output is a double
            var isDouble = double.TryParse(priceDiffOutput, out double priceDiff);

            if (isDouble)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Fail");
            }
            Assert.True(isDouble);
        }
    }
}

