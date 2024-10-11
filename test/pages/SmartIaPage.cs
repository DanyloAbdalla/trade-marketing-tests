using System.Diagnostics;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela SmartIA da plataforma
/// </summary>
public class SmartIaPage
{
    private IWebDriver webDriver;
    private string nomeCampanha = "MassaAutomatizada";
    private string emailResponsavel = "daniela.sorrilha@meucliente.app.br";
    private string whatsAppResponsavel = "15988086091";
    private string nomeResponsavel = "Danylo Homologacao";
    private string mensagemCabecalho = "Campanha Homologacao";
    private string[] nomesAtivos = { "Display de Chão", "Panfleto", "Ponta de gondola" };

    public SmartIaPage(IWebDriver webDriver)
    {
        this.webDriver = webDriver;
    }

    /// <summary>
    /// Método para realizar uma nova campanha no SmartIA
    /// </summary>
    /// <returns></returns>
    public SmartIaPage NovaCampanhaSmartIA()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.NovoRegistro)).Click();
        Thread.Sleep(2000);

        return this;
    }

    /// <summary>
    /// Método para preencher os campos obrigatórios na criação da campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherCamposCampanha()
    {
        Dsl.Esperar1Segundo();
        webDriver.FindElement(By.XPath(GlobalVariables.Campanhas)).Click();

        CarregarImagemCampanha();
        PreencherNomeCampanha();
        PreencherInicioVigencia();
        PreencherFimVigencia();
        PreencherEmailResponsavel();
        PreencherWhatsAppResponsavel();
        PreencherDataLimite();
        PreencherNomeResponsavel();
        PreencherMensagemCabecalho();

        return this;
    }

    /// <summary>
    /// Método para carregar a imagem da campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage CarregarImagemCampanha()
    {
        IWebElement imageInput = webDriver.FindElement(By.XPath(GlobalVariables.CarregarImagem));

        imageInput.SendKeys("C:\\TestProjectMeuCliente\\logomeucliente.png");

        return this;
    }

    /// <summary>
    /// Método para preencher o nome da campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherNomeCampanha()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.NomeCampanha)).SendKeys(nomeCampanha);

        return this;
    }

    /// <summary>
    /// Método para preencher o início da vigencia da campanha
    /// Avançando 2 mês
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherInicioVigencia()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.InicioVigenciaCampanha)).Click();
        Thread.Sleep(500);

        Dsl.PreencherCalendariosInicioVigencia(webDriver, GlobalVariables.InicioVigenciaCampanhaAvancarData, 2);

        return this;
    }

    /// <summary>
    /// Método para preencher o fim da vigencia da campanha
    /// Avançando 3 mês
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherFimVigencia()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.FimVigenciaCampanha)).Click();
        Thread.Sleep(500);

        Dsl.PreencherCalendariosFimVigencia(webDriver, GlobalVariables.FimVigenciaCampanhaAvancarData, 3);

        return this;
    }

    /// <summary>
    /// Método para preencher o email do responsável pela campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherEmailResponsavel()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.EmailResposavel)).SendKeys(emailResponsavel);

        return this;
    }

    /// <summary>
    /// Método para preencher o whatsapp do responsável pela campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherWhatsAppResponsavel()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.WhatsAppResposavel)).SendKeys(whatsAppResponsavel);

        return this;
    }

    /// <summary>
    /// Método para preencher a data limte da campanha
    /// Avançando 1 mês
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherDataLimite()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.DataLimiteCampanha)).Click();
        Thread.Sleep(500);

        PreencherCalendarioDataLimite(GlobalVariables.DataLimiteCampanhaAvancarData, 1);

        return this;
    }

    /// <summary>
    /// Método para preencher o nome do responsável pela campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherNomeResponsavel()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.NomeResposavel)).SendKeys(nomeResponsavel);

        return this;
    }

    /// <summary>
    /// Método para preencher mensagem de cabeçalho da campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherMensagemCabecalho()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.MensagemCabecalhoCampanha)).SendKeys(mensagemCabecalho);

        return this;
    }

    /// <summary>
    /// Método para abrir a edição de um plano existente
    /// </summary>
    /// <returns></returns>
    public SmartIaPage AbrirEdicaoDaCampanha()
    {
        Thread.Sleep(500);

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MenuSuspensoVarejos);
        webDriver.FindElement(By.XPath(GlobalVariables.EditarCampanha)).Click();

        return this;
    }

    /// <summary>
    /// Método para abrir o menu suspenso Varejos
    /// </summary>
    /// <returns></returns>
    public SmartIaPage AbrirMenuSuspensoVarejos()
    {
        Thread.Sleep(2000);
        webDriver.FindElement(By.XPath(GlobalVariables.MenuSuspensoVarejos)).Click();

        return this;
    }

    /// <summary>
    /// Método para adicionar o varejo na campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage AdicionarVarejo()
    {
        AbrirMenuSuspensoVarejos();

        webDriver.FindElement(By.XPath(GlobalVariables.PesquisarVarejo)).SendKeys("Meu Cliente");
        Thread.Sleep(500);

        webDriver.FindElement(By.XPath(GlobalVariables.SelecionarVarejo)).Click();
        webDriver.FindElement(By.XPath(GlobalVariables.AdicionarVarejo)).Click();

        var varejoSelecionado = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.VarejoSelecionado);

        Debug.Assert(varejoSelecionado == 1, "Display com o varejo selecionado não foi apresentado na tela");

        return this;
    }

    /// <summary>
    /// Método para realizar a varredura dos ativos que estão com disponibilidade de inventário, para que possam ser reservados na campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage RealizarVarredura()
    {
        var mensagemSucessoEsperada = "Campanhaeditadacomsucesso!";

        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.VarrerAtivos);
        webDriver.FindElement(By.XPath(GlobalVariables.VarrerAtivos)).Click();

        var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.Mensagens);

        Dsl.ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);

        return this;
    }

    /// <summary>
    /// Método para selecionar e reservar os ativos
    /// </summary>
    /// <returns></returns>
    public SmartIaPage SelecionarEReservarAtivos()
    {
        AbrirSelecaoDeAtivosReservados();

        foreach (var nomeAtivo in nomesAtivos)
        {
            Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarAtivosCampanha, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeAtivo);

            webDriver.FindElement(By.XPath(GlobalVariables.SelecionarAtivoCampanha)).Click();
            webDriver.FindElement(By.XPath(GlobalVariables.ReservarQuantidadeAtivoLojasCampanha)).Click();
            ReservarAtivoPorLojas(1); //reservando 1 espaço para o ativo nas lojas
        }

        return this;
    }

    /// <summary>
    /// Método para editar a quantidade dos ativos reservados
    /// </summary>
    /// <returns></returns>
    public SmartIaPage EditarQuantidadesDosAtivosReservados()
    {
        AbrirSelecaoDeAtivosReservados();

        foreach (var nomeAtivo in nomesAtivos)
        {
            Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarAtivosCampanha, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeAtivo);

            webDriver.FindElement(By.XPath(GlobalVariables.ReservarQuantidadeAtivoLojasCampanha)).Click();
            ReservarAtivoPorLojas(2); //reservando 2 espaços para o ativo nas lojas
        }

        return this;
    }


    /// <summary>
    /// Método para reservar um novo ativo para as lojas da campanha
    /// </summary>
    /// <param name="nomeAtivo"></param>
    /// <returns></returns>
    public SmartIaPage ReservarNovosAtivosPorLoja(string nomeAtivo)
    {
        AbrirSelecaoDeAtivosReservados();
        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarAtivosCampanha, GlobalVariables.PreencherFiltro, GlobalVariables.BuscarRegistro, nomeAtivo);

        webDriver.FindElement(By.XPath(GlobalVariables.SelecionarAtivoCampanha)).Click();
        webDriver.FindElement(By.XPath(GlobalVariables.ReservarQuantidadeAtivoLojasCampanha)).Click();
        ReservarAtivoPorLojas(2); //reservando 1 espaço para o ativo nas lojas

        return this;
    }

    /// <summary>
    /// Método para abrir a seleção de ativos à serem reservados para a campanha, com o varejo pré-selecionado
    /// </summary>
    /// <returns></returns>
    public SmartIaPage AbrirSelecaoDeAtivosReservados()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.SelecionarAtivosCampanha)).Click();
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.SalvarAtivosCampanha);

        return this;
    }

    /// <summary>
    /// Método para reservar os ativos por loja
    /// </summary>
    /// <returns></returns>
    public SmartIaPage ReservarAtivoPorLojas(int quantidadeReserva)
    {
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.ReservarAtivoLojasCampanha);

        var quantidadeLojas = Dsl.ObterQuantidadeLinhasNoElementoTabelaSemLinhaInvisivel(webDriver, GlobalVariables.QuantidadeLojasReservaCampanha);
        var valorReservaEsperado = quantidadeLojas * quantidadeReserva;

        webDriver.FindElement(By.XPath(GlobalVariables.PreencherReservarTodasLojasCampanha)).SendKeys(quantidadeReserva.ToString());
        webDriver.FindElement(By.XPath(GlobalVariables.ReservarAtivoLojasCampanha)).Click();
        webDriver.FindElement(By.XPath(GlobalVariables.FecharReservaAtivoLojaCampanha)).Click();

        var valor = Dsl.ObterDadosDoAtributoValueDoElemento(webDriver, GlobalVariables.QuantidadeReservadaAtivoCampanha);
        var valorReservaAtual = Convert.ToInt16(valor);

        Debug.Assert(valorReservaAtual == valorReservaEsperado, "Quantidade de reservas calculada incorretamente");

        return this;
    }

    /// <summary>
    /// Método para selecionar datas limite, baseado na dia corrente
    /// Avançando para os meses seguintes se qtdAvancarMeses for maior que 0
    /// </summary>
    /// <param name="XPath"></param>
    /// <param name="qtdAvancarMeses"></param>
    public SmartIaPage PreencherCalendarioDataLimite(string XPath, int qtdAvancarMeses)
    {
        DateTime dataAtual = DateTime.Now;
        var diaAtual = dataAtual.Day;

        if (qtdAvancarMeses == 0)
        {
            webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[3]//div[text()='{diaAtual}'])[1]")).Click();
        }
        else if (qtdAvancarMeses > 0)
        {
            for (int i = 0; i < qtdAvancarMeses; i++)
            {
                webDriver.FindElement(By.XPath(XPath)).Click();
            }

            webDriver.FindElement(By.XPath($"((//div[@class='ant-picker-body'])[3]//div[text()='{diaAtual}'])[1]")).Click();
        }

        return this;
    }

    /// <summary>
    /// Método para salvar a campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage SalvarCampanha(string contexto)
    {
        var mensagemSucessoEsperada = "";

        if (contexto.Contains("NovaCampanha"))
            mensagemSucessoEsperada = "Campanhacriadacomsucesso!";
        if (contexto.Contains("EditarCampanha"))
            mensagemSucessoEsperada = "Campanhaeditadacomsucesso!";

        webDriver.FindElement(By.XPath(GlobalVariables.SalvarRegistro)).Click();
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.Mensagens);

        var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.Mensagens);

        Dsl.ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);

        return this;
    }

    /// <summary>
    /// Método para salvar os ativos selecionados e reservados para a campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage SalvarAtivosReservados()
    {
        var mensagemSucessoEsperada = "AtivosSelecionadoscomSucesso!";

        webDriver.FindElement(By.XPath(GlobalVariables.SalvarAtivosCampanha)).Click();
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.Mensagens);

        var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.Mensagens);

        Dsl.ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);
        Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.Mensagens);

        return this;
    }

    /// <summary>
    /// Método para buscar as campanhas criadas
    /// </summary>
    /// <returns></returns>
    public SmartIaPage BuscarCampanhas()
    {
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.StatusCampanha);
        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarCampanha, GlobalVariables.PesquisarCampanha, GlobalVariables.BuscarRegistro, nomeCampanha);

        return this;
    }

    /// <summary>
    /// Método para voltar na tela de campanhas do SmartIA
    /// </summary>
    /// <returns></returns>
    public SmartIaPage FecharCampanha()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.VoltarTela)).Click();
        Thread.Sleep(1500);

        return this;
    }

    /// <summary>
    /// Método para validar status na lista de campanhas, após criação ou alteração 
    /// </summary>
    /// <returns></returns>
    public SmartIaPage ValidarStatusDaCampanha(string statusCampanhaEsperado)
    {
        var statusCampanhaAtual = webDriver.FindElement(By.XPath(GlobalVariables.StatusCampanha)).Text;
        Assert.That(statusCampanhaAtual, Does.Contain(statusCampanhaEsperado));

        return this;
    }
}