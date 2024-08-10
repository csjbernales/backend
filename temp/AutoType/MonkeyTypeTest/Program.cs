using OpenQA.Selenium;
using OpenQA.Selenium.Edge;


IWebDriver driver = new EdgeDriver();
driver.Url = "https://monkeytype.com/";
driver.FindElements(By.ClassName("buttons"))[0].Click();
System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> space = driver.FindElements(By.XPath("//div[@id='typingTest']//div[@id='wordsWrapper']"));

IWebElement wordsWrapper = driver.FindElement(By.Id("wordsWrapper"));

List<IWebElement> words = driver.FindElements(By.XPath("//div[@id='words']")).ToList();

wordsWrapper.SendKeys(words[0].Text);
driver.Quit();