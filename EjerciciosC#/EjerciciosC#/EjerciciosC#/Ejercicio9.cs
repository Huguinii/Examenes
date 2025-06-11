using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosC
{
    public class Ejercicio9
    {
        /*
         Haz una función que pueda tomar cualquier número entero no
         negativo como argumento y devolverlo con sus dígitos en orden
         descendente. Esencialmente, reordenar los dígitos para crear el
         mayor número posible.
         Entrada: 42145 Salida: 54421
         Entrada: 145263 Salida: 654321
         Entrada: 123456789 Salida: 987654321
        */
        public static int OrdenarDigitosDesc(int numero)
        {
            return int.Parse(
                new string(
                    numero.ToString()
                          .OrderByDescending(c => c)
                          .ToArray()
                )
            );
        }

    }
}
