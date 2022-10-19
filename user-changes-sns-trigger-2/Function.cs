using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using MediatR;
using user_changes_domain;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace user_changes_sns_trigger_2
{
    public class Function
    {
       
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
            foreach(var record in evnt.Records)
            {
                await ProcessRecordAsync(record, context);
            }
        }

        private async Task ProcessRecordAsync(SNSEvent.SNSRecord record, ILambdaContext context)
        {
            context.Logger.LogLine($"PROCESSAMENTO DE REGISTROS 2 {record.Sns.Subject} - {record.Sns.Message}");
            var titulo = JsonSerializer.Deserialize<EnvioTitulo>(record.Sns.Message);
            context.Logger.LogLine($"TITULO DESERIALIZADO  2 {titulo.idLinha} - {titulo.codEspecieDoc} - {titulo.dataVencimento}");
            //Comunicar com outra SNS

            await Task.CompletedTask;
        }
    }
}
