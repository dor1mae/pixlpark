using RabbitMqConsumer.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var listener = new RabbitMqListener("amqps://suqdmlrq:o6BEparo_nOCTMYmbIS69n16Drr-SwJc@kangaroo.rmq.cloudamqp.com/suqdmlrq",
                "MyQueue");
            Console.WriteLine("Нажмите Enter, чтобы завершить работу программы");
            Console.ReadLine();
        }
    }
}
