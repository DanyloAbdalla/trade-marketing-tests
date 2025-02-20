/// <summary>
/// Classe com atributos de configuração de execução
/// </summary>
public class ClassSettings
{
    public string ClassName { get; set; }
    public List<FixtureSettings> FixturesNames { get; set; }
    public List<TestSettings> Tests { get; set; }
}