using InmarCodeTestData;
using InmarCodeTestData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InmarCodeTestApis.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OfferController : ControllerBase
  {
    private OfferService _Service;
    public OfferController(OfferService service)
    {
      _Service = service;
    }

    [HttpGet]
    [Route("GetTodaysOffer")]
    public IList<Offer> GetTodaysOffer()
    {
      return _Service.GetTodaysOffers();
    }

    [HttpGet]

    [Route("GetAllProduct")]
    public IList<Product> GetAllProduct()
    {
      var products = _Service.SortedProducts();
      var lowerstPriceProducts = products.ToList().TakeLast(3);
      return lowerstPriceProducts.ToList();
    }

    [HttpGet]

    [Route("GetProductWithSecondLowestPrice")]
    public Product GetProductWithSecondLowestPrice()
    {
    
      var products = _Service.SortedProducts();
      var count = products.Count();
      var lowerstPriceProducts = products.Reverse().ToList()[count -1];
      return lowerstPriceProducts;
    }

    [HttpPost]

    [Route("AddProduct")]
    public Product AddProduct(ProductResource productResource)
    {
      var product = _Service.AddProduct(productResource.Name, productResource.Price, 
        productResource.Description);
      return product;
     
    }
  }
}
