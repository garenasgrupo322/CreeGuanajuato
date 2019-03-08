using System;
using System.Collections.Generic;

namespace CreeGuanajuato.Models
{
    public class GoogleAPI
    {
        public List<result> results { get; set; }
        public string status { get; set; }

    }

    public class result
    {
        public geometry geometry { get; set; }
    }

    public class geometry
    {
        public location location { get; set; }
    }

    public class location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}
