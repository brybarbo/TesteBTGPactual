using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

/*
 BTG Pactual (IT-PME)
 Autor: Bryan Barbosa
 Data: 14/09/2020
 Comentários: Teste de proficiência em raciocínio lógico e conhecimentos de programação
*/

namespace TesteBTG
{

    class Program
    {
        /// <summary>
        /// INPUT dos Dados
        /// ComprimentoMaximoDoArray =  Soma do comprimeto dos tijolos por linha
        /// NumeroDeTijolosPorLinha = Quantidade permitido por linha
        /// NumeroDeTijolosPorAltura = Quantidade permitido por coluna
        /// QuantidadeMaximoDeTijolos = Quatidade maxima de tijolos permitido no muro
        /// QuantidadeDeTijolos = Quatidade de tijolos colocado no muro
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("********Teste BTG - Bryan Barbosa******");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            // DADOS INPUT
            var comprimentoMaximoDoArray = ObterSomaDoComprimento();
            var numeroDeTijolosPorLinha = ObterComprimentoAltura();
            var numeroDeTijolosPorAltura = ObterComprimentoAltura();
            var quantidadeMaximoDeTijolos = 20000;
            int quantidadeDeTijolos = default;

            Console.WriteLine($"ComprimentoMaximoDoArray: {comprimentoMaximoDoArray}");
            Console.WriteLine($"NumeroDeTijolosPorAltura: {numeroDeTijolosPorLinha}");
            Console.WriteLine($"QuantidadeDeTijolos: {numeroDeTijolosPorAltura}");
            Console.WriteLine($"QuantidadeMaximoDeTijolos: {quantidadeMaximoDeTijolos}");
            Console.WriteLine();
            //Exemplo do exercício, para testar basta comentar a linha 36
            //var matrizTijolos = new int[][] {
            //new int[] { 1, 2, 2, 1 },
            //new int[] { 3, 1, 2 },
            //new int[] { 1, 3, 2 },
            //new int[] { 2, 4 },
            //new int[] { 3, 1, 2 },
            //new int[] { 1, 3, 1, 1 } };

            var matrizTijolos = ObterMatriz(ref quantidadeDeTijolos, comprimentoMaximoDoArray, quantidadeMaximoDeTijolos);
            var lstCortes = new List<int>();

            // Varrer as linhas
            // O(n^2)
            for (int linha = default; linha < matrizTijolos.Length; linha++)
            {
                int comprimento = default;
                for (int coluna = default; coluna < matrizTijolos[linha].Length; coluna++)
                {
                    comprimento += matrizTijolos[linha][coluna];
                    if (comprimento >= comprimentoMaximoDoArray)
                        continue;

                    lstCortes.Add(CortarTijolos(matrizTijolos, comprimento, linha));
                }
            }

            Console.WriteLine("O(n^2) - Varrer linhas");
            Console.WriteLine();
            Console.WriteLine($"Quantidade Menor de Cortes {lstCortes.OrderBy(x => x).First()}");
            Console.WriteLine("========================================");
            Console.WriteLine($"Quantidade Maior de Cortes {lstCortes.OrderBy(x => x).Last()}");
            Console.WriteLine("========================================");
            Console.WriteLine($"Quantidade total de Tijolos {quantidadeDeTijolos}");
            Console.WriteLine("========================================");
            stopwatch.Stop();
            Console.WriteLine($"Tempo de execução: {stopwatch.ElapsedMilliseconds} ms");
            Console.ReadLine();
        }

        /// <summary>
        /// Algoritimo percorre para entrocar o caminho que corte menos tijolos
        /// </summary>
        /// <param name="matriz"></param>
        /// <param name="comprimentoRef"></param>
        /// <param name="linha"></param>
        /// <returns>Número de Cortes</returns>
        internal static int CortarTijolos(int[][] matriz, int comprimentoRef, int linha)
        {
            //"O(n^2) - Cortar Tijolos"
            int cortes = default;
            for (int l = default; l < matriz.Length; l++)
            {
                // Pula linha vigente 
                if (l.Equals(linha)) continue;

                int comprimento = default;
                for (int c = default; c < matriz[l].Length; c++)
                {
                    // CASO 01: Se comprimento do(s) tijolo(s) for maior que comprimento referência, efetua a contagem do corte. 
                    // CASO 02: Se comprimento do(s) tijolo(s) for menor que comprimento referência, ele soma com o proximo tijolo.
                    // CASO 03: Se comprimento do(s) tijolo(s) for igual ao comprimento referência, ele passa pelas arestas.
                    comprimento += matriz[l][c];
                    if (comprimento > comprimentoRef)
                    {
                        cortes++;
                        break;
                    }

                    if (comprimento.Equals(comprimentoRef)) break;
                }
            }

            return cortes;
        }

        /// <summary>
        /// Obtêm o número 1 á 10000
        /// </summary>
        /// <returns></returns>
        internal static int ObterComprimentoAltura()
        {
            return new Random().Next(1, 10000);
        }

        /// <summary>
        ///  Obtêm o número 1 á 10000
        /// </summary>
        /// <returns></returns>
        internal static int ObterSomaDoComprimento()
        {
            return new Random().Next(1, 10000);
        }

        /// <summary>
        /// Preenche um array dinâmico com os seguintes valores:
        /// QuantidadeDeTijolos: Quantidade máxima de tijolos adicionados
        /// ComprimentoMaximoDoArray: Quantidade máxima permitido por o array interno
        /// QuantidadeMaximoDeTijolos: Quantidade máxima de tijolos permitido na matriz
        /// </summary>
        /// <param name="quantidadeDeTijolos"></param>
        /// <param name="comprimentoMaximoDoArray"></param>
        /// <param name="quantidadeMaximoDeTijolos"></param>
        /// <returns></returns>
        internal static int[][] ObterMatriz(ref int quantidadeDeTijolos, int comprimentoMaximoDoArray, int quantidadeMaximoDeTijolos)
        {
            var matrizTijolo = new List<int[]>();
            // O(n^2)
            for (int i = 0; i < comprimentoMaximoDoArray; i++)
            {
                int soma = default;
                var lstComprimento = new List<int>();
                for (int j = 0; j < comprimentoMaximoDoArray; j++)
                {
                    int comprimento = ObterComprimentoAltura();
                    soma += comprimento;
                    quantidadeDeTijolos++;
                    if (soma == comprimentoMaximoDoArray)
                    {
                        lstComprimento.Add(comprimento);
                        break;
                    }

                    if (soma > comprimentoMaximoDoArray)
                    {
                        soma -= comprimento;
                        var restante = comprimentoMaximoDoArray - soma;
                        lstComprimento.Add(restante);
                        break;
                    }

                    lstComprimento.Add(comprimento);
                }

                if (quantidadeDeTijolos >= quantidadeMaximoDeTijolos)
                    break;

                matrizTijolo.Add(lstComprimento.ToArray());
            }
            Console.WriteLine("O(n^2) - ObterMatriz");
            return matrizTijolo.ToArray();
        }
    }
}
