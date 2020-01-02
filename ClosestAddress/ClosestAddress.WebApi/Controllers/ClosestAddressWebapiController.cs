using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClosestAddress.WebApi.Controllers
{
    public class ClosestAddressWebapiController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "value";
        }
        protected void GetTopLocation(string originAddress, string destinationAddress)
        {
            var requestUri = string.Format("https://maps.googleapis.com/maps/api/distancematrix/xml" + "?origins={0}&destinations={1}&key={2}",
               Uri.EscapeDataString(originAddress), Uri.EscapeDataString(destinationAddress), "AIzaSyByr898hPiRTzISYBMw13TnNjNmHyIkvH8");
            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            if (response == null)
            {
               string jsonResponse= "@{\"destination_addresses\" : [ \"New York, NY, USA\" ],\"origin_addresses\" : [ \"Washington, DC, USA\" ],\"rows\" : [{\"elements\" : [{\"distance\" : {\"text\" : \"225 mi\",\"value\" : 361715},\"duration\" : {\"text\" : \"3 hours 49 mins\",\"value\" : 13725},\"status\" : \"OK\"}] } ], \"status\" : \"OK\"}";

            }

        }
    }
}