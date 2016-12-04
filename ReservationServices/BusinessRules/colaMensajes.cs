using ReservationServices.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Messaging;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ReservationServices.BusinessRules
{
    public class colaMensajes
    {
        public Task<string> colaPedidoCorreoAsync(BEOrden obj)
        {
            return Task.Run(() =>
            {
                var rutaCola = @".\private$\pedidos";
                if (!MessageQueue.Exists(rutaCola))
                    MessageQueue.Create(rutaCola);

                var cola = new MessageQueue(rutaCola);
                var mensaje = new Message();
                mensaje.Label = obj.ALF_NUME_PEDI;
                mensaje.Body = new BEOrden() {
                    ALF_NUME_PEDI = obj.ALF_NUME_PEDI,
                    ALF_NUME_DOCU = obj.ALF_NUME_DOCU,
                    ALF_CORR = obj.ALF_CORR,
                    ALF_NOMB = obj.ALF_NOMB,
                    ALF_TIPO_DEPO = obj.ALF_TIPO_DEPO,
                    ALF_TIPO_CANC = obj.ALF_TIPO_CANC,
                    FEC_RESE = obj.FEC_RESE,
                    ALF_HORA = obj.ALF_HORA,
                    MON_PAGA = obj.MON_PAGA,
                    IND_ENVI = "P"
                };
                cola.Send(mensaje);

                // ENviando correo al cliente
                var mail = new MailMessage();
                var SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential("centenarioupc@gmail.com", "tecsolutions");
                SmtpServer.EnableSsl = true;

                mail.From = new MailAddress("centenarioupc@gmail.com");
                mail.To.Add(obj.ALF_CORR);
                mail.Subject = "Confirmar Pago";
                mail.IsBodyHtml = true;
                mail.Body = "TIPO DE DEPORTE:" + obj.ALF_TIPO_DEPO + "<br />" +
                            "TIPO DE CANCHA:" + obj.ALF_TIPO_CANC + "<br />" +
                            "FECHA:" + obj.FEC_RESE + "<br />" +
                            "HORARIO:" + obj.ALF_HORA + "<br />" +
                            "CTA. BANCO CONTINENTAL: 0011-0193-0200171633<br />CTA. BANCO DE CREDITO: 193-2288304-0-58<br />" +
                            "MONTO A PAGAR: " + obj.MON_PAGA + "";
                SmtpServer.Send(mail);

                //Actualizando cola de mensaje enviado
                //mensaje.Body = new BEOrden()
                //{
                //    ALF_NUME_PEDI = obj.ALF_NUME_PEDI,
                //    ALF_NUME_DOCU = obj.ALF_NUME_DOCU,
                //    ALF_CORR = obj.ALF_CORR,
                //    ALF_NOMB = obj.ALF_NOMB,
                //    ALF_TIPO_DEPO = obj.ALF_TIPO_DEPO,
                //    ALF_TIPO_CANC = obj.ALF_TIPO_CANC,
                //    FEC_RESE = obj.FEC_RESE,
                //    ALF_HORA = obj.ALF_HORA,
                //    MON_PAGA = obj.MON_PAGA,
                //    IND_ENVI = "E"
                //};
                //cola.Send(mensaje);
                return "OK";
            });
        }

        public List<BEOrden> GetAllPedidos()
        {
            var lstMessages = new List<BEOrden>();
            var rutaCola = @".\private$\pedidos";
            if (!MessageQueue.Exists(rutaCola))
            {
                using (var messageQueue = new MessageQueue(rutaCola))
                {
                    Message[] messages = messageQueue.GetAllMessages();
                    foreach (Message message in messages)
                    {
                        message.Formatter = new XmlMessageFormatter(new Type[] { typeof(BEOrden) });
                        var orden = (BEOrden)message.Body;
                        lstMessages.Add(orden);
                    }
                }                
            }
            return lstMessages;
        }
    }
}