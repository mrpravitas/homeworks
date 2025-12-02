internal class TaskItem
{
    private string _name;
    private string _description;
    private Priority _priority;
    private Category _category;
    private Status _status;

    public TaskItem(string name, string description, Priority priority, Category category)
    {
        _name = name;
        _description = description;
        _priority = priority;
        _category = category;
        _status = Status.New;
    }

    public string Name => _name;
    public string Description => _description;
    public Priority Priority => _priority;
    public Category Category => _category;
    public Status Status => _status;

    public void MarkAsDone()
    {
        _status = Status.Done;
    }
}
