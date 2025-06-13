using Spectre.Console;
namespace BoilerControllerApplication
{
    internal class UserInterface
    {
        public static void Main(string[] args)
        {
            BoilerController boilerController = new();
            bool isExit = false;
            while (!isExit)
            {
                string choice = AnsiConsole.Prompt(
                       new SelectionPrompt<string>()
                           .Title("Boiler Controller Application")
                           .PageSize(10)
                           .AddChoices(new[] {
                        "Start Boiler Sequence",
                        "Stop Boiler Sequence",
                        "Simulate Boiler Error",
                        "Toggle Run Interlock Switch",
                        "Reset Lockout",
                        "View Event Log",
                        "Exit Application"
                           }));

                switch (choice)
                {
                    case "Start Boiler Sequence":
                        AnsiConsole.MarkupLine("[blue]Starting Boiler Sequence[/]");
                        boilerController.StartBoilerSequence();
                        break;
                    case "Stop Boiler Sequence":
                        AnsiConsole.MarkupLine("[blue]Stopping Boiler Sequence[/]");
                        boilerController.StopBoilerSequence();
                        break;
                    case "Simulate Boiler Error":
                        AnsiConsole.MarkupLine("[blue]Simulating Boiler Error[/]");
                        boilerController.SimulateBoilerError();
                        break;
                    case "Toggle Run Interlock Switch":
                        AnsiConsole.MarkupLine("[blue]Toggling InterLock Switch[/]");
                        boilerController.ToggleInterlockSwitch();
                        break;
                    case "Reset Lockout":
                        AnsiConsole.MarkupLine("[blue]Reseting Lockout[/]");
                        boilerController.ResetLockout();
                        break;
                    case "View Event Log":
                        AnsiConsole.MarkupLine("[blue]Viewing Eventlog[/]");
                        boilerController.ViewEventLog();
                        break;
                    case "Exit Application":
                        isExit = true;
                        AnsiConsole.MarkupLine("[bold]Exiting...[/]");
                        break;
                }
                AnsiConsole.WriteLine();
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}

