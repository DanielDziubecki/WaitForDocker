namespace WaitForDocker.Shell
{
    public interface IShell
    {
        string GetFileName();
        string CommandConstructor(string command, Output? output = Output.Hidden, string dir = "");
    }
}