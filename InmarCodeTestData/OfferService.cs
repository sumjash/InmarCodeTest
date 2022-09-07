using InmarCodeTestData.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InmarCodeTestData
{
  public class OfferService
  {
    private IList<Product> _Inventory;

    public OfferService()
    {
      _Inventory = FillProducts();
    }

    public Product AddProduct(string name, decimal price, string desc)
    {
      var product = new Product(name, price, desc);
      _Inventory.Add(product);
      return product;
    }

    private IList<Product> FillProducts()
    {
      return new List<Product>()
      {
        new Product("P1",1000,"P1 Desc"),
        new Product("P2",200,"P2 Desc"),
        new Product("P3",400,"P3 Desc"),
        new Product("P4",700,"P4 Desc"),
        new Product("P5",600,"P5 Desc"),
        new Product("P6",800,"P6 Desc")
      };
    }

    public IList<Product> SortedProducts()
    {
      _Inventory.ToList().Sort(new ProductComparer());
       Array.Sort<Product>(_Inventory.ToArray());
      return _Inventory;
    }

    public IList<Product> GetAllProducts()
    {
      return _Inventory;
    }

    public IList<Offer> GetTodaysOffers()
    {
      var randomOffers = new List<Offer>();
      string offerNamePrefix = "ComboPackage";
      var random = new Random();
      for(int i=0;i<=3;i++)
      {
        var offerName = string.Format("{0}{1}", offerNamePrefix, i + 1);
        var productList = new List<Product>();
        for (int j = 0; j <=2; j++)
        {
          var randomNumber1 = random.Next(0, 5);
          var product = _Inventory[randomNumber1];
          productList.Add(product);
        }
        randomOffers.Add(new Offer(offerName, productList));
      }

      return randomOffers;
    }
  }
}
