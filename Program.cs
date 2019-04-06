/* CIS 476 | Term Project | Winter 2019 */

using System;
using System.Collections.Generic;
using System.IO;
using SmartyStreets;
using SmartyStreets.USStreetApi;

namespace routeMining
{
    internal class Program
    {
        // Class to hold authorization ID and authorization token
        // for SmartyStreets API
        public class smartyStreet_Credentials
        {
            public string authId = "d104ca15-4c71-fd06-ef5a-d83ea48062c1";
            public string authToken = "Y3osq0zukbsCWzLq3KMu";
        }

        //Class to hold Singleton of the smartyStreet_Credentials() class
        public class Singleton
        {
            private static Singleton instance;
            public static List<smartyStreet_Credentials> smartyStreet_CredentialsList { get; set; }
            public Singleton() { }
            public static Singleton Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    if (smartyStreet_CredentialsList == null)
                    {
                        smartyStreet_CredentialsList = new List<smartyStreet_Credentials>();
                    }
                    return instance;
                }
            }

            //Method to obtain Singleton list of data from smartyStreet_Credentials()
            public List<smartyStreet_Credentials> getCredentialList(smartyStreet_Credentials userData)
            {
                Singleton singleton = Singleton.Instance;
                var list = singleton.AddCredential(userData);
                return list;
            }

            // Method to add data from smartyStreet_Credentials() to List<smartyStreet_Credentials> 
            public List<smartyStreet_Credentials> AddCredential(smartyStreet_Credentials smartyStreet_Credentials)
            {
                smartyStreet_CredentialsList.Add(smartyStreet_Credentials);
                return smartyStreet_CredentialsList;
            }
        }

        // Handler Interface
        public interface IHandlerInterface_API
        {
            // Method for building the chain of responsibility
            IHandlerInterface_API SetNextObject(IHandlerInterface_API handler);

            //  Method for executing a request (conversion type)
            string Handle(string request);
        }

        // Handler Abstract Class
        public abstract class API_abstractHandler : IHandlerInterface_API
        {
            protected IHandlerInterface_API nextObject;

            public IHandlerInterface_API SetNextObject(IHandlerInterface_API handler)
            {
                nextObject = handler;
                return handler;
            }

            public virtual string Handle(string request)
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

        // Concrete class to handle API call for Address Validation 
        // to the SmartyStreets API
        class AddressValidation : API_abstractHandler
        {
            public override string Handle(string request)
            {
                if ((request as string) == "addressValidation")
                {
                    Singleton Singleton = new Singleton();
                    smartyStreet_Credentials credentials = new smartyStreet_Credentials(); 
                    var userInfo = Singleton.getCredentialList(credentials);

                    var authId = userInfo[0].authId;
                    var authToken = userInfo[0].authToken;

                    var client = new ClientBuilder(authId, authToken).BuildUsStreetApiClient();

                    var lookup = new Lookup
                    {
                        Street = "4901 Evergreen Rd",
                        City = "Dearborn",
                        State = "MI",
                        ZipCode = "48128",
                        MaxCandidates = 1,
                        MatchStrategy = Lookup.INVALID // "invalid" is the most permissive match
                    };

                    var lookup2 = new Lookup
                    {
                        Street = "5101 Evergreen Rd",
                        City = "Dearborn",
                        State = "MI",
                        ZipCode = "48128",
                        MaxCandidates = 1,
                        MatchStrategy = Lookup.INVALID // "invalid" is the most permissive match
                    };

                    try
                    {
                        client.Send(lookup);
                        client.Send(lookup2);
                    }
                    catch (SmartyException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    var candidates = lookup.Result;
                    var candidates2 = lookup2.Result;

                    if (candidates.Count == 0 || candidates2.Count == 0)
                    {
                        return "No candidates. This means the address is not valid.";
                    }

                    var firstCandidate = candidates[0];
                    var secondCandidate = candidates2[0];

                    Console.WriteLine("Address is valid.");
                    return ("Address at " + lookup.Street + " " + lookup.City + " " + lookup.State + " " + lookup.ZipCode + " is verified and Address at " + lookup2.Street + " " + lookup2.City + " " + lookup2.State + " " + lookup2.ZipCode + " is verified.");
                }
                else
                {
                    return base.Handle(request);
                }
            }
        }

        // Concrete class to handle API call for Carrier Routes 
        // to the SmartyStreets API
        class CarrierRoute : API_abstractHandler
        {
            public override string Handle(string request)
            {
                if ((request as string) == "carrierRoute")
                {
                    Singleton Singleton = new Singleton();
                    smartyStreet_Credentials credentials = new smartyStreet_Credentials();
                    var userInfo = Singleton.getCredentialList(credentials);

                    var authId = userInfo[0].authId;
                    var authToken = userInfo[0].authToken;

                    var client = new ClientBuilder(authId, authToken).BuildUsStreetApiClient();

                    var lookup = new Lookup
                    {
                        Street = "4901 Evergreen Rd",
                        City = "Dearborn",
                        State = "MI",
                        ZipCode = "48128",
                        MaxCandidates = 1,
                        MatchStrategy = Lookup.INVALID // "invalid" is the most permissive match
                    };

                    var lookup2 = new Lookup
                    {
                        Street = "5101 Evergreen Rd",
                        City = "Dearborn",
                        State = "MI",
                        ZipCode = "48128",
                        MaxCandidates = 1,
                        MatchStrategy = Lookup.INVALID // "invalid" is the most permissive match
                    };

                    try
                    {
                        client.Send(lookup);
                        client.Send(lookup2);
                    }
                    catch (SmartyException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    var candidates = lookup.Result;
                    var candidates2 = lookup2.Result;

                    if (candidates.Count == 0 || candidates2.Count == 0)
                    {
                        return "No candidates. This means the address is not valid.";
                    }

                    var firstCandidate = candidates[0];
                    var secondCandidate = candidates2[0];

                    Console.WriteLine("Address is valid.");
                    var output1 = "Carrier Route for address at " + lookup.Street + " " + lookup.City + " " + lookup.State + " " + lookup.ZipCode + " is " + firstCandidate.Metadata.CarrierRoute + ".\n";
                    var output2 = "Carrier Route for address at " + lookup2.Street + " " + lookup2.City + " " + lookup2.State + " " + lookup2.ZipCode + " is " + secondCandidate.Metadata.CarrierRoute + ".";
                    return (output1 + output2);
                }
                else
                {
                    return base.Handle(request);
                }
            }
        }

        // Decorator Abstract Class
        // Extends API_abstractHandler class to be interchangeable with
        // its concrete decorators
        public abstract class Decorator : API_abstractHandler
        {
            public abstract override string Handle(string request);
        }

        public class add_API_Name : Decorator
        {
            private API_abstractHandler handler;

            public add_API_Name() { }


            public add_API_Name(API_abstractHandler handler)
            {
                this.handler = handler;
            }

            public override string Handle(string request)
            {
                string getResult = handler.Handle(request);
                return getResult;
            }
        }

        // Client invokes chain of responsibility pattern by calling API_abstractHandler
        private class ClientRequest
        {
            public static string ClientInput(API_abstractHandler handler, string conversionType)
            {
                object result = handler.Handle(conversionType);

                if (result != null)
                {
                    Console.Write($"Result: {result}\n");
                    return ($"{result}");
                }
                else
                {
                    Console.WriteLine($"{conversionType} was not utilized.\n");
                    return ($"{conversionType} was not utilized.\n");
                } 
            }
        }

        public static void Main(string[] args)
        {
            // Create object for smartyStreets_Credentials() and Singleton()
            // Create var to hold singleton instance of smartyStreet_Credentials()
            smartyStreet_Credentials credentials = new smartyStreet_Credentials();
            Singleton Singleton = new Singleton();
            var userInfo = Singleton.getCredentialList(credentials); 

            // Create object for AddressValidation() and CarrierRoute()
            // Create chain link of objects to establish Chain of Responsibility 
            API_abstractHandler addValid = new AddressValidation();
            API_abstractHandler carrRoute = new CarrierRoute();
            addValid.SetNextObject(carrRoute); 

            Console.WriteLine("Address Validation");  //test address validation
            ClientRequest.ClientInput(addValid, "addressValidation");
            Console.WriteLine();

            Console.WriteLine("Carrier Route"); //test carrier route
            ClientRequest.ClientInput(addValid, "carrierRoute");
            Console.WriteLine();

            Console.WriteLine("Test Decorator for Address Validation");
            addValid = new add_API_Name(addValid); // add decorator for address validation
            ClientRequest.ClientInput(addValid, "addressValidation"); //make request with decorator
            Console.WriteLine();

            Console.WriteLine("Test Decorator for Carrier Route");
            addValid = new add_API_Name(addValid); 
            ClientRequest.ClientInput(addValid, "addressValidation");
            Console.WriteLine();
        }

    }
}
