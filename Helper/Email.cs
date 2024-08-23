using System.Net.Mail;
using System.Net;

namespace DesafioSenaiCimatec.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;
        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                // Obtém as configurações do appsettings.json
                string host = _configuration.GetValue<string>("EmailSettings:SmtpServer");
                string nome = _configuration.GetValue<string>("EmailSettings:SenderName");
                string username = _configuration.GetValue<string>("EmailSettings:SenderEmail");
                string senha = _configuration.GetValue<string>("EmailSettings:Password");
                int porta = _configuration.GetValue<int>("EmailSettings:SmtpPort");
                bool usarSsl = _configuration.GetValue<bool>("EmailSettings:UseSsl");

                // Configura a mensagem de e-mail
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(username, nome)
                };
                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                // Configura o cliente SMTP
                SmtpClient smtp = new SmtpClient(host, porta)
                {
                    Credentials = new NetworkCredential(username, senha),
                    EnableSsl = usarSsl
                };

                // Envia o e-mail
                smtp.Send(mail);
                return true;
            }
            catch (System.Exception e)
            {
                // Log do erro
                Console.WriteLine($"Erro ao enviar email: {e.Message}");
                return false;
            }
        }
    }
}













