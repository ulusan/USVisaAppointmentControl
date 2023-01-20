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
            //int target = 48;
            for (int i = 0; i < 30; i++)
            {
                //Download ChromeDriver according to your Google Chrome version.
                var driver = new ChromeDriver(@"C:\Users\User\Desktop\chromedriver_win32 (2)");
            //UsVisa URL
            driver.Navigate().GoToUrl("https://ais.usvisa-info.com/tr-tr/niv/schedule/45910810/appointment");
            Thread.Sleep(3000);

            //Modal button
            var modalOkButton = driver.FindElement(By.CssSelector("button.ui-button.ui-corner-all.ui-widget"));
            modalOkButton.Click();

            //The e-mail input
            var emailInput = driver.FindElement(By.Id("user_email"));
            //The password input
            var passwordInput = driver.FindElement(By.Id("user_password"));

            //Enter e-mail address
            emailInput.SendKeys("emre_ulusan@outlook.com");

            //Enter password
            passwordInput.SendKeys("Mind2023");

            //Policy checkbox
            IWebElement checkbox = driver.FindElement(By.Id("policy_confirmed"));
            Actions action = new Actions(driver);
            action.MoveToElement(checkbox).Click().Perform();

            //Submit Button
            IWebElement submitButton = driver.FindElement(By.XPath("//input[@class='button primary' and @value='Oturum Aç']"));
            submitButton.Click();

            Thread.Sleep(3000);

            //Select Element
            var selectElement = driver.FindElement(By.Id("appointments_consulate_appointment_facility_id"));
            //Options
            var optionAnkara = selectElement.FindElement(By.XPath("//option[text()='Ankara']"));
            var optionIstanbul = selectElement.FindElement(By.XPath("//option[text()='Istanbul']"));

            optionAnkara.Click();
            Thread.Sleep(5000);

            var div = driver.FindElement(By.Id("consulate_date_time_not_available"));
            var small = div.FindElement(By.TagName("small"));
            var text = small.Text;
            Console.WriteLine(text);

            optionIstanbul.Click();

            var div1 = driver.FindElement(By.Id("consulate_date_time_not_available"));
            var small1 = div1.FindElement(By.TagName("small"));
            var text1 = small1.Text;
            Console.WriteLine(text1);

            var accountSid = "AC7172334af7269bee75e6873eb4c18c38";
            var authToken = "b1e4ce804bbb67a45e64e5b29d5db10c";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new Twilio.Types.PhoneNumber("whatsapp:+905321321184"));
            messageOptions.From = new Twilio.Types.PhoneNumber("whatsapp:+14155238886");
            messageOptions.Body = "USVISA'dan mesajınız var = " + text1 + "[developed UlusanSoftware]";

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);


            Thread.Sleep(1800000);

            //driver.Close();
            }
        }
    }
}