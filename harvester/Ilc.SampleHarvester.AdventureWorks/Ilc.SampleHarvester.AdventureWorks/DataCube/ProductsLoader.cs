using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;
using Ilc.DataCube.Contract;
using Ilc.BusinessObjects;
using Ilc.BusinessObjects.Common;
using Ilc.BusinessObjects.AdventureWorks;

namespace Ilc.SampleHarvester.AdventureWorks.DataCube
{
    public class ProductsLoader
    {
        /// <summary>
        /// Creates a ProductsLoader class.
        /// </summary>
        /// <param name="connectionString">A connection string to a AdventureWorkds database.</param>
        public ProductsLoader() { }

        /// <summary>
        /// Loads Product informations for a company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public List<Product> LoadProductByCompany(Company company)
        {
            List<Product> result = null;
            using (var service = new SampleService.SampleServiceClient())
            {
                result = service.LoadProductByCompany(company);
            }
            return result;
        }
        
        /// <summary>
        /// Loads the image data for a Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductPhoto> GetProductPhoto(string productId)
        {
            var result = new List<ProductPhoto>();
            using (var service = new SampleService.SampleServiceClient())
            {
                var photos = service.GetProductPhoto(productId);
                foreach (var photo in photos)
                {
                    result.Add(ReadPhoto(photo));
                }                
            }
            return result;
        }

        /// <summary>
        /// Converts the webservice response to the BusinessObject we defined in Ilc.BusinessObjects.AdventureWorks
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private ProductPhoto ReadPhoto(Dictionary<string, string> reader)
        {
            var p = new ProductPhoto
            {
                Id = int.Parse(reader["Id"]),
                LargePhotoFileName = reader["LargePhotoFileName"],
                ModifiedDate = DateTime.Parse(reader["ModifiedDate"]),
            };
            p.LargePhotoB64 = reader["LargePhotoB64"];
            return p;
        }
    }
}