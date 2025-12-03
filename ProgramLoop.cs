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
                        Console.WriteLine("Enter task name (required):\n");
                        string taskName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(taskName))
                        {
                            ShowMessage("Task name cannot be empty");
                            break;
                        }
                        Console.Clear();

                        Console.WriteLine("Enter task description (optional):\n");
                        string? taskDescription = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(taskDescription))
                        {
                            taskDescription = null;
                        }
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

                        TaskItem newTask = new TaskItem(taskName, taskDescription, selectedPrioriry, selectedCategory);
                        _tasks.Add(newTask);
                        break;
                    case Menu.ShowTasks:
                        Console.Clear();
                        Console.WriteLine("Your tasks:");
                        ShowTasks();
                        Console.ReadLine();
                        break;
                    case Menu.StatusFilter:
                        Console.Clear();
                        Console.WriteLine("Select status to filter:");
                        foreach (Status status in Enum.GetValues(typeof(Status)))
                        {
                            Console.WriteLine($"{(int)status}: {status}");
                        }
                        Console.WriteLine();
                        string statusToFilterStr = Console.ReadLine();
                        Status statusToFilter;
                        if (!Status.TryParse(statusToFilterStr, out statusToFilter))
                        {
                            ShowMessage("Incorrect input");
                            break;
                        }
                        Console.Clear();

                        ShowTasksFilteredByStatus(statusToFilter);
                        Console.ReadLine();

                        break;
                    case Menu.CategoryFilter:
                        Console.Clear();
                        Console.WriteLine("Select status to filter:");
                        foreach (Category category in Enum.GetValues(typeof(Category)))
                        {
                            Console.WriteLine($"{(int)category}: {category}");
                        }
                        Console.WriteLine();
                        string categoryToFilterStr = Console.ReadLine();
                        Category categoryToFilter;
                        if (!Category.TryParse(categoryToFilterStr, out categoryToFilter))
                        {
                            ShowMessage("Incorrect input");
                            break;
                        }
                        Console.Clear();

                        ShowTasksFilteredByCategory(categoryToFilter);
                        Console.ReadLine();

                        break;
                    case Menu.MarkAsDone:
                        Console.Clear();
                        Console.WriteLine("Select task to mark as done (0 to cancel):");
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

                                TaskItem task = _tasks[index - 1];
                                task.MarkAsDone();
                                ShowMessage($"Task \"{task.Name}\" has been marked as done");
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
                    case Menu.MarkAsInProgress:
                        Console.Clear();
                        Console.WriteLine("Select task to mark as in progress (0 to cancel):");
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

                                TaskItem task = _tasks[index - 1];
                                task.MarkAsInProgress();
                                ShowMessage($"Task \"{task.Name}\" has been marked as in progress");
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
                    case Menu.ShowStatistics:
                        ShowStatistics();
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
            _tasks = _tasks.OrderByDescending(t => t.Priority).ToList();

            for (int i = 0; i < _tasks.Count; i++)
            {
                TaskItem task = _tasks[i];

                string description = task.Description == null ? "" : $": {task.Description}";
                Console.WriteLine($"{i+1}. {task.Name}{description} | " +
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

    private void ShowTasksFilteredByStatus(Status filter)
    {
        if (_tasks.Count != 0)
        {
            _tasks = _tasks.OrderByDescending(t => t.Priority).ToList();

            for (int i = 0; i < _tasks.Count; i++)
            {
                TaskItem task = _tasks[i];
                if (task.Status == filter)
                {
                    Console.WriteLine($"{i + 1}. {task.Name}: {task.Description} | " +
                        $"Category: {task.Category}, Priority: {task.Priority}, Status: {task.Status}");
                }
            }
            Console.WriteLine();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("You have no task");
        }
    }

    private void ShowTasksFilteredByCategory(Category filter)
    {
        if (_tasks.Count != 0)
        {
            _tasks = _tasks.OrderByDescending(t => t.Category).ToList();

            for (int i = 0; i < _tasks.Count; i++)
            {
                TaskItem task = _tasks[i];
                if (task.Category == filter)
                {
                    Console.WriteLine($"{i + 1}. {task.Name}: {task.Description} | " +
                        $"Category: {task.Category}, Priority: {task.Priority}, Status: {task.Status}");
                }
            }
            Console.WriteLine();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("You have no task");
        }
    }

    private void ShowStatistics()
    {
        string statistics;

        int tasksCount = _tasks.Count;
        int newTasks = _tasks.Where(t => t.Status == Status.New).Count();
        int inProgressTasks = _tasks.Where(t => t.Status == Status.InProgress).Count();
        int doneTasks = _tasks.Where(t => t.Status == Status.Done).Count();

        statistics = $"New tasks: {newTasks} | Tasks in progress: {inProgressTasks} | Done tasks: {doneTasks}" +
            $" | All tasks: {tasksCount}";

        ShowMessage(statistics);
    }
}
