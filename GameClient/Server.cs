using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Generic;
namespace Team_27_Game{
    public class Server{
        public static int maxPlayer{
            private set;
            get;
        }

        public static int port{
            get;
            private set;
        }

        public static Dictionary<int,Client> clients = new Dictionary<int,Client>(); 
              
        public static TcpListener tcpListener;

        public static void start(int _maxPlayer, int _port){
            maxPlayer = _maxPlayer;
            port = _port;
            InitializeServerData();
            tcpListener = new TcpListener(IPAddress.Any,port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback),null);

            Console.WriteLine("Server Started on port {0}",port);
        }
        
        public static void TCPConnectCallback(IAsyncResult result){
            TcpClient client = tcpListener.EndAcceptTcpClient(result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback),null);
            Console.WriteLine("Incoming connection from {0}",client.Client.RemoteEndPoint);
            for(int i=1;i<= maxPlayer;i++){
                if(Server.clients[i].tcp.socket==null){
                    Server.clients[i].tcp.Connect(client);
                    return;
                }

                Console.WriteLine("Failed to connect: Server Full");
            }
        } 

        private static void InitializeServerData(){
            for(int i=1;i<=maxPlayer;i++){
                Server.clients.Add(i,new Client(i));
            }
        }
    }
}