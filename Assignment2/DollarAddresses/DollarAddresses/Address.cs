using System.Collections.Generic;


namespace DollarAddresses
{

    public class Address
    {
        public int ADDRESS_NUMBER { get; set; }
        public string STREETNAME { get; set; }
        public string SUFFIX { get; set; }

    }

    public class Feature
    {
        public Address attributes { get; set; }
    }


    public class RootObject
    { 
        public List<Feature> features { get; set; }
    }
}
