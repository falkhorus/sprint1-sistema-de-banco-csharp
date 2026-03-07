using System;

namespace SistemaBancario
{
    public class ContaEmpresarial : ContaBancaria
    {
        // Propriedade exclusiva da Conta Empresarial
        public decimal LimiteEmprestimo { get; private set; }

        public ContaEmpresarial(string numeroConta, string titular, decimal saldoInicial, decimal limiteEmprestimo)
            : base(numeroConta, titular, saldoInicial)
        {
            LimiteEmprestimo = limiteEmprestimo;
        }

        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do saque deve ser maior que zero.");
            }

            if (Saldo >= valor)
            {
                Saldo -= valor;
                Console.WriteLine($"Saque de R$ {valor} efetuado na Conta Empresarial. Saldo atual: R$ {Saldo}");
            }
            else
            {
                Console.WriteLine($"[Erro] Saldo insuficiente para saque. Saldo atual: R$ {Saldo}");
            }
        }

        // METODO EXCLUSIVO: Apenas empresas podem pedir esse tipo de emprestimo extra.
        public void RealizarEmprestimo(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do emprestimo deve ser maior que zero.");
            }

            if (valor <= LimiteEmprestimo)
            {
                Saldo += valor; // O dinheiro do emprestimo entra no saldo
                LimiteEmprestimo -= valor; // O limite disponivel diminui
                Console.WriteLine($"Emprestimo de R$ {valor} realizado! Novo saldo: R$ {Saldo}. Limite restante: R$ {LimiteEmprestimo}");
            }
            else
            {
                Console.WriteLine($"[Erro] Valor solicitado (R$ {valor}) ultrapassa o limite disponivel (R$ {LimiteEmprestimo}).");
            }
        }



        public void ExibirExtrato()
        {
            Thread.Sleep(5000);
            Console.WriteLine("--- EXTRATO CONTA EMPRESARIAL ---");
            Console.WriteLine($"Titular: {Titular}");
            Console.WriteLine($"Numero da Conta: {NumeroConta}");
            Console.WriteLine($"Saldo atual: R$ {Saldo}");
            Console.WriteLine($"Limite de emprestimo disponivel: R$ {LimiteEmprestimo}");
            Console.WriteLine("------------------------------");
            Thread.Sleep(5000);
        }






    }
}