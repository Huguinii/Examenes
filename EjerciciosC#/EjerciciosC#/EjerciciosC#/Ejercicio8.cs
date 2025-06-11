using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosC
{
    public class Ejercicio8
    {
        /*
         Implementa una función de diferencia, que devuelva un array que
         tenga todos los valores de la lista pasada como primer parámetro
         que no están presentes en la lista b manteniendo su orden. Si un
         valor está presente en b, todas sus apariciones deben ser eliminadas
         de la otra:
         arrayDiff([1,2],[1]) == [2]
         arrayDiff([1,2,2,2,3],[2]) == [1,3]
         La representación binaria de 1234 es 10011010010, por lo que la función
         debería devolver 5 en este caso
        */
        public static List<int> ArrayDiff(List<int> a, List<int> b)
        {
            var conjuntoB = new HashSet<int>(b);
            return a.Where(x => !conjuntoB.Contains(x)).ToList();
        }

        public static int ContarUnosEnBinario(int numero)
        {
            return Convert.ToString(numero, 2).Count(c => c == '1');
        }


    }
}
