using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Classes {
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChat {
        [OperationContract]
        User Connect(string NickName, string Password);

        [OperationContract]
        void Disconnect(int ID);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, int ID);
    }

    public interface IServerChatCallback {
        [OperationContract(IsOneWay = true)]
        void MessageCallback(string message);
    }
}
