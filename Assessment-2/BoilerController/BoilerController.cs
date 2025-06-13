namespace BoilerControllerApplication
{
    internal class BoilerController
    {
        const string EventLogPath = "BoilerLog.txt";
        public string BoilerStatus { get; set; } = "Lockout";
        public string InterlockSwitchStatus { get; set; } = "Open";
        public List<string> EventLog { get; set; } = Helper.CreateOrLoadErrorLog(EventLogPath);

        public BoilerController()
        {
            File.AppendAllText(EventLogPath, Environment.NewLine+"TimeStamp            Event                  Event Data" + Environment.NewLine);
            LogEvent("Boiler Initialized");
            Helper.WriteInGreen("Boiler initialized successfully");
        }
        /// <summary>
        /// Function to log an event to the EventLog list.
        /// </summary>
        /// <param name="eventDescription"></param>
        /// <param name="eventData"></param>
        private void LogEvent(string eventDescription, string eventData = "")
        {
            string timestamp = DateTime.Now.ToString();
            string logEntry = $"{timestamp}, {eventDescription}, {eventData}";
            File.AppendAllText(EventLogPath,logEntry+ Environment.NewLine);
            EventLog.Add(logEntry);
        }
        /// <summary>
        /// Function to check the conditions and start the boiler.
        /// </summary>
        public void StartBoilerSequence()
        {
            if (BoilerStatus.Equals("Ready"))
            {
                Helper.WriteInYellow("Pre-Purge Phase started...");
                BoilerStatus = "Pre-Purge";
                CountDown(10);
                Helper.WriteInGreen("\nPre-Purging Completed");
                LogEvent("Pre-Purge completed");

                Helper.WriteInYellow("Ignition Phase started...");
                BoilerStatus = "Ignition";
                CountDown(10);
                Helper.WriteInGreen("\nIgnition Completed");
                LogEvent("Ignition phase completed");

                Helper.WriteInGreen("\nBoiler is now operational");
                BoilerStatus = "Operational";
                LogEvent("Boiler now operational");
            }
            else
            {
                Helper.WriteInRed("Error: Boiler is not ready to start.");
                PrintCurrentStatus();
                Console.WriteLine("Action Required: Reset Lockout");
                LogEvent($"Error: Boiler start failed - Not ready, System in {BoilerStatus}");
            }
        }
        /// <summary>
        /// Function to check and stop the boiler sequence.
        /// </summary>
        public void StopBoilerSequence()
        {
            if (BoilerStatus == "Operational")
            {
                BoilerStatus = "Lockout";
                Helper.WriteInGreen("Boiler is stopped and locked out.");
                LogEvent("Boiler stopped. System in Lockout");
            }
            else
            {
               Helper.WriteInRed("Error: Boiler is not operational.");
               PrintCurrentStatus();
               LogEvent("Error: Boiler stop failed - Not ready", "System in Lockout");
            }
        }
        /// <summary>
        /// Function to check and reset boiler status to ready.
        /// </summary>
        public void ResetLockout()
        {
            if (InterlockSwitchStatus == "Closed")
            {
                BoilerStatus = "Ready";
                Helper.WriteInGreen("Boiler is now ready");
                LogEvent("Boiler Status changed to Ready");
            }
            else
            {
                Helper.WriteInRed("Error: Interlock switch must be in 'Closed' position.");
                PrintCurrentStatus();
                Console.WriteLine("Action Required: Toggle Interlock Switch");
                LogEvent("Error: Boiler stop failed - Not ready", "InterlockSwitch is Open");
            }
        }
        /// <summary>
        /// Function to toggle the interlock switch between open and closed states.
        /// </summary>
        public void ToggleInterlockSwitch()
        {
            InterlockSwitchStatus = (InterlockSwitchStatus == "Open") ? "Closed" : "Open";
            Helper.WriteInYellow("Interlock Switch toggled to");
            if (InterlockSwitchStatus.Equals("Open"))
            {
                Helper.WriteInGreen("Open");
                if(!BoilerStatus.Equals("Lockout"))
                {
                    BoilerStatus = "Lockout";
                    LogEvent("Boiler stopped due to interlock. System in Lockout");
                }
            }
            else
                Helper.WriteInRed("Closed");
            LogEvent("Interlock Switch toggled to", InterlockSwitchStatus);
        }
       
        /// <summary>
        /// Function to simulate a boiler error if the system is operational.
        /// </summary>
        public void SimulateBoilerError()
        {
            if (BoilerStatus == "Operational")
            {
                BoilerStatus = "Lockout";
                Helper.WriteInYellow("Simulating Error");
                CountDown(3);
                Helper.WriteInRed("Error: Simulated failure. System in Lockout.");
                LogEvent("Error: Simulated boiler error", "System in Lockout");
            }
            else
            {
                Helper.WriteInRed("Error: Boiler must be operational to simulate failure.");
                PrintCurrentStatus();
                LogEvent("Error: Boiler error simulation failed - Not ready", "System in lockout");
            }
        }
        /// <summary>
        /// Function to display EventLogs.
        /// </summary>
        public void ViewEventLog()
        {
            Console.WriteLine("Event Log:");
            foreach (var logEntry in EventLog)
            {
                Console.WriteLine(logEntry);
            }
        }
        /// <summary>
        /// Function to perform countdown.
        /// </summary>
        /// <param name="timeInSeconds"></param>
        public void CountDown(int timeInSeconds)
        {
            for (int i = timeInSeconds-1; i >=0; i--)
            {
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write(i);
                Console.ResetColor();
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }
        /// <summary>
        /// Function to print current boiler and interlock switch status.
        /// </summary>
        public void PrintCurrentStatus()
        {
            Helper.WriteInYellow($"\nBoiler Status :");
            if (BoilerStatus.Equals("Lockout"))
                Helper.WriteInRed(BoilerStatus);
            else
                Helper.WriteInGreen(BoilerStatus);
            Helper.WriteInYellow($"Interlock Switch Status :");
            if(InterlockSwitchStatus.Equals("Open"))
                Helper.WriteInGreen(InterlockSwitchStatus);
            else
                Helper.WriteInRed(InterlockSwitchStatus);
        }
    }
}
