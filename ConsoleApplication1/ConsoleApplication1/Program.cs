using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApplication1
{
    class Program
    {
        const int MaxCountUser = 2;
        const int MaxCountTrans = 10;
        File f = new File();
       
        static int cb = 0;
        static void Main(string[] args)
        {
            string file; string state;
            
            Program p = new Program();

            //потоки
            for (int i = 0; i < MaxCountUser; i++)
            {
                Thread myThread = new Thread(p.use);
                myThread.Start();

            }

            Thread.Sleep(100);
            Console.ReadLine();

            //p.f.Change_state_file();
            p.f.Display_info();
            Console.Read();

        }
        void use()
        {
            Random r = new Random();
            cb = r.Next(1, 10);
            //cb++;
            Console.WriteLine(cb);
            User us = new User(cb);
            File f = new File();

            for (int i = 0; i < MaxCountTrans; i++)
            {
                string file; string state;
                file = us.Choice_file();
                state = us.Change_state();
                Console.WriteLine("Пользователь" + us.name.ToString() + " отправляет запрос на " + state.ToString() + " файл " + file.ToString());
                us.Send(f, file, state);
                string stfile; string stat;
                stfile = f.Change_state_file(file);
                stat = f.state_file(file, stfile);
                Console.WriteLine("Файловая система отправляет " + stat.ToString() );
                f.Send(us, stat);
            }
        }
    }
}
