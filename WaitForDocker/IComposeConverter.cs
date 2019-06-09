namespace WaitForDocker
{
    public interface IComposeConverter<out T>
    {
        T Convert(string yamlCompose);
    }
}