using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TradeMarketingWebTestProject;

public class DataLoader
{
    private static dynamic massaDados;

    public static void CarregarArquivo()
    {
        string json = File.ReadAllText("datatest.json");
        massaDados = JsonConvert.DeserializeObject<dynamic>(json);
    }

    public static dynamic ObterDados(string suite, string teste, string data)
    {
        return massaDados[suite][teste][data];
    }

    public static List<string> ObterDadosEmLista(string suite, string teste, string data)
    {
        return ((JArray)massaDados[suite][teste][data]).ToObject<List<string>>();
    }

    public static List<MensagemFeedback> ObterMensagensDeFeedback(string suite, string teste, string data)
    {
        return ((JArray)massaDados[suite][teste][data]).ToObject<List<MensagemFeedback>>();
    }
}