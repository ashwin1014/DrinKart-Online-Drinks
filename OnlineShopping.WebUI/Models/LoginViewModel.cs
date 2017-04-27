using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace OnlineShopping.WebUI.Models
{

    public class LoginViewModel
    {
        [Required(ErrorMessage ="User Name Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Enter Password")]
        [StringLength(50, MinimumLength =6)]
        public string Password { get; set; }
    }
}