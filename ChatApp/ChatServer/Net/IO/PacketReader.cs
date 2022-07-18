using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Net.IO
{
    class PacketReader : BinaryReader  //解讀封包
    {
        private NetworkStream _ns; //網路流(從網路得來的資料)
        public PacketReader(NetworkStream ns) : base(ns) //先得到ns 再把ns丟到base class運作 最後再_ns = ns;
        {
            _ns = ns;
        }
        public string ReadMessage()
        {
            byte[] msgBuffer;//開buffer緩衝區
            
            var length = ReadInt32();//(讀取當時的stream)得到的message長度
            //Console.WriteLine("@@@:"+length.ToString()); 
            msgBuffer = new byte[length];
            _ns.Read(msgBuffer, 0, length); //代表有多少緩衝區可以使用，先從第0的位置開始放入(offset)，然後資料有多少長度
                                            //Read : 從NetworkStream讀取資料並放入msgbuffer

            var msg = Encoding.ASCII.GetString(msgBuffer); //將byte陣列"解讀"成String
            //Console.WriteLine("@@@" + msg);
            return msg; //回傳message

        }
    }
}
