using ClosestAddress.Constants;
using ClosestAddress.Tests.Constants;
using ClosestAddress.WebApi.Controllers;
using ClosestAddress.WebApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ClosestAddress.Tests
{
    [TestClass]
    public class ClosestAddressTest
    {
        /// <summary>
        /// Get all addresses: it should return list of addresses
        /// </summary>
        [TestMethod]
        public void GetAllAddress_ShouldReturnResult()
        {
            Addresses addresses = new Addresses();
            var result = addresses.GetAllAddresses();
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Should return top closest address
        /// </summary>
        [TestMethod]
        public void GetClosestAddress_ShouldReturnResult()
        {
            ClosestAddressWebapiController controller = new ClosestAddressWebapiController();
            var result = controller.GetClosestAddress(AddressConstants.OriginAddress);
            Assert.IsNotNull(result);
        }
        /// <summary>
        /// Should return top 5 closest address
        /// </summary>
        [TestMethod]
        public void GetClosestAddress_ShouldReturnFiveResult()
        {
            ClosestAddressWebapiController controller = new ClosestAddressWebapiController();
            var result = controller.GetClosestAddress(AddressConstants.OriginAddress);
            Assert.AreEqual(result.Count, AddressConstant.AddressCount);
        }
        /// <summary>
        /// Should return top closest address in sort order
        /// </summary>
        [TestMethod]
        public void GetClosestAddress_ShouldReturnNearestResult()
        {
            ClosestAddressWebapiController controller = new ClosestAddressWebapiController();
            var result = controller.GetClosestAddress(AddressConstants.OriginAddress);
            var expectedResult = result.OrderBy(x => x.KM);
            Assert.IsTrue(expectedResult.SequenceEqual(result));
        }
    }
}
