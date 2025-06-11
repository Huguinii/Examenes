using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosC
{
    public class Ejercicio1
    {
        /*
         Haz una función que calcule y devuelva el número de vocales en la
         cadena dada. Consideraremos a, e, i, o, u como vocales. La cadena de
         entrada sólo consta de letras minúsculas y/o espacios
        */

        public static int ContarVocales(string cadena)
        {
            int contador = 0;
            string vocales = "aeiou";

            foreach (char c in cadena)
            {
                if (vocales.Contains(c))
                {
                    contador++;
                }
            }

            return contador;
        }

    }
}
