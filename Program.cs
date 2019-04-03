using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace routeMining
{
    internal class Program
    {

        // Handler Interface
        public interface IHandlerInterface_API
        {
            // Method for building the chain of responsibility
            IHandlerInterface_API SetNextObject(IHandlerInterface_API handler);

            //  Method for executing a request (conversion type)
            object Handle(object request);
        }

        // Handler Abstract Class
        private abstract class API_abstractHandler : IHandlerInterface_API
        {
            protected IHandlerInterface_API nextObject;

            public IHandlerInterface_API SetNextObject(IHandlerInterface_API handler)
            {
                nextObject = handler;
                return handler;
            }

            public virtual object Handle(object request)
            {
                if (nextObject != null)
                {
                    return nextObject.Handle(request);
                }
                else
                {
                    return null;
                }
            }
        }

        // Concrete class to handle API call to ups
        class API_ups : API_abstractHandler
        {

            public override object Handle(object request)
            {
                if ((request as string) == "ups")
                {
                    Console.WriteLine("ups");
                    return request;
                }
                else
                {
                    return base.Handle(request);
                }
            }
        }

        // Concrete clsas to handle API call to smartyStreets
        class API_smartyStreets : API_abstractHandler
        {
            public override object Handle(object request)
            {
                if ((request as string) == "ss")
                {
                    Console.WriteLine("SMARTY STREETS");
                    return request;
                }
                else
                {
                    return base.Handle(request);
                }
            }
        }

        // Details of Client Request
        private class ClientRequest
        {
            // Overloaded Constructor
            public static void ClientInput(API_abstractHandler handler, string conversionType)
            {
                object result = handler.Handle(conversionType);

                if (result != null)
                {
                    Console.Write($"Result: {result}\n");
                }
                else
                {
                    Console.WriteLine($"{conversionType} was not utilized.\n");
                } 
            }
        }

        private static void Main(string[] args)
        {
            //Create the chain links
            var ups = new API_ups();
            var ss = new API_smartyStreets();

            ups.SetNextObject(ss); // create chain

            Console.WriteLine("First call with ups:\n");
            ClientRequest.ClientInput(ups, "ups");
            Console.WriteLine();

            Console.WriteLine("Second call with smartyStreets:\n");
            ClientRequest.ClientInput(ups, "ss");
            Console.WriteLine();
        }

    }
}
