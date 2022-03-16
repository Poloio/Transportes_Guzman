using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TransGuzman_Entities
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage="Nos hace falta un nombre de usuario.")]
        [Display(Name = "Nombre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Oops. Nos falta tu contraseña...")]
        [MinLength(10, ErrorMessage = "La contraseña debe tener 10 caracteres al menos.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
