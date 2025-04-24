// Services/SmsService.cs
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class SmsService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public SmsService(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    //public async Task SendOtpAsync(string mobileNumber, string otp)
    //{
    //    var apiKey = _configuration["Fast2SMS:ApiKey"];
    //    var message = $"Your OTP is {otp}";

    //    var content = new StringContent(
    //        $"sender_id=FSTSMS&message={message}&language=english&route=p&numbers={mobileNumber}",
    //        Encoding.UTF8,
    //        "application/x-www-form-urlencoded");

    //    _httpClient.DefaultRequestHeaders.Clear();
    //    _httpClient.DefaultRequestHeaders.Add("authorization", apiKey);

    //    var response = await _httpClient.PostAsync("https://www.fast2sms.com/dev/bulkV2", content);

    //    var responseContent = await response.Content.ReadAsStringAsync();
    //    // Optional: log or handle response
    //}

    public async Task SendOtpAsync(string mobileNumber, string otp)
    {
        var apiKey = _configuration["Fast2SMS:ApiKey"];
        var message = $"Your OTP is {otp}";

        var content = new StringContent(
            $"sender_id=FSTSMS&message={message}&language=english&route=p&numbers={mobileNumber}",
            Encoding.UTF8,
            "application/x-www-form-urlencoded");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("authorization", apiKey);

        var response = await _httpClient.PostAsync("https://www.fast2sms.com/dev/bulkV2", content);
        var result = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Fast2SMS Response: " + result); // log it for testing
    }

}
