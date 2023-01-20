using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 60; i++)
            {
                //Download ChromeDriver according to your Google Chrome version.
                var driver = new ChromeDriver(@"C:\Users\User\Desktop\chromedriver_win32 (2)");
                //UsVisa URL
                driver.Navigate().GoToUrl("https://ais.usvisa-info.com/tr-tr/niv/schedule/45910810/appointment");
                Thread.Sleep(3000);
                try
                {
                    //Modal button
                    var modalOkButton = driver.FindElement(By.CssSelector("button.ui-button.ui-corner-all.ui-widget"));
                    modalOkButton.Click();
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Tıklama düğmesi bulunamadı");
                }

                Thread.Sleep(1000);

                try
                {
                    //The e-mail input
                    var emailInput = driver.FindElement(By.Id("user_email"));
                    //The password input
                    var passwordInput = driver.FindElement(By.Id("user_password"));

                    //Enter e-mail address
                    emailInput.SendKeys("emre_ulusan@outlook.com");

                    //Enter password
                    passwordInput.SendKeys("Mind2023");
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Input bulunamadı");
                }

                Thread.Sleep(1000);

                try
                {
                    //Policy checkbox
                    IWebElement checkbox = driver.FindElement(By.Id("policy_confirmed"));
                    Actions action = new Actions(driver);
                    action.MoveToElement(checkbox).Click().Perform();
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("checkbox bulunamadı");
                }
                Thread.Sleep(1000);

                try
                {
                    //Submit Button
                    IWebElement submitButton = driver.FindElement(By.XPath("//input[@class='button primary' and @value='Oturum Aç']"));
                    submitButton.Click();
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Tıklama düğmesi bulunamadı");
                }

                Thread.Sleep(2000);

                try
                {
                    IWebElement submitButton1 = driver.FindElement(By.XPath("//input[@class='button primary small' and @value='Devam Et']"));
                    submitButton1.Click();
                    IWebElement accordionTitle = driver.FindElement(By.XPath("//a[@class='accordion-title' and @aria-controls='gw8vv3-accordion']"));
                    accordionTitle.Click();
                    IWebElement submitButton21 = driver.FindElement(By.XPath("//input[@class='button small primary small-only-expanded' and @value='Randevu Zamanı Al']"));
                    submitButton21.Click();
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Tıklama düğmesi bulunamadı");
                }
                Thread.Sleep(1000);

                try
                {
                    //Select Element
                    var selectElement = driver.FindElement(By.Id("appointments_consulate_appointment_facility_id"));
                    //Options
                    var optionAnkara = selectElement.FindElement(By.XPath("//option[text()='Ankara']"));
                    var optionIstanbul = selectElement.FindElement(By.XPath("//option[text()='Istanbul']"));
                    optionAnkara.Click();
                    Thread.Sleep(1000);
                    optionIstanbul.Click();
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Tıklama düğmesi bulunamadı");

                }
                Thread.Sleep(1000);

                try
                {
                    var div = driver.FindElement(By.Id("consulate_date_time_not_available"));
                    var small = div.FindElement(By.TagName("small"));
                    var text = "ISTANBUL = " + small.Text + "\n" + "\n" + "ANKARA = " + small.Text;
                    Console.WriteLine(text);
                    var accountSid = "AC7172334af7269bee75e6873eb4c18c38";
                    var authToken = "b1e4ce804bbb67a45e64e5b29d5db10c";
                    TwilioClient.Init(accountSid, authToken);
                    var messageOptions = new CreateMessageOptions(
                        new Twilio.Types.PhoneNumber("whatsapp:+905321321184"));
                    messageOptions.From = new Twilio.Types.PhoneNumber("whatsapp:+14155238886");
                    messageOptions.Body = "USVISA'dan Bildirim => " + "\n" + "\n" + text + "\n" + "\n" + "[Developed by UlusanSoftware]";
                    var message = MessageResource.Create(messageOptions);
                    Console.WriteLine(message.Body);
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Tıklama düğmesi bulunamadı");
                }

                //Chrome closes and reopens after 30 minutes.
                driver.Quit();
                Thread.Sleep(1800000);
                
            }
        }
    }
}