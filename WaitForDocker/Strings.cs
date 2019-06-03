namespace WaitForDocker
{
    public static class Strings
    {
        public static string CleanSpecialCharacters(string value)
        {
            value = value.Replace("\r", "").Replace("\n", "");
            return value;
        }
    }
}
