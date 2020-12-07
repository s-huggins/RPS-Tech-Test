using System;
using System.Linq;
using SMG_Test.Data;

namespace SMG_Test
{
  class Program
  {
    private static string dbPath = "./Data/rps.db";
    static void Main(string[] args)
    {
      ProcessCommandLineArgs(args);

      new Game(
        new GameDisplayer(),
        new InputReader(),
        new GameContext(dbPath)
      ).Run();
    }

    private static void ProcessCommandLineArgs(string[] args)
    {
      if (args.Length == 0) // nothing to do
        return;

      args = args.Select(arg => arg.ToLowerInvariant()).ToArray();

      if (args.Contains("--drop-db"))
      {
        System.Console.WriteLine("Clearing database...");
        long recordsDeleted = new GameContext(dbPath).Clear();
        System.Console.WriteLine(
          $"Cleared {recordsDeleted}" + (recordsDeleted != 1 ? " records." : " record."));
        Environment.Exit(0);
      }

      if (args.Contains("--stats"))
      {
        System.Console.WriteLine("Generating statistics report...");
        System.Console.WriteLine();

        var stats = new GameContext(dbPath).GenerateStatistics();

        System.Console.WriteLine("---Statistics---");
        System.Console.WriteLine();
        System.Console.WriteLine("{0,-35}{1}", "Total rounds played: ", stats.TotalGames);
        System.Console.WriteLine(
          "{0,-35}{1} ({2:0.00%})", "Total rounds won: ", stats.TotalPlayerWins, stats.WinPercentage);
        System.Console.WriteLine(
          "{0,-35}{1} ({2:0.00%})", "Total rounds lost: ", stats.TotalPlayerLosses, stats.LossPercentage);
        System.Console.WriteLine(
          "{0,-35}{1} ({2:0.00%})", "Total rounds tied: ", stats.TotalTies, stats.TiePercentage);

        System.Console.WriteLine();

        System.Console.WriteLine(
          "{0,-35}{1} ({2:0.00%})",
          "Total rock moves (player): ", stats.PlayerRockTotal, stats.PlayerRockPercentage);
        System.Console.WriteLine(
          "{0,-35}{1} ({2:0.00%})",
          "Total paper moves (player): ", stats.PlayerPaperTotal, stats.PlayerPaperPercentage);
        System.Console.WriteLine(
          "{0,-35}{1} ({2:0.00%})",
          "Total scissors moves (player): ", stats.PlayerScissorsTotal, stats.PlayerScissorsPercentage);

        System.Console.WriteLine();

        System.Console.WriteLine(
          "{0,-35}{1} ({2:0.00%})",
          "Total rock moves (computer): ", stats.ComputerRockTotal, stats.ComputerRockPercentage);
        System.Console.WriteLine(
          "{0,-35}{1} ({2:0.00%})",
          "Total paper moves (computer): ", stats.ComputerPaperTotal, stats.ComputerPaperPercentage);
        System.Console.WriteLine(
          "{0,-35}{1} ({2:0.00%})",
          "Total scissors moves (computer): ", stats.ComputerScissorsTotal, stats.ComputerScissorsPercentage);

        Environment.Exit(0);
      }
    }
  }
}
