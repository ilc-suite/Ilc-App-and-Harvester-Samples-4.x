using Ilc.BusinessObjects.Common;
using Ilc.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Ilc.SampleHarvester.AdventureWorks.DataCube
{
    public class CompanyLoader
    {
        /// <summary>
        /// Creates a CompanyLoader class.
        /// </summary>        
        public CompanyLoader() { }

        /// <summary>
        /// Loads one or more companies by its name.
        /// </summary>
        /// <param name="name">The name of the company to be loaded.</param>
        /// <returns>A list of compnaies</returns>
        public List<Company> LoadCompanyByName(string name)
        {
            List<Company> result = null;
            using (var service = new SampleService.SampleServiceClient())
            {
                result = service.LoadCompanyByName(name);
            }
            
            return result;

        }

        /// <summary>
        /// Loads a company by its id
        /// </summary>
        /// <param name="businessEntityId">The unique identifier for the company.</param>
        /// <returns></returns>
        public Company LoadCompany(long businessEntityId)
        {
            Company result = null;
            using (var service = new SampleService.SampleServiceClient())
            {
                result = service.LoadCompany(businessEntityId);
            }
            return result;
        }

        /// <summary>
        /// Loads a company by the email address of one of its contacts.
        /// </summary>
        /// <param name="email">the email address of the contact person.</param>
        /// <returns>A List of companies</returns>
        public List<Company> LoadCompanyByContactEmail(string email)
        {
            List<Company> result = null;
            using (var service = new SampleService.SampleServiceClient())
            {
                result = service.LoadCompanyByContactEmail(email);
            }
            return result;
        }        
    }
}