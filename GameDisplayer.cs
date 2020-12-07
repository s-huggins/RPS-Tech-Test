namespace SMG_Test
{
  public class GameDisplayer : IDisplayer
  {
    public void Print(string line)
    {
      System.Console.Write(line);
    }

    public void PrintFormat(string formatStr, params object[] args)
    {
      System.Console.Write(string.Format(formatStr, args));
    }

    public void PrintLine(string line)
    {
      System.Console.WriteLine(line);
    }

    public void PrintLine()
    {
      System.Console.WriteLine();
    }

    public void PrintLineFormat(string formatStr, params object[] args)
    {
      System.Console.WriteLine(string.Format(formatStr, args));
    }
  }
}