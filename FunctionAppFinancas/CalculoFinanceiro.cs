using System;

namespace FunctionAppFinancas
{
    public static class CalculoFinanceiro
    {
        public static double CalcularValorComJurosCompostos(
            double? valorEmprestimo, int? numMeses, double? percTaxa)
        {
            if (valorEmprestimo is null || numMeses is null || percTaxa is null ||
                valorEmprestimo <= 0 || numMeses <= 0 || percTaxa <= 0)
                throw new Exception("Parâmetros para cálculo inválidos!");

            // FIXME: Simulação de falha
            return valorEmprestimo.Value * Math.Pow(1 + (percTaxa.Value / 100), numMeses.Value); 
            //return Math.Round(
            //    valorEmprestimo.Value * Math.Pow(1 + (percTaxa.Value / 100), numMeses.Value), 2);
        }
    }
}