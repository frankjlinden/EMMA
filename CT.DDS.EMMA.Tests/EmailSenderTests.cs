using CT.DDS.EMMA.Con.Models;
using MimeKit;
using Xunit;


namespace CT.DDS.EMMA.Con.Tests
{
    public class EmailSenderTests 
    {
        [Fact]
        public void Send()
        {
            SmtpConfig cfg = new SmtpConfig()
            {
                Host = "smtp.gmail.com",
                Port = 465,
                UserName = "frankjlindenct@gmail.com",
                Password = "P@ss4CTGaccount"
            };

            IEmailSender emailSender = new EmailSender(cfg);
            MimeMessage msg = new MimeMessage();
            
            // Split by semicolon and add an addresss for each one
            string[] addresses =  Utils.SplitAddresses("frankjlinden@yahoo.com;frankjlinden@gmail.com");
        
            for (int i = 0; i < addresses.Length; i++)
              msg.Bcc.Add(new MailboxAddress(addresses[i]));

            msg.Subject = "test subject -:)";
            
            msg.Sender = (new MailboxAddress("frankjlindenct@gmail.com"));
            msg.Body = new TextPart("plain") { Text = "Thismessageisamessageandthatisallthatitis." };
            string connect;
            string auth="";
            string send="";

            connect = emailSender.Connect();

            if (connect == "Connected")
            auth = emailSender.Authenticate(cfg.UserName,cfg.Password);

            if (auth =="Authenticated")
        //  send = emailSender.Send(msg);

            Assert.True(connect == "Connected");
            Assert.True(auth == "Authenticated");
            Assert.True(send == "Sent");
        }
       

    }

}

