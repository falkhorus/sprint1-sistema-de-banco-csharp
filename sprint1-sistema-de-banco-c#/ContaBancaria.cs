using System;

namespace SistemaBancario
{
    // 1. CLASSE ABSTRATA: O 'abstract' significa que esta classe é só um molde base. 
    // Não dá para ciar uma "ContaBancaria" genérica, apenas as específicas (Corrente, etc).
    public abstract class ContaBancaria
    {
        // 2. ENCAPSULAMENTO E PROPRIEDADES: 
        // 'get' permite ler o valor de fora. 
        // 'private set' impede que o número e o titular sejam alterados depois de criados.
        // 'protected set' permite que o saldo seja alterado apenas por esta classe e pelas classes filhas.
        public string NumeroConta { get; private set; }
        public string Titular { get; private set; }
        public decimal Saldo { get; protected set; }

        // 3. CONSTRUTOR: É a função que roda no momento em que criamos o objeto, para configurá-lo.
        public ContaBancaria(string numeroConta, string titular, decimal saldoInicial)
        {
            NumeroConta = numeroConta;
            Titular = titular;
            Saldo = saldoInicial;
        }

        // Método comum: Todas as contas depositam da mesma forma.
        public void Depositar(decimal valor)
        {
            if (valor <= 0)
            {
                // 4. TRATAMENTO DE EXCEÇÕES: Isso daqui protege o sistema contra  valores absurdos.
                throw new ArgumentException("O valor do depósito deve ser maior que zero.");
            }

            Saldo += valor; // Adiciona o valor ao saldo
            Console.WriteLine($"Depósito de R$ {valor} realizado. Novo saldo de {Titular}: R$ {Saldo}");
        }

        // 5. POLIMORFISMO: O método 'Sacar' é 'abstract'. Ele não tem corpo (código) aqui.
        // Isso obriga as classes filhas (ContaCorrente, ContaPoupanca) a criarem suas próprias regras de saque!
        public abstract void Sacar(decimal valor);
    }
}