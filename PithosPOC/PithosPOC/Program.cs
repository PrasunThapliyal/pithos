using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using System.Net;

namespace PithosPOC
{
    internal class Program
    {
        public const string AccessKey = "AKIAIOSFODNN7EXAMPLE";
        public const string SecretKey = "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            AWSConfigsS3.UseSignatureVersion4 = false;
            AmazonS3Config config = new AmazonS3Config();
            config.ProxyHost = "localhost";
            config.ProxyPort = 2025;
            config.RegionEndpoint = RegionEndpoint.USEast1;
            config.UseHttp = true;
            config.SignatureVersion = "s3";

#if DEBUG
            config.Timeout = TimeSpan.FromMinutes(20);
            //config.ReadWriteTimeout = TimeSpan.FromMinutes(20);
#endif

            Amazon.Runtime.BasicAWSCredentials creds = new Amazon.Runtime.BasicAWSCredentials(AccessKey, SecretKey);

            var s3Client = new AmazonS3Client(creds, config);

            ListBucket(s3Client);

            CreateBucket(s3Client);
        }

        private static void ListBucket(AmazonS3Client s3Client)
        {
            ListBucketsResponse response = s3Client.ListBucketsAsync().GetAwaiter().GetResult();
            int numBuckets = 0;
            if (response != null)
            {
                if (response.Buckets != null &&
                    response.Buckets.Count > 0)
                {
                    numBuckets = response.Buckets.Count;
                }
                Console.WriteLine("Found " + numBuckets + " Amazon S3 bucket(s).");

                int i = 0;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                for (int i1 = 0; i1 < response.Buckets.Count; i1++)
                {
                    S3Bucket bucket = response.Buckets[i1];
                    i++;
                    Console.WriteLine("Bucket " + i.ToString() + ": " + bucket.BucketName);
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }

        private static void CreateBucket(AmazonS3Client s3Client)
        {
            // Create bucket example
            var bucketName = "TestBucket";
            PutBucketRequest request = new PutBucketRequest();
            request.BucketName = bucketName;
            request.UseClientRegion = true;

            PutBucketResponse response = s3Client.PutBucketAsync(request).GetAwaiter().GetResult();
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Create bucket '" + bucketName + "' success.");
            }
            else
            {
                Console.WriteLine("Create bucket '" + bucketName + "' failed.");
            }
        }
    }
}