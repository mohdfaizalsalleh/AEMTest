using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class WellInfoModel
    {
        public int id { get; set; }
        public int platformId { get; set; }
        public string uniqueName { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}
