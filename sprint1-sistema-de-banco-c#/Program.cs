using System;
using System.Collections.Generic;

namespace SistemaBancario
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ContaBancaria> listaDeContas = new List<ContaBancaria>();
            bool sistemaRodando = true;

            while (sistemaRodando)
            {
                Console.WriteLine("\n--- MENU DO SISTEMA BANCÁRIO ---");
                Console.WriteLine("1. Criar uma nova Conta Corrente");
                Console.WriteLine("2. Criar uma nova Conta Poupança");
                Console.WriteLine("3. Criar uma nova Conta Empresarial");
                Console.WriteLine("4. Fazer um Depósito");
                Console.WriteLine("5. Fazer um Saque e ver Extrato");
                Console.WriteLine("6. Sair");
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
                            break;

                        case "4": // a função desse case é apenas para depositar, sem gerar extrato
                            if (listaDeContas.Count == 0)
                            {
                                Console.WriteLine("Nenhuma conta foi criada ainda. Crie uma conta primeiro.");
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
                                break;
                            }

                            Console.Write("Digite o número da conta para saque: ");
                            string numeroSaq = Console.ReadLine()!;

                            // Busca a conta na lista
                            ContaBancaria contaSaque = listaDeContas.Find(c => c.NumeroConta == numeroSaq)!;

                            if (contaSaque == null)
                            {
                                Console.WriteLine("[ERRO] Conta não encontrada com esse número.");
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
                            break;

                        case "6": // Antigo case 4 (Sair)
                            sistemaRodando = false;
                            Console.WriteLine("Encerrando o sistema. Até logo!");
                            break;

                        default:
                            Console.WriteLine("Opção inválida. Digite um número de 1 a 6.");
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
    }
}