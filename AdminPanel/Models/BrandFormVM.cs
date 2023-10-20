using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class BrandFormVM
    {
        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(256)]
        public string Name { get; set; }
    }
}
