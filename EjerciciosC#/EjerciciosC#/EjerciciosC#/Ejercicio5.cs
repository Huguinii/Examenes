using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosC
{
    public class Ejercicio5
    {
        /*
          Implementar la función que toma como argumento una secuencia de
          enteros o string y devuelve una lista de elementos sin ningún
          elemento repetido y preservando el orden original de los elementos.
        */
        public static List<int> EliminarRepetidos(List<int> secuencia)
        {
            var vistos = new HashSet<int>();
            var resultado = new List<int>();

            foreach (var numero in secuencia)
            {
                if (vistos.Add(numero)) // Add devuelve false si ya existe
                {
                    resultado.Add(numero);
                }
            }

            return resultado;
        }

    }
}
