using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Domain.Entities
{
           public class ShippingDetails
        {
            [Required(ErrorMessage = "Enter a Name")]
            public string Name { get; set; }
            [Required(ErrorMessage ="Enter first Address Line")]
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string Line3 { get; set; }
            [Required(ErrorMessage = "Enter City Name")]
            public string City { get; set; }

            [Required(ErrorMessage = "Enter State")]
            public string State { get; set; }
            public string Zip { get; set; }

            [Required(ErrorMessage = "Enter Country")]
            public string Country { get; set; }
            public bool GiftWrap { get; set; }
        }
}

