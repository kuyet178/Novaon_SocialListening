using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AioCore.Shared.Extensions;

public static class AppiumExtensions
{
    public static bool Wait(this RemoteWebDriver driver, string inputPath, bool isXpath = true)
    {
        var retriesCounter = 0;
        while (retriesCounter < 20)
        {
            if (isXpath)
            {
                var element = driver.FindElement(By.XPath(inputPath));
                if (element is not null) return true;
                Thread.Sleep(500);
            }
            else
            {
                var pageSource = driver.PageSource;
                if (pageSource.Contains(inputPath)) return true;
                Thread.Sleep(500);
            }

            retriesCounter++;
        }

        return true;
    }
}