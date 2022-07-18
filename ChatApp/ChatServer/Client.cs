using ChatServer.Net.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Client
    {
        //屬性
        public string Username { get; set; }
        public Guid UID { get; set; } //唯一識別碼
        public TcpClient ClientSocket { get; set; }
        PacketReader _packetReader;

        
        public Client(TcpClient client)
        {
            //賦予
            ClientSocket = client; //使用client socket
            UID = Guid.NewGuid(); //建立識別碼
            _packetReader = new PacketReader(ClientSocket.GetStream()); 

            var opcode = _packetReader.ReadByte();
            Username = _packetReader.ReadMessage(); //
            Console.WriteLine($"[{DateTime.Now}]: Client has connected with the username: {Username}");

            Task.Run(() => Process());
        }
        void Process()
        {
            while (true)
            {
                try
                {
                    var opcode = _packetReader.ReadByte();//確認封包是屬於哪個部分的
                    switch (opcode)
                    {
                        case 5:
                            //是5則接收訊息
                            var msg = _packetReader.ReadMessage();
                            Console.WriteLine($"[{ DateTime.Now}]: Message received!{msg}");
                            Program.BroadcastMessage($"[{DateTime.Now}]: [{Username}] {msg}");
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception) //斷連
                {
                    Console.WriteLine($"[{UID.ToString()}]: Disconnected!");
                    Program.BroadcastDisconnect(UID.ToString());
                    ClientSocket.Close();
                    break;
                }
            }
        }
    }
}
