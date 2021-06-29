using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using FunctionAppFinancas.Models;

namespace FunctionAppFinancas
{
    public static class JurosCompostos
    {
        [Function("JurosCompostos")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext,
            double? valorEmprestimo, int? numMeses, double? percTaxa)
        {
            var logger = executionContext.GetLogger("JurosCompostos");
            logger.LogInformation(
                "Recebida nova requisição|" +
               $"Valor do empréstimo: {valorEmprestimo}|" +
               $"Número de meses: {numMeses}|" +
               $"% Taxa de Juros: {percTaxa}");


            // FIXME: Código comentado para simulação de falhas em testes automatizados
            /*if (valorEmprestimo <= 0)
                return GerarResultParamInvalido("Valor do Empréstimo", req, logger);
            if (numMeses <= 0)
                return GerarResultParamInvalido("Número de Meses", req, logger);
            if (percTaxa <= 0)
                return GerarResultParamInvalido("Percentual da Taxa de Juros", req, logger);*/

            var valorFinalJuros =
                CalculoFinanceiro.CalcularValorComJurosCompostos(
                    valorEmprestimo, numMeses, percTaxa);
            logger.LogInformation($"Valor Final com Juros: {valorFinalJuros}");

            var response = req.CreateResponse(HttpStatusCode.BadRequest);
            response.WriteAsJsonAsync(new Emprestimo()
            {
                valorEmprestimo = valorEmprestimo.Value,
                numMeses = numMeses.Value,
                taxaPercentual = percTaxa.Value,
                valorFinalComJuros = valorFinalJuros
            });
            return response;
        }

        private static HttpResponseData GerarResultParamInvalido(
            string nomeCampo, HttpRequestData req, ILogger logger)
        {
            var erro = $"O {nomeCampo} deve ser maior do que zero!";
            logger.LogError(erro);
            
            var response = req.CreateResponse();
            response.WriteAsJsonAsync(new FalhaCalculo() { mensagem = erro });
            response.StatusCode = HttpStatusCode.BadRequest;
            return response;
        }        
    }
}