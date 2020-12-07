using System;
using System.Threading;

namespace SMG_Test
{
  class Program
  {
    static void Main(string[] args)
    {
      Move playerMove;
      Move computerMove;
      bool playerGoesFirst = Game.SelectFirstPlayer() == PlayerType.Human;

      Console.WriteLine("---Rock Paper Scissors---\n");

      if (playerGoesFirst)
      {
        Console.WriteLine("Player goes first!");

        playerMove = Game.GetPlayerMove();
        Console.WriteLine($"\nPlayer chose {playerMove}\n");

        computerMove = Game.GetComputerMove();
        Console.WriteLine($"Computer chose {computerMove}\n");
      }
      else
      {
        Console.WriteLine("Computer goes first!");

        computerMove = Game.GetComputerMove();
        System.Console.WriteLine("Computer is deciding...\n");
        Thread.Sleep(250);

        playerMove = Game.GetPlayerMove();
        Console.WriteLine($"Player chose {playerMove}\n");
        Console.WriteLine($"Computer chose {computerMove}\n");
      }

      // decide round winner
      PlayerResult result = Game.CalculateResult(playerMove, computerMove);
      if (result == PlayerResult.Tie)
        System.Console.WriteLine("Tie!");
      else if (result == PlayerResult.Win)
        System.Console.WriteLine("Player wins!");
      else
        System.Console.WriteLine("Computer wins!");
    }
  }
}
