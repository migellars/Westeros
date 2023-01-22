using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SharedKernel.Resources.Util
{
    public static class Utils
    {
        private static readonly IHttpContextAccessor _contextAccessor;
        public static string GenereateRandonAlphaNumeric(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public static string GenerateRandonAlphaNumeric()
        {
            return $"{Guid.NewGuid().ToString().Remove(5).ToUpper()}-{Guid.NewGuid().ToString().Remove(5).ToUpper()}";
        }

        /// <summary>
        /// Generate code of count 5
        /// </summary>
        /// <returns>Random letters and numbers of count 5</returns>
        public static string GenerateProgramCode()
        {
            return RandomString.GenerateRandomLettersAndNumbers(5).ToUpper();
        }
        public static string RandomDigits(int length = 16)
        {
            var firstFourthen = DateTime.Now.ToString("yyyyMMddhhmmss");
            var random = new Random();
            var s = string.Empty;
            for (var i = 0; i < length; i++)
                s = string.Concat(s, random.Next(10).ToString());

            var number = firstFourthen + s;
            return number;
        }
        public static string CardRandom()
        {
            string? sReturn = "";
            Random random = new Random();
            var iRandom1 = Convert.ToInt32((9999 * random.Next(9999)).ToString().Replace("-", ""));
            var iRandom2 = Convert.ToInt32((999999 * random.Next(999999)).ToString().Replace("-", ""));
            var iRandom3 = Convert.ToInt32((99999 * random.Next(99999)).ToString().Replace("-", ""));
            const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string s1 = Alphabet.Substring(iRandom1 % 26, 2);
            string s3 = Alphabet.Substring(iRandom2 % 26, 2);
            string s5 = Alphabet.Substring(iRandom3 % 26, 2);
            string s2 = iRandom1.ToString().Substring(0, 2);
            string s4 = iRandom2.ToString().Substring(0, 2);
            string s6 = iRandom3.ToString().Substring(0, 2);
            return s4 + s2 + s1 + s5 + s4 + s6 + "_$#%";
        }

        public static DateTime FirstDayOfWeek(DateTime dt)
        {
            var culture = Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }



     
        public static string BinaryToString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                throw new ArgumentNullException("binary");

            if ((binary.Length % 8) != 0)
                throw new ArgumentException("Binary string invalid (must divide by 8)", "binary");

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < binary.Length; i += 8)
            {
                string section = binary.Substring(i, 8);
                int ascii = 0;
                try
                {
                    ascii = Convert.ToInt32(section, 2);
                }
                catch (System.Exception ex)
                { 
                    throw new ArgumentException("Binary string contains invalid section: " + section, "binary");
                }
                builder.Append((char)ascii);
            }
            return builder.ToString();
        }

        public static string AbsoluteUrl(this IHttpContextAccessor httpContextAccessor, IHttpContextAccessor contextAccessor, string relativeUrl, object parameters = null)
        {
            var request = httpContextAccessor.HttpContext.Request;

            var url = new Uri(new Uri($"{request.Scheme}://{request.Host.Value}"), relativeUrl).ToString();

            if (parameters != null)
            {
                url = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(url, ToDictionary(parameters));
            }

            return url;
        }

        public static string AbsoluteUrlCustom(IHttpContextAccessor accessor, string host, string relativeUrl, object parameters = null)
        {
            var request = accessor.HttpContext.Request;
            request.Host = new HostString(host);

            var url = new Uri(new Uri($"{request.Scheme}://{request.Host.Value}"), relativeUrl).ToString();

            if (parameters != null)
            {
                url = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(url, ToDictionary(parameters));
            }

            return url;
        }
        private static Dictionary<string, string> ToDictionary(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
