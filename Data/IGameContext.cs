using SMG_Test.Data.Models;

namespace SMG_Test.Data
{
  public interface IGameContext
  {
    void SaveResult(Round gameRound);
  }
}