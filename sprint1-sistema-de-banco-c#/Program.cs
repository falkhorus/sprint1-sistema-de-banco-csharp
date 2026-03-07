using System;
using System.Collections.Generic;
using System.Timers;

namespace SistemaBancario
{
    class Program
    {
        // A lista agora fica aqui, fora do Main
        static List<ContaBancaria> listaDeContas = new List<ContaBancaria>();

        static void Main(string[] args)
        {

            System.Timers.Timer relogioRendimento = new System.Timers.Timer(10000);
            relogioRendimento.Elapsed += AplicarRendimentoAutomatico;
            relogioRendimento.AutoReset = true;
            relogioRendimento.Enabled = true;

            bool sistemaRodando = true;

            while (sistemaRodando)
            {
                Thread.Sleep(3000);
                Console.Clear(); // Limpa a tela toda vez que o menu recarrega
                Console.WriteLine(); // Deixa a primeira linha livre para a notificação do relógio (O temporizador de Juros)
                Console.WriteLine("------ FALCON BANK ------");
                Console.WriteLine("Aqui você pode muito mais!\n");
                Console.WriteLine("1. Criar uma nova Conta Corrente");
                Console.WriteLine("2. Criar uma nova Conta Poupança");
                Console.WriteLine("3. Criar uma nova Conta Empresarial");
                Console.WriteLine("4. Fazer um Depósito");
                Console.WriteLine("5. Fazer um Saque e ver Extrato");
                Console.WriteLine("6. Pedir Empréstimo (Conta Empresarial)");
                Console.WriteLine("7. Sair");
                Console.Write("Escolha uma opção: ");

                string opcaoEscolhida = Console.ReadLine()!;

                try
                {
                    switch (opcaoEscolhida)
                    {
                        case "1":
                            Console.Write("Digite o número da conta corrente: ");
                            string numeroCorrente = Console.ReadLine()!;
                            Console.Write("Digite o nome do titular: ");
                            string titularCorrente = Console.ReadLine()!;

                            // Cria Conta Corrente com 5 reais de taxa
                            ContaCorrente novaCorrente = new ContaCorrente(numeroCorrente, titularCorrente, 0, 5.00m);
                            listaDeContas.Add(novaCorrente);
                            Console.WriteLine("Conta Corrente criada com sucesso!");
                            Thread.Sleep(2000); // Pausa por 2 segundos para o usuário ler a mensagem antes de limpar a tela
                            break;

                        case "2":
                            Console.Write("Digite o número da conta poupança: ");
                            string numeroPoupanca = Console.ReadLine()!;
                            Console.Write("Digite o nome do titular: ");
                            string titularPoupanca = Console.ReadLine()!;

                            // Cria Conta Poupança com taxa de rendimento de 0.05m (5 por cento)
                            ContaPoupanca novaPoupanca = new ContaPoupanca(numeroPoupanca, titularPoupanca, 0, 0.05m);
                            listaDeContas.Add(novaPoupanca);
                            Console.WriteLine("Conta Poupança criada com sucesso!");
                            Thread.Sleep(2000);
                            break;

                        case "3":
                            Console.Write("Digite o número da conta empresarial: ");
                            string numeroEmpresarial = Console.ReadLine()!;
                            Console.Write("Digite o nome do titular: ");
                            string titularEmpresarial = Console.ReadLine()!;

                            // Cria Conta Empresarial com limite de 1000 reais
                            ContaEmpresarial novaEmpresarial = new ContaEmpresarial(numeroEmpresarial, titularEmpresarial, 0, 1000.00m);
                            listaDeContas.Add(novaEmpresarial);
                            Console.WriteLine("Conta Empresarial criada com sucesso!");
                            Thread.Sleep(2000);
                            break;

                        case "4": // a função desse case é apenas para depositar, sem gerar extrato
                            if (listaDeContas.Count == 0)
                            {
                                Console.WriteLine("Nenhuma conta foi criada ainda. Crie uma conta primeiro.");
                                Thread.Sleep(2000);
                                break;
                            }

                            Console.Write("Digite o número da conta para depósito: ");
                            string numeroDep = Console.ReadLine()!;

                            // Busca a conta na lista
                            ContaBancaria contaDeposito = listaDeContas.Find(c => c.NumeroConta == numeroDep)!;

                            // Verifica se a conta realmente existe (se não encontrou, o resultado é null)
                            if (contaDeposito == null)
                            {
                                Console.WriteLine("[ERRO] Conta não encontrada com esse número.");
                                Thread.Sleep(2000);
                                break; // Interrompe a operação e volta pro menu
                            }

                            Console.Write($"Quanto deseja depositar para {contaDeposito.Titular}? R$ ");
                            decimal valorDeposito = Convert.ToDecimal(Console.ReadLine());

                            contaDeposito.Depositar(valorDeposito);
                            break;

                        case "5":                          // a funçaõ desse case é fazer o saque e depois mostrar o extrato, se a conta for do tipo que implementa a interface IImprimivel
                            if (listaDeContas.Count == 0)
                            {
                                Console.WriteLine("Nenhuma conta foi criada ainda. Crie uma conta primeiro.");
                                Thread.Sleep(2000);
                                break;
                            }

                            Console.Write("Digite o número da conta para saque: ");
                            string numeroSaq = Console.ReadLine()!;

                            // Busca a conta na lista
                            ContaBancaria contaSaque = listaDeContas.Find(c => c.NumeroConta == numeroSaq)!;

                            if (contaSaque == null)
                            {
                                Console.WriteLine("[ERRO] Conta não encontrada com esse número.");
                                Thread.Sleep(2000);
                                break;
                            }

                            Console.Write($"Seu saldo é R$ {contaSaque.Saldo}. Quanto deseja sacar? R$ ");
                            decimal valorSaque = Convert.ToDecimal(Console.ReadLine());

                            contaSaque.Sacar(valorSaque);

                            if (contaSaque is IImprimivel contaQueImprime)
                            {
                                Console.WriteLine("\nGerando comprovante...");
                                contaQueImprime.ExibirExtrato();
                            }
                            Thread.Sleep(2000);
                            break;

                        case "6":
                            if (listaDeContas.Count == 0)
                            {
                                Console.WriteLine("Nenhuma conta foi criada ainda. Crie uma conta primeiro.");
                                break;
                            }

                            Console.Write("Digite o número da conta para o empréstimo: ");
                            string numeroEmp = Console.ReadLine()!;

                            ContaBancaria contaBusca = listaDeContas.Find(c => c.NumeroConta == numeroEmp)!;

                            if (contaBusca == null)
                            {
                                Console.WriteLine("[ERRO] Conta não encontrada com esse número.");
                                break;
                            }

                            // Aqui está o segredo: verificamos se a conta encontrada é realmente do TIPO ContaEmpresarial
                            // Se for, ele cria uma variável temporária chamada contaEmpresa para podermos usar o Limite
                            if (contaBusca is ContaEmpresarial contaEmpresa)
                            {
                                Console.Write($"Seu limite disponível é R$ {contaEmpresa.LimiteEmprestimo:F2}. Quanto deseja pedir? R$ ");
                                decimal valorEmprestimo = Convert.ToDecimal(Console.ReadLine());

                                contaEmpresa.RealizarEmprestimo(valorEmprestimo);
                            }
                            else
                            {
                                // Se a pessoa digitar o número de uma conta corrente ou poupança, o sistema barra
                                Console.WriteLine("[ERRO] Operação negada. Apenas contas empresariais podem solicitar empréstimo.");
                            }
                            break;

                        case "7":
                            sistemaRodando = false;
                            relogioRendimento.Stop();
                            Console.WriteLine("Encerrando o sistema. Até logo!");
                            Thread.Sleep(2000);
                            break;

                        default:
                            Console.WriteLine("Opção inválida. Digite um número de 1 a 7.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n[ERRO DE FORMATAÇÃO] Por favor, digite apenas números válidos.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\n[ERRO DE OPERAÇÃO] {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO INESPERADO] Aconteceu um problema: {ex.Message}");
                }
      
            }
        }


        static void AplicarRendimentoAutomatico(object? source, ElapsedEventArgs e)
        {
            foreach (ContaBancaria conta in listaDeContas)
            {
                if (conta is ContaPoupanca poupanca)
                {
                    if (poupanca.Saldo > 0)
                    {
                        // 1. Faz a matemática do rendimento acontecer em silêncio
                        poupanca.RenderJuros();

                        // 2. Salva a posição exata onde a barrinha de digitação está agora
                        int posicaoEsquerda = Console.CursorLeft;
                        int posicaoTopo = Console.CursorTop;

                        // 3. Move o cursor invisivelmente para a primeira linha (0) e primeira coluna (0)
                        Console.SetCursorPosition(0, 0);

                        // 4. Cria a mensagem e usa PadRight para preencher o resto da linha com espaços, apagando o texto velho
                        string mensagem = $"[NOTIFICAÇÃO] A poupança de {poupanca.Titular} rendeu! Novo Saldo: R$ {poupanca.Saldo:F2}";
                        Console.Write(mensagem.PadRight(Console.WindowWidth - 1));

                        // 5. Devolve o cursor para o lugar original onde você estava digitando
                        Console.SetCursorPosition(posicaoEsquerda, posicaoTopo);
                    }
                }
            }
        }


    }
}