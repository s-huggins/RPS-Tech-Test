namespace SMG_Test
{
  public interface IDisplayer
  {
    void Print(string line);
    void PrintLine(string line);
    void PrintLine();
    void PrintFormat(string formatStr, params object[] args);
    void PrintLineFormat(string formatStr, params object[] args);
  }
}