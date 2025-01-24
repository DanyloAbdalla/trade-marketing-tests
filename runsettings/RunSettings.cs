using Newtonsoft.Json;

/// <summary>
/// Classe para ler o arquivo Json, que contêm as conigurações de execução dos testes
/// Buscando as configurações de cada teste, no que diz respeito a execução ou não execução do mesmo
/// </summary>
public class RunSettings
{
    public List<ClassSettings> TestsClasses { get; set; }

    /// <summary>
    /// Método para ler o arquivo Json, carregando o mesmo em memória
    /// </summary>
    /// <returns></returns>
    public static RunSettings LoadSettings()
    {
        var jsonString = File.ReadAllText("apprunsettings.json");
        return JsonConvert.DeserializeObject<RunSettings>(jsonString);
    }

    /// <summary>
    /// Método para buscar as configurações do teste no arquivo Json, identificando se o mesmo será executado ou não executado
    /// </summary>
    /// <param name="className"></param>
    /// <param name="fixtureName"></param>
    /// <param name="testName"></param>
    /// <returns>Retorna true ou false para variável skip</returns>
    public bool ToSkip(string className, string fixtureName, string testName)
    {
        var testClass = TestsClasses.FirstOrDefault(tc => tc.ClassName == className);
        if(testClass != null)
        {
            if(!string.IsNullOrEmpty(fixtureName))
            {
                var fixture = testClass.FixturesNames?.FirstOrDefault(f => f.FixtureName == fixtureName);
                if(fixture != null)
                {
                    var test = fixture.Tests.FirstOrDefault(t => t.TestName == testName);
                    if(test != null)
                        return test.TestSkip;
                }
            }
            else
            {
                var test = testClass.Tests?.FirstOrDefault(t => t.TestName == testName);
                if(test != null)
                    return test.TestSkip;
            }
        }
        return false;
    }
}