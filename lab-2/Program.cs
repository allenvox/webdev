using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

class MarginalValera
{
    // Параметры Валеры
    public int Health { get; set; }
    public int Alcohol { get; set; }
    public int Joy { get; set; }
    public int Fatigue { get; set; }
    public int Money { get; set; }

    // Конфигурация игры
    private GameConfig config;

    // Загрузка конфигурации из JSON
    public void LoadConfig(string configFilePath)
    {
        if (File.Exists(configFilePath))
        {
            string json = File.ReadAllText(configFilePath);
            config = JsonSerializer.Deserialize<GameConfig>(json);

            // Инициализируем параметры Валеры из конфигурации
            Health = config.InitialParameters.Health;
            Alcohol = config.InitialParameters.Alcohol;
            Joy = config.InitialParameters.Joy;
            Fatigue = config.InitialParameters.Fatigue;
            Money = config.InitialParameters.Money;

            Console.WriteLine("Конфигурация игры загружена.");
        }
        else
        {
            Console.WriteLine("Файл конфигурации не найден.");
            Environment.Exit(1);
        }
    }

    // Бесконечный цикл игры
    public void StartGame()
    {
        while (true)
        {
            Console.WriteLine($"Здоровье: {Health}, Алкоголь: {Alcohol}, Жизнерадостность: {Joy}, Усталость: {Fatigue}, Деньги: {Money}");
            Console.WriteLine("Выберите действие: 1. Пойти на работу, 2. Созерцать природу, 3. Пить вино, 4. Сходить в бар, 5. Выпить с маргиналами, 6. Петь в метро, 7. Спать, 8. Сохранить, 9. Загрузить");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GoToWork();
                    break;
                case "2":
                    ContemplateNature();
                    break;
                case "3":
                    DrinkWineAndWatch();
                    break;
                case "4":
                    GoToBar();
                    break;
                case "5":
                    DrinkWithMarginals();
                    break;
                case "6":
                    SingInSubway();
                    break;
                case "7":
                    Sleep();
                    break;
                case "8":
                    SaveGame();
                    break;
                case "9":
                    LoadGame();
                    break;
                default:
                    Console.WriteLine("Неизвестное действие");
                    break;
            }

            if (Health <= 0)
            {
                Console.WriteLine("Валера умер. Игра окончена.");
                break;
            }
        }
    }

    // Действия
    public void GoToWork()
    {
        if (Alcohol < config.Actions.GoToWork.RequiredAlcohol && Fatigue < config.Actions.GoToWork.RequiredFatigue)
        {
            Joy += config.Actions.GoToWork.JoyChange;
            Alcohol += config.Actions.GoToWork.AlcoholChange;
            Money += config.Actions.GoToWork.MoneyChange;
            Fatigue += config.Actions.GoToWork.FatigueChange;
            Console.WriteLine("Валера пошел на работу.");
        }
        else
        {
            Console.WriteLine("Валера слишком пьян или устал для работы.");
        }
    }

    public void ContemplateNature()
    {
        Joy += config.Actions.ContemplateNature.JoyChange;
        Alcohol += config.Actions.ContemplateNature.AlcoholChange;
        Fatigue += config.Actions.ContemplateNature.FatigueChange;
        Console.WriteLine("Валера созерцал природу.");
    }

    public void DrinkWineAndWatch()
    {
        Joy += config.Actions.DrinkWineAndWatch.JoyChange;
        Alcohol += config.Actions.DrinkWineAndWatch.AlcoholChange;
        Fatigue += config.Actions.DrinkWineAndWatch.FatigueChange;
        Health += config.Actions.DrinkWineAndWatch.HealthChange;
        Money += config.Actions.DrinkWineAndWatch.MoneyChange;
        Console.WriteLine("Валера пил вино и смотрел сериал.");
    }

    public void GoToBar()
    {
        Joy += config.Actions.GoToBar.JoyChange;
        Alcohol += config.Actions.GoToBar.AlcoholChange;
        Fatigue += config.Actions.GoToBar.FatigueChange;
        Health += config.Actions.GoToBar.HealthChange;
        Money += config.Actions.GoToBar.MoneyChange;
        Console.WriteLine("Валера сходил в бар.");
    }

    public void DrinkWithMarginals()
    {
        Joy += config.Actions.DrinkWithMarginals.JoyChange;
        Health += config.Actions.DrinkWithMarginals.HealthChange;
        Alcohol += config.Actions.DrinkWithMarginals.AlcoholChange;
        Fatigue += config.Actions.DrinkWithMarginals.FatigueChange;
        Money += config.Actions.DrinkWithMarginals.MoneyChange;
        Console.WriteLine("Валера пил с маргиналами.");
    }

    public void SingInSubway()
    {
        Joy += config.Actions.SingInSubway.JoyChange;
        Alcohol += config.Actions.SingInSubway.AlcoholChange;
        Fatigue += config.Actions.SingInSubway.FatigueChange;
        Money += config.Actions.SingInSubway.MoneyChange;

        if (Alcohol > config.Actions.SingInSubway.BonusAlcoholMin && Alcohol < config.Actions.SingInSubway.BonusAlcoholMax)
        {
            Money += config.Actions.SingInSubway.BonusMoney;
            Console.WriteLine("Валера пел в метро и заработал больше денег!");
        }
        else
        {
            Console.WriteLine("Валера пел в метро.");
        }
    }

    public void Sleep()
    {
        if (Alcohol < config.Actions.Sleep.BadDreamsAlcoholLimit)
        {
            Health += config.Actions.Sleep.HealthGain;
            Console.WriteLine("Валера хорошо выспался.");
        }

        if (Alcohol > config.Actions.Sleep.BadDreamsAlcoholLimit)
        {
            Joy -= config.Actions.Sleep.BadDreamsJoyPenalty;
            Console.WriteLine("Валере снились кошмары.");
        }

        Alcohol += config.Actions.Sleep.AlcoholReduction;
        Fatigue += config.Actions.Sleep.FatigueReduction;
    }

    // Сохранение игры в файл
    public void SaveGame()
    {
        using (StreamWriter writer = new StreamWriter("savegame.txt"))
        {
            writer.WriteLine(Health);
            writer.WriteLine(Alcohol);
            writer.WriteLine(Joy);
            writer.WriteLine(Fatigue);
            writer.WriteLine(Money);
        }
        Console.WriteLine("Игра сохранена.");
    }

    // Загрузка игры из файла
    public void LoadGame()
    {
        if (File.Exists("savegame.txt"))
        {
            using (StreamReader reader = new StreamReader("savegame.txt"))
            {
                Health = int.Parse(reader.ReadLine());
                Alcohol = int.Parse(reader.ReadLine());
                Joy = int.Parse(reader.ReadLine());
                Fatigue = int.Parse(reader.ReadLine());
                Money = int.Parse(reader.ReadLine());
            }
            Console.WriteLine("Игра загружена.");
        }
        else
        {
            Console.WriteLine("Файл сохранения не найден.");
        }
    }
}

// Модель конфигурации
public class GameConfig
{
    public InitialParameters InitialParameters { get; set; }
    public Actions Actions { get; set; }
}

public class InitialParameters
{
    public int Health { get; set; }
    public int Alcohol { get; set; }
    public int Joy { get; set; }
    public int Fatigue { get; set; }
    public int Money { get; set; }
}

public class Actions
{
    public ActionConfig GoToWork { get; set; }
    public ActionConfig ContemplateNature { get; set; }
    public ActionConfig DrinkWineAndWatch { get; set; }
    public ActionConfig GoToBar { get; set; }
    public ActionConfig DrinkWithMarginals { get; set; }
    public SingInSubwayConfig SingInSubway { get; set; }
    public SleepConfig Sleep { get; set; }
}

public class ActionConfig
{
    public int RequiredAlcohol { get; set; } // Минимальный уровень алкоголя для выполнения действия
    public int RequiredFatigue { get; set; } // Минимальный уровень усталости для выполнения действия
    public int JoyChange { get; set; }
    public int AlcoholChange { get; set; }
    public int FatigueChange { get; set; }
    public int MoneyChange { get; set; }
    public int HealthChange { get; set; }
}

public class SingInSubwayConfig : ActionConfig
{
    public int BonusAlcoholMin { get; set; }
    public int BonusAlcoholMax { get; set; }
    public int BonusMoney { get; set; }
}

public class SleepConfig
{
    public int HealthGain { get; set; }
    public int BadDreamsAlcoholLimit { get; set; }
    public int BadDreamsJoyPenalty { get; set; }
    public int AlcoholReduction { get; set; }
    public int FatigueReduction { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        MarginalValera valera = new MarginalValera();
        valera.LoadConfig("./config.json");
        valera.StartGame();
    }
}
