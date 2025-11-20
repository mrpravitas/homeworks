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
        InitializeGame();

        while (true)
        {
            if (_player.IsDead)
            {
                ShowMessage("You died. Game over.");
                break;
            }

            ShowMenu();

            string input = Console.ReadLine();

            if (int.TryParse(input, out int commandNumber))
            {
                GameCommands command = (GameCommands)commandNumber;

                switch (command)
                {
                    case GameCommands.Attack:
                        ShowMessage($"You hit an enemy for {_player.Damage} damage");
                        _player.Attack(_enemy);
                        ShowMessage( $"Enemy has {_enemy.CurrentHealth} health left");
                        Console.Clear();
                        AttackPlayer(_player);
                        NextTurn();
                        break;
                    case GameCommands.UseSuperPower:
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
                    case GameCommands.UsePotion:
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
                            var weapon = _weapons[index];

                            if (!weapon.IsPurchased)
                            {
                                if (weapon.Buy(_player))
                                {
                                    ShowMessage($"You bought {weapon.Name} for {weapon.Cost} coins");
                                }
                                else
                                {
                                    ShowMessage("No enough money");
                                }
                            }
                            else
                            {
                                if (_player.EquippedWeapon != null && _player.EquippedWeapon == weapon)
                                {
                                    weapon.Unequip(_player);
                                    ShowMessage($"You unequipped {weapon.Name}");
                                }
                                else
                                {
                                    weapon.Equip(_player);
                                    ShowMessage($"You equipped {weapon.Name} (+{weapon.DamageBonus} damage)");
                                }
                            }
                        }

                        break;
                    case GameCommands.BuyPotion:
                        Console.Clear();
                        ShowPotions();
                        Console.Write("Select potion:");
                        string selectedPotion = Console.ReadLine();

                        if (int.TryParse(selectedPotion, out index) &&
                            index >= 0 && index < _potions.Count)
                        {
                            if (_player.Potion != null)
                            {
                                ShowMessage($"You already have a potion");
                            }
                            else
                            {
                                if (_player.SpendCoins(_potions[index].Cost))
                                {
                                    _player.AddPotion(_potions[index]);
                                    ShowMessage($"You bought {_potions[index].Name}");
                                }
                                else
                                {
                                    ShowMessage("You don't have enough coins");
                                }
                            }
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
            else
            {
                ShowMessage("Wrong command");
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
            new Weapon("Wooden Sword", cost: 0, damageBonus: 3),
            new Weapon("Iron Sword", cost: 20, damageBonus: 5)
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
        Random random = new Random();
        int enemyType = random.Next(3);

        switch (enemyType)
        {
            case 0:
                _enemy = new Enemy(
                    "Evil Bob",
                    maxHealth: random.Next(12, 18),
                    damage: random.Next(3, 5), 
                    reward: random.Next(7, 10)
                );

                ShowMessage($"New enemy has been spawned! \n " +
                    $"{_enemy.Name} (HP: {_enemy.CurrentHealth}, Damage: {_enemy.Damage}," +
                    $" Reward: {_enemy.Reward} coins)");

                break;
            case 1:
                _enemy = new Assassin(
                    "Assassin Bob",
                    maxHealth: random.Next(9, 12),
                    damage: random.Next(6, 8), 
                    reward: random.Next(11, 16),
                    dodgeChance: 0.2f
                );

                ShowMessage($"New enemy has been spawned! \n " +
                    $"{_enemy.Name} (HP: {_enemy.CurrentHealth}, Damage: {_enemy.Damage}," +
                    $" Reward: {_enemy.Reward} coins)");

                ShowMessage("This enemy has a chance to dodge attack!");

                break;
            case 2:
                _enemy = new Tank(
                    "Big Bob",
                    maxHealth: random.Next(20, 25),
                    damage: random.Next(1, 3),
                    reward: random.Next(11, 16),
                    defence: random.Next(2, 4)
                    );

                ShowMessage($"New enemy has been spawned! \n " +
                    $"{_enemy.Name} (HP: {_enemy.CurrentHealth}, Damage: {_enemy.Damage}," +
                    $" Reward: {_enemy.Reward} coins)");

                ShowMessage("This enemy reduces incoming damage!");

                break;
        }
    }

    private void ShowMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter the command:\n");

        foreach (GameCommands command in Enum.GetValues(typeof(GameCommands)))
        {
            Console.WriteLine($"{(int)command}: {command}");
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
            string costInfo = weapon.IsPurchased ? "" : $", cost: {weapon.Cost} coins";
            Console.WriteLine($"{i}: {weapon.Name}, +{weapon.DamageBonus} damage{costInfo}");
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
                $"damage boost: {potion.DamageBoost}, duration: {potion.Duration}, " +
                $"price: {potion.Cost} coins" );
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

        if (_remainingPotionTurns > 0)
        {
            _activePotion = _player.Potion;
        }

        _player.Potion.Use(_player);

        ShowMessage($"Now your health is {_player.CurrentHealth} and your damage is {_player.Damage}");

        return true;
    }

    private void NextTurn()
    {
        if (_enemy.IsDead)
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
