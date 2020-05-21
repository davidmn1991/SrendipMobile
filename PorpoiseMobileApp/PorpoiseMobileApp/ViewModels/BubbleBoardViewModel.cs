using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Acr.Settings;
using PorpoiseMobileApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using MvvmCross.Platform;
using System.Collections.Generic;

namespace PorpoiseMobileApp.ViewModels
{
    public class BubbleBoardViewModel : SrendipViewModel<BubbleBoardViewModel>
    {
        public BubbleBoardViewModel()
        {
        }
        private readonly IPorpoiseWebApiClient client;

        private IImageService _imageService;

        public List<BubbleList> bubbleLists = new List<BubbleList>();

        public int bubble_id;

        public BubbleBoardViewModel(IPorpoiseWebApiClient client, ISettings settings, IEncryptionService encryption)
        {
            this.client = client;
            this._imageService = Mvx.Resolve<IImageService>();

            Debug.WriteLine("LOADING LOGIN IMAGE");
            //Add key image to map
        }

        public string bubble_selected;

        public void ShowSettings()
        {

            ShowViewModel<ProfileViewModel>();
        }

        public void ShowMyResume()
        {
            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("bubble_id", bubble_id.ToString());

            //param.Add("last_name", last_name);

            ShowViewModel<MyResumeViewModel>(param);
        }

        public async void GetBubbleList()
        {

            try
            {

                //ShowViewModel<HomeViewModel>();

                var result = await client.GetBubblesList();
                Debug.WriteLine("RESULT");
                if (result != null && result.code.Equals(200))
                {
                    Debug.WriteLine("Near Profiles Count " + result.data.Count);

                    foreach(BubbleList bubbleList in result.data)
                    {
                        bubbleLists.Add(bubbleList);
                    }

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

        public void ShowMyPets()
        {
            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("bubble_id", bubble_id.ToString());

            ShowViewModel<PetListViewModel>(param);
        }

        public void ShowMyBiggestPetPeeve()
        {
            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("bubble_id", bubble_id.ToString());

            ShowViewModel<BiggestPetPeeveViewModel>(param);
        }

        public void ShowProfileBubble()
        {
            var param = new System.Collections.Generic.Dictionary<string, string>();

            param.Add("bubble_selected", bubble_selected);

            param.Add("bubble_id", bubble_id.ToString());

            ShowViewModel<ProfileBubbleViewModel>(param);
        }
    }
}
