using System;
using System.Data.SQLite;
using SMG_Test.Data.Models;

namespace SMG_Test.Data
{
  // Preserves connection for duration of application
  public class GameContext : IGameContext, IDisposable
  {
    private string _dbPath;
    private SQLiteConnection _connection;

    public GameContext(string dbPath)
    {
      _dbPath = dbPath;

      var connectionStringBuilder = new SQLiteConnectionStringBuilder();
      connectionStringBuilder.DataSource = dbPath;

      _connection = new SQLiteConnection(connectionStringBuilder.ConnectionString);
      try
      {
        _connection.Open();
      }
      catch
      {
        System.Console.WriteLine("Database connection failed. Exiting game...");
        Environment.Exit(1);
      }

      EnsureTableExists();
    }

    private void EnsureTableExists()
    {
      try
      {
        var tableCmd = _connection.CreateCommand();
        tableCmd.CommandText =
          @"CREATE TABLE IF NOT EXISTS rounds
          (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            date TEXT,
            player_move TEXT,
            computer_move TEXT,
            player_result TEXT
          );
        ";
        tableCmd.ExecuteNonQuery();
      }
      catch
      {
        System.Console.WriteLine("Database connection failed. Exiting game...");
        Environment.Exit(1);
      }
    }

    public void SaveResult(Round round)
    {
      try
      {
        var saveCommand = _connection.CreateCommand();
        saveCommand.CommandText = $@"INSERT INTO rounds (date, player_move, computer_move, player_result)
        VALUES
        (
          '{DateTimeOffset.UtcNow.ToString()}',
          '{round.PlayerMove.ToString().ToUpperInvariant()}',
          '{round.ComputerMove.ToString().ToUpperInvariant()}',
          '{round.GameResultString}'
        );
      ";
        saveCommand.ExecuteNonQuery();
      }
      catch
      {
        System.Console.WriteLine("Failed to save round to db. Exiting game...");
        Environment.Exit(1);
      }
    }

    public Stats GenerateStatistics()
    {
      var stats = new Stats();
      // feed stats through to receive mutations
      GenerateResultStats(stats);
      GenerateMoveStats(stats, PlayerType.Computer);
      GenerateMoveStats(stats, PlayerType.Human);

      return stats;
    }

    private void GenerateResultStats(Stats stats)
    {
      try
      {
        var statsCommand = _connection.CreateCommand();
        statsCommand.CommandText = @"
          SELECT COUNT(*), player_result
          FROM rounds
          GROUP BY player_result;
        ";

        using var reader = statsCommand.ExecuteReader();
        while (reader.Read())
        {
          long total = reader.GetInt64(0);
          string result = reader.GetString(1);

          switch (result)
          {
            case "W":
              stats.TotalPlayerWins = total;
              break;
            case "L":
              stats.TotalPlayerLosses = total;
              break;
            case "T":
              stats.TotalTies = total;
              break;
          }
        }
      }
      catch
      {
        System.Console.WriteLine("Failed to generate statistics.");
        Environment.Exit(1);
      }
    }

    private void GenerateMoveStats(Stats stats, PlayerType player)
    {
      try
      {
        var statsCommand = _connection.CreateCommand();
        var playerCol = player == PlayerType.Human ? "player_move" : "computer_move";

        statsCommand.CommandText = $@"
          SELECT COUNT(*), { playerCol }
          FROM rounds
          GROUP BY { playerCol };
        ";

        using var reader = statsCommand.ExecuteReader();
        while (reader.Read())
        {
          long total = reader.GetInt64(0);
          string move = reader.GetString(1);

          switch (move)
          {
            case "ROCK":
              if (player == PlayerType.Human)
                stats.PlayerRockTotal = total;
              else
                stats.ComputerRockTotal = total;
              break;
            case "PAPER":
              if (player == PlayerType.Human)
                stats.PlayerPaperTotal = total;
              else
                stats.ComputerPaperTotal = total;
              break;
            case "SCISSORS":
              if (player == PlayerType.Human)
                stats.PlayerScissorsTotal = total;
              else
                stats.ComputerScissorsTotal = total;
              break;
          }
        }
      }
      catch
      {
        System.Console.WriteLine("Failed to generate statistics.");
        Environment.Exit(1);
      }
    }

    public long Clear()
    {
      try
      {
        long rowCount = CountRows();
        var eraseCommand = _connection.CreateCommand();
        eraseCommand.CommandText = "DROP TABLE IF EXISTS rounds;";
        eraseCommand.ExecuteNonQuery();
        return rowCount;
      }
      catch
      {
        // db or table don't exist, so noop
        return 0;
      }
    }



    public void Dispose()
    {
      Dispose(true);
    }

    private long CountRows()
    {
      var countCommand = _connection.CreateCommand();
      countCommand.CommandText = "SELECT COUNT(*) FROM rounds;";
      return (long)countCommand.ExecuteScalar();
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        _connection.Close();
        GC.SuppressFinalize(this);
      }
    }
  }
}