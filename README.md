# ILC App and Harvester Samples

A collection of app and harvester samples that illustrates how to build your own ILC apps and harvester.

The projects demonstrate the different functionalities of harvester and apps. Using the ILC SDK you can test these samples without having a ILC Server set up.

## Harvester

**Collect InfoPoints**  
This sample is a harvester that collects InfoPoints of type Company.

**Collect InformationObjects**  
In this sample the harvester collects InformationObjects for an InfoPoint of type Company.
It collects InformationObjects of type Company and ContactPerson.

**Expand InformationObjects**  
This harvester shows how to extend InformationObjects. The harvester extends InformationObjects of type ContactPerson (e.g. the objects that have been collected by the last example) with a picture for the contact.

**Custom InformationObjects and collect Details**  
This example shows how to define custom InformationObjects. It defines InformationObjects of type 'BikeProduct' and 'ProductPicture'. The harvester collects objects of this type Company, ContactPerson and BikeProduct for a Company InfoPoint. In addition to that the use of detail links is demonstrated. A details link is added to the collected BikeProducts. With this detail link an app can request detailed data for a BikeProduct. In this example the harvester returns a picture for the product.

**CredentialAuth**  
The 'Expand InformationObjects' harvester is extended to use credential authentication. 
This sample illustrates how to implement authentication for 3rd party applications. 
The harvester is modified to now only serve pictures to authenticated users.


## Apps

These sample apps display InformationObjects of different types. Each project has a folder 'TestData' that contains json files with sample data for the different InformationObject. These test data can be used to test the apps with the ILC App Test Center. This test data has been created with the sample harvester. If you test the sample harvester with the ILC Harvester Test Center you can also export the collected data as json yourself and use it as test data for the sample apps.

**CompaniesApp**  
This app displays objects of type Company. Of all three samples it is the simplest one for it only defines a custom detail view with detail controller.

**ContactsApp**  
This app displays objects of type ContactPerson. It defines a custom listitem and details view.

**ProductsApp**  
The last sample app displays BikeProducts ( as created by the 'Custom InformationObjects and collect Details' harvester). It demonstrates how to handle detail links on the client side. The details link for the product picture that is added by the harvester us resolved and the picture displayed in the details view. 
Furthermore the app has a custom board view and controller.



