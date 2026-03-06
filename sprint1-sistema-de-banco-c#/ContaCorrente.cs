using System;

namespace SistemaBancario
{
    // 1. HERANÇA: O símbolo ":" significa que ContaCorrente "é uma" ContaBancaria.
    // Ela herda o Titular, NumeroConta, Saldo e o método Depositar.
    public class ContaCorrente : ContaBancaria
    {
        // Propriedade exclusiva da Conta Corrente
        public decimal TaxaSaque { get; private set; }

        // 2. CONSTRUTOR DA CLASSE FILHA: Recebe os dados do usuário.
        // O termo "base(...)" pega os 3 primeiros dados e envia direto para o construtor da classe Pai resolver.
        public ContaCorrente(string numeroConta, string titular, decimal saldoInicial, decimal taxaSaque)
            : base(numeroConta, titular, saldoInicial)
        {
            TaxaSaque = taxaSaque;
        }

        // 3. POLIMORFISMO: O comando 'override' (sobrescrever) indica que ele tá
        // preenchendo aquele método 'Sacar' que estava em branco na classe Pai.
        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do saque deve ser maior que zero.");
            }

            // A regra da Conta Corrente: desconta o valor do saque + a taxa
            decimal valorTotal = valor + TaxaSaque;

            if (Saldo >= valorTotal)
            {
                Saldo -= valorTotal; // O mesmo que: Saldo = Saldo - valorTotal
                Console.WriteLine($"Saque de R$ {valor} efetuado na Conta Corrente. Taxa cobrada: R$ {TaxaSaque}. Saldo atual: R$ {Saldo}");
            }
            else
            {
                Console.WriteLine($"[Erro] Saldo insuficiente. Tentativa de sacar R$ {valor} + R$ {TaxaSaque} (Taxa). Saldo atual: R$ {Saldo}");
            }
        }
    }
}