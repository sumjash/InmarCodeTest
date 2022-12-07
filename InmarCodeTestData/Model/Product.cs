using System;
using System.Collections.Generic;

namespace InmarCodeTestData
{
  public class Product //: IComparable<Product>
  {
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public Product(string name,decimal price,string description)
    {
      Name = name;
      Price = price;
      Description = description;
    }

    public int CompareTo(Product obj)
    {
      if (this.Price == obj.Price)
        return 0;
      if (this.Price > obj.Price)
        return 1;
      else
        return -1;
        
    }
  }

  public class ProductComparer : IComparer<Product>
  {
    public int Compare(Product x, Product y)
    {
      return x.CompareTo(y);
    }
  }
}
