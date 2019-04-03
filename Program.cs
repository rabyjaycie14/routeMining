using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace routeMining
{
    internal class Program
    {
        public class userCredentials_UPS
        {
            public string accessLicenseNumber = "abcd1234";
            public string userID = "username";
            public string password = "password";
            public string domain = "https://wwwcie.ups.com/ups.app/xml/XAV";
        }

        public class userCredentials_SS
        {
            public string scheme = "https";
            public string hostname = "us-street.api.smartystreets.com";
            public string path = "/street-address";
            public string query = "String? auth-id=123&auth-token=abc";
        }

        // Singleton class used to create a single instance of the credentials needed to utilize
        // the UPS API
        public class singleton_UPS
        {
            private static singleton_UPS instance;
            public static List<userCredentials_UPS> userCredentials_UPSList { get; set; }
            public singleton_UPS() { }
            public static singleton_UPS Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new singleton_UPS();
                        Console.WriteLine("UPS instance is null");

                    }
                    if (userCredentials_UPSList == null)
                    {
                        userCredentials_UPSList = new List<userCredentials_UPS>();
                        Console.WriteLine("UPS instance list is null");

                    }
                    Console.WriteLine("SS instance and list are NOT null");
                    return instance;
                }
            }
            public List<userCredentials_UPS> AddCredential(userCredentials_UPS userCredentials_UPS)
            {
                userCredentials_UPSList.Add(userCredentials_UPS);
                return userCredentials_UPSList;
            }

            public List<userCredentials_UPS> veryifyCredentials_UPS(userCredentials_UPS userData)
            {
                string accessLicenseNumber = userData.accessLicenseNumber;
                string userID = userData.userID;
                string password = userData.password;
                string domain = userData.domain;

                singleton_UPS singleton = singleton_UPS.Instance;
                var list = singleton.AddCredential(userData);

                return list;
            }
        }

        // Singleton class used to create a single instance of the credentials needed to utilize
        // the SmartyStreets API
        public class singleton_SS
        {
            private static singleton_SS instance;
            public static List<userCredentials_SS> userCredentials_SSList { get; set; }
            public singleton_SS() { }
            public static singleton_SS Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new singleton_SS();
                        Console.WriteLine("SS instance is null");
                    }
                    if (userCredentials_SSList == null)
                    {
                        userCredentials_SSList = new List<userCredentials_SS>();
                        Console.WriteLine("SS instance list is null");
                    }
                    Console.WriteLine("SS instance and list are NOT null");
                    return instance;
                }
            }
            public List<userCredentials_SS> AddCredential(userCredentials_SS userCredentials_SS)
            {
                userCredentials_SSList.Add(userCredentials_SS);
                return userCredentials_SSList;
            }

            public List<userCredentials_SS> veryifyCredentials_SS(userCredentials_SS userData)
            {
                string scheme = userData.scheme;
                string hostname = userData.hostname;
                string path = userData.path;
                string query = userData.query;

                singleton_SS singleton = singleton_SS.Instance;
                var list = singleton.AddCredential(userData);

                return list;
            }
        }

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
            
            API_ups ups = new API_ups();
            API_smartyStreets ss = new API_smartyStreets();
            userCredentials_UPS creds_UPS = new userCredentials_UPS();
            userCredentials_SS creds_SS = new userCredentials_SS();
            singleton_UPS singleton1 = new singleton_UPS();
            singleton_SS singleton2 = new singleton_SS();

            ups.SetNextObject(ss); 

            var test1 = singleton1.veryifyCredentials_UPS(creds_UPS);
            var test2 = singleton2.veryifyCredentials_SS(creds_SS);

            Console.WriteLine("UPS concrete handler:");
            ClientRequest.ClientInput(ups, "ups");
            Console.WriteLine();

            Console.WriteLine("SS concrete handler:");
            ClientRequest.ClientInput(ups, "ups");
            Console.WriteLine();

            Console.WriteLine("UPS Singleton Instance: ");
            Console.WriteLine("Access license number: " + test1[0].accessLicenseNumber);
            Console.WriteLine("User ID: " + test1[0].userID);
            Console.WriteLine("Password: " + test1[0].password);
            Console.WriteLine("Domain: " + test1[0].domain);
            Console.WriteLine();

            Console.WriteLine("SS Singleton Instance: ");
            Console.WriteLine("Access license number: " + test2[0].scheme);
            Console.WriteLine("User ID: " + test2[0].hostname);
            Console.WriteLine("Password: " + test2[0].path);
            Console.WriteLine("Domain: " + test2[0].query);


            Console.WriteLine();
        }

    }
}
