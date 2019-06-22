namespace WaitForDocker.ComposeProcessing
{
    internal interface IComposeConverter<out T>
    {
        T Convert(string yamlCompose);
    }
}