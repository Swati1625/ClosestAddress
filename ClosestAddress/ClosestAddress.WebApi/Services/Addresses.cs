using ClosestAddress.Cache;
using ClosestAddress.Constants;
using ClosestAddress.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ClosestAddress.WebApi.Services
{
    public class Addresses : IAddresses
    {
        CustomCache CustomCache = new CustomCache();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<string> GetAllAddresses()
        {
            var addressList = new List<string>();
            try
            {
                string cacheKey = CustomCache.GetCacheKey(AddressConstant.AddressCacheKey).ToString();
                if (CustomCache.Exists(cacheKey))
                {
                    CustomCache.Get(cacheKey, out addressList);
                    if (addressList == null)
                    {
                        CustomCache.Clear(cacheKey);
                    }
                }
                if (!CustomCache.Exists(cacheKey))
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
                                        addressList.Add(address);
                                    }
                                }
                            }
                        }
                    }
                    if (addressList != null && addressList.Count > 0)
                    {
                        CustomCache.Add(addressList, cacheKey, Convert.ToDouble(AddressConstant.CacheDurantion));
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