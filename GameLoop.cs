public class GameLoop
{
    private Player _player;
    private Enemy _enemy;
    private List<Weapon> _weapons;
    private List<Potion> _potions;
    private Potion _activePotion;

    private int _remainingPotionTurns;

    public void Run()
    {
        string[] commands = [
            GameCommands.Attack,
            GameCommands.WeaponSpecialAbility,
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

            ShowMenu(commands);

            string input = Console.ReadLine();

            switch (input)
            {
                case GameCommands.Attack:
                    _player.Attack(_enemy);
                    ShowMessage($"You hit an enemy for {_player.Damage} damage \n" +
                        $"Enemy has {_enemy.CurrentHealth} health left");
                    Console.Clear();
                    AttackPlayer(_player);
                    NextTurn();
                    break;
                case GameCommands.WeaponSpecialAbility:
                    if (_player.EquippedWeapon == null)
                    {
                        ShowMessage("You don't have a weapon");
                    }
                    else
                    {
                        Console.Clear();
                        _player.EquippedWeapon.Use(_player);
                        AttackPlayer(_player);
                    }
                    break;
                case GameCommands.Potion:
                    if (UsePotion())
                    {
                        AttackPlayer(_player);
                        NextTurn();
                    }
                    break;
                case GameCommands.ChangeWeapon:
                    Console.Clear();
                    ShowWeapons();
                    Console.Write("Select weapon (choose equipped again to unequip): ");
                    string selectedWeapon = Console.ReadLine();

                    if (int.TryParse(selectedWeapon, out int index) && 
                        index >= 0 && index < _weapons.Count)
                    {
                        var equipable = _weapons[index];

                        if (_player.EquippedWeapon != null && _player.EquippedWeapon == equipable)
                        {
                            equipable.Unequip(_player);
                            ShowMessage($"You unequipped {equipable.Name}");
                        }
                        else
                        {
                            equipable.Equip(_player);
                            ShowMessage($"You equipped {equipable.Name} (+{equipable.DamageBonus} damage)");
                        }
                    }

                    break;
                case GameCommands.ChangePotion:
                    Console.Clear();
                    ShowPotions();
                    Console.Write("Select potion:");
                    string selectedPotion = Console.ReadLine();

                    if (int.TryParse(selectedPotion, out index) &&
                        index >= 0 && index <= _potions.Count)
                    {
                        _player.AddPotion(_potions[index]);
                        ShowMessage($"You select {_potions[index].Name}");
                    }
                    break;
                case GameCommands.Status:
                    ShowStatus();
                    break;
                case GameCommands.Exit:
                    ShowMessage("Goodbye");
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
            new Weapon("Wooden Sword", cost: 0, damageBonus: 4),
            new Weapon("Iron Sword", cost: 0, damageBonus: 5)
        };

        _potions = new List<Potion>
        {
            new Potion("Healing potion", heal: 15, damageBoost: 0, duration: 0, cost: 10),
            new Potion("Strength potion", heal: 5, damageBoost: 10, duration: 3, cost: 20)
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
        Console.WriteLine($"Weapon: {_player.EquippedWeapon?.Name ?? "none"}, " +
            $"+{_player.EquippedWeapon?.DamageBonus ?? 0} damage bonus");
        Console.WriteLine($"Damage: {_player.Damage}");
        Console.WriteLine($"Potion: {_player.Potion?.Name ?? "none"}");
        Console.WriteLine($"Balance: {_player.Balance} coins\n");

        Console.WriteLine($"Enemy: {_enemy.Name}");
        Console.WriteLine($"Health: {_enemy.CurrentHealth}/{_enemy.MaxHealth}");
        Console.WriteLine($"Damage: {_enemy.Damage}");
        Console.ReadLine();
    }

    private void ShowWeapons()
    {
        Console.WriteLine("Avaible weapons:\n");

        for (int i = 0; i < _weapons.Count; i++)
        {
            Weapon weapon = _weapons[i];
            Console.WriteLine($"{i}: {weapon.Name}, +{weapon.DamageBonus} damage");
        }
        Console.WriteLine();
    }

    private void ShowPotions()
    {
        Console.WriteLine("Avaible potions:\n");

        for (int i = 0; i < _potions.Count; i++)
        {
            Potion potion = _potions[i];
            Console.WriteLine($"{i}: {potion.Name}, heal: {potion.Heal}, " +
                $"damage boost: {potion.DamageBoost}, duration: {potion.Duration}");
        }
        Console.WriteLine();
    }

    private bool UsePotion()
    {
        if (_player.Potion == null)
        {
            ShowMessage("You have no potion");
            return false;
        }

        if (_activePotion != null)
        {
            ShowMessage("You cannot use another potion until the current one wears off");
            return false;
        }

        _remainingPotionTurns = _player.Potion.Duration;
        
        ShowMessage($"You use {_player.Potion.Name}");

        _player.Potion.Use(_player);

        if (_remainingPotionTurns > 0)
        {
            _activePotion = _player.Potion;
        }

        return true;
    }

    private void NextTurn()
    {
        if (_enemy.CurrentHealth <= 0)
        {
            _player.AddCoins(_enemy.Reward);
            ShowMessage($"Enemy died. You get {_enemy.Reward} coins.");
            SpawnNewEnemy();
        }

        if (_activePotion != null)
        {
            _remainingPotionTurns--;

            if (_remainingPotionTurns <= 0)
            {
                _player.DecreaseDamage(_activePotion.DamageBoost);
                ShowMessage($"The effect of {_activePotion.Name} has worn off");
                _activePotion = null;
            }
            else
            {
                ShowMessage($"Effect of {_activePotion.Name} active: {_remainingPotionTurns} turns remaining.");
            }
        }
    }

    private void AttackPlayer(Player player)
    {
        if (!_enemy.IsDead)
        {
            _enemy.Attack(player);
            ShowMessage($"Enemy hit you for {_enemy.Damage} damage");
            Console.Clear();
        }
    }
}
