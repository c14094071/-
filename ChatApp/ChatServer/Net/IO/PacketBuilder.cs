using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Net.IO
{
    class PacketBuilder //建立封包
    {
        MemoryStream _ms;  //讀取或寫入備份存放區的記憶體
        public PacketBuilder()
        {
            _ms = new MemoryStream();
        }
        public void WriteOpCode(byte opcode) // opcode 用來指示電腦執行某一個運算功能
        {
            _ms.WriteByte(opcode);
        }
        public void WriteMessage(string msg) //寫入訊息
        {
            var msgLength = msg.Length;  //message長度
            _ms.Write(BitConverter.GetBytes(msgLength));  //寫入資料流
            _ms.Write(Encoding.ASCII.GetBytes(msg));
        }
        public byte[] GetPacketBytes()
        {
            return _ms.ToArray();

        }
    }
}
