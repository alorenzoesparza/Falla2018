namespace Falla2018.Backend.Models
{
    using Domain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class UserView : User
    {
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(20, ErrorMessage = "El campo {0} debe contener como máximo {1} caracteres y un minimo de {2} caracteres.", MinimumLength = 2)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The field {0} is required")]
        [Compare("Password", ErrorMessage = "El password y la confirmación no coinciden.")]
        [Display(Name = "Confirmar Contraseña")]
        public string PasswordConfirm { get; set; }
    }
}