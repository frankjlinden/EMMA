
using CT.DDS.EMMA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.DDS.EMMA.Send
{
    public interface IEmailSender
    {
        void Send(Message msg);
        string Connect();
        string Authenticate(string user, string password);
        void Disconnect();
        bool IsConnected();
        bool IsAuthenthicated();
    }

}
