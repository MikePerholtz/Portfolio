using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using Microsoft.AspNetCore.Identity;

namespace Portfolio.Data.Entities
{
  public class ContactUsMessage
  {
      public int Id { get; set; }
      public string ContactName { get; set; }
      public string Email { get; set; }
      public string Message { get ; set; }      
      public Point Location { get; set; }
  }
}
