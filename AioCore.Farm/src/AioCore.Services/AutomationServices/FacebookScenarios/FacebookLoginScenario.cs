using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AioCore.Services.AutomationServices.FacebookScenarios;

public class FacebookLoginScenario : IFacebookScenario
{
    public StepType StepType => StepType.Login;

    public async Task<bool> ExecuteAsync(RemoteWebDriver driver, FacebookAccount actionModel)
    {
        var userNameElement = driver.FindElement(By.XPath(
            "//android.widget.EditText[@text=\"Phone or email\" or @text=\"Số điện thoại hoặc email\" or @content-desc=\"Username\" or @content-desc=\"Tên người dùng\"]"));
        var passwordElement = driver.FindElement(By.XPath(
            "//android.widget.EditText[@text=\"Password\" or @text=\"Mật khẩu\" or @content-desc=\"Password\" or @content-desc=\"Mật khẩu\"]"));
        var buttonLogin = driver.FindElement(By.XPath("//*[@content-desc=\"Log In\" or @content-desc=\"Đăng nhập\"]"));

        userNameElement.SendKeys(actionModel.Uid);
        passwordElement.SendKeys(actionModel.Password);
        buttonLogin.Click();
        
        return await Task.FromResult(true);
    }
}