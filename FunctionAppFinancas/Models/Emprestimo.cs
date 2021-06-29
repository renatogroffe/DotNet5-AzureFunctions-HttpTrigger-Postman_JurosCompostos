namespace FunctionAppFinancas.Models
{
    public class Emprestimo
    {
        public double valorEmprestimo { get; set; }
        public int numMeses { get; set; }
        public double taxaPercentual { get; set; }
        public double valorFinalComJuros { get; set; }
    }
}