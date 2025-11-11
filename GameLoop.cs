public class GameLoop
{
    private Player _player;
    private Enemy _enemy;
    private Random _random = new Random();
    private List<Weapon> _weapons;

    public void Run()
    {
        string[] commands = [
            GameCommands.Attack,
            GameCommands.Potion,
            GameCommands.ChangeWeapon,
            GameCommands.ChangePotion,
            GameCommands.Status,
            GameCommands.Exit
        ];

        InitializeGame();

        while (true)
        {
            if (_player.IsDead)
            {
                ShowMessage("You died. Game over.");
                break;
            }

            if (_enemy.CurrentHealth <= 0)
            {
                _player.AddCoins(_enemy.Reward);
                ShowMessage($"Enemy died. You get {_enemy.Reward} coins.");
                SpawnNewEnemy();
                continue;
            }

            ShowMenu(commands);

            string input = Console.ReadLine();

            switch (input)
            {
                case GameCommands.Attack:
                    _player.Attack(_enemy);
                    ShowMessage($"You hit an enemy for {_player.Damage} damage \n" +
                        $"Enemy has {_enemy.CurrentHealth} health left");
                    Console.Clear();
                    if (!_enemy.IsDead)
                    {
                        _enemy.Attack(_player);
                        ShowMessage($"Enemy hit you for {_enemy.Damage} damage");
                        Console.Clear();
                    }
                    break;
                case GameCommands.Potion:
                    _player.Heal(10);
                    break;
                case GameCommands.ChangeWeapon:
                    Console.Clear();
                    ShowWeapons();
                    Console.Write("Select weapon (-1 to unequip weapon): ");
                    string selectedWeapon = Console.ReadLine();

                    if (int.TryParse(selectedWeapon, out int index) && 
                        index >= -1 && index < _weapons.Count)
                    {
                        if (index == -1)
                        {
                            _player.UnequipWeapon();
                            ShowMessage($"You unequipped weapon");
                        }
                        else
                        {
                            Weapon weapon = _weapons[index];
                            weapon.Use(_player);
                            ShowMessage($"You equipped {weapon.Name} (+{weapon.DamageBonus} damage)");
                        }
                    }

                    break;
                case GameCommands.ChangePotion:

                    break;
                case GameCommands.Status:
                    ShowStatus();
                    break;
                case GameCommands.Exit:

                    return;
                default:
                    ShowMessage("Wrong command");
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

    private void InitializeGame()
    {
        SpawnPlayer();
        SpawnNewEnemy();

        _weapons = new List<Weapon>
        {
            new Weapon("Wooden Sword", 0, 4),
            new Weapon("Iron Sword", 0, 5)
        };
    }

    private void SpawnPlayer()
    {
        _player = new Player(name: "Bob the Hero", maxHealth: 50, damage: 5);
    }

    private void SpawnNewEnemy()
    {
        _enemy = new Enemy("Evil Bob", 15, 3, 10);
        ShowMessage($"New enemy has been spawned! \n " +
            $"{_enemy.Name} (HP: {_enemy.CurrentHealth}, Damage: {_enemy.Damage}, Reward: {_enemy.Reward} coins)");
    }

    private void ShowMenu(string[] commands)
    {
        Console.Clear();

        Console.WriteLine("Enter the command:\n");

        foreach (var command in commands)
        {
            Console.WriteLine(command);
        }
        Console.WriteLine();
    }

    private void ShowStatus()
    {
        Console.Clear();

        Console.WriteLine($"Player: {_player.Name}");
        Console.WriteLine($"Health: {_player.CurrentHealth}/{_player.MaxHealth}");
        Console.WriteLine($"Weapon: {_player.EquippedWeapon?.Name ?? "none"}");
        Console.WriteLine($"Damage: {_player.Damage} + {_player.EquippedWeapon?.DamageBonus ?? 0} bonus");
        Console.WriteLine($"Balance: {_player.Balance} coins\n");

        Console.WriteLine($"Enemy: {_enemy.Name}");
        Console.WriteLine($"Health: {_enemy.CurrentHealth}/{_enemy.MaxHealth}");
        Console.WriteLine($"Damage: {_enemy.Damage}");
        Console.ReadLine();
    }

    private void ShowWeapons()
    {
        Console.WriteLine("Avaible weapon:\n");

        for (int i = 0; i < _weapons.Count; i++)
        {
            Weapon weapon = _weapons[i];
            Console.WriteLine($"{i}: {weapon.Name}, +{weapon.DamageBonus} damage");
        }
        Console.WriteLine();
    }
}
