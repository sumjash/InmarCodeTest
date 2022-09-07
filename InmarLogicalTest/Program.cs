using System;
using System.Text;

namespace InmarLogicalTest
{
  class Program
  {

    public void PrintFizzBuzz(int number)
    {
      bool isDivideByThree = number % 3 == 0;

      bool isDivideByFive = number % 5 == 0;
      bool isDivideByBoth = isDivideByThree && isDivideByFive;
      if (isDivideByThree)
        Console.WriteLine("fizz");
      if (isDivideByFive)
        Console.WriteLine("buzz");
      if (isDivideByBoth)
        Console.WriteLine("fizzbuzz");
    }

    public string Reverse(string s)
    {
      if (string.IsNullOrEmpty(s))
        return string.Empty;

      StringBuilder builder = new StringBuilder();
      var strLength = s.Length;
      for(int i=strLength-1;i>=0;i--)
      {
        builder.Append(s[i]);
      }
      return builder.ToString();
    }
    static void Main(string[] args)
    {
      Program p = new Program();
      p.PrintFizzBuzz(15);
      Console.WriteLine(p.Reverse("abcde"));
      Console.WriteLine("Hello World!");
    }
  }
}
