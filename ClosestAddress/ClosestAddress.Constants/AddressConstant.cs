using System.Web.Configuration;

namespace ClosestAddress.Constants
{
    public class AddressConstant
    {
        //CSV Location
        public static string CSVLocation = "~/File/addressListAustralia.txt";
        // Display total number of address
        public static int AddressCount = 5;
        //Cache key name
        public static string AddressCacheKey = "AddressCache";
        //Cache duration
        public static string CacheDurantion = "1500";
        //Google map API key URL
        public static string GoogleAPIUrl = "https://maps.googleapis.com/maps/api/distancematrix/json";
        //Google api key configure in Web.config
        public static string GoogleAPIKey = WebConfigurationManager.AppSettings["GoogleAPIKey"];
        //Sample json of closest address
        public static string JsonResponse = "{\"destination_addresses\": [\"86 Castlereagh Street, Tahmoor NSW 2573\",\"71 Akonna Street, Wynnum QLD 4178\",\"55 Bank Street, Traralgon VIC 3844\",\"2 The Causeway, Kingston ACT 2604\",\"15 Brockhoff Drive, Burwood VIC 3125\"],\"origin_addresses\": [\"37 Sharp Street, Newtown VIC 3220\"],\"rows\": [{\"elements\": [{\"distance\": {\"text\": \"100 mi\",\"value\": 361715},\"duration\": {\"text\": \"1 hours 49 mins\",\"value\": 13725},\"status\": \"OK\"},{\"distance\": {\"text\": \"200 mi\",\"value\": 361715},\"duration\": {\"text\": \"2 hours 49 mins\",\"value\": 13725},\"status\": \"OK\"},{\"distance\": {\"text\": \"300 mi\",\"value\": 361715},\"duration\": {\"text\": \"3 hours 49 mins\",\"value\": 13725},\"status\": \"OK\"},{\"distance\": {\"text\": \"400 mi\",\"value\": 361715},\"duration\": {\"text\": \"4 hours 49 mins\",\"value\": 13725},\"status\": \"OK\"},{\"distance\": {\"text\": \"500 mi\",\"value\": 361715},\"duration\": {\"text\": \"5 hours 49 mins\",\"value\": 13725},\"status\": \"OK\"}]}],\"status\": \"OK\"}";
    }
}
