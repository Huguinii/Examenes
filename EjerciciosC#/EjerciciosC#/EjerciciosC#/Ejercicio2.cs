using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosC
{
    public class Ejercicio2
    {
        /*
         Los cajeros automáticos permiten códigos PIN de 4 o 6 dígitos y los
         códigos PIN no pueden contener más que exactamente 4 dígitos o
         exactamente 6 dígitos. Si a la función se le pasa una cadena de PIN
         válida, devuelve true, de lo contrario devuelve false.
        */
        public static bool EsPinValido(string pin)
        {
            // Verifica si la longitud es 4 o 6 y si todos los caracteres son dígitos
            return (pin.Length == 4 || pin.Length == 6) && pin.All(char.IsDigit);
        }

    }
}
