using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Acr.Settings;
using PorpoiseMobileApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using MvvmCross.Platform;
using PorpoiseMobileApp.Models;

namespace PorpoiseMobileApp.ViewModels
{
    public class PetListViewModel : SrendipViewModel<PetListViewModel>
    {
        public PetListViewModel()
        {
        }
       
        public static List<Bubbledetails> bubble_details = new List<Bubbledetails>();

        //public Bubbledetails pet_details = new Bubbledetails();

        private static string bubble_id;

        public string type = "LIST";

        private IImageService _imageService;

        private IPorpoiseWebApiClient client;

        public byte[] Bytes;

        public string petName;


        public PetListViewModel(IPorpoiseWebApiClient client)
        {
            this.client = client;
            //this._pictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();
            //this._settings = Mvx.Resolve<ISettings>();
            this._imageService = Mvx.Resolve<IImageService>();
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

        public async void ChangeImage()
        {
            try
            {
                if (!Bytes.Equals(null))
                {
                    try
                    {
                        //InFlight = true;
                        // Save Image
                        Stream stream = new MemoryStream(Bytes);
                        stream.Seek(0, SeekOrigin.Begin);

                        var keyName = AccountInfo.Profile_email + "/" + "pets" + "/" + petName + ".jpeg";

                        var imageResponse = await _imageService.ProcessImage(keyName, stream);
                        if (imageResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var PhotoUrl = "https://" + "srendip-dev" + ".s3.amazonaws.com/" + keyName;
                            Debug.WriteLine("PHOTOURL " + PhotoUrl);
                            var PhotoContentType = "image/jpeg";
                            var PhotoKeyName = keyName;

                            // expect the stream to be disposed after immediately this method returns.
                            // PerformUploadPhotoEvent(true);
                        }
                        else
                        {
                            Debug.WriteLine(imageResponse.HttpStatusCode);
                        }

                    }

                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);

                    }
                }
            }

            catch(Exception ex)
            {

            }
        }

        public void ShowBubbleBoard()
        {

            ShowViewModel<BubbleBoardViewModel>();
        }

        public async void AddPetsBubble()
        {

            try
            {
                foreach(Models.PetsList petsList in AddPetViewModel.PetList)
                {
                    Bytes = petsList.PetBytes;

                    petName = petsList.PetName;

                    ChangeImage();
                }

                //ShowViewModel<HomeViewModel>();

                var result = await client.AddBubbleFile(Convert.ToInt32(bubble_id),type,bubble_details);
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

        public void ShowAddPet()
        {
            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("bubble_id", bubble_id.ToString());

            ShowViewModel<AddPetViewModel>(param);
        }
    }
}
