using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InmarCodeTestData.Model
{
  public class Offer
  {
    public string Name { get; set; }
    public IList<Product> Products { get; set; }

    public Offer(string name, IList<Product> products)
    {
      Name = name;
      Products = products;
    }
  }
}
