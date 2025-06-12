using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosC
{
    public class Ejercicio11
    {
        /*
         El teorema de los cuatro cuadrados de Lagrange, también conocido
         como conjetura de Bachet, afirma que todo número natural puede
         representarse como la suma de cuatro cuadrados enteros.
         Haz una función que devuelva un array con los cuatro números naturales
         que cumplan el teorema dado un número natural pasado como argumento.
        */
        public static int[] CuatroCuadrados(int n)
        {
            for (int a = 0; a * a <= n; a++)
            {
                for (int b = 0; a * a + b * b <= n; b++)
                {
                    for (int c = 0; a * a + b * b + c * c <= n; c++)
                    {
                        int dCuadrado = n - (a * a + b * b + c * c);
                        int d = (int)Math.Sqrt(dCuadrado);
                        if (d * d == dCuadrado)
                            return new int[] { a, b, c, d };
                    }
                }
            }
            return null; // no debería pasar nunca según el teorema
        }

    }
}
