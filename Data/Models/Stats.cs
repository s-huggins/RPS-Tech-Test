namespace SMG_Test.Data.Models
{
  public class Stats
  {
    // result stats
    public long TotalGames => TotalPlayerWins + TotalPlayerLosses + TotalTies;
    public long TotalPlayerWins { get; set; }
    public long TotalPlayerLosses { get; set; }
    public long TotalTies { get; set; }

    // move stats
    public long PlayerRockTotal { get; set; }
    public long PlayerPaperTotal { get; set; }
    public long PlayerScissorsTotal { get; set; }
    public long ComputerRockTotal { get; set; }
    public long ComputerPaperTotal { get; set; }
    public long ComputerScissorsTotal { get; set; }

    // result percentages
    public double WinPercentage => CalculatePercentage(TotalPlayerWins, TotalGames);
    public double LossPercentage => CalculatePercentage(TotalPlayerLosses, TotalGames);
    public double TiePercentage => CalculatePercentage(TotalTies, TotalGames);

    // move percentages, NB TotalGames equals total moves because 1 move is made each game
    public double PlayerRockPercentage => CalculatePercentage(PlayerRockTotal, TotalGames);
    public double PlayerPaperPercentage => CalculatePercentage(PlayerPaperTotal, TotalGames);
    public double PlayerScissorsPercentage => CalculatePercentage(PlayerScissorsTotal, TotalGames);
    public double ComputerRockPercentage => CalculatePercentage(ComputerRockTotal, TotalGames);
    public double ComputerPaperPercentage => CalculatePercentage(ComputerPaperTotal, TotalGames);
    public double ComputerScissorsPercentage => CalculatePercentage(ComputerScissorsTotal, TotalGames);

    public void Add(Round round)
    {
      switch (round.GameResult)
      {
        case GameResult.PlayerWin:
          ++TotalPlayerWins;
          break;
        case GameResult.PlayerLose:
          ++TotalPlayerLosses;
          break;
        case GameResult.Tie:
          ++TotalTies;
          break;
      }
      switch (round.PlayerMove)
      {
        case Move.Rock:
          ++PlayerRockTotal;
          break;
        case Move.Paper:
          ++PlayerPaperTotal;
          break;
        case Move.Scissors:
          ++PlayerScissorsTotal;
          break;
      }
      switch (round.ComputerMove)
      {
        case Move.Rock:
          ++ComputerRockTotal;
          break;
        case Move.Paper:
          ++ComputerPaperTotal;
          break;
        case Move.Scissors:
          ++ComputerScissorsTotal;
          break;
      }

    }

    public Stats Snapshot => this.MemberwiseClone() as Stats;

    private double CalculatePercentage(long num, long total)
    {
      return total == 0 ? 0 : (double)num / total;
    }
  }
}