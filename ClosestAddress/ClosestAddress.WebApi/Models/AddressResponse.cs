using System.Collections.Generic;

namespace ClosestAddress.WebApi.Models
{
    public class AddressResponse
    {
        public AddressResponse()
        {
            AddressResults = new List<Address>();
        }
        public List<Address> AddressResults { get; set; }
        public int ResultCount { get; set; }
    }
}