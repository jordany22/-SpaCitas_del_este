using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaCitas_del_este.Utiles
{
    public class Validaciones
    {
        public static bool EsEmailValido(string email)
        {
            return Regex.IsMatch(email, @"^\S+@\S+\.\S+$");
        }

        public static bool EsTelefonoValido(string telefono)
        {
            return Regex.IsMatch(telefono, @"^[0-9]{8,10}$");
        }

        public static bool EsSoloLetras(string texto)
        {
            return Regex.IsMatch(texto, @"^[a-zA-Z\s]+$");
        }

        public static bool ValidarCamposFormulario(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox && string.IsNullOrWhiteSpace(control.Text))
                {
                    MessageBox.Show($"El campo {control.Name.Replace("txt", "")} es obligatorio",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus();
                    return false;
                }
            }
            return true;
        }

        public static bool FechaEsFutura(DateTime fecha)
        {
            return fecha >= DateTime.Today;
        }
    }
}
    

