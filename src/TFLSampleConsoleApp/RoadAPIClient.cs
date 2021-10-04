using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TFLSampleConsoleApp
{
   public class RoadAPIClient
    {
        /// <summary>
        /// This method responsible to call API and based on the API success-status it will prepare console output
        /// </summary>
        /// <param name="roadID"></param>
        /// <returns></returns>
        public static async Task GetDataWithAuthentication(string roadID)
        {
            //read user name/APPID from App.config file
            string userName = ConfigurationManager.AppSettings["ApiUserName"];

            //read password/APP Key from App.config file
            string password = ConfigurationManager.AppSettings["ApiPassword"];

            //read API base URL from App.config file
            string apiURL = ConfigurationManager.AppSettings["ApiURL"] + roadID;

            //Call GetServiceResponse method to call Road API
            var strResponse = await GetServiceResponse(userName, password, apiURL);



            //if SuccessStatusCode of API is true 
            if (strResponse.IsSuccessStatusCode)
            {
                //call DesrializeResponse and populate lstResponse
                List<ResponseEntity> lstResponse = DesrializeResponse(strResponse.response);

                //iterate lstResponse and prepare console response
                foreach (ResponseEntity item in lstResponse)
                {

                    Console.WriteLine(" The status of the " + roadID + " is as follows");
                    Console.WriteLine(" Road Name is " + item.DisplayName);
                    Console.WriteLine(" Road Status is " + item.StatusSeverity);
                    Console.WriteLine(" Road Status Descriptio is " + item.StatusSeverityDescription);
                }

                Environment.Exit(0);
            }
            else //if SuccessStatusCode of API is true 
            {
                Console.WriteLine(roadID + " is not a valid road!");
                Console.Write("Please enter valid road ID");
                Environment.Exit(1);

            }




        }

        /// <summary>
        /// This method is responsible to setup credentials and make actual call to road API and convert response in the Json and populate 
        /// custom object Service Response with response and service call success-status
        /// </summary>
        /// <param name="userName">TLF API subscription user name</param>
        /// <param name="password">TLF API subscription password</param>
        /// <param name="apiURL">TLF Road API URL</param>
        /// <returns>ServiceResponse</returns>
        public static async Task<ServiceResponse> GetServiceResponse(string userName, string password, string apiURL)
        {
            //encode API credentials in the byte Array 
            var authCredential = Encoding.UTF8.GetBytes("{" + userName + "}:{" + password + "}");
            HttpResponseMessage response = null;

            //create serviceResponse  object
            ServiceResponse serviceResponse = new ServiceResponse();

            //HTTP client scope 
            using (var client = new HttpClient())
            {
                //setup credentials in the Authentication Header of API
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authCredential));
                //Setup API URL
                client.BaseAddress = new Uri(apiURL);

                //Call Road API and return response in service response  object
                response = await client.GetAsync(apiURL);
            }

            //read response object
            var readTask = response.Content.ReadAsStringAsync().ConfigureAwait(false);

            //populate rawResponse with serviceResponse in string format
            var rawResponse = readTask.GetAwaiter().GetResult();

            //setup service call success  status code
            serviceResponse.IsSuccessStatusCode = response.IsSuccessStatusCode;

            //populate response in the serviceResponse object
            serviceResponse.response = rawResponse;


            //return serviceResponse object
            return serviceResponse;

           
        }
        /// <summary>
        /// Desrialize response from Json to ResponseEntity object
        /// </summary>
        /// <param name="rawResponse"></param>
        /// <returns></returns>

        public static List<ResponseEntity> DesrializeResponse(string rawResponse)
        {
            // convert json response to custom object ResponseEntity
            var objResponse = JsonConvert.DeserializeObject<List<ResponseEntity>>(rawResponse);

            return objResponse;

        }


    }

   
}
