namespace WaitForDocker.ComposeProcessing
{
    public interface IComposeConverter<out T>
    {
        T Convert(string yamlCompose);
    }
}