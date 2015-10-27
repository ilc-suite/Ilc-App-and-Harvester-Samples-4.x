# ILC App and Harvester Samples

## Sample Harvester: CredentialAuth

The Ilc.SampleHarvester.ExpandContact harvester of step 3 is extended to use credential authentication. 
This sample illustrates how to implement authentication for 3rd party applications. 
The harvester is modified to now only serve pictures to authenticated users.

Since this example uses a web server it is not possible to test the harvester without having an ILC system with mongo db set up.
The harvester needs a configuration with tenant and web server url.
To configure the harvester complete following steps:
 - Open your mongo db and add a collection 'Picture.Server' to the Ilc.Config database.
 - Start the web server and copy the web server url
 - To the 'Picture.Server' collection add an item with tenant and web server url. For example:  
		{
			"Tenant" : "Demo",
			"Url" : "http://localhost:57211",
		}
 Now you can test the harvester. Note that the tenant in the tab 'Environment' in the Harvester Testcenter has to match the tenant that is configured in your mongo db. If you then go to the tab 'AvailableApis' and press the play button, the api 'CredentialsTest' should appear as result. Now you can connect to the api under 'UpdateCredentials' with the user "admin" and password "admin".
