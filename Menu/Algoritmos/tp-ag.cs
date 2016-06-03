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
    public class tp_ag
    {
        private StreamReader leitor;

        public tp_ag(StreamReader leitor) { this.leitor = leitor; }

        public struct Prato
        {
            public double custo;
            public double lucro;
            public double beneficio;
        }

        public void ConstruirPrato(ref Prato novo, double custo, double lucro)
        {
            novo.custo = custo;
            novo.lucro = lucro;
            novo.beneficio = lucro / custo;
        }

        public void ImprimirCardapio(int quantidade, List<int> cardapio, double resposta)
        {
            if (resposta != 0.0)
            {
                for (int i = 0; i < quantidade; i++)
                    Console.Write("{0:D} ", cardapio[i]);
                Console.WriteLine();
            }
            else Console.WriteLine();
        }

        public double EncontrarProximoPrato(int qtdPratos, ref int ultimoPrato, int ultimoVezes,
            List<Prato> opcoes, List<int> cardapio, int orcamento)
        {
            int posicaoVencedora = 0;
            double custoBeneficioVencedor = 0;
            double lucroVencedor = 0;
            double auxBeneficio = 0;
            double auxLucro = 0;

            /* Encontra o proximo prato seguindo a estrategia
               gulosa de pegar o melhor custo beneficio. */
            for (int i = 0; i < qtdPratos; i++)
            {
                if (i == ultimoPrato) // Ta repetindo o ultimo prato?? se sim
                {
                    if (ultimoVezes > 1) // Se repetiu mais de um, vai ser 0.0.
                    {
                        auxBeneficio = 0;
                        auxLucro = 0;
                    }
                    else // Caso contrário, vai ter lucro pela metade (repetiu apenas uma vez).
                    {
                        auxLucro = opcoes[i].lucro * 0.5;
                        auxBeneficio = auxLucro / opcoes[i].custo;
                    }
                }
                else // Caso não repetiu, pega beneficio normal.
                {
                    auxBeneficio = opcoes[i].beneficio;
                    auxLucro = opcoes[i].lucro;
                }

                if (auxBeneficio > custoBeneficioVencedor) // Pegar o melhor custo beneficio.
                {
                    posicaoVencedora = i;
                    custoBeneficioVencedor = auxBeneficio;
                    lucroVencedor = auxLucro;
                }
            }

            // Vê se consegue pagar o prato. (Estora o orçamento?)
            if (opcoes[posicaoVencedora].custo <= orcamento)
                orcamento -= (int)opcoes[posicaoVencedora].custo;
            else
                return -1; // Se estourar, retorna -1.

            // Atualiza as variaveis de controle das opcoes anteriores.
            if (posicaoVencedora == ultimoPrato) // Se o prato repetiu, incrementa 1 nas vezes.
                ultimoVezes++;
            else
            {
                ultimoPrato = posicaoVencedora; // Se o prato não tiver sido repetiro, eu zero o ultimo vezes.
                ultimoVezes = 0;
            }

            // Adiciona o prato ao vetor.
            cardapio.Add(posicaoVencedora + 1);

            return lucroVencedor;
        }

        public int EncontrarMaisBarato(int qtdPratos, List<Prato> opcoes)
        {
            int resposta = 0;
            int custoAtual = (int)opcoes[0].custo;

            for (int i = 0; i < qtdPratos; i++)
            {
                if (custoAtual > opcoes[i].custo)
                {
                    resposta = i;
                    custoAtual = (int)opcoes[i].custo;
                }
            }

            return resposta;
        }

        public double CardapioGuloso(int numDias, int qtdPratos, int orcamento, List<Prato> opcoes, List<int> cardapio)
        {
            double aux = 0;
            double resposta = 0.0;
            int ultimoPrato = -1;
            int ultimoVezes = 0;

            int maisBarato = EncontrarMaisBarato(qtdPratos, opcoes);

            // Verifica se é possivel montar um cardapio.
            if ((numDias * opcoes[maisBarato].custo) > orcamento)
                return resposta;

            for (int i = 0; i < numDias; i++)
            {
                aux = EncontrarProximoPrato(qtdPratos, ref ultimoPrato, ultimoVezes, opcoes, cardapio, orcamento);
                resposta += aux; // Agrego ao lucro maximo obtido

                if (aux == -1) // Estorou o orçamento anteriormente no EncontrarProximoPrato? retorna zero então.
                    return 0.0;
            }

            return resposta;
        }

        public void LerPratos(int quantidade, List<Prato> opcoes)
        {
            int custo, lucro;
            Prato auxiliar = new Prato();

            for (int i = 0; i < quantidade; i++)
            {
                // Lê o custo e o lucro.
                string linha = this.leitor.ReadLine();
                string[] partes = linha.Split(' ');
                custo = int.Parse(partes[0]);
                lucro = int.Parse(partes[1]);

                // Constroi o prato e o adiciona nas opcoes.
                ConstruirPrato(ref auxiliar, custo, lucro);
                opcoes.Add(auxiliar);
            }
        }

        public int Resolver()
        {
            var initialMemory = System.GC.GetTotalMemory(true);
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int numDias = 0;
            int qtdPratos = 0;
            int orcamento = 0;
            double resposta = 0;

            while (true)
            {
                // Lê do arquivo o numero de dias, quantidade de pratos e o orcamento.
                string linha = this.leitor.ReadLine();
                string[] partes = linha.Split(' ');
                numDias = int.Parse(partes[0]);
                qtdPratos = int.Parse(partes[1]);
                orcamento = int.Parse(partes[2]);

                // Se não há mais entradas, encerra o loop.
                if (numDias == 0 && qtdPratos == 0 && orcamento == 0)
                    break;

                var opcoes = new List<Prato>();
                var cardapio = new List<int>();

                // Lê do arquivo os pratos.
                LerPratos(qtdPratos, opcoes);

                // Encontra e imprime o lucro total pela estrategia gulosa.
                resposta = CardapioGuloso(numDias, qtdPratos, orcamento, opcoes, cardapio);
                Console.WriteLine("{0,2:f1}", resposta);
                //Console.WriteLine("{0:0.0}", Math.Round(resposta, 2));

                // Imprime o cardapio.
                ImprimirCardapio(numDias, cardapio, resposta);
            } // Final leitura.

            // Fecha arquivo.
            this.leitor.Close();

            var finalMemory = System.GC.GetTotalMemory(true);
            Console.WriteLine("Initial memory usage: " + initialMemory);
            Console.WriteLine("Final memory usage: " + finalMemory);
            Console.WriteLine("Memory usage: " + (finalMemory - initialMemory) + "kb");

            sw.Stop();
            Console.WriteLine("{0}", sw.Elapsed);

            return 0;
        }
        
        static void Main(string[] args)
        {
            tp_ag program = new tp_ag(new StreamReader("./Entradas/entrada1.txt"));
            program.Resolver();
        }
    }
}