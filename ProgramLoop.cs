internal class ProgramLoop
{
    private List<TaskItem> _tasks;

    internal void Start()
    {
        _tasks = new List<TaskItem>();

        while (true)
        {
            ShowMenu();

            string input = Console.ReadLine();

            if (int.TryParse(input, out int actionNumber))
            {
                Menu action = (Menu)actionNumber;

                switch (action)
                {
                    case Menu.AddTask:
                        Console.Clear();
                        Console.WriteLine("Enter task name:\n");
                        string taskName = Console.ReadLine();
                        Console.Clear();

                        Console.WriteLine("Enter task description:\n");
                        string taskDescription = Console.ReadLine();
                        Console.Clear();

                        Console.WriteLine("Select category:");
                        foreach (Category category in Enum.GetValues(typeof(Category)))
                        {
                            Console.WriteLine($"{(int)category}: {category}");
                        }
                        Console.WriteLine();
                        string selectedCategoryStr = Console.ReadLine();
                        Category selectedCategory;
                        if (!Category.TryParse(selectedCategoryStr, out selectedCategory))
                        {
                            ShowMessage("Incorrect input");
                            break;
                        }
                        Console.Clear();

                        Console.WriteLine("Select proirity:");
                        foreach (Priority priority in Enum.GetValues(typeof(Priority)))
                        {
                            Console.WriteLine($"{(int)priority}: {priority}");
                        }
                        Console.WriteLine();
                        string selectedPriorityStr = Console.ReadLine();
                        Priority selectedPrioriry;
                        if (!Priority.TryParse(selectedPriorityStr, out selectedPrioriry))
                        {
                            ShowMessage("Incorrect input");
                            break;
                        }
                        Console.Clear();

                        TaskItem newTask = new TaskItem(taskName, taskDescription, selectedPrioriry, selectedCategory, Status.New);
                        _tasks.Add(newTask);
                        break;
                    case Menu.ShowTasks:
                        Console.Clear();
                        Console.WriteLine("Your tasks:");
                        ShowTasks();
                        Console.ReadLine();
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
                        Console.Clear();
                        Console.WriteLine("Select task to delete (0 to cancel):");
                        ShowTasks();
                        if (_tasks.Count != 0)
                        {
                            string selectedTask = Console.ReadLine();

                            if (int.TryParse(selectedTask, out int index) && index >= 0 && index <= _tasks.Count)
                            {
                                if (index == 0)
                                {
                                    break;
                                }

                                string name = _tasks[index - 1].Name;
                                _tasks.RemoveAt(index - 1);
                                ShowMessage($"Task \"{name}\" has been deleted");
                            }
                            else
                            {
                                ShowMessage("Incorrect input");
                            }
                        }
                        else
                        {
                            Console.ReadLine();
                        }
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

    private void ShowTasks()
    {
        if (_tasks.Count != 0)
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                TaskItem task = _tasks[i];
                Console.WriteLine($"{i+1}. {task.Name}: {task.Description} | " +
                    $"Category: {task.Category}, Priority: {task.Priority}, Status: {task.Status}");
            }
            Console.WriteLine();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("You have no task");
        }
    }
}
