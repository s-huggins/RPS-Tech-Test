using System;
using SMG_Test.Types;
namespace SMG_Test.Data.Models
{
  public class Round
  {
    public long Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public Move PlayerMove { get; set; }
    public Move ComputerMove { get; set; }
    public GameResult GameResult { get; set; }

    public string GameResultString
    {
      get
      {
        if (GameResult == GameResult.PlayerWin)
          return "W";
        else if (GameResult == GameResult.PlayerLose)
          return "L";
        else
          return "T";
      }
    }
  }
}