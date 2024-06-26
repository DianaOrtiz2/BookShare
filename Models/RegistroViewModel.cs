using System.Xml.XPath;
using System.ComponentModel.DataAnnotations;
using System;
using System.Security.Cryptography.X509Certificates;
namespace BookShare.Models;

public class RegistroViewModel
{
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [EmailAddress(ErrorMessage = "el campo debe ser un correo electronico valido")]
    public string Email {get; set;}

    [Required(ErrorMessage = "El campo {0} es requerido")]
    [DataType(DataType.Password)]
    public string Password {get; set;}
}
