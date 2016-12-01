using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication1
{
    public class File : CollectionBase
    {
        ConcurrentBag<string> cb_file = new ConcurrentBag<string>();
        ConcurrentBag<string> cb_state = new ConcurrentBag<string>();
        string stfile = "free";
        const int slep = 4;

        public void Get_info(string file, string state)
        {
            Console.WriteLine("Файловая система принимает: Имя файла " + file.ToString() + " для  " + state.ToString());
            cb_file.Add(file.ToString());
            cb_state.Add(state.ToString());
        }

        public string Change_state_file(string file)
        {
            foreach (string c in cb_state)
                if (c == "note")
                {
                    stfile = "busy";
                }
            return stfile;
        }
        string stat = "";
        public string state_file(string file, string stfile)
        {
            foreach (string sp in cb_file)
            {
                foreach (string c in cb_state)
                {
                    if ((c == "read") && (stfile == "free"))
                    {
                        stat = "чтение";
                    }
                    else
                    {
                        stat = "ожидание";
                    }
                }
            }
            return stat;
        }

        public void Display_info()
        {
            foreach (string sp in cb_file)
            {
                foreach (string c in cb_state)
                {
                    if ((c == "read") && (stfile == "free"))
                    {
                        Console.WriteLine("чтение");
                    }
                    else
                    {
                        Console.WriteLine("ожидание");
                    }
                }
            }
        }

        public void Send(User sv, string state)
        {
            int sl = 0;
            Random r = new Random();
            sl = r.Next(10, 100);
            Thread.Sleep(sl * slep);
            sv.Get_info(state);
        }
    }
}
