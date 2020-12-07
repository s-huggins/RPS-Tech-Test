using System;
using System.Linq;
using System.Threading;

namespace SMG_Test
{
  public class Game
  {
    private static Random _rand = new Random();

    public static void Run()
    {

      bool continueGame = true;

      Console.WriteLine("---Rock Paper Scissors---\n");

      while (continueGame)
      {
        PlayRound();

        System.Console.WriteLine("\nWould you like to play again? (y/n)");
        continueGame = System.Console.ReadLine().ToLowerInvariant()[0] == 'y';
      }
    }

    private static void PlayRound()
    {
      Move playerMove;
      Move computerMove;
      bool playerGoesFirst = SelectFirstPlayer() == PlayerType.Human;

      if (playerGoesFirst)
      {
        Console.WriteLine("Player goes first!");

        playerMove = GetPlayerMove();
        Console.WriteLine($"\nPlayer chose {playerMove}\n");

        computerMove = GetComputerMove();
        Console.WriteLine($"Computer chose {computerMove}\n");
      }
      else
      {
        Console.WriteLine("Computer goes first!");

        computerMove = GetComputerMove();
        System.Console.WriteLine("Computer is deciding...\n");
        Thread.Sleep(250);

        playerMove = GetPlayerMove();
        Console.WriteLine($"Player chose {playerMove}\n");
        Console.WriteLine($"Computer chose {computerMove}\n");
      }

      // decide round winner
      GameResult result = CalculateResult(playerMove, computerMove);
      if (result == GameResult.Tie)
        System.Console.WriteLine("Tie!");
      else if (result == GameResult.PlayerWin)
        System.Console.WriteLine("Player wins!");
      else
        System.Console.WriteLine("Computer wins!");
    }

    private static PlayerType SelectFirstPlayer()
    {
      return _rand.NextDouble() < 0.5 ? PlayerType.Human : PlayerType.Computer;
    }

    private static Move GetPlayerMove()
    {
      Console.WriteLine("Player, choose your move:\n");

      int playerInput;

      PrintMoveOptions();
      while (!int.TryParse(Console.ReadLine(), out playerInput) || !IsValidMove(playerInput))
      {
        System.Console.WriteLine("\nThat was not a valid move. Please try again!");
        PrintMoveOptions();
      }

      return (Move)(playerInput - 1);
    }

    private static Move GetComputerMove()
    {
      return (Move)_rand.Next(0, 3);
    }

    private static GameResult CalculateResult(Move playerMove, Move computerMove)
    {
      if (playerMove == computerMove)
        return GameResult.Tie;

      // test all possible player wins
      if (playerMove == Move.Rock && computerMove == Move.Scissors)
      {
        return GameResult.PlayerWin;
      }
      if (playerMove == Move.Paper && computerMove == Move.Rock)
      {
        return GameResult.PlayerWin;

      }
      if (playerMove == Move.Scissors && computerMove == Move.Paper)
      {
        return GameResult.PlayerWin;
      }

      // not tie nor player win implies computer won
      return GameResult.PlayerLose;
    }

    private static void PrintMoveOptions()
    {
      string composite = "{0,-20} ({1})";
      Console.WriteLine(string.Format(composite, "Rock", 1));
      Console.WriteLine(string.Format(composite, "Paper", 2));
      Console.WriteLine(string.Format(composite, "Scissors", 3));
      Console.WriteLine();
    }

    private static bool IsValidMove(int moveVal)
    {
      return new int[] { 1, 2, 3 }.Contains(moveVal);
    }

  }
}