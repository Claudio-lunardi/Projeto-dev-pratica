using MaisSaude.Common.Connections;
using MaisSaude.Infra.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MaisSaude.Email
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMQFactory _rabbitMQFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, RabbitMQFactory rabbitMQFactory)
        {
            _logger = logger;
            _rabbitMQFactory = rabbitMQFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var canal = _rabbitMQFactory.GetChannel();
                BasicGetResult retorno = canal.BasicGet("Email", false);

                if (retorno != null)
                {
                    var dados = JsonConvert.DeserializeObject<ModelRabbit>(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                    await EnviarEmail(dados.Email, dados.Nome);
                    canal.BasicAck(retorno.DeliveryTag, true);
                }
                await Task.Delay(5000, stoppingToken);
            }
        }

        #region GERAR EMAIL
        private async Task EnviarEmail(string Email, string nome)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("Turma1@devpratica.com.br");
            message.To.Add(Email);
            message.Subject = "Bem-Vindo!";
            message.IsBodyHtml = true;
            message.Body = EmailBoasVindas(nome);

            var smtpCliente = new SmtpClient("smtp.kinghost.net")
            {
                Port = 587,
                Credentials = new NetworkCredential("Turma1@devpratica.com.br", "Senha@senha10"),
                EnableSsl = false,

            };
            smtpCliente.Send(message);
        }

        private string EmailBoasVindas(string nome)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<p>Parab�ns <b>{nome},</b></p>");
            sb.Append($"<p>Seja muito bem-vindo a <b>MaisSaude</b></p>");
            sb.Append($"<p>Estamos muito felizes de voc� fazer parte da <b>MaisSaude</b>.</p>");
            sb.Append($"<br>");
            sb.Append($"<p>Grande abra�o</p>");

            return sb.ToString();
        }
        #endregion
    }
}