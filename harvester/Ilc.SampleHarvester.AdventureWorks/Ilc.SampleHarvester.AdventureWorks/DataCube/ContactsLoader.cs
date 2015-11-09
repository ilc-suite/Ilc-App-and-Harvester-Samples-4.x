using Ilc.BusinessObjects.Common;
using Ilc.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Ilc.SampleHarvester.AdventureWorks.DataCube
{
    public class ContactsLoader
    {
        /// <summary>
        /// Creates a ContactsLoader class.
        /// </summary>        
        public ContactsLoader() { }

        /// <summary>
        /// Loads one or more companies by its name.
        /// </summary>
        /// <param name="name">The name of the company to be loaded.</param>
        /// <returns>A list of compnaies</returns>
        public List<ContactPerson> LoadContactsByCompany(Company company)
        {
            List<ContactPerson> result = null;
            using (var service = new SampleService.SampleServiceClient())
            {
                result = service.LoadContactsByCompany(company);
            }
            return result;
        }        
    }
}