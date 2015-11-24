using Ilc.BusinessObjects;
using Ilc.BusinessObjects.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ilc.BusinessObjects.AdventureWorks
{
    [DataContract, Export(typeof(InformationObject))]
    public class BikeProduct : Product
    {
        [DataMember]
        public string Articlenumber { get; set; }

        [DataMember]
        public bool MakeFlag { get; set; }

        [DataMember]
        public bool FinishedGoodsFlag { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public decimal StandardCost { get; set; }

        [DataMember]
        public decimal ListPrice { get; set; }

        [DataMember]
        public string Size { get; set; }

        [DataMember]
        public string SizeMeasure { get; set; }

        [DataMember]
        public string WeightMeasure { get; set; }

        [DataMember]
        public decimal? Weight { get; set; }

        [DataMember]
        public int DaysToManufacture { get; set; }

        [DataMember]
        public string ProductLine { get; set; }

        [DataMember]
        public string Class { get; set; }

        [DataMember]
        public string Style { get; set; }

        [DataMember]
        public int SubCategoryId { get; set; }

        [DataMember]
        public int ProductModelId { get; set; }

        [DataMember]
        public DateTime? SellEndDate { get; set; }

        [DataMember]
        public int CustomerOrderQty { get; set; }
    }
}
