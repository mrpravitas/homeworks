public class GameLoop
{
    public void Run()
    {
        string input;

        string[] commands = [
            GameCommands.Attack,
            GameCommands.Potion,
            GameCommands.ChangeWeapon,
            GameCommands.ChangePotion,
            GameCommands.Status,
            GameCommands.Exit
        ];

        while (true)
        {
            Console.Clear();

            Console.WriteLine("Enter the command:\n");

            foreach (var command in commands)
            {
                Console.WriteLine(command);
            }
            Console.WriteLine();

            input = Console.ReadLine();

            switch (input)
            {
                case GameCommands.Attack:
                    ShowMessage("You attacked the enemy");
                    break;
                case GameCommands.Potion:
                    ShowMessage("You use the potion");
                    break;
                case GameCommands.ChangeWeapon:
                    ShowMessage("You changed the weapon");
                    break;
                case GameCommands.ChangePotion:
                    ShowMessage("You changed the potion");
                    break;
                case GameCommands.Status:
                    ShowMessage("Your stats will be here");
                    break;
                case GameCommands.Exit:
                    Console.Clear();
                    return;
                default:
                    ShowMessage("Wrong Command");
                    break;
            }
        }
    }

    private void ShowMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Console.ReadLine();
    }
}
