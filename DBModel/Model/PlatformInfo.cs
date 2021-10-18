using System;
using System.Collections.Generic;
using System.Text;

namespace DBModel.Model
{
    public class PlatformInfo
    {
        public int Id { get; set; }
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
        public string UniqueName { get; set; }
        public string Latitute { get; set; }
        public string Longitude { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
