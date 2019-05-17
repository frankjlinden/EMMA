using CT.DDS.EMMA.Send.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;

namespace CT.DDS.EMMA.Send
{
   
    public class EmailSender : IEmailSender
    {
        SmtpConfig _smtpConfig { get; set; }
        SmtpClient _smtpClient { get; set; }

        public EmailSender(SmtpConfig smtpConfig)
        {
            _smtpConfig = smtpConfig;
            _smtpClient = new SmtpClient();

        }

        public string Connect()
        {
            try
            {
                _smtpClient.Connect(_smtpConfig.Host, _smtpConfig.Port, 0);
                if (_smtpClient.IsConnected)
                    return "Connected";
                else
                    return "Disconnected";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string Authenticate(string user, string password)
        {
            try
            {
                _smtpClient.Authenticate( user,  password);
                if (!_smtpClient.IsAuthenticated)
                    return "Authentication Failed";
                else
                    return "Authenticated";
            }
            catch (AuthenticationException ex)
            {
                return "Invalid user name or password.";

            }
            catch (SmtpCommandException ex)
            {
                return $"Error trying to authenticate: {ex.Message}";
            }
            catch (SmtpProtocolException ex)
            {
                return $"Protocol error while trying to authenticate: {ex.Message}";
            }
        }

        public void Send(Message msg)
        {
            MimeMessage emailMsg = msg.GetMimeMessage();
            try
            {
              _smtpClient.Send(emailMsg);
                msg.Status = MessageStatus.Sent;
            }
            catch (SmtpCommandException ex)
            {
                switch (ex.ErrorCode)
                {
                    case SmtpErrorCode.RecipientNotAccepted:
                        msg.Status = MessageStatus.RecipientNotAccepted;
                        msg.ErrorText = $"Recipient not accepted: {ex.Mailbox.Address}";
                        break;
                    case SmtpErrorCode.SenderNotAccepted:
                        msg.Status = MessageStatus.SenderNotAccepted;
                        msg.ErrorText = $"Sender not accepted: {ex.Mailbox.Address}";
                        break;
                    case SmtpErrorCode.MessageNotAccepted:
                        msg.Status = MessageStatus.MessageNotAccepted;
                        msg.ErrorText= $"Message not accepted. Check server disk space.";
                        break;
                    default:
                        msg.Status = MessageStatus.CommandError;
                        msg.ErrorText = $"Exception: {ex.StatusCode}:{ex.Message}:";
                        break;
                }
            }
            catch (SmtpProtocolException ex)
            {
                msg.Status = MessageStatus.ProtocolError;
                msg.ErrorText = $"Protocol error while sending message: {ex.Message}";
            }
            catch (Exception ex)
            {
                msg.Status = MessageStatus.ProgramError;
                msg.ErrorText = $"Program error while sending message: {ex.Message}";
            }
        }

        public void Disconnect()
        {
            if (_smtpClient.IsConnected)
                _smtpClient.Disconnect(true);
        }

        public bool IsConnected()
        {
            return _smtpClient.IsConnected;
        }

        public bool IsAuthenthicated()
        {
            return _smtpClient.IsAuthenticated;
        }

     
    }






}
