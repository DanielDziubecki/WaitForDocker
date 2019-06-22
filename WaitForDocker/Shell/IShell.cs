namespace WaitForDocker.Shell
{
    internal interface IShell
    {
        string GetFileName();
        string CommandConstructor(string command);
    }
}