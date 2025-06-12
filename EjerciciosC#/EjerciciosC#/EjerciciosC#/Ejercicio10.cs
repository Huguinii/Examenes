using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosC
{
    public class Ejercicio10
    {
        /*
         Escriba una función que tome un número decimal como entrada, y
         devuelva el número de bits que son iguales a uno en la
         representación binaria de ese número. Comprueba que la entrada no
         sea negativa.
        */
        public static int ContarBitsEnUno(int numero)
        {
            if (numero < 0)
                throw new ArgumentException("El número no puede ser negativo.");

            return Convert.ToString(numero, 2).Count(c => c == '1');
        }

    }
}
