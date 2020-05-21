using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Acr.Settings;
using PorpoiseMobileApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PorpoiseMobileApp.ViewModels
{
    public class BiggestPetPeeveViewModel : SrendipViewModel<BiggestPetPeeveViewModel>
    {
        public BiggestPetPeeveViewModel()
        {
        }

        public string bubble_id;

        public string description;

        public string type = "TEXT";


        private readonly IPorpoiseWebApiClient client;

        private IImageService _imageService;



        public BiggestPetPeeveViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
        {
            this.client = client;

            Debug.WriteLine("LOADING LOGIN IMAGE");
            //Add key image to map
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

        public async void AddBubble()
        {

            try
            {

                //ShowViewModel<HomeViewModel>();

                var result = await client.AddBubble(Convert.ToInt32(bubble_id), description, type);
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {
                    Debug.WriteLine("Bubble Added");
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

        public void ShowBubbleBoard()
        {

            ShowViewModel<BubbleBoardViewModel>();
        }
    }
}
