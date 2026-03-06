using System;

namespace SistemaBancario
{
    // HERANCA: A ContaPoupanca herda da nossa base ContaBancaria
    public class ContaPoupanca : ContaBancaria
    {
        // Propriedade exclusiva da Poupanca
        public decimal TaxaRendimento { get; private set; }

        public ContaPoupanca(string numeroConta, string titular, decimal saldoInicial, decimal taxaRendimento)
            : base(numeroConta, titular, saldoInicial)
        {
            TaxaRendimento = taxaRendimento;
        }

        // POLIMORFISMO: A regra de saque aqui e simples, nao tem taxa.
        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do saque deve ser maior que zero.");
            }

            if (Saldo >= valor)
            {
                Saldo -= valor;
                Console.WriteLine($"Saque de R$ {valor} efetuado na Conta Poupanca. Saldo atual: R$ {Saldo}");
            }
            else
            {
                Console.WriteLine($"[Erro] Saldo insuficiente. Tentativa de sacar R$ {valor}. Saldo atual: R$ {Saldo}");
            }
        }

        // METODO EXCLUSIVO: Apenas a poupanca tem rendimento.
        public void RenderJuros()
        {
            decimal rendimento = Saldo * TaxaRendimento;
            Saldo += rendimento; // Adiciona o rendimento ao saldo
            Console.WriteLine($"Rendimento de R$ {rendimento} aplicado. Novo saldo: R$ {Saldo}");
        }
    }
}
