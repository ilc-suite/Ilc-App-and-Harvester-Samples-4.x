# ILC App and Harvester Samples

## Sample Harvester: AdventureWorks

This project implements the necessary interfaces to work as an InformationPoint Harvester for an Ilc System.
The harvester collects InfoPoints of type Company.

### Collect InfoPoints

To make an Infopoint Harvester you need to implement the function ResolveInfoPoints and insert your data into the parameter *IInfoPointDataInterface dataInterface*.
The parameter context contains information about the searchterm the user entered and other inforamtions about the currently startet search.

```csharp
        public void ResolveInfoPoints(InfoPointProcess context, IInfoPointDataInterface dataInterface)
        {
            var companies = new List<Company>();
            ...
            dataInterface.Insert(companies);
            ...
        }
```


### Collect InformationObjects

The harvester also collects InformationObjects of type Company and ContactPerson for a Company InfoPoint. 
In order to get informations transported to the apps the function CollectInformations is called of your Harvester.
To return informations into the InformationProcess you insert them into the *IInformationDataInterface dataInterface* parameter

```csharp
        public void CollectInformations(InformationProcess context, InfoPoint infoPoint, IInformationDataInterface dataInterface)
        {
            ...
            dataInterface.Insert(contacts);
            ...
        }
```

You need to tell the system what type of InformationObjects you can return. Therefore the function GetCollectTypes is called of your Harvester.
Simply return your Object types in a list.

```csharp
        public List<ObjectType> GetCollectTypes(string tenant)
        {
            return new List<ObjectType> 
            {
                new ObjectType(typeof(Company), "Company"),                
                new ObjectType(typeof(ContactPerson), "ContactPerson"),        
            };
        }
```

### Custom InformationObjects and collect Details

In an Advanced scenario you want to define your own custom InformationObjects. 
We add a new dll project Ilc.BusinessObjects.AdventureWorks in which we define 
the custom types BikeProduct and ProductPhoto.
To create a proper InformationObject your class has to inherit from *InformationObject* or a subclass.
It is necessary to make a seperate dll project cause this dll is distributed to the server.

```csharp
        public class BikeProduct : Ilc.BusinessObjects.Common.Product
        {
            ...
        }
```

The harvesters CollectInformations function can collect informations of products and add DetailLinks for each product.

```csharp
        public void CollectInformations(InformationProcess context, InfoPoint infoPoint, IInformationDataInterface dataInterface)
        {
            ...
            var detailsLink = new List<DetailsLink>();
            detailsLink.Add(dataInterface.CreateDetailsLink(Constants.ProductPhotoDetailslink, product.Id));
            dataInterface.Insert(product, null, detailsLink);
            ...
        }
```

With the DetailsLink a client can request detailed information which only will be available when
an information is viewed in the details view. 
