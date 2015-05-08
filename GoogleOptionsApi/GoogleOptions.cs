using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoogleOptionsApi
{
    public static class GoogleOptions
    {
        public static async Task<OptionData> GetOptionChains(string symbol, int? year=null,int? month=null,int? day=null)
        {
            var data = await Task.Run(() =>
            {
                using (var web = new WebClient())
                {
                    string url;

                    if (year != null && month != null & day != null)
                    {
                        url = string.Format("http://www.google.com/finance/option_chain?q={0}&expd={1}&expm={2}&expy={3}&output=json",
                            symbol,day.Value,month.Value,year.Value);
                    }
                    else
                    {
                        url = string.Format("http://www.google.com/finance/option_chain?q={0}&output=json", symbol);
                    }

                    return 
                        web.DownloadString(url);
                }
            });

            //Clean up badly formatted JSON to embrace with quotes
            data = Regex.Replace(data, @"(\w+:)(\d+\.?\d*)", "$1\"$2\"");
            data = Regex.Replace(data, @"(\w+):", "\"$1\":");

            //Make object
            var optionData = data.FromJson<OptionData>();
            return optionData;
        }
    }
}