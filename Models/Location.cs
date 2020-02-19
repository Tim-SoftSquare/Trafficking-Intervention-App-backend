using System;

namespace Trafficking_Intervention_backend {
    public class LocationEntity {
        public int locationID {get; set; }
        public string name {get; set; }
        public string address {get; set; }
        public string city {get; set; }
        public string state {get; set; }        
        public string zipCode {get; set; }
        public string locationType {get; set;}

        
    }
}