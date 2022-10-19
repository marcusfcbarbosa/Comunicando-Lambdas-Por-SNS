using Amazon.Runtime;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace user_changes_domain
{
    public static class Acceess
    {
        public static string GetRoute() => GetSeetings().GetValue<string>("Queue");

        public static string GetTopiARN() => GetSeetings().GetValue<string>("SNS_ARN");

        public static IConfigurationRoot GetSeetings() => new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsetttings.json")
               .AddEnvironmentVariables()
               .Build();

        public static AmazonSQSClient GetCredentials()
        {
            var accessKey = GetSeetings().GetValue<string>("AccessKey");
            var secret = GetSeetings().GetValue<string>("Secret");
            var credential = new BasicAWSCredentials(accessKey, secret);
            return new AmazonSQSClient(credentials: credential, region: Amazon.RegionEndpoint.SAEast1);
        }
    }
}
