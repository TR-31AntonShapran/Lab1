using System;
using System.Collections.Generic;
using System.Text;

public class Game
{
    public string OpponentName { get; set; }
    public int Rating { get; set; }
    public bool IsWin { get; set; }
    public int GameIndex { get; set; }

    public Game(string opponentName, int rating, bool isWin, int gameIndex)
    {
        OpponentName = opponentName;
        Rating = rating;
        IsWin = isWin;
        GameIndex = gameIndex;
    }
}

public class GameAccount
{
    public string UserName { get; private set; }
    public int CurrentRating { get; private set; }
    public int GamesCount { get; private set; }
    private List<Game> gameHistory;

    public GameAccount(string userName)
    {
        UserName = userName;
        CurrentRating = 1;
        GamesCount = 0;
        gameHistory = new List<Game>();
    }

    public void WinGame(string opponentName, int rating)
    {
        if (rating < 0)
        {
            throw new ArgumentException("Ранг не може бути від'ємний.");
        }

        CurrentRating += rating;
        GamesCount++;
        gameHistory.Add(new Game(opponentName, rating, true, GamesCount));
    }

    public void LoseGame(string opponentName, int rating)
    {
        if (rating < 0)
        {
            throw new ArgumentException("Ранг не може бути від'ємний.");
        }

        CurrentRating -= rating;
        if (CurrentRating < 1)
        {
            CurrentRating = 1;
        }

        GamesCount++;
        gameHistory.Add(new Game(opponentName, rating, false, GamesCount));
    }

    public void GetStats()
    {
        Console.WriteLine($"Статистика для {UserName}:");
        foreach (var game in gameHistory)
        {
            string result = game.IsWin ? "Перемога" : "Поразка";
            Console.WriteLine($"Гра {game.GameIndex}: Опонент: {game.OpponentName}, Результат: {result}, Рейтинг: {game.Rating}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        GameAccount player1 = new GameAccount("Гравець1");
        GameAccount player2 = new GameAccount("Гравець2");

        player1.WinGame("Гравець2", 10);
        player2.LoseGame("Гравець1", 10);

        player1.LoseGame("Гравець2", 5);
        player2.WinGame("Гравець1", 5);

        player1.GetStats();
        player2.GetStats();
    }
}
