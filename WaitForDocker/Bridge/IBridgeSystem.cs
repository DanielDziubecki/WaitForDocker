namespace WaitForDocker.Bridge
{
    public interface IBridgeSystem
    {
        string GetFileName();
        string CommandConstructor(string command, Output? output = Output.Hidden, string dir = "");
        void Browse(string url);
    }
}