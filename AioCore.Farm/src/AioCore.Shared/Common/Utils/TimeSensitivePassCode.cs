using OtpNet;

namespace AioCore.Shared.Common.Utils;

public class TimeSensitivePassCode
{
    public static string GetOTP(string code)
    {
        var otbKeyByte = Base32Encoding.ToBytes(code);
        var totp = new Totp(otbKeyByte);
        return totp.ComputeTotp();
    }
}