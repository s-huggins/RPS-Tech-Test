namespace SMG_Test
{
  class Program
  {
    static void Main(string[] args)
    {
      new Game(
        new GameDisplayer(),
        new InputReader()
      ).Run();
    }
  }
}
