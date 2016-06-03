//
// PUC Minas
// Sistemas de Informação
//
// Trabalho Integrado
// Técnicas Avançadas de Programação
//
// Igor Octaviano Ribeiro Rezende
// Pedro Augusto Duarte de Almeida
// Gabriel Silas do Carmo
// Danilo Oliveira de Andrade
// Victor Hugo Medeiros
// 
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace Menu.Algoritmos
{
    public class tp_pd
    {
        public int[] itens;
        public List<double> melhores = new List<double>();
        private StreamReader leitor;

        /// <summary>
        /// Recebe-se o stream por construtor devido a refatoração
        /// feita a comportar testes unitários.
        /// </summary>
        public tp_pd(StreamReader leitor) { this.leitor = leitor; }

        // Variáveis para teste.
        public int k;
        public int n;
        public int m;

        // Variáveis internas.
        public int numeroDeDias;
        public int numeroDePratos;
        public int orcamento;

        public int[] custos;
        public int[] lucros;

        private bool EoFim()
        {
            return (numeroDeDias == 0 && numeroDePratos == 0 && orcamento == 0);
        }

        public void Resolver()
        {
            var initialMemory = System.GC.GetTotalMemory(true);
            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (true)
            {           
                // Carregar valores iniciais do arquivo
                string[] partes = leitor.ReadLine().Split(' ');
                numeroDeDias = int.Parse(partes[0]);
                numeroDePratos = int.Parse(partes[1]);
                orcamento = int.Parse(partes[2]);

                if (EoFim())
                {
                    k = numeroDeDias;
                    n = numeroDePratos;
                    m = orcamento;
                }

                if (EoFim())
                    break;

                custos = new int[numeroDePratos];
                lucros = new int[numeroDePratos];
                for (int i = 0; i < numeroDePratos; i++)
                {
                    partes = leitor.ReadLine().Split(' ');
                    custos[i] = int.Parse(partes[0]);
                    lucros[i] = int.Parse(partes[1]);
                }

                // Tabela
                // tabela[diasRestantes, orcamentoRestante, ultimoPrato, oDobroOuMais]
                double[,,,] tabela = new double[numeroDeDias, orcamento + 1, numeroDePratos, 2];
                /* tabela[x, y, z, c] = soma sobre todos os itens de 
                   tabela[x-1, y-cost, i, 0 ou 1 dependendo] */

                for (int i = 0; i <= orcamento; i++)
                    for (int j = 0; j < numeroDePratos; j++)
                        for (int k = 0; k < 2; k++)
                            tabela[0, i, j, k] = 0;

                for (int i = 1; i < numeroDeDias; i++)
                    for (int j = 0; j < numeroDePratos; j++)
                        for (int k = 0; k < 2; k++)
                            tabela[i, 0, j, k] = -1;

                for (int i = 1; i < numeroDeDias; i++)
                    for (int j = 1; j <= orcamento; j++)
                        for (int k = 0; k < numeroDePratos; k++)
                            for (int l = 0; l < 2; l++)
                            {
                                tabela[i, j, k, l] = -1;
                                for (int item = 0; item < numeroDePratos; item++)
                                {
                                    if (custos[item] > j) // Item fora do orçamento? passa pra proxima iteração.
                                        continue;

                                    // Se caso estiver no orçameto:                              [Se k é o mesmo prato anterior, seleciona 1 (repetiu)
                                    //[Decrementa dia][Decrementa do orçamento o custo do prato]  Ou 0 caso não sejam iguais.]
                                    double prox = tabela[i - 1, j - custos[item], item, k == item ? 1 : 0]; 
                                                                                               
                                    if (prox < -0.5)
                                        continue;

                                    double lucroMaxPratoAtual = 0;

                                    if (k == item) // Se k é o mesmo prato anterior
                                    {
                                        if (l == 1) // Se k é igual ao prato anterior e ainda sim repetiu denovo, recebe 0.
                                            lucroMaxPratoAtual = 0;
                                        else if (l == 0) // Se k é igual ao prato anterior mais repetiu uma vez só, então é metade.
                                            lucroMaxPratoAtual = (0.5 * lucros[item]);
                                    }
                                    else // k é diferente (prato novo)
                                        lucroMaxPratoAtual = lucros[item]; // Recebe lucro normalmente.

                                    if (prox + lucroMaxPratoAtual > tabela[i, j, k, l]) // Se o lucro prox mais o lucro do prato atual é maior que o valor da tabela
                                        tabela[i, j, k, l] = prox + lucroMaxPratoAtual; // então tabela recebe o novo lucro maximo. 
                                }
                            }

                this.itens = new int[numeroDeDias];

                double[] financas = new double[orcamento + 1];
                financas[0] = 0;

               for (int i = 1; i <= orcamento; i++)
                {
                    for (int item = 0; item < numeroDePratos; item++)
                    {
                        if (custos[item] > i)
                            continue;

                        if (tabela[numeroDeDias - 1, i - custos[item], item, 0] < 0)
                            continue;

                        double possivel = lucros[item] + tabela[numeroDeDias - 1, i - custos[item], item, 0];

                        if (possivel > financas[i])
                            financas[i] = possivel;
                    }
                }

                double melhor = financas[orcamento];

                //                      Math.Abs para pegar o valor absoluto.                 1e-6 = 0.000001 em decimal.
                while (orcamento > 0 && Math.Abs(financas[orcamento - 1] - financas[orcamento]) < 1e-6) 
                    orcamento--;

                Console.WriteLine("{0:0.0}", melhor);
                this.melhores.Add(melhor);

                if (melhor <= 0)
                    continue;

                for (int item = 0; item < numeroDePratos; item++)
                {
                    if (custos[item] > orcamento)
                        continue;

                    if (tabela[numeroDeDias - 1, orcamento - custos[item], item, 0] < 0)
                        continue;

                    double possivel = lucros[item] + tabela[numeroDeDias - 1, orcamento - custos[item], item, 0];

                    //                            1e-6 = 0.000001 em decimal.
                    if (Math.Abs(possivel - melhor) < 1e-6) 
                    {
                        itens[0] = item;
                        break;
                    }
                }

                double atual = custos[itens[0]];
                int atual_i = numeroDeDias - 1, atual_j = orcamento - custos[itens[0]], atual_k = itens[0], atual_l = 0;

                while (atual_i > 0)
                {
                    for (int item = 0; item < numeroDePratos; item++)
                    {
                        if (custos[item] > atual_j)
                            continue;

                        double prox = tabela[atual_i - 1, atual_j - custos[item], item, atual_k == item ? 1 : 0];

                        if (prox < -0.5)
                            continue;

                        double lucroMaxPratoAtual = 0;
                        if (atual_k == item)
                        {
                            if (atual_l == 1) // Se k é igual ao prato anterior e ainda sim repetiu denovo, recebe 0.
                                lucroMaxPratoAtual = 0;
                            else if (atual_l == 0) // Se k é igual ao prato anterior mais repetiu uma vez só, então é metade.
                                lucroMaxPratoAtual = 0.5 * lucros[item];
                        }
                        else
                            lucroMaxPratoAtual = lucros[item];

                        //  Math.Abs para pegar o valor absoluto.                 1e-6 = 0.000001 em decimal.
                        if (Math.Abs(prox + lucroMaxPratoAtual - tabela[atual_i, atual_j, atual_k, atual_l]) < 1e-6)
                        {
                            atual += lucroMaxPratoAtual;
                            itens[numeroDeDias - atual_i] = item;
                            atual_i--;
                            atual_j -= custos[item];
                            atual_l = (atual_k == item ? 1 : 0);
                            atual_k = item;
                            break;
                        }
                    }
                }

                for (int i = 0; i < itens.Length; i++)
                    itens[i] += 1;

                foreach (int i in itens)
                    Console.Write(i + " ");
                Console.WriteLine();
            }

            // Fecha arquivo.
            this.leitor.Close();

            var finalMemory = System.GC.GetTotalMemory(true);
            Console.WriteLine("Initial memory usage: " + initialMemory);
            Console.WriteLine("Final memory usage: " + finalMemory);
            Console.WriteLine("Memory usage: " + (finalMemory - initialMemory) + "kb");

            sw.Stop();
            Console.WriteLine("{0}", sw.Elapsed);
        }
        /*
        static void Main(string[] args)
        {
            tp_pd menu = new tp_pd(new StreamReader("./Entradas/entrada1.txt"));
            menu.Resolver();
        }*/
    }
}
