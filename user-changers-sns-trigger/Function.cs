using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MediatR;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using user_changes_domain;
using user_changes_domain.Commands;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace user_changers_sns_trigger
{
    public class Function
    {
        IMediator _mediator;
        public Function()
        {
            this._mediator = Startup.ConfigureServices().GetService<IMediator>();
        }
        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SNS event object and can be used 
        /// to respond to SNS messages.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(SNSEvent evnt, ILambdaContext context)
        {
            foreach (var record in evnt.Records)
            {
                await ProcessRecordAsync(record, context);
            }
        }

        private async Task ProcessRecordAsync(SNSEvent.SNSRecord record, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine($"1- PROCESSAMENTO DE REGISTROS {record.Sns.Subject} - {record.Sns.Message}");
                var titulo = JsonSerializer.Deserialize<EnvioTitulo>(record.Sns.Message);
                context.Logger.LogLine($"2- TITULO DESERIALIZADO {titulo.idLinha} - {titulo.codEspecieDoc} - {titulo.dataVencimento}");
                var retorno = await _mediator.Send(new EnviarTituloCommand
                {
                    codEspecieDoc = titulo.codEspecieDoc,
                    dataVencimento = titulo.dataVencimento,
                    idLinha = titulo.idLinha,
                    seuNumero = titulo.seuNumero
                });
                context.Logger.LogLine($"3 -RETORNO DA CHAMADA USANDO MEDIATOR { retorno }");
                //Comunicar com outra SNS
                await EnviaFila2(titulo, context);
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"5- RETORNO DA CHAMADA USANDO MEDIATOR { ex.Message }");
            }
            await Task.CompletedTask;
        }

        public async Task EnviaFila2(EnvioTitulo titulo, ILambdaContext context)
        {
            try
            {
                var snsClient = new AmazonSimpleNotificationServiceClient(Amazon.RegionEndpoint.SAEast1);
                var request = new PublishRequest
                {
                    TopicArn = Acceess.GetTopiARN(),
                    Message = JsonSerializer.Serialize(titulo),
                    Subject = $"ENVIO TITUTLOS MARCUS {DateTime.Now.ToString()}"
                };
                await snsClient.PublishAsync(request);
                context.Logger.LogLine($"4 - ENVIANDO PARA A SEGUNDA FILA user-changes-topic-2");
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"6 - ERRO NO ENVIO PARA SEGUNDA FILA ARN = { Acceess.GetTopiARN() }  user-changes-topic-2 { ex.Message }");
            }
        }
    }
}