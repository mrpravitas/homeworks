internal class ProgramLoop
{
    private List<TaskItem> _tasks;

    internal void Start()
    {
        while (true)
        {
            ShowMenu();

            string input = Console.ReadLine();

            if (int.TryParse(input, out int actionNumber))
            {
                Menu action = (Menu)actionNumber;

                switch (action)
                {
                    case Menu.AddTaks:
                        ShowMessage("Task added");
                        break;
                    case Menu.ShowTasks:
                        ShowMessage("Your tasks:");
                        break;
                    case Menu.StatusFilter:
                        ShowMessage("Your tasks filtered by status");
                        break;
                    case Menu.PriorityFilter:
                        ShowMessage("Your tasks filtered by priority");
                        break;
                    case Menu.CategoryFilter:
                        ShowMessage("Your tasks filtered by category");
                        break;
                    case Menu.MarkAsDone:
                        ShowMessage("Mark task as done:");
                        break;
                    case Menu.DeleteTask:
                        ShowMessage("Select task to delete:");
                        break;
                    case Menu.Exit:
                        ShowMessage("Goodbye");
                        return;
                    default:
                        ShowMessage("Wrong input");
                        break;
                }
            }
            else
            {
                ShowMessage("Wrong input");
            }
        }
    }

    private void ShowMenu()
    {
        Console.Clear();

        foreach (var action in Enum.GetValues(typeof(Menu)))
        {
            Console.WriteLine($"{(int)action}: {action}");
        }
        Console.WriteLine();

        Console.Write("Select action: ");
    }

    private void ShowMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Console.ReadLine();
    }
}
