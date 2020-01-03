using ClosestAddress.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClosestAddress.WebApi.Interfaces
{
    public interface IAddresses
    {
         List<Address> GetAllAddresses();
    }
}