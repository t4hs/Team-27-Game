using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
namespace Team_27_Game{
    public class Client{
        public int id;
        public TCP tcp;
        
        public static int dataBufferSize = 4096;
        
        public Client(int id){
            this.id = id;
            tcp = new TCP(id);
        }
        public class TCP{
            public TcpClient socket;
            private readonly int id;
            private NetworkStream stream;
            private byte[] receiveBuffer;
            public TCP(int id){
                this.id = id;
            }

            public void Connect(TcpClient socket){
                this.socket = socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;
                stream = socket.GetStream();
                receiveBuffer = new byte[dataBufferSize];
                stream.BeginRead(receiveBuffer,0,dataBufferSize,ReceiveCallback,null);

                //TODO send welcome packets
            }

            private void ReceiveCallback(IAsyncResult result){
                try
                {
                    int byteLength = stream.EndRead(result);
                    if(byteLength <= 0){
                        return;
                    }

                    byte [] data = new byte[byteLength];

                    Array.Copy(receiveBuffer,data,byteLength);

                    stream.BeginRead(receiveBuffer,0, dataBufferSize,ReceiveCallback,null);
                }
                catch (System.Exception ex)
                {
                     // TODO

                     Console.WriteLine("Error while receiving tcp data: {0}", ex.Message);
                }
            }
        }
    }
}