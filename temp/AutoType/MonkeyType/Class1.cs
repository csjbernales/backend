using OpenQA.Selenium;

namespace MonkeyType
{
    public class Class1
    {

        public Class1()
        {
            driver.Url = "https://www.google.com/";
            IWebElement space = driver.FindElement(By.Id("words"));

            space.SendKeys("Hello world!");
            space.Submit();
            driver.Quit();
        }


    }
}
