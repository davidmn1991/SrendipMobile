using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Acr.Settings;
using PorpoiseMobileApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Runtime;
using Amazon;
using Amazon.S3.Model;
using System.IO;
using System.Collections.Generic;
using PorpoiseMobileApp.Models;
using System.Text.RegularExpressions;

namespace PorpoiseMobileApp.ViewModels
{
    public class MyResumeViewModel : SrendipViewModel<MyResumeViewModel>
    {
        public MyResumeViewModel()
        {
        }

        public string bubble_id;

        public string file_path;

        public string file_name;

        public string type = "LIST";

        public const string bucket_name = "srendip-dev";

        public byte[] file_content;

        private IAmazonS3 _s3Client { get; set; }

        private IPorpoiseWebApiClient client;

        public  List<Bubbledetails> bubble_details = new List<Bubbledetails>();

        public Bubbledetails user_details = new Bubbledetails();

        TransferUtility transferUtility;

        //_s3Client = new AmazonS3Client(new BasicAWSCredentials("AKIATE2WMHOSFVI45ELX", "cTRkVu55/7u/H4DzWlR67f0QP9j/pwhz0Ohlm0hJ"), RegionEndpoint.USEast1);


        public MyResumeViewModel(IPorpoiseWebApiClient client)
        {
            this.client = client;
            //this._pictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();
            //this._settings = Mvx.Resolve<ISettings>();
            //this._imageService = Mvx.Resolve<IImageService>();
            //this._rotator = Mvx.Resolve<IImageRotateService>();
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {

            try
            {


                if (parameters.Data.ContainsKey("bubble_id"))
                {
                    bubble_id = parameters.Data["bubble_id"];

                    Debug.WriteLine("bubble_id " + bubble_id);
                }


            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.ToString());

            }

        }

        public void ShowBubbleBoard()
        {

            ShowViewModel<BubbleBoardViewModel>();
        }

        public async void AddResumeBubble()
        {

            try
            {

                //ShowViewModel<HomeViewModel>();

                file_name = Regex.Replace(file_name, @"\s+", "_");

                user_details.file_url = "https://" + "srendip-dev" + ".s3.amazonaws.com/" + AccountInfo.Profile_email + "/" + "Profile_resume" + "/" + file_name;

                user_details.detail = file_name;

                bubble_details.Add(user_details);

                var result = await client.AddBubbleFile(Convert.ToInt32(bubble_id), type, bubble_details);
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {


                    ShowViewModel<BubbleBoardViewModel>();

                }
                else
                {

                    //Debug.WriteLine("ERROR PERFORMING SIGN IN RESULT " + result.message);


                }
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine("ERROR PERFORMING SIGN IN HTTPREQUEST");

            }


        }

        public void UploadFile()
        {

            _s3Client = new AmazonS3Client(new BasicAWSCredentials("AKIATE2WMHOSFVI45ELX", "cTRkVu55/7u/H4DzWlR67f0QP9j/pwhz0Ohlm0hJ"), RegionEndpoint.USEast1);

            var transferUtility = new TransferUtility(_s3Client);

            Stream stream = new MemoryStream(file_content);

            file_name = Regex.Replace(file_name, @"\s+", "_");

            var putRequest = new TransferUtilityUploadRequest
            {
                BucketName = bucket_name,
                Key = AccountInfo.Profile_email + "/" + "Profile_resume" + "/"+ file_name,
                InputStream = stream,
                CannedACL = S3CannedACL.PublicReadWrite,
                ContentType = ""

            };

            transferUtility.UploadAsync(putRequest);

            AddResumeBubble();



        }
    }
}
