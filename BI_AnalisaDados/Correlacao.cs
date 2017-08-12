using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BI_AnalisaDados
{
    public static class Correlacao
    {
        //Soma a lista

        public static double CorrerPearson(int[] array1, int[] array2)
        {
            double numerador = /* passou */ Numerador(array1, array2);
            double denominador = Denominador(array1, array2);
            return numerador / denominador;
        }

        //Monta o valor final da parte de cima
        public static double Numerador(int[] array1, int[] array2)
        {
            int tamanho = array1.Length;
            double res =  (/* passou */ SomatorioXY(array1, array2) - /* passou */ (MultiplicacaoXY(array1, array2)) / tamanho) ;
            return res;
        }

        //Monta o valor final da parte de baixo
        private static double Denominador(int[] array1, int[] array2)
        {
            int tamanho = array1.Length;
            double resu1 = Math.Sqrt((PotenciaXouY(array1) - (SomatorioPotenciaXouY(array1) / tamanho)));

            double resu2 = Math.Sqrt((PotenciaXouY(array2) - (SomatorioPotenciaXouY(array2) / tamanho)));

            return resu1 * resu2;
        }
        private static double MultiplicacaoXY(int[] array1, int[] array2)
        {
            double res = array1.Sum() * array2.Sum();
            return res;
        }
        //Soma as duas colunas
        private static double SomatorioXY(int[] array1, int[] array2)
        {
            int tamanho = array1.Length;
            double resultado = 0;
            for(int i = 0; i< tamanho; i++)
            {
                resultado += (array1[i] * array2[i]); 
            }

            return resultado;
        }

        //Elevar o Somatório de Xi ou Yi ao quadrado

        private static double PotenciaXouY(int [] array)
        {
            double resultado = 0;
            foreach(int i in array)
            {
                resultado += Math.Pow(i, 2);
            }
            return resultado;
            //return Math.Pow(array.Sum(), 2);
        }

        private static double SomatorioPotenciaXouY(int[] array)
        {
            return Math.Pow(array.Sum(), 2);
        }
    }
}
