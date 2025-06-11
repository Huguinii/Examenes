using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosC
{
    public class Ejercicio6
    {
        /*  Escribe una función que tome un parámetro positivo num y devuelva
            su persistencia multiplicativa, que es el número de veces que debes
            multiplicar los dígitos de num hasta llegar a un solo dígito.
            Por ejemplo (Entrada--> Salida):
            39--> 3 (porque 3*9 = 27, 2*7 = 14, 1*4 = 4 y el 4 sólo tiene un dígito)
            999--> 4 (porque 9*9*9 = 729, 7*2*9 = 126, 1*2*6 = 12, y finalmente 1*2 = 2)
            4--> 0 (porque el 4 ya es un número de un dígito)
        */
        public static int PersistenciaMultiplicativa(int num)
        {
            int contador = 0;

            while (num >= 10) // Mientras tenga más de un dígito
            {
                int producto = 1;
                foreach (char c in num.ToString())
                {
                    producto *= (c - '0'); // Convertir carácter a número y multiplicar
                }

                num = producto;
                contador++;
            }

            return contador;
        }

    }
}
