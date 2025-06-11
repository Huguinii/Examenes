using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosC
{
    public class Ejercicio3
    {
        /*
         Haz una función que como parámetro reciba un array de números y
         obtenga el número que menos repeticiones haya tenido. En caso de
         empate devuelve el número más pequeño.
        */
        public static int NumeroMenosRepetido(int[] numeros)
        {
            var frecuencia = new Dictionary<int, int>();

            // Contar ocurrencias de cada número
            foreach (int num in numeros)
            {
                if (frecuencia.ContainsKey(num))
                    frecuencia[num]++;
                else
                    frecuencia[num] = 1;
            }

            // Buscar la mínima frecuencia
            int minFrecuencia = frecuencia.Values.Min();

            // Filtrar los números con esa frecuencia y devolver el menor
            return frecuencia
                .Where(pair => pair.Value == minFrecuencia)
                .Select(pair => pair.Key)
                .Min();
        }

    }
}
