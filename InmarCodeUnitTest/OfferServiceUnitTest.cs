using InmarCodeTestApis.Controllers;
using InmarCodeTestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InmarCodeUnitTest
{
  [TestClass]
  public class OfferServiceUnitTest
  {
    [TestMethod]
    public void TestMethod1()
    {
      OfferService service = new OfferService();
      OfferController controller = new OfferController(service);
      var products = service.GetAllProducts();
      var apiProducts = controller.GetAllProduct();
      Assert.AreEqual(products.Count, apiProducts.Count);

    }
  }
}
