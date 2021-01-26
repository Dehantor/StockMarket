using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerEF
{
    /// <summary>
    /// Класс для получения конфигурационных данных с клиентом
    /// </summary>
    public class MessageClient
    {
        static Setting _setting;
        public MessageClient(Setting setting)
        {
            _setting = setting;
        }
        /// <summary>
        /// Асинхонный метод
        /// </summary>
        public void receiveMessageAsync()
        {
            Task.Run(new Action(receiveMessage));
        }
        static int port = 8005; // порт для приема входящих запросов
        /// <summary>
        /// Синхронный метод
        /// </summary>
        void receiveMessage()
        {
            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);
                // начинаем прослушивание
                listenSocket.Listen(10);
                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    try
                    {
                        string[] value = builder.ToString().Split('-');
                        //изменяем конфигурационный файл, поскольку пльзователь будет вводить в минутах умножаем на 60000
                        _setting.SetSetting(Convert.ToInt32(value[0]), Convert.ToInt32(value[1]) * 60000);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message+":" +builder.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

