using System;
using SMG_Test.Data.Models;

namespace SMG_Test
{
  public class StatsReporter
  {
    private readonly IDisplayer _displayer;

    public StatsReporter(IDisplayer displayer)
    {
      _displayer = displayer ?? throw new ArgumentNullException(nameof(displayer));
    }

    public void PrintReport(Stats stats)
    {
      _displayer.PrintLine("---Statistics---");
      _displayer.PrintLine();
      // game stats
      _displayer.PrintLineFormat("{0,-35}{1}", "Total rounds played: ", stats.TotalGames);
      _displayer.PrintLineFormat(
        "{0,-35}{1} ({2:0.00%})", "Total rounds won: ", stats.TotalPlayerWins, stats.WinPercentage);
      _displayer.PrintLineFormat(
        "{0,-35}{1} ({2:0.00%})", "Total rounds lost: ", stats.TotalPlayerLosses, stats.LossPercentage);
      _displayer.PrintLineFormat(
        "{0,-35}{1} ({2:0.00%})", "Total rounds tied: ", stats.TotalTies, stats.TiePercentage);

      _displayer.PrintLine();
      // player move stats
      _displayer.PrintLineFormat(
        "{0,-35}{1} ({2:0.00%})",
        "Total rock moves (player): ", stats.PlayerRockTotal, stats.PlayerRockPercentage);
      _displayer.PrintLineFormat(
        "{0,-35}{1} ({2:0.00%})",
        "Total paper moves (player): ", stats.PlayerPaperTotal, stats.PlayerPaperPercentage);
      _displayer.PrintLineFormat(
        "{0,-35}{1} ({2:0.00%})",
        "Total scissors moves (player): ", stats.PlayerScissorsTotal, stats.PlayerScissorsPercentage);

      _displayer.PrintLine();
      // computer move stats
      _displayer.PrintLineFormat(
        "{0,-35}{1} ({2:0.00%})",
        "Total rock moves (computer): ", stats.ComputerRockTotal, stats.ComputerRockPercentage);
      _displayer.PrintLineFormat(
        "{0,-35}{1} ({2:0.00%})",
        "Total paper moves (computer): ", stats.ComputerPaperTotal, stats.ComputerPaperPercentage);
      _displayer.PrintLineFormat(
        "{0,-35}{1} ({2:0.00%})",
        "Total scissors moves (computer): ", stats.ComputerScissorsTotal, stats.ComputerScissorsPercentage);
    }
  }
}