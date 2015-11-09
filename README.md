# ILC App and Harvester Samples

A collection of app and harvester samples that illustrates how to build your own ILC apps and harvester.

The projects demonstrate the different functionalities of harvester and apps. Using the ILC SDK you can test these samples without having a ILC Server set up.

## Harvester

**AdventureWorks Harvester**  
This sample is a harvester that demonstrates following functionalities

- Collect InfoPoints
- Collect InformationObjects
- Custom InformationObjects and collect Details


**ContactPicutre Harvester**  
This sample is a harvester that demonstrats following functionalities

- Expand InformationObjects
- Authentication with CredentialAuth

## Apps

These sample apps display InformationObjects of different types. Each project has a folder 'TestData' that contains json files with sample data for the different InformationObject. These test data can be used to test the apps with the ILC App Test Center. This test data has been created with the sample harvester. If you test the sample harvester with the ILC Harvester Test Center you can also export the collected data as json yourself and use it as test data for the sample apps.

**CompaniesApp**  
This app displays objects of type Company. Of all three samples it is the simplest one for it only defines a custom detail view with detail controller.

**ContactsApp**  
This app displays objects of type ContactPerson. It defines a custom listitem and details view.

**ProductsApp**  
The last sample app displays BikeProducts ( as created by the 'Custom InformationObjects and collect Details' harvester). It demonstrates how to handle detail links on the client side. The details link for the product picture that is added by the harvester us resolved and the picture displayed in the details view.
Furthermore the app has a custom board view and controller.
