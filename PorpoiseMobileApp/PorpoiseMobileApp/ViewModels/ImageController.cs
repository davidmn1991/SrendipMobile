


using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PorpoiseMobileApp.ViewModels
{
    public static  class ImageController
    {
		// Singleton
		

		

        private static System.Collections.Generic.IDictionary<string, byte[]> images;

        public static System.Collections.Generic.IDictionary<string, byte[]> Images{

            get{


				if (images == null)
				{

					 images = new System.Collections.Generic.Dictionary<string, byte[]>();


				}
                

                return images;

            }
           

        }


      


        static string name;

		public static async Task AddImageFromUrl(string url, string imageName)
		{
			System.Net.HttpWebRequest request = null;


			   byte[] b = null;

            name = imageName;

          
			
			request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

			request.Method = "GET";

			using (var response = (System.Net.HttpWebResponse)await request.GetResponseAsync())
			{

				if (request.HaveResponse)
				{
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						System.IO.Stream receiveStream = response.GetResponseStream();
						using (System.IO.BinaryReader br = new System.IO.BinaryReader(receiveStream))
						{
							b = br.ReadBytes(500000);
							if (b != null)
							{
                                
								

								if (!Images.ContainsKey(name))
								{

									// hold image

									byte[] image = new byte[b.Length];

									image = b;

									Images.Add(imageName, b);

									// Debug.WriteLine("MAP SIZE: " + getImages.Count);

									b = null;

								}



							}
							br.Dispose();
						}
					}

				}

            }





          

		}

    }
}
