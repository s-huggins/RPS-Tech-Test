using System;

namespace SMG_Test
{
  class Program
  {
    static void Main(string[] args)
    {
      var rand = new Random();
      Move playerMove;
      Move computerMove;

      Console.WriteLine("---Rock Paper Scissors---\n");

      Console.WriteLine("Player, choose your move:\n");
      // print move choices
      string composite = "{0,-20} ({1})";
      Console.WriteLine(string.Format(composite, "Rock", 1));
      Console.WriteLine(string.Format(composite, "Paper", 2));
      Console.WriteLine(string.Format(composite, "Scissors", 3));
      // log chosen move
      playerMove = (Move)int.Parse(Console.ReadLine()); // put in function
      Console.WriteLine($"Player chose {playerMove}!");
      computerMove = (Move)rand.Next(0, 3); // put in function
      Console.WriteLine($"Computer chose {computerMove}!");
      // decide round winner
      if (playerMove == computerMove)
        Console.WriteLine("Tie!");
      // player plays rock
      else if (playerMove == Move.Rock)
      {
        if (computerMove == Move.Scissors)
          System.Console.WriteLine("Player wins!");
        else
          System.Console.WriteLine("Computer wins!");
      }
      // player plays paper
      else if (playerMove == Move.Paper)
      {
        if (computerMove == Move.Rock)
          System.Console.WriteLine("Player wins!");
        else
          System.Console.WriteLine("Computer wins!");
      }
      // player plays scissors
      else if (playerMove == Move.Scissors)
      {
        if (computerMove == Move.Paper)
          System.Console.WriteLine("Player wins!");
        else
          System.Console.WriteLine("Computer wins!");
      }
      else
        System.Console.WriteLine("Something went wrong");
    }
  }
}
