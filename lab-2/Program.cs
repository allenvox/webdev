using System;
using System.IO;

class MarginalValera
{
    // Параметры Валеры
    public int Health { get; set; } = 100;
    public int Alcohol { get; set; } = 0;
    public int Joy { get; set; } = 0;
    public int Fatigue { get; set; } = 0;
    public int Money { get; set; } = 100;

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
        if (Alcohol < 50 && Fatigue < 10)
        {
            Joy -= 5;
            Alcohol -= 30;
            Money += 100;
            Fatigue += 70;
            Console.WriteLine("Валера пошел на работу.");
        }
        else
        {
            Console.WriteLine("Валера слишком пьян или устал для работы.");
        }
    }

    public void ContemplateNature()
    {
        Joy += 1;
        Alcohol -= 10;
        Fatigue += 10;
        Console.WriteLine("Валера созерцал природу.");
    }

    public void DrinkWineAndWatch()
    {
        Joy -= 1;
        Alcohol += 30;
        Fatigue += 10;
        Health -= 5;
        Money -= 20;
        Console.WriteLine("Валера пил вино и смотрел сериал.");
    }

    public void GoToBar()
    {
        Joy += 1;
        Alcohol += 60;
        Fatigue += 40;
        Health -= 10;
        Money -= 100;
        Console.WriteLine("Валера сходил в бар.");
    }

    public void DrinkWithMarginals()
    {
        Joy += 5;
        Health -= 80;
        Alcohol += 90;
        Fatigue += 80;
        Money -= 150;
        Console.WriteLine("Валера пил с маргиналами.");
    }

    public void SingInSubway()
    {
        Joy += 1;
        Alcohol += 10;
        Fatigue += 20;
        Money += 10;

        if (Alcohol > 40 && Alcohol < 70)
        {
            Money += 50;
            Console.WriteLine("Валера пел в метро и заработал больше денег!");
        }
        else
        {
            Console.WriteLine("Валера пел в метро.");
        }
    }

    public void Sleep()
    {
        if (Alcohol < 30)
        {
            Health += 90;
            Console.WriteLine("Валера хорошо выспался.");
        }

        if (Alcohol > 70)
        {
            Joy -= 3;
            Console.WriteLine("Валере снились кошмары.");
        }

        Alcohol -= 50;
        Fatigue -= 70;
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

class Program
{
    static void Main(string[] args)
    {
        MarginalValera valera = new MarginalValera();
        valera.StartGame();
    }
}
