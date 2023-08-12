using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace MaisSaude.ServicoEmail
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMQFactory _rabbitMQFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(RabbitMQFactory rabbitMQFactory, ILogger<Worker> logger)
        {
            _rabbitMQFactory = rabbitMQFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var canal = _rabbitMQFactory.GetChannel();
                BasicGetResult retorno = canal.BasicGet("cliente", false);

                if (retorno != null)
                {
                    var dados = JsonConvert.DeserializeObject<object>(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                    await EnviarEmail(dados.Email, dados.Nome);
                    canal.BasicAck(retorno.DeliveryTag, true);
                }
                else
                {
                    canal.BasicAck(retorno.DeliveryTag, false);
                }


                await Task.Delay(5000, stoppingToken);
            }
        }


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
            StreamReader leitor = new StreamReader(@"C:\Users\Claud\source\repos\Claudio-lunardi\CAR-LOCADORA\CarLocadora.EnviarEmail\TemplateEmail\TemplateEmail.cshtml", Encoding.UTF8);
            var conteudo = leitor.ReadToEnd();
            var TemplateEmail = conteudo.Replace("Nome¢", nome);

            //StringBuilder sb = new StringBuilder();
            //sb.Append($"<p>Parabéns <b>{nome},</b></p>");
            //sb.Append($"<p>Seja muito bem-vindo a <b>CAR-LOCADORA.</b></p>");
            //sb.Append($"<p>Estamos muito felizes de você fazer parte da <b>CAR-LOCADORA</b>.</p>");
            //sb.Append($"<br>");
            //sb.Append($"<p>Grande abraço</p>");

            return TemplateEmail;
        }

    }
}