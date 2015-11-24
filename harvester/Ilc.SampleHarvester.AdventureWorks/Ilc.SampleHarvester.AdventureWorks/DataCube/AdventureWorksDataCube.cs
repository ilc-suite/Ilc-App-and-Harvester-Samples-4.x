using System;
using System.Linq;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using Ilc.BusinessObjects;
using Ilc.DataCube.Contract;
using Ilc.BusinessObjects.Common;
using Ilc.BusinessObjects.AdventureWorks;

namespace Ilc.SampleHarvester.AdventureWorks.DataCube
{
    [Export(typeof(IDataCube))]
    public class AdventureWorksDataCube : IDataCube, IDetailsDataCube
    {
        ///<summary>
        /// This function is called when a new Context is generated and InfoPoints are beeing collected by IlcCore Server
        /// Step 1 
        /// The task is to collect InfoPoints of Type Company
        ///</summary>        
        public void ResolveInfoPoints(InfoPointProcess context, IInfoPointDataInterface dataInterface)
        {
            if (context.Context == null)
                return;

            var parser = new ContextParser(context.Context);
            var companyLoader = new CompanyLoader();

            if (parser.IsCompanyName)
            {
                var companies = companyLoader.LoadCompanyByName(parser.CompanyName);
                dataInterface.Insert(companies);
            }
            else if (parser.IsDebitorNumber || parser.IsKreditorNumber)
            {
                var company = companyLoader.LoadCompany(parser.DebitorNumber);
                dataInterface.Insert(company);
            }
            else if (parser.IsEmailAddress)
            {
                var companies = companyLoader.LoadCompanyByContactEmail(parser.EmailAddress.ToString());
                dataInterface.Insert(companies);
            }
        }

        public List<ObjectType> GetCollectTypes(string tenant)
        {
            return new List<ObjectType> 
            {
                new ObjectType(typeof(Company), "Company"),                
                new ObjectType(typeof(ContactPerson), "ContactPerson"),
                new ObjectType(typeof(BikeProduct), "BikeProduct"),
            };
        }

        /// <summary>
        /// This function is called when the IlcCore Server request informations for an InfoPoint.
        /// Step 2
        /// To get Informations into your apps you need insert the appropiate BusinessObject into the dataInterface
        /// </summary>
        /// <param name="context"></param>
        /// <param name="infoPoint"></param>
        /// <param name="dataInterface"></param>
        public void CollectInformations(InformationProcess context, InfoPoint infoPoint, IInformationDataInterface dataInterface)
        {
            var company = infoPoint.Value as Company;
            if (company == null)
                return;
            dataInterface.Insert(company);
            dataInterface.Flush();

            if (dataInterface.MustCollect<ContactPerson>())
            {
                var contactsLoader = new ContactsLoader();
                var contacts = contactsLoader.LoadContactsByCompany(company);
                dataInterface.Insert(contacts);
                dataInterface.Flush();
            }
            
            // Step 3 
            // we Add the ProductsLoader and the Ilc.BusinessObjects.AdventureWorks Library
            // to showcase the Detail-Loading procedure
            // Details-Loading is deferred by creating and setting an DetailsLink for the InformationObject
            if (dataInterface.MustCollect<BikeProduct>())
            {
                var productsLoader = new ProductsLoader();
                var products = productsLoader.LoadProductByCompany(company);

                foreach (var product in products)
                {
                    var detailsLink = new List<DetailsLink>();
                    detailsLink.Add(dataInterface.CreateDetailsLink(Constants.ProductPhotoDetailslink, product.Id));
                    dataInterface.Insert(product, null, detailsLink);
                }
                dataInterface.Flush();
            }
        }

        /// <summary>
        /// This Harvester does not Expand Informations. You can leave this empty
        /// </summary>        
        public void ExpandInformations(InformationProcess context, List<string> informationIds, IInformationDataInterface dataInterface)
        {
            
        }

        /// <summary>
        /// Step 3 
        /// When the Client Opens the InformationObject in Detailsview it requests the Detailslink to be loaded.
        /// Though you can collect many different types of Details you have to identify them by there DetailsLinkName, which are best stored in Constants
        /// </summary>
        /// <param name="process"></param>
        /// <param name="dataInterface"></param>
        public void CollectDetails(DetailsProcess process, IDetailsDataInterface dataInterface)
        {
            if (dataInterface.IsIdDetailsLink(Constants.ProductPhotoDetailslink))
            {
                var productsLoader = new ProductsLoader();
                var productid = dataInterface.GetLinkId();
                var items = productsLoader.GetProductPhoto(productid);
                var item = items.FirstOrDefault();
                dataInterface.Set(item);
            }
        }
    }
}
