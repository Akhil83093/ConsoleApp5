using System;
using System.Collections.Generic;

namespace Task
{
   
    public class Appliance
    {
        // Constructor initializes properties of the Appliance class
           public Appliance(string name, int id)
        {
            this.Name = name;
            this.State = State.Off; // Set the default state to Off
            this.Id = id;
        }

      
        public int Id { get; private set; }

       
        public string Name { get; private set; }

        
        public State State { get; set; }
    }
}


namespace Task
{
    // Define a class named AC that inherits from Appliance
    class AC : Appliance
    {
        // Constructor initializes the AC with a specific name and ID
        public AC(int id) : base("AC", id)
        {
        }
    }
}


namespace Task
{
  
    class Bulb : Appliance
    {
       
        public Bulb(int id) : base("Bulb", id)
        {
        }
    }
}


namespace Task
{
    class Fan : Appliance
    {
        // Constructor initializes the Fan with a specific name and ID
        public Fan(int id) : base("Fan", id)
        {
        }
    }
}


namespace Task
{
    //State to represent appliance states (Off and On)
    public enum State
    {
        Off = 0,
        On = 1
    }
}


namespace TASK
{
    // Define a class named Switch
    class Switch
    {
        // Properties for SwitchId and SwitchName
        public int SwitchId { get; set; }
        public String SwitchName { get; set; }

        // Constructor initializes Switch properties
        public Switch(int switchId, string switchName)
        {
            this.SwitchId = switchId;
            this.SwitchName = switchName;
        }
    }
}


namespace Task
{
    // Define a class named Program
    public class Program
    {
        // Main method
        public static void Main()
        {
            try
            {
                // Create an instance of SwitchBoardSimulator
                SwitchBoardSimulator switchBoard = new();

                // Create and display the switchboard simulator
                switchBoard.CreateSwitchBoardSimulator();
                switchBoard.DisplaySwitchBoardSimulator();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

namespace Task
{
    // Define a class named SwitchBoardSimulator
    class SwitchBoardSimulator
    {
        // List to store instances of appliances
        private List<Appliance> ApplianceList;

        // Constructor initializes an empty list of appliances
        public SwitchBoardSimulator()
        {
            this.ApplianceList = new List<Appliance>();
        }

        // Method to create a switchboard simulator with user input for appliance counts
        public void CreateSwitchBoardSimulator()
        {
            // Read counts for Bulbs, ACs, and Fans from the user
            int bulbCount = ReadApplicanceCount("Bulbs");
            int acCount = ReadApplicanceCount("AC");
            int fanCount = ReadApplicanceCount("Fan");

            // Initialize an ID for appliances
            int applianceId = 1;

            // Create instances of Bulbs, ACs, and Fans based on user input counts
            for (int i = 0; i < bulbCount; i++)
            {
                this.ApplianceList.Add(new Bulb(applianceId++));
            }
            for (int i = 0; i < acCount; i++)
            {
                this.ApplianceList.Add(new AC(applianceId++));
            }
            for (int i = 0; i < fanCount; i++)
            {
                this.ApplianceList.Add(new Fan(applianceId++));
            }
        }

        // Method to display the switchboard simulator and handle user interactions
        public void DisplaySwitchBoardSimulator()
        {
            
            while (true)
            {
                // Display the available devices on the switchboard
                DisplayAvailableDevices();

                // Prompt the user to select a device by ID or exit
                string message = "Enter Device Id To Select  : \nEnter 0 To Exit";
                int applianceId = ReadUserInput(message);

                // Check if the user chose to exit
                if (applianceId == 0)
                {
                    Console.WriteLine("EXIT");
                    break;
                }
                else
                {
                    // Check and update the state of the selected appliance
                    CheckAndUpdateApplianceStatus(applianceId);
                }
            }
        }

        // Method to display the available devices on the switchboard
        private void DisplayAvailableDevices()
        {
            Console.WriteLine("\nDisplaying Switch Board Devices");

            // Check if there are no appliances
            if (this.ApplianceList.Count == 0)
            {
                Console.WriteLine("No Appliances Found");
                return;
            }

            // Display the name, id, and state of each appliance
            foreach (var appliance in this.ApplianceList)
            {
                Console.WriteLine(appliance.Name + " " + appliance.Id + " is \"" + appliance.State + "\"");
            }
        }

        // Method to read the count of a specific type of appliance from the user
        private int ReadApplicanceCount(string applianceName)
        {
            string message = "Enter Number Of " + applianceName + "'s : ";
            return ReadUserInput(message);
        }

        // Method to read an integer input from the user, with error handling
        private int ReadUserInput(string message)
        {
            int userInput;

            while (true)
            {
                Console.WriteLine(message);

                try

                {
                    userInput = Convert.ToInt32(Console.ReadLine());

                    // Check if the input is negative
                    if (userInput < 0)
                    {
                        Console.WriteLine("Please enter a non-negative value.");
                        continue;  // Continue the loop to re-ask for input
                    }

                    // If the input is non-negative, break out of the loop
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input. Please enter a valid integer.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Invalid Input. The entered value is too large.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }

            return userInput;
        }

        // Method to check and update the state of the selected appliance
        private void CheckAndUpdateApplianceStatus(int applianceId)
        {
            // Find the appliance with the specified ID
            var requiredAppliance = this.ApplianceList.SingleOrDefault(appliance => appliance.Id == applianceId);

            // Check if the appliance is found
            if (requiredAppliance != null)
            {
                // Display options for the selected appliance
                string message = "1. Select " + requiredAppliance.Name + " " + requiredAppliance.Id +
                                  " is \"" + requiredAppliance.State + "\"" ;

                // Read the user's choice
                int selectedOption = ReadUserInput(message);

                // Perform actions based on the user's choice
                switch (selectedOption)
                {
                    case 1:
                        UpdateDeviceStatus(requiredAppliance);
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No Appliance Found");
            }
        }

        // Method to update the state of the selected appliance (Toggle On/Off)
        private void UpdateDeviceStatus(Appliance requiredAppliance)
        {
            requiredAppliance.State = (requiredAppliance.State == State.Off) ? State.On : State.Off;
        }
    }
}
