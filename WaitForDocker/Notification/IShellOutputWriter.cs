namespace WaitForDocker.Notification
{
    public interface IShellOutputWriter
    {
        void StandardOutput(string message);
        void StandardError(string message);
        void StandardLine();
    }
}