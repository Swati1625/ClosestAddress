using ClosestAddress.Constants;
using ClosestAddress.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ClosestAddress.WebApi.BAL
{
    public class Addresses
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<Address> GetAllAddresses()
        {
           var addressList = new List<Address>();
            try
            {
                string filePath = HttpContext.Current.Server.MapPath(AddressConstant.CSVLocation);
                if (!string.IsNullOrWhiteSpace((filePath)))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            if (!string.IsNullOrWhiteSpace(reader.ReadLine()))
                            {
                                string address = reader.ReadLine().Trim();
                                if (!string.IsNullOrWhiteSpace(address))
                                {
                                    addressList.Add(new Address
                                    {
                                        Name = address
                                    });
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
            return addressList;
        }
    }
}