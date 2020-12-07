using System;
using System.Threading;

namespace SMG_Test
{
  public class Game
  {
    private static Random _rand = new Random();

    public static PlayerType SelectFirstPlayer()
    {
      return _rand.NextDouble() < 0.5 ? PlayerType.Human : PlayerType.Computer;
    }

    public static Move GetPlayerMove()
    {
      Console.WriteLine("Player, choose your move:\n");

      PrintMoveOptions();

      return (Move)(int.Parse(Console.ReadLine()) - 1);
    }

    public static Move GetComputerMove()
    {
      return (Move)_rand.Next(0, 3);
    }

    public static PlayerResult CalculateResult(Move playerMove, Move computerMove)
    {
      if (playerMove == computerMove)
        return PlayerResult.Tie;

      // test all possible player wins
      if (playerMove == Move.Rock && computerMove == Move.Scissors)
      {
        return PlayerResult.Win;
      }
      if (playerMove == Move.Paper && computerMove == Move.Rock)
      {
        return PlayerResult.Win;

      }
      if (playerMove == Move.Scissors && computerMove == Move.Paper)
      {
        return PlayerResult.Win;
      }

      // not tie nor player win implies computer won
      return PlayerResult.Lose;
    }

    private static void PrintMoveOptions()
    {
      string composite = "{0,-20} ({1})";
      Console.WriteLine(string.Format(composite, "Rock", 1));
      Console.WriteLine(string.Format(composite, "Paper", 2));
      Console.WriteLine(string.Format(composite, "Scissors", 3));
      Console.WriteLine();
    }

  }
}