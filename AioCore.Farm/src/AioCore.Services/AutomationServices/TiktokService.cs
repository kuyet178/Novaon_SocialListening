using OpenQA.Selenium.Appium.MultiTouch;
using System.Drawing;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using System.Text.RegularExpressions;
using System.Xml;
using OpenQA.Selenium;

namespace AioCore.Services.AutomationServices;

public interface ITiktokService
{
    Task Swipe(string deviceId, int x1, int y1, int x2, int y2, int duration);

    Task<String> GetClipboard(string deviceId);

    Task TakeScreenshot(IWebDriver driver, Point point, Size size);


}
public class TiktokService : ITiktokService
{
    private readonly IADBService _adbService;

    public TiktokService(IADBService adbService)
    {
        _adbService = adbService;
    }

    public async Task<string> GetClipboard(string deviceId)
    {
        var regex = new Regex("data=\"(.*?)\"");

        string cmd = "shell am broadcast -a clipper.get";

        string raw_Status = await _adbService.ExecuteAsync(deviceId, cmd);

        string proc_Status = string.Join(" ", Regex.Split(raw_Status, @"(?:\r\n|\n|\r|\\)"));

        if (regex.Match(proc_Status).Success)
        {

            string result = regex.Match(proc_Status).Groups[1].Value;

            return result;

        }
        return null;
    }


    public async Task Swipe(string deviceId, int x1, int y1, int x2, int y2, int duration)
    {

        try
        {

            var cmd = $"shell input touchscreen swipe {x1} {y1} {x2} {y2} {duration}";

            await _adbService.ExecuteAsync(deviceId, cmd);

        }

        catch (Exception)
        {
        }
    }


    public async Task TakeScreenshot(IWebDriver driver, Point point, Size size)
    {
        try
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".jpg";

            Byte[] byteArray = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;

            Bitmap screenshot = new Bitmap(new MemoryStream(byteArray));

            Rectangle croppedImage = new Rectangle(point, size);

            screenshot = screenshot.Clone(croppedImage, screenshot.PixelFormat);

            screenshot.Save(String.Format(@"E:\1_Work\Work_Novaon\IMG\" + fileName, System.Drawing.Imaging.ImageFormat.Jpeg));

        }
        catch (Exception)
        {

        }
    }
}
