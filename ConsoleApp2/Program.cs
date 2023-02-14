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
                var driver = new ChromeDriver(@"/Users/ulusan/Desktop/chromedriver_mac_arm64");
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
                    Console.WriteLine("Close düğmesi bulunamadı");
                }

                Thread.Sleep(1000);

                try
                {
                    //The e-mail input
                    var emailInput = driver.FindElement(By.Id("user_email"));
                    //The password input
                    var passwordInput = driver.FindElement(By.Id("user_password"));

                    //Enter e-mail address
                    emailInput.SendKeys("e-mail");

                    //Enter password
                    passwordInput.SendKeys("password");
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("SignIn Inputs bulunamadı");
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
                    Console.WriteLine("Oturum Aç Butonu Bulunamadı");
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
                    Console.WriteLine("Devam et/ Randevu zamanı al bulunamadı");
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
                    Console.WriteLine("Select İşlemi Yapılmadı");
                }
                Thread.Sleep(1000);

                try
                {
                    var div = driver.FindElement(By.Id("consulate_date_time_not_available"));
                    var small = div.FindElement(By.TagName("small"));
                    var text = "ISTANBUL = " + small.Text + "\n" + "\n" + "ANKARA = " + small.Text;
                    Console.WriteLine(text);
                    var accountSidEmre = "AC7172334af7269bee75e6873eb4c18c38";
                    var authTokenEmre = "b1e4ce804bbb67a45e64e5b29d5db10c";
                    TwilioClient.Init(accountSidEmre, authTokenEmre);
                    var messageOptionsEmre = new CreateMessageOptions(
                        new Twilio.Types.PhoneNumber("whatsapp:+905559999999"));
                    messageOptionsEmre.From = new Twilio.Types.PhoneNumber("whatsapp:+14155238886");
                    messageOptionsEmre.Body = "USVISA'dan Bildirim => " + "\n" + "\n" + text + "\n" + "\n" + "[Developed by UlusanSoftware]";
                    var messageEmre = MessageResource.Create(messageOptionsEmre);

                    var accountSidKerim = "AC39c990d54f767c706bec922e4f522a94";
                    var authTokenKerim = "9e8ac538290e58e49307bd96e0c7ca4b";
                    TwilioClient.Init(accountSidKerim, authTokenKerim);
                    var messageOptionsKerim = new CreateMessageOptions(
                        new Twilio.Types.PhoneNumber("whatsapp:+905559999999"));
                    messageOptionsKerim.From = new Twilio.Types.PhoneNumber("whatsapp:+14155238886");
                    messageOptionsKerim.Body = "USVISA'dan Bildirim => " + "\n" + "\n" + text + "\n" + "\n" + "[Developed by KerimSekili]";
                    var messageKerim = MessageResource.Create(messageOptionsKerim);

                    Console.WriteLine(messageEmre.Body);
                    Console.WriteLine(messageKerim.Body);
                }
                catch (NoSuchElementException)
                {
                    var div = driver.FindElement(By.Id("consulate_date_time_not_available"));
                    var small = div.FindElement(By.TagName("small"));
                    var text = "ISTANBUL = " + small.Text + "\n" + "\n" + "ANKARA = " + small.Text;
                    Console.WriteLine("Mesaj Gönderilemedi ya da şuan randevu var hızlıca kontrol et = " + "\n" + text);
                }

                //Chrome closes and reopens after 30 minutes.
                driver.Quit();
                Thread.Sleep(900000);

            }
        }
    }
}
