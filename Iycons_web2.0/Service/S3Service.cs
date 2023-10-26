using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Threading.Tasks;
using Iycons_web2._0.Model; // Import the Media class namespace
namespace Iycons_web2._0.Service
{
    public class S3Service
    {
        private readonly string bucketName = "your-s3-bucket-name"; // Replace with your S3 bucket name
        private readonly string accessKey = "your-aws-access-key";
        private readonly string secretKey = "your-aws-secret-key";
        private readonly string s3BaseUrl = "https://your-s3-bucket-url.s3.amazonaws.com/"; // Replace with your S3 bucket URL

        public async Task<Media> UploadImageAsync(string filePath, string s3ObjectName)
        {
            var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USWest2); // Change the region to your desired AWS region

            try
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = s3ObjectName, // The name you want to give the object in your S3 bucket
                    InputStream = fileStream,
                    CannedACL = S3CannedACL.PublicRead // Optional: Set the ACL for the object (public read in this case)
                };

                var response = await s3Client.PutObjectAsync(request);

                Console.WriteLine("Image uploaded successfully!");
                Console.WriteLine($"ETag: {response.ETag}");

                // Construct the S3 URL
                var s3ObjectUrl = s3BaseUrl + s3ObjectName;

                // Create a Media object to store in the database
                var media = new Media
                {
                    Path_Name = s3ObjectUrl
                };

                return media;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Image upload failed: " + ex.Message);
                return null; // Return null to indicate failure
            }
        }
    }

}
