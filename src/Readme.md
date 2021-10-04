# Introduction
 This solution have console application TFLSampleConsoleApp to consume Road API and unit test project TFLSampleConsoleAppTests

In this console applicaton TFLSampleConsoleApp project have following classes
1. ResponseEntity is used to map Road API response object

2. ServiceResponse is used to populate response object in JSON format and API success status

3. RoadAPIClient class have functionality to call Road API and prepare console response

4. Program call calls GetDataWithAuthentication of RoadAPIClient class 

RoadAPIClient class have method GetDataWithAuthentication that is required API credentials to call road API to achive this
API subscription APP ID/ user name and App Key/ password to be provided in the Appsetting section of App.Config file

<appSettings>
		<add key="ApiURL" value="https://api.tfl.gov.uk/Road/" />
		<add key="ApiUserName" value="UserName" />
		<add key="ApiPassword" value="Password" />
</appSettings>

# How to Build and Use
Build the solution
After sucessfull build open the common prompt and navigate to path 
cd "..\TFLSampleConsoleApp\bin\Release\netcoreapp3.1\"
provide executable name TFLSampleConsoleApp.exe
..\TFLSampleConsoleApp\bin\Release\netcoreapp3.1> TFLSampleConsoleApp.exe

Below screen will populate on commond prompt and provide valid road name A2 then result will be as follows


 Transport of London : Consume Road API :
---------------------------------------------------
 Enter The Road Name :

 
  Transport of London : Consume Road API :
---------------------------------------------------
 Enter The Road Name : A2

 
  Transport of London : Consume Road API :
---------------------------------------------------
 Enter The Road Name : A2

 The status of the A2 is as follows

 Road Name is A2

 Road Status is Good

 Road Status Description is No Exceptional Delays


 Below screen will populate on common prompt and provide invalid road name 'XYZ' then result will be as follows
 Transport of London : Consume Road API :
---------------------------------------------------
 Enter The Road Name :

 
  Transport of London : Consume Road API :
---------------------------------------------------
 Enter The Road Name : XYZ

 
  Transport of London : Consume Road API :
---------------------------------------------------
 Enter The Road Name : XYZ
 
 XYZ is not a valid road!
 Please enter valid road ID

 # Run unit and functional test

 Open the calll RoadAPIClientTests call under TFLSampleConsoleAppTests
 Under the SetupCredentials method provide API subscription user name and password 

 static void SetupCredentials(out string userName, out string password, out string apiURL, string roadID)
        {
            userName = "APP ID";
            password = "APP Key";
            apiURL = "https://api.tfl.gov.uk/Road/" + roadID;
         }

 Once UserName and Pasword setup then build the project TFLSampleConsoleAppTests
 Update nuget package if any missing
 run the test from visual studio

 There are 4 test methods are avalaible 
 2 unit test and 2 functional test




Hope this helps.



