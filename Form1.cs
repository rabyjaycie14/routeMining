using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using SmartyStreets;
using SmartyStreets.USStreetApi;

namespace routeMining_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click_1(object sender, EventArgs e) { } // NOT NEEDED -- VS WONT LET ME DELETE?

        // Input: City
        private void textBox1_TextChanged(object sender, EventArgs e) { }

        // Input: Street address
        private void textBox2_TextChanged(object sender, EventArgs e) { }

        // Input: State
        private void textBox3_TextChanged(object sender, EventArgs e) { }

        // Input: Zip Code
        private void textBox4_TextChanged(object sender, EventArgs e) { }

        // Address Verification will appear here after button click
        private void textBox5_TextChanged(object sender, EventArgs e) { }

        // Carrier Route will appear here after button click
        private void textBox6_TextChanged(object sender, EventArgs e) { }

        // Export Verified Address file path will appear here after button click
        private void textBox7_TextChanged(object sender, EventArgs e) { }

        // Export Carrier Route information path will appear here after button click
        private void textBox8_TextChanged(object sender, EventArgs e) { }


        // Verify Address
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> userInput = new List<string>();
            userInput.Clear();

            // Get User Input
            string Street = textBox2.Text;
            string City = textBox1.Text;
            string State = textBox3.Text;
            string ZipCode = textBox4.Text;

            // Make list of User Input
            userInput.Add(Street);
            userInput.Add(City);
            userInput.Add(State);
            userInput.Add(ZipCode);


            // Create object for AddressValidation() and CarrierRoute()
            API_abstractHandler addValid = new AddressValidation();
            API_abstractHandler carrRoute = new CarrierRoute();

            addValid.SetNextObject(carrRoute); //create chain link
            addValid = new add_API_Name(addValid); //add decorator

            string output = ClientRequest.ClientInput(addValid, "addressValidation", userInput); //get results of address validation + decorator
            textBox5.Text = string.Empty; //clear textbox
            textBox5.AppendText(output); //output to textbox
        }

        // Get Carrier Route
        private void button2_Click(object sender, EventArgs e)
        {
            List<string> userInput = new List<string>();
            userInput.Clear();

            // Get User Input
            string Street = textBox2.Text;
            string City = textBox1.Text;
            string State = textBox3.Text;
            string ZipCode = textBox4.Text;

            // Make list of User Input
            userInput.Add(Street);
            userInput.Add(City);
            userInput.Add(State);
            userInput.Add(ZipCode);

            // Create object for AddressValidation() and CarrierRoute()
            API_abstractHandler addValid = new AddressValidation();
            API_abstractHandler carrRoute = new CarrierRoute();

            addValid.SetNextObject(carrRoute); //create chain link
            carrRoute = new add_API_Name(carrRoute); //add decorator

            string output = ClientRequest.ClientInput(carrRoute, "carrierRoute", userInput); //get results of address validation + decorator
            textBox6.Text = string.Empty; //clear textbox
            textBox6.AppendText(output); //output to textbox
        }

        // Export Address Information
        private void button3_Click(object sender, EventArgs e)
        {
            string CWD = System.IO.Directory.GetCurrentDirectory();
            string fileName = (CWD + @"\Export_AddressInformation");
            textBox7.Text = string.Empty;
            textBox7.AppendText(fileName);

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("New file created: {0}", DateTime.Now.ToString());
                    sw.WriteLine("Address Validation provided by SmartyStreets API");
                    sw.WriteLine(@"https://smartystreets.com/products/single-address");
                    sw.WriteLine("Address: " + textBox2.Text + " " + textBox1.Text + " " + textBox3.Text + " " + textBox4.Text);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        // Export Carrier Route Information
        private void button4_Click(object sender, EventArgs e)
        {
            string CWD = System.IO.Directory.GetCurrentDirectory();
            string fileName = (CWD + @"\Export_CarrierRouteInformation");
            textBox8.Text = string.Empty;
            textBox8.AppendText(fileName);

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("New file created: {0}", DateTime.Now.ToString());
                    sw.WriteLine("Carrier Route provided by SmartyStreets API");
                    sw.WriteLine(@"https://smartystreets.com/products/single-address");
                    sw.WriteLine("Carrier Route for " + textBox2.Text + " " + textBox1.Text + " " + textBox3.Text + " " + textBox4.Text + " is" + textBox6.Text);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }

        // Clear input fields
        private void button5_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
        }


        // Class to hold authorization ID and authorization token
        // for SmartyStreets API
        public class smartyStreet_Credentials
        {
            public string authId = "ENTER YOUR OWN AUTHORIZATION ID";
            public string authToken = "ENTER YOUR OWN AUTHORIZATION TOKEN";
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
            string Handle(string request,List<string> userInput);
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

            public virtual string Handle(string request, List<string> userInput)
            {
                if (nextObject != null)
                {
                    return nextObject.Handle(request, userInput);
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
            public override string Handle(string request, List<string> userInput)
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
                        Street = userInput[0],
                        City = userInput[1],
                        State = userInput[2],
                        ZipCode = userInput[3],
                        MaxCandidates = 1,
                        MatchStrategy = Lookup.INVALID // "invalid" is the most permissive match
                    };

                    try
                    {
                        client.Send(lookup);
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

                    if (candidates.Count == 0)
                    {
                        return "Address is not valid";
                    }

                    var firstCandidate = candidates[0];

                    return ("Address is valid");
                }
                else
                {
                    return base.Handle(request, userInput);
                }
            }
        }

        // Concrete class to handle API call for Carrier Routes 
        // to the SmartyStreets API
        class CarrierRoute : API_abstractHandler
        {
            public override string Handle(string request, List<string> userInput)
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
                        Street = userInput[0],
                        City = userInput[1],
                        State = userInput[2],
                        ZipCode = userInput[3],
                        MaxCandidates = 1,
                        MatchStrategy = Lookup.INVALID // "invalid" is the most permissive match
                    };

                    try
                    {
                        client.Send(lookup);
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

                    if (candidates.Count == 0)
                    {
                        return "Address is not valid";
                    }

                    var firstCandidate = candidates[0];

                    return (firstCandidate.Metadata.CarrierRoute);
                }
                else
                {
                    return base.Handle(request, userInput);
                }
            }
        }

        // Decorator Abstract Class
        // Extends API_abstractHandler class to be interchangeable with
        // its concrete decorators
        public abstract class Decorator : API_abstractHandler
        {
            public abstract override string Handle(string request, List<string> userInput);
        }

        public class add_API_Name : Decorator
        {
            private API_abstractHandler handler;

            public add_API_Name() { }


            public add_API_Name(API_abstractHandler handler)
            {
                this.handler = handler;
            }

            public override string Handle(string request, List<string> userInput)
            {
                string getResult = handler.Handle(request, userInput);
                return getResult + " (provided by SmartyStreets API)";
            }
        }

        // Client invokes chain of responsibility pattern by calling API_abstractHandler
        private class ClientRequest
        {
            public static string ClientInput(API_abstractHandler handler, string conversionType, List<string> userInput)
            {
                object result = handler.Handle(conversionType, userInput);

                if (result != null)
                {
                    return ($"{result}");
                }
                else
                {
                    return ($"{conversionType} was not utilized.\n");
                }
            }
        }
    }
}
