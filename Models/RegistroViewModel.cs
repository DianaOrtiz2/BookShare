using System.Xml.XPath;
using System.ComponentModel.DataAnnotations;
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BookShare.Models;

public class RegistroViewModel
{
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [EmailAddress(ErrorMessage = "el campo debe ser un correo electronico valido")]
    public string Email {get; set;}

    [Required(ErrorMessage = "El campo {0} es requerido")]
    [DataType(DataType.Password)]
    public string Password {get; set;}

    [Required(ErrorMessage = "El campo {0} es requerido")]
    public string FullName { get; set; }

    [Required]
     public DateTime BirthDate { get; set; }

    [Required]
     public string gender {get; set;}

     public IEnumerable<SelectListItem> genderOptions { get; set; }


}
