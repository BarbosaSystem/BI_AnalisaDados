using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BI_AnalisaDados
{
    public class Aritmetica
    {
        public string Nome { get; set; }
        public int Minimo { get; set; }
        public int Maximo { get; set; }
        public int Soma { get; }
        public string Media { get; }
        public decimal desvio { get; }
        public List<Aritmetica> listas { get; set; }
        

        public Aritmetica(string _nome, int[] array)
        {
            Nome = _nome;
            Minimo = array.Min();
            Maximo = array.Max();
            Soma = array.Sum();
            decimal result = Soma / (decimal)array.Length;
            Media = result.ToString("N2");
            desvio = Convert.ToDecimal(calculoSD(array).ToString("N2"));
        }

        private double calculoSD(int[] valores)
        {
            //Tira a média
            double xMedio = valores.Sum() / valores.Length;

            //Parte1: Cálculo do (x - xMedio)^2
            double[] Parte1 = new double[valores.Length]; //declara a variavel que armanezará (x - xMedio)^2

            for (int i = 0; i < valores.Length; i++)
            {
                Parte1[i] = Math.Pow(xMedio - valores[i], 2);// Calcula e armazena (x - xMedio)^2;
            }

            double stdDev = Math.Sqrt(Parte1.Sum() / Parte1.Length);//Extrai a raiz quadrada da somatória de (x - xMedio)^2 dividido por N

            return stdDev;
        }
    }

    

}
