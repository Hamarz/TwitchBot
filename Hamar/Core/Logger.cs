using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamar.Core
{
    public class Logger
    {
        private Queue<string> queue;
        public Logger()
        {
            queue = new Queue<string>();
        }



        public void Log(string input)
        {
            queue.Enqueue(input);
        }

        public void Log(Exception ex)
        {
            Log(ex.Message);
        }

        public string GetLatest()
        {
            try
            {
                if (queue.Count <= 0)
                    return string.Empty;

                return queue.Dequeue();
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }
    }
}
