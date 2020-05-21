using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Amazon.S3;
using Amazon.CognitoIdentity;
using Amazon.S3.Transfer;
using Amazon;
using System.Diagnostics;
using Amazon.Runtime;

namespace PorpoiseMobileApp.Services
{
    public class ImageService : IImageService
    {
        private IAmazonS3 _s3Client { get; set; }
        //production-mobileuploads
        private const string BUCKETNAME = "srendip-dev";
        private const string S3_IDENTITYPOOL = "us-east-1:b930fc37-9507-4215-8ce5-2f783a2311d8";
        // private AmazonS3Client s3Client;
        private TransferUtility transferUtility;

        public ImageService()
        {
            
            _s3Client = new AmazonS3Client(new BasicAWSCredentials("AKIATE2WMHOSFVI45ELX", "cTRkVu55/7u/H4DzWlR67f0QP9j/pwhz0Ohlm0hJ"), RegionEndpoint.USEast1);

            transferUtility = new TransferUtility(_s3Client);
        }



        public async Task<PutObjectResponse> ProcessImage(string keyName, Stream fileContent)
        {
            PutObjectResponse response;
            var putRequest = new PutObjectRequest
            {
                BucketName = BUCKETNAME,
                Key = keyName,
                InputStream = fileContent,
                CannedACL = S3CannedACL.PublicReadWrite,
                ContentType = "image/jpeg"

            };
            response = await _s3Client.PutObjectAsync(putRequest);
            return response;

        }

        public async Task<DeleteObjectResponse> RemoveImage(string keyName)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = BUCKETNAME,
                Key = keyName
            };

            DeleteObjectResponse response;
            response = await _s3Client.DeleteObjectAsync(deleteRequest);
            return response;

        }

        public async Task<string> CopyImage(string sourceKey, string destinationKey)
        {
            CopyObjectRequest request = new CopyObjectRequest()
            {
                SourceBucket = BUCKETNAME,
                SourceKey = sourceKey,
                DestinationBucket = BUCKETNAME,
                DestinationKey = destinationKey,
                CannedACL = S3CannedACL.PublicReadWrite
            };
            CopyObjectResponse response = await _s3Client.CopyObjectAsync(request);
            return string.Format("https://{0}.s3.amazonaws.com/{1}", BUCKETNAME, destinationKey);
        }

        public  void getImageFromUrl(string url)
        {
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;


            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

            request.Method = "GET";

            request.BeginGetResponse(Response_Completed, request);






        }
        void Response_Completed(IAsyncResult result)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)result.AsyncState;
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.EndGetResponse(result);
            byte[] b = null;

			if (request.HaveResponse)
			{
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					System.IO.Stream receiveStream = response.GetResponseStream();
					using (System.IO.BinaryReader br = new System.IO.BinaryReader(receiveStream))
					{
						b = br.ReadBytes(500000);
						br.Dispose();
					}
				}
			}

			
        }
        public virtual async Task<Stream> DownloadImage(string keyName, bool webPost)
        {
            try
			{if (!webPost)
				{
					Debug.WriteLine("KEYNAME in S3 " + keyName);

					var request = new GetObjectRequest();
					request.BucketName = BUCKETNAME;
					request.Key = keyName;
					var response = await _s3Client.GetObjectAsync(request);
					return response.ResponseStream;
				}
			else{
				        Debug.WriteLine("WEBAPP POST KEYNAME: "+keyName);
					var request = new GetObjectRequest();
					request.BucketName = "porpoise-production";
					request.Key = keyName;
					var response = await _s3Client.GetObjectAsync(request);
					return response.ResponseStream;
			}
            }
			catch (Exception ex){
			throw;
            }
        }
    }
}
