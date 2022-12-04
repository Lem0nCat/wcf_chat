using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Classes {
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat {
        List<User> users = new List<User>();
        int nextID = 1; 

        public User Connect(string NickName, string Password) {
            User user = new User() {
                ID = nextID++,
                Nickname = NickName,
                Password = Password,
                operationContext = OperationContext.Current
            };
            SendMessage($": {user.Nickname} подключился к чату", 0);
            users.Add(user);

            return user;
        }

        public void Disconnect(int ID) {
            var user = users.FirstOrDefault(i => i.ID == ID);
            if (user != null) {
                users.Remove(user);
                SendMessage($": {user.Nickname} покинул чат!", 0);
            }
        }

        public void SendMessage(string message, int ID) {
            foreach (var item in users) {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(i => i.ID == ID);
                if (user != null) {
                    answer += $": {user.Nickname} ";
                }

                answer += message;

                item.operationContext.GetCallbackChannel<IServerChatCallback>().MessageCallback(answer);
            }
        }
    }
}
