using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using Aliyun.Api;
using Aliyun.Api.DNS.DNS20150109.Request;
using Newtonsoft.Json;

namespace Kaedei.AliyunDDNSClient
{
	class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				var configs = File.ReadAllLines("config.txt");
				var accessKeyId = configs[0].Trim(); //Access Key ID，如 DR2DPjKmg4ww0e79
				var accessKeySecret = configs[1].Trim(); //Access Key Secret，如 ysHnd1dhWvoOmbdWKx04evlVEdXEW7 
				var domainName = configs[2].Trim(); //域名，如 google.com
				var rr = configs[3].Trim(); //子域名，如 www
				Console.WriteLine("Updating {0} of domain {1}", rr, domainName);

				var aliyunClient = new DefaultAliyunClient("http://dns.aliyuncs.com/", accessKeyId, accessKeySecret);
				var req = new DescribeDomainRecordsRequest() { DomainName = domainName };
				var response = aliyunClient.Execute(req);

				var updateRecord = response.DomainRecords.FirstOrDefault(rec => rec.RR == rr && rec.Type == "A");
				if (updateRecord == null)
					return;
				Console.WriteLine("Domain record IP is " + updateRecord.Value);

				//获取IP
				var htmlSource = new HttpClient().GetStringAsync("http://www.ip.cn/").Result;
				var ip = Regex.Match(htmlSource, @"(?<=<code>)[\d\.]+(?=</code>)", RegexOptions.IgnoreCase).Value;
				Console.WriteLine("Current IP is " + ip);

				if (updateRecord.Value != ip)
				{
					var changeValueRequest = new UpdateDomainRecordRequest()
					{
						RecordId = updateRecord.RecordId,
						Value = ip,
						Type = "A",
						RR = rr
					};
					aliyunClient.Execute(changeValueRequest);
					Console.WriteLine("Update finished.");
				}
				else
				{
					Console.WriteLine("IPs are same now. Exiting");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			Thread.Sleep(5000);
		}
	}
}