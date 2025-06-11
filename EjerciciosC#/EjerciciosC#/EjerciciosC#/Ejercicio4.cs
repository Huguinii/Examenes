using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosC
{
    public class Ejercicio4
    {
        /*
          Dada unarray de enteros, encuentra todo los números que aparecen
          un número impar de veces
        */
        public static List<int> NumerosConFrecuenciaImpar(int[] numeros)
        {
            var frecuencia = new Dictionary<int, int>();

            // Contar la frecuencia de cada número
            foreach (int num in numeros)
            {
                if (frecuencia.ContainsKey(num))
                    frecuencia[num]++;
                else
                    frecuencia[num] = 1;
            }

            // Filtrar los números con frecuencia impar
            return frecuencia
                .Where(pair => pair.Value % 2 != 0)
                .Select(pair => pair.Key)
                .ToList();
        }

    }
}
