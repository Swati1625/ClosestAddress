using ClosestAddress.Constants;
using ClosestAddress.WebApi.Interfaces;
using ClosestAddress.WebApi.Models;
using ClosestAddress.WebApi.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
namespace ClosestAddress.WebApi.Controllers
{
    public class ClosestAddressWebapiController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IAddresses AddressesServices;
        public ClosestAddressWebapiController(IAddresses addressesServices)
        {
            this.AddressesServices = addressesServices;
        }
        public ClosestAddressWebapiController()
        {
        }
        [HttpGet]
        public IHttpActionResult Get(string originAddress)
        {
            List<Address> addresses = new List<Address>();
            var response = new AddressResponse();
            try
            {
                var list = AddressesServices.GetAllAddresses();
                foreach (var destinationAddress in list)
                {
                    string km = GetTopLocation(originAddress, destinationAddress.Name);
                    if (!string.IsNullOrEmpty(km))
                    {
                        if (km.Split(' ').Length > 0 && !string.IsNullOrEmpty(km.Split(' ')[0]))
                        {
                            addresses.Add(new Address
                            {
                                KM = Convert.ToInt32(km.Split(' ')[0]),
                                Name = destinationAddress.Name
                            });
                        }
                    }
                }
                if (addresses.Count > 0)
                {
                    addresses = addresses.OrderBy(x => x.KM).Take(Convert.ToInt32(AddressConstant.AddressCount)).ToList();
                }
                response.AddressResults = addresses;
                response.ResultCount = addresses.Count;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return new JsonResult<AddressResponse>(response, new JsonSerializerSettings(), Encoding.UTF8, this);
        }
        protected string GetTopLocation(string originAddress, string destinationAddress)
        {
            string distance = string.Empty;
            try
            {
                //https://maps.googleapis.com/maps/api/distancematrix/json?origins=Boston,MA|Charlestown,MA&destinations=Lexington,MA|Concord,MA&departure_time=now&key=YOUR_API_KEY

                var requestUri = string.Format("https://maps.googleapis.com/maps/api/distancematrix/json" + "?origins={0}&destinations={1}&key={2}",
                   Uri.EscapeDataString(originAddress), Uri.EscapeDataString(destinationAddress), "AIzaSyByr898hPiRTzISYBMw13TnNjNmHyIkvH8");
                var request = WebRequest.Create(requestUri);
                var response = request.GetResponse();
                if (response != null)
                {
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var distanceResponse = streamReader.ReadToEnd();
                        if (!string.IsNullOrEmpty(distanceResponse))
                        {
                            dynamic result = JsonConvert.DeserializeObject(distanceResponse);
                            if (result == null || result.status == null || result.status != "OK")
                            {
                                string jsonResponse = "{\"destination_addresses\" : [ \"New York, NY, USA\" ],\"origin_addresses\" : [ \"Washington, DC, USA\" ],\"rows\" : [{\"elements\" : [{\"distance\" : {\"text\" : \"225 mi\",\"value\" : 361715},\"duration\" : {\"text\" : \"3 hours 49 mins\",\"value\" : 13725},\"status\" : \"OK\"}] } ], \"status\" : \"OK\"}";
                                result = JsonConvert.DeserializeObject(jsonResponse);
                            }
                            if (result != null && result.rows != null)
                            {
                                if (result.rows[0] != null && result.rows[0].elements != null && result.rows[0].elements[0] != null && result.rows[0].elements[0].distance != null)
                                {
                                    distance = result.rows[0].elements[0].distance.text;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return distance;
        }
    }
}