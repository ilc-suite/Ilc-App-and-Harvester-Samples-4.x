using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using Ilc.BusinessObjects;
using Ilc.DataCube.Contract;
using Ilc.BusinessObjects.Common;

namespace Ilc.SampleHarvester.ContactPictures.DataCube
{
    [Export(typeof(IDataCube))]
    public class ContactPicutresDataCube : Ilc.DataCube.SingleCredentialsDataCube, IDataCube, IDataCubeWithCredentialsAuth
    {
        public void ResolveInfoPoints(InfoPointProcess context, IInfoPointDataInterface dataInterface)
        {
            
        }

        public List<ObjectType> GetCollectTypes(string tenant)
        {
            return new List<ObjectType>();
        }

        public void CollectInformations(InformationProcess context, InfoPoint infoPoint, IInformationDataInterface dataInterface)
        {
            
        }

        /// <summary>
        /// This function is called when the Harvester is configured to Expand objects of a certain type
        /// Step 4
        /// Now we are expanding Person objects with a picture url, that we retrive from a different PictureService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="informationIds"></param>
        /// <param name="dataInterface"></param>
        public void ExpandInformations(InformationProcess context, List<string> informationIds, IInformationDataInterface dataInterface)
        {
            var pictures = new PictureService();

            // Get the stored credentials, so we can authenticate on the PictureService
            var identity = base.GetCredentials(context.Session);
            if (identity != null)
            {
                var authResult = pictures.Authenticate(identity.Username, identity.GetPassword());
                if (authResult == false)
                {
                    ResetCredentials(context.Session, identity.Username);
                    Ilc.NotificationManager.NotificationHandler.Instance.FireApiLoginExpired(context.Session.Tenant, identity.UserId, AppName, AppName);
                }
            }
            else
                Ilc.NotificationManager.NotificationHandler.Instance.FireApiLoginExpired(context.Session.Tenant, identity.UserId, AppName, AppName);

            
            // Iterate throu the informations and check if it is a Person object,
            // that we can try to expand with the PictureUrl
            // and signaling the server if we updated an object or not.
            for (int i = 0; i < informationIds.Count; i++)
            {
                var informationId = informationIds[i];
                var item = dataInterface.GetExisting(informationId);
                var person = item as Person;
                if (person != null)
                {
                    
                    var url = pictures.Get(person);
                    person.PictureUrl = url;
                    
                    if(!string.IsNullOrEmpty(url))
                        dataInterface.Update(informationId, item);
                    else
                        dataInterface.NoUpdate(informationId);
                }

                // Flush the data every 25 items so long running iterations will get there updates in between. 
                // A final flush is automatically invoked so you dont need to take care of that.
                if (i % 25 == 0)
                    dataInterface.Flush();
            }
        }

        /// <summary>
        /// Rquired property for IDataCubeWithCredentialsAuth to identify your autheticating service
        /// </summary>
        public override string AppName
        {
            get { return "ContactPicutres"; }
        }

        /// <summary>
        /// Rquired property for IDataCubeWithCredentialsAuth
        /// </summary>
        public override string Category
        {
            get { return "Social"; }
        }

        /// <summary>
        /// Rquired function for IDataCubeWithCredentialsAuth.
        /// This function is called to get sure the Harvester is ready to authorize user
        /// Step 5
        /// Only correct configured Harvester can test credentials. Check your Configuration in this function :)
        /// </summary>
        protected override bool IsConfigurated(SessionInfo session)
        {
            return true;
        }

        /// <summary>
        /// Rquired function for IDataCubeWithCredentialsAuth.        
        /// This function is called when the user tries to enter his credentials for the service used in the Background.
        /// Step 5
        /// To be able to store only correct credentials, this function tests the credentails and stores them,
        /// when the returned value is true.
        /// </summary>
        protected override bool TestCredentials(SessionInfo session, Ilc.DataCube.Data.CredentialsApiIdentity identity)
        {
            var service = new PictureService();
            var test = service.Authenticate(identity.Username, identity.GetPassword());            
            return test;
        }
    }
}
