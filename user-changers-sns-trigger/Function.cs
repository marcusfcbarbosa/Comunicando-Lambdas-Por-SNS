using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using user_changes_domain;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace user_changers_sns_trigger
{
    public class Function
    {
        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {

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
            context.Logger.LogLine($"PROCESSAMENTO DE REGISTROS {record.Sns.Subject} - {record.Sns.Message}");
            var titulo = JsonSerializer.Deserialize<EnvioTitulo>(record.Sns.Message);
            context.Logger.LogLine($"TITULO DESERIALIZADO {titulo.idLinha} - {titulo.codEspecieDoc} - {titulo.dataVencimento}");
            //Comunicar com outra SNS
            await EnviaFila2(titulo, context);

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
                context.Logger.LogLine($"ENVIANDO PARA A SEGUNDA FILA user-changes-topic-2");
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"ERRO NO ENVIO PARA SEGUNDA FILA ARN = { Acceess.GetTopiARN() }  user-changes-topic-2 { ex.Message }");
            }
        }

    }
}
