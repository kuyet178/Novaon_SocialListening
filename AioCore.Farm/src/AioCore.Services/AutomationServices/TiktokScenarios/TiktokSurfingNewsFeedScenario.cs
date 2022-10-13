using AioCore.Domain.Aggregates.DeviceAggregate;
using AioCore.Domain.Aggregates.PlatformAccountAggregate.MappingModels;
using AioCore.Shared.Common.Constants;
using AntDesign;
using AntDesign.Internal;
using AntDesign.Locales;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Point = System.Drawing.Point;

namespace AioCore.Services.AutomationServices.TiktokScenarios;

public class TiktokSurfingNewsFeedScenario : ITiktokScenario
{
    private readonly ITiktokService _tiktokService;
    public TiktokSurfingNewsFeedScenario(ITiktokService tiktokService)
    {
        _tiktokService = tiktokService;
    }
    public StepType StepType => StepType.SurfingNewsFeed;

    public async Task<bool> ExecuteAsync(RemoteWebDriver driver, TiktokAccount actionModel)
    {
        try
        {

            //Account
            var userName = driver.FindElementById("com.ss.android.ugc.trill:id/i35").Text;

            var followingNumber = driver.FindElementById("com.ss.android.ugc.trill:id/c27").Text;

            var followerNumber = driver.FindElementById("com.ss.android.ugc.trill:id/c1x").Text;

            var totalLike = driver.FindElementById("com.ss.android.ugc.trill:id/b79").Text;

            //Video
            var screenResolution = driver.Manage().Window.Size;

            var backButton = driver.FindElementById("com.ss.android.ugc.trill:id/e9p");

            var menuVideoButton = driver.FindElementByXPath("//android.widget.RelativeLayout[@content-desc=\"Video\"]");

            string status = "";

            int index = 7;

            //Get info from first video

            await _tiktokService.Swipe("emulator-5554", menuVideoButton.Location.X, menuVideoButton.Location.Y, 0, (backButton.Location.Y+backButton.Size.Height), 500);

            //     var video = driver.FindElementById("com.ss.android.ugc.trill:id/hqd");
            while (true)
            {

                var video = driver.FindElementByXPath(String.Format("/ hierarchy / android.widget.FrameLayout / android.widget.FrameLayout / android.widget.LinearLayout / android.widget.FrameLayout / android.widget.FrameLayout / android.widget.FrameLayout / android.widget.HorizontalScrollView / android.widget.LinearLayout / android.widget.LinearLayout / android.widget.RelativeLayout / android.widget.LinearLayout / android.widget.LinearLayout / android.widget.FrameLayout / android.widget.LinearLayout / X.0GZ / android.widget.FrameLayout / android.widget.FrameLayout / android.widget.FrameLayout / androidx.recyclerview.widget.RecyclerView / android.widget.FrameLayout[{0}] / android.widget.ImageView", index.ToString()));

                var videoWidth = video.Size.Width;

                var videoHeight = video.Size.Height;

                var videoLocation = video.Location;

                int videoStartX = videoLocation.X;

                int videoStartY = videoLocation.Y;

                int videoEndX = videoLocation.X + videoWidth;

                int videoEndY = videoLocation.Y + videoHeight;


                if (index == 7)
                {
                    menuVideoButton = driver.FindElementByXPath("//android.widget.RelativeLayout[@content-desc=\"Video\"]");

                    int menuVideoButtonEndX = menuVideoButton.Location.X + menuVideoButton.Size.Width;

                    int menuVideoButtonEndY = menuVideoButton.Location.Y + menuVideoButton.Size.Height;

                    //  await _tiktokService.Swipe("emulator-5554", video.Location.X, video.Location.Y, (menuVideoButton.Location.X + menuVideoButton.Size.Width), (menuVideoButton.Location.Y + menuVideoButton.Size.Height), 500);
                    await _tiktokService.Swipe("emulator-5554", videoStartX, videoStartY, 0, menuVideoButtonEndY, 5000);

                    index = 0;

                    continue;
                }

                await _tiktokService.TakeScreenshot(driver, new Point(videoStartX, videoStartY), new Size(videoWidth, videoHeight));

                Thread.Sleep(1000);

                video.Click();

                Thread.Sleep(700);
                try
                {
                    var filterFirstTime = driver.FindElementByXPath("//android.widget.TextView[@text='Vuốt lên để xem thêm']");

                    await _tiktokService.Swipe("emulator-5554", screenResolution.Width / 2, screenResolution.Height / 4, screenResolution.Width / 2, screenResolution.Height / 8, 100);

                    Thread.Sleep(500);

                    await _tiktokService.Swipe("emulator-5554", screenResolution.Width / 2, screenResolution.Height / 4, screenResolution.Width / 2, screenResolution.Height, 100);

                }
                catch (Exception)
                {

                }

                #region After open video

                //  var pageSource = driver.PageSource;
                var shareButton = driver.FindElement(By.Id("com.ss.android.ugc.trill:id/g50"));

                var shareNumber = shareButton.Text;

                if (shareNumber.Equals("Chia sẻ"))
                {

                    shareNumber = "0";

                }

                try
                {
                    var seeMoreButton = driver.FindElement(By.Id("com.ss.android.ugc.trill:id/hx8"));
                    if (seeMoreButton != null)
                    {
                        seeMoreButton.Click();
                    }
                }
                catch (Exception)
                {

                }

                var likeNumber = driver.FindElement(By.Id("com.ss.android.ugc.trill:id/b82")).Text;

                var cmtNumber = driver.FindElement(By.Id("com.ss.android.ugc.trill:id/aqx")).Text;

                var saveNumber = driver.FindElement(By.Id("com.ss.android.ugc.trill:id/br4")).Text;

                try
                {

                    status = driver.FindElement(By.Id("com.ss.android.ugc.trill:id/b5v")).Text;

                }
                catch (Exception)
                {

                    status = "";

                }

                var date = driver.FindElement(By.Id("com.ss.android.ugc.trill:id/hqr")).Text;

                shareButton.Click();

                var copyUrlButton = driver.FindElementByXPath("//android.widget.TextView[@text='Sao chép Liên kết']");

                copyUrlButton.Click();

                var urlVideo = await _tiktokService.GetClipboard("emulator-5554");

                driver.Navigate().Back();

                #region Write 2 file
                string result = "----\nID: " + userName + "\nFollowing: " + followingNumber + "\nFollower: " + followerNumber + "\nTotal like: " + totalLike + "\nUrl video: " + urlVideo
                    + "\nStatus: " + status + "\nDate: " + date + "\nLikes: " + likeNumber + "\nComments: " + cmtNumber + "\nSaves: " + saveNumber + "\nShares: " + shareNumber;
                TextWriter tw = new StreamWriter(string.Format(@"E:\1_Work\Work_Novaon\txtTest.txt"), true);
                tw.WriteLine(result);
                tw.Close();
                #endregion

                Thread.Sleep(1000);
                #endregion

                index++;
            }


        }

        catch (Exception ex)
        {

            string err = ex.ToString();

            throw;
        }
        return await Task.FromResult(true);
    }
}

