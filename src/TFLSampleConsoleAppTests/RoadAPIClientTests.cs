using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFLSampleConsoleApp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TFLSampleConsoleApp.Tests
{
    [TestClass()]
    public class RoadAPIClientTests
    {
        static void SetupCredentials(out string userName, out string password, out string apiURL, string roadID)
        {
            userName = "APP ID";
            password = "APP Key";
            apiURL = "https://api.tfl.gov.uk/Road/" + roadID;


        }


        [TestMethod()]
        public void DesrializeResponseTest()
        {
            //arrange
            var rawResponse = "[{'$type':'Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities','id':'a2','displayName':'A2','statusSeverity':'Closure','statusSeverityDescription':'Closure','bounds':'[[-0.0857,51.44091],[0.17118,51.49438]]','envelope':'[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]','url':'/Road/a2'}]";
            string dispayName = "";

            //act
            List<ResponseEntity> lstResponseEntities = RoadAPIClient.DesrializeResponse(rawResponse);
            foreach (ResponseEntity item in lstResponseEntities)
            {
                dispayName = item.DisplayName;
            }
            // assert
            Assert.AreEqual(dispayName, "A2");
        }



        [TestMethod()]
        public async Task CallAPITest()
        {
            //Arrange 
            string roadID = "A2";
            string userName, password, apiURL;
            SetupCredentials(out userName, out password, out apiURL, roadID);

            //act
            ServiceResponse serviceResponse = await RoadAPIClient.GetServiceResponse(userName, password, apiURL);
            List<ResponseEntity> lstResponseEntities = RoadAPIClient.DesrializeResponse(serviceResponse.response);

            // assert
            Assert.AreEqual(1, lstResponseEntities.Count);
        }

        [TestMethod()]
        public async Task FailedFunctionalTest()
        {
            //Arrange 
            string userName, password, apiURL, roadID;
            roadID = "Test";
            SetupCredentials(out userName, out password, out apiURL, roadID);


            //act
            ServiceResponse serviceResponse = await RoadAPIClient.GetServiceResponse(userName, password, apiURL);

            // assert
            Assert.AreEqual(false, serviceResponse.IsSuccessStatusCode);


        }

        [TestMethod()]
        public async Task SucessFunctionalTest()
        {
            //Arrange 
            string roadID = "A2";
            string userName, password, apiURL;
            SetupCredentials(out userName, out password, out apiURL, roadID);
            ResponseEntity responseEntity = new ResponseEntity();

            //act
            ServiceResponse serviceResponse = await RoadAPIClient.GetServiceResponse(userName, password, apiURL);
            List<ResponseEntity> lstResponseEntities = RoadAPIClient.DesrializeResponse(serviceResponse.response);
            foreach (ResponseEntity item in lstResponseEntities)
            {
                responseEntity.DisplayName = item.DisplayName;
                responseEntity.StatusSeverity = item.DisplayName;
                responseEntity.StatusSeverityDescription = item.DisplayName;
            }

            // assert
            Assert.AreEqual("A2", responseEntity.DisplayName);
            Assert.IsNotNull(responseEntity.StatusSeverity);
            Assert.IsNotNull(responseEntity.StatusSeverityDescription);
        }

        
    }



}