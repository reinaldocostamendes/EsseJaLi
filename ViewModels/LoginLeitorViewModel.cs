using System.ComponentModel.DataAnnotations;

namespace EsseJaLi.ViewModels
{
    public class LoginLeitorViewModel
    {
        internal string ReturnUrl;

        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }    
    }
}
