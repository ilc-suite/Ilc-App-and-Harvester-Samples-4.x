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
        private Ilc.Diagnostics.ILoggingService logger = Ilc.Diagnostics.LoggingService.Instance;

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
        public List<BikeProduct> LoadProductByCompany(Company company)
        {
            List<BikeProduct> result = new List<BikeProduct>();
            using (var service = new SampleService.SampleServiceClient())
            {
                List<Product> response = service.LoadProductByCompany(company);

                response.ForEach(x =>
                {
                    var bike = new BikeProduct()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        InstallationDate = x.InstallationDate,
                    };
                    ConvertPropertyBag(x, bike);
                    result.Add(bike);
                });
            }
            return result;
        }

        private BikeProduct ConvertPropertyBag(Product x, BikeProduct bike)
        {
            try
            {
                if (x.PropertyBag.Keys.Contains("Articlenumber"))
                    bike.Articlenumber = x.PropertyBag["Articlenumber"].ToString();
                if (x.PropertyBag.Keys.Contains("MakeFlag"))
                    bike.MakeFlag = (bool)x.PropertyBag["MakeFlag"];
                if (x.PropertyBag.Keys.Contains("FinishedGoodsFlag"))
                    bike.FinishedGoodsFlag = (bool)x.PropertyBag["FinishedGoodsFlag"];
                if (x.PropertyBag.Keys.Contains("Color"))
                    bike.Color = (string)x.PropertyBag["Color"];
                if (x.PropertyBag.Keys.Contains("StandardCost"))
                    bike.StandardCost = (decimal)x.PropertyBag["StandardCost"];
                if (x.PropertyBag.Keys.Contains("ListPrice"))
                    bike.ListPrice = (decimal)x.PropertyBag["ListPrice"];
                if (x.PropertyBag.Keys.Contains("Size"))
                    bike.Size = (string)x.PropertyBag["Size"];
                if (x.PropertyBag.Keys.Contains("SizeMeasure"))
                    bike.SizeMeasure = (string)x.PropertyBag["SizeMeasure"];
                if (x.PropertyBag.Keys.Contains("WeightMeasure"))
                    bike.WeightMeasure = (string)x.PropertyBag["WeightMeasure"];
                if (x.PropertyBag.Keys.Contains("Weight"))
                    bike.Weight = (decimal?)x.PropertyBag["Weight"];
                if (x.PropertyBag.Keys.Contains("DaysToManufacture"))
                    bike.DaysToManufacture = (int)x.PropertyBag["DaysToManufacture"];
                if (x.PropertyBag.Keys.Contains("ProductLine"))
                    bike.ProductLine = (string)x.PropertyBag["ProductLine"];
                if (x.PropertyBag.Keys.Contains("Class"))
                    bike.Class = (string)x.PropertyBag["Class"];
                if (x.PropertyBag.Keys.Contains("Style"))
                    bike.Style = (string)x.PropertyBag["Style"];
                if (x.PropertyBag.Keys.Contains("SubCategoryId"))
                    bike.SubCategoryId = (int)x.PropertyBag["SubCategoryId"];
                if (x.PropertyBag.Keys.Contains("ProductModelId"))
                    bike.ProductModelId = (int)x.PropertyBag["ProductModelId"];
                if (x.PropertyBag.Keys.Contains("SellEndDate"))
                    bike.SellEndDate = (DateTime?)x.PropertyBag["SellEndDate"];
                if (x.PropertyBag.Keys.Contains("CustomerOrderQty"))
                    bike.CustomerOrderQty = (int)x.PropertyBag["CustomerOrderQty"];
            }
            catch(Exception ex)
            {
                logger.Warning("could not convert propertybag!", ex);
            }
            return bike;
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