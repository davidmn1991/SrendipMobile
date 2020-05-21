using System;
using MvvmCross.Core.ViewModels;
using PorpoiseMobileApp.Client;
using Acr.Settings;
using PorpoiseMobileApp.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using PorpoiseMobileApp.Models;

namespace PorpoiseMobileApp.ViewModels
{
    public class AddPetViewModel : SrendipViewModel<AddPetViewModel>
    {

        public static List<PetsList> PetList = new List<PetsList>();

        public PetsList PetInfo = new PetsList();

        public Bubbledetails pet_details = new Bubbledetails();

        public string petName;

        public byte[] PetBytes;

        public AddPetViewModel()
        {
        }

        public void ShowPetList()
        {

            ShowViewModel<PetListViewModel>();
        }

        public void GoToPets()
        {
            PetInfo.PetName = petName;

            PetInfo.PetBytes = PetBytes;

            pet_details.detail = petName;

            try
            {
                if (!PetBytes.Equals(null))
                {
                    pet_details.file_url = "https://srendip-dev.s3.amazonaws.com/" + AccountInfo.Profile_email + "/pets/" + PetInfo.PetName + ".jpeg";


                }
            }
            catch(Exception ex)
            {

            }

            PetListViewModel.bubble_details.Add(pet_details);
            PetList.Add(PetInfo);

            ShowViewModel<PetListViewModel>();
        }
    }
}
