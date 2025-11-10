internal class Program
{
    static void Main(string[] args)
    {
        const string attackCommand = "Attack";
        const string potionCommand = "Use potion";
        const string changeWeaponCommand = "Change weapon";
        const string changePotionCommand = "Change potion";
        const string statusCommand = "Status";
        const string ExitCommand = "Exit";

        string input;

        while (true)
        {
            Console.Clear();

            Console.WriteLine("Enter the command:");
            Console.Write("\n");
            Console.WriteLine(attackCommand);
            Console.WriteLine(potionCommand);
            Console.WriteLine(changeWeaponCommand);
            Console.WriteLine(changePotionCommand);
            Console.WriteLine(statusCommand);
            Console.WriteLine(ExitCommand);
            Console.WriteLine("\n");

            input = Console.ReadLine();

            switch (input)
            {
                case attackCommand:
                    Console.Clear();
                    Console.WriteLine("You attacked the enemy");
                    Console.ReadLine();
                    break;
                case potionCommand:
                    Console.Clear();
                    Console.WriteLine("You use the potion");
                    Console.ReadLine();
                    break;
                case changeWeaponCommand:
                    Console.Clear();
                    Console.WriteLine("You changed the weapon");
                    Console.ReadLine();
                    break;
                case changePotionCommand:
                    Console.Clear();
                    Console.WriteLine("You changed the potion");
                    Console.ReadLine();
                    break;
                case statusCommand:
                    Console.Clear();
                    Console.WriteLine("Your statistics will be here");
                    Console.ReadLine();
                    break;
                case ExitCommand:
                    Console.Clear();
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong Command");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
