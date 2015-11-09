# ILC App and Harvester Samples

## Sample Harvester for contact pictures

A new harvester project Ilc.SampleHarvester.ContactPictures for demonstrating expanding InformationObjects and Credential authentication.

### Expand InformationObjects

This harvester gets the InformationObjects of type Person, that were put into the DataInterface 
from the Ilc.SampleHarvester.AdventureWorks and extends the Persons data with an url of an image.
This happens, as the name might suggest, in the ExpandInformations function you find in the ContactPictureDatacute.cs file.

```csharp
    public void ExpandInformations(InformationProcess context, List<string> informationIds, IInformationDataInterface dataInterface)
    {
       ...
    }
```

### CredentialAuth

The harvester is extended to use credential authentication. 
This sample illustrates how to implement authentication for 3rd party applications. 
The harvester is modified to only serve pictures to authenticated users.

In order to ease implementation for authentication scenarios, you can simply derive your DataCube class form one of either the following classes in the **Ilc.DataCube** namespace
- SingleCredentialsDataCube
- SingleCredentialsTokenDataCube
- SingleOAuth2DataCube
- SingleOAuthDataCube

```csharp
    public class ContactPicutresDataCube : Ilc.DataCube.SingleCredentialsDataCube, IDataCube, IDataCubeWithCredentialsAuth
    {
        ...
    }
```

and implementing necessary function of the **IDataCubeWithCredential** or **IDataCubeWithOAuthApi** interface.

> This Sample handles authentication by itself for demonstrational purpose! In productive environment you will authenticate against a external systems