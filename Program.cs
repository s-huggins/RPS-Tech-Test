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

      // normalize command line args into lower case
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

        var stats = new GameContext(dbPath).GetHistoryStats();

        new StatsReporter(new GameDisplayer()).PrintReport(stats);

        Environment.Exit(0);
      }
    }
  }
}
