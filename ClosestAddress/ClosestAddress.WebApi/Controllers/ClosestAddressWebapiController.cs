using ClosestAddress.Constants;
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
        Addresses AddressesServices = new Addresses();
        [HttpGet]
        public IHttpActionResult Get(string originAddress)
        {
            var response = new AddressResponse();
            try
            {
                var addresses = GetClosestAddress(originAddress);
                if (addresses.Count > 0)
                {
                    response.AddressResults = addresses;
                    response.ResultCount = addresses.Count;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return new JsonResult<AddressResponse>(response, new JsonSerializerSettings(), Encoding.UTF8, this);
        }
        public List<Address> GetClosestAddress(string address)
        {
            var list = AddressesServices.GetAllAddresses();
            string allAddresses = String.Join("|", list);
            var addresses = GetTopLocation(address, allAddresses);
            if (addresses.Count > 0)
            {
                addresses = addresses.OrderBy(x => x.KM).Take(AddressConstant.AddressCount).ToList();
            }
            return addresses;
        }
        private List<Address> GetTopLocation(string originAddress, string destinationAddresses)
        {
            string distance = string.Empty;
            List<Address> addresses = new List<Address>();
            try
            {
                var requestUri = string.Format(AddressConstant.GoogleAPIUrl + "?origins={0}&destinations={1}&key={2}",
                   Uri.EscapeDataString(originAddress), Uri.EscapeDataString(destinationAddresses), AddressConstant.GoogleAPIKey);
                var request = WebRequest.Create(requestUri);
                var response = request.GetResponse();
                if (response != null)
                {
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var distanceResponse = streamReader.ReadToEnd();
                        if (!string.IsNullOrEmpty(distanceResponse))
                        {
                            var result = JsonConvert.DeserializeObject<Response>(distanceResponse);
                            if (result == null || result.status == null || result.status != "OK")
                            {
                                result = JsonConvert.DeserializeObject<Response>(AddressConstant.JsonResponse);
                            }
                            if (result?.rows != null && result.destination_addresses != null)
                            {
                                int index = 0;
                                foreach (var destination in result.destination_addresses)
                                {
                                    if (result?.rows[0]?.elements[index]?.distance != null)
                                    {
                                        string km = result.rows[0].elements[index].distance.text;
                                        if (!string.IsNullOrEmpty(km))
                                        {
                                            if (km.Split(' ').Length > 0 && !string.IsNullOrEmpty(km.Split(' ')[0]))
                                            {
                                                addresses.Add(new Address
                                                {
                                                    Name = destination,
                                                    KM = Convert.ToInt32(km.Split(' ')[0])
                                                });
                                            }
                                        }
                                    }
                                    index++;
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
            return addresses;
        }
    }
}