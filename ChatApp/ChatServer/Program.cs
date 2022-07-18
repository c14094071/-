using ChatServer.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    class Program
    {
        static List<Client> _users;

        static TcpListener _listener; 
        
        static void Main(string[] args)
        {
            _users = new List<Client>();

            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"),7891);
            _listener.Start();
            while (true) //持續監聽
            {
                var client = new Client(_listener.AcceptTcpClient());
                _users.Add(client);
                /* Broadcast the connection to everyone on the server*/
                BroadcastConnection();
            }

            
            

        }
        static void BroadcastConnection() //建立廣播連結
        {
            foreach(var user in _users)
            {
                foreach(var usr in _users)
                {
                    var broadcastPacket = new PacketBuilder();  //建立廣播封包
                    broadcastPacket.WriteOpCode(1); // opcode =1 代表來源
                    broadcastPacket.WriteMessage(usr.Username); 
                    broadcastPacket.WriteMessage(usr.UID.ToString());
                    user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes()); 
                    ///client對usr送封包，因此聊天室的每一個人都會自己和其他人握手
                }
            }
        }

        public static void BroadcastMessage(string message) //對其他人傳訊息
        {
            foreach(var user in _users)
            {
                var msgPacket = new PacketBuilder(); 
                msgPacket.WriteOpCode(5);
                msgPacket.WriteMessage(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }
        public static void BroadcastDisconnect(string uid) //取消廣播連結
        {
            var disconnectedUser = _users.Where(x => x.UID.ToString() == uid).FirstOrDefault(); 
            //where代表從list中找出uid與之相同的，如果找不到會回傳null(FirstOrDefault()的功用)
            _users.Remove(disconnectedUser);
            //移除該用戶
            foreach (var user in _users) //並通知其他人此人離線了
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.WriteOpCode(10);
                broadcastPacket.WriteMessage(uid);
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());

            }
            BroadcastMessage($"[{disconnectedUser.Username}] Disconnected!");

        }
    }
}
