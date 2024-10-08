using NUnit.Framework;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela Planos da plataforma
/// </summary>
public class PlanosContratosPage
{
    private IWebDriver webDriver;
    private string[] nomesAtivos = { "Display de Chão", "Panfleto", "Ponta de gondola" };
    private string[] abasPlano = { "Dados do Plano", "Ativos Alocados", "Preços Serviços", "Fluxo de Pagamentos", "Histórico", "Anexos", "Book Fotográfico", "Painel da indústria" };

    public PlanosContratosPage(IWebDriver webDriver)
    {
        this.webDriver = webDriver;
    }

    /// <summary>
    /// Método para realizar uma nova simulação de plano\contrato
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage NovaSimulacaoDePlano()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.NovoRegistro)).Click();
        Thread.Sleep(500);

        return this;
    }

    /// <summary>
    /// Métodos para preencher o campo Indústria
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherCampoIndustria()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.PesquisarIndustria)).Click();
        webDriver.FindElement(By.XPath(GlobalVariables.PesquisarIndustria)).SendKeys("ALIMENTOS ZAELI LTDA");
        Thread.Sleep(500);

        webDriver.FindElement(By.XPath(GlobalVariables.SelecionarIndustria)).Click();

        return this;
    }

    /// <summary>
    /// Método para preencher o campo Campanha
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherCampoCampanha(string nomeCampanha)
    {
        webDriver.FindElement(By.XPath(GlobalVariables.PreencherCampanha)).SendKeys(nomeCampanha);

        return this;
    }

    /// <summary>
    /// Método para filtrar e selecionar os ativos
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SelecionarAtivos()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.SelecionarAtivos)).Click();
        webDriver.FindElement(By.XPath(GlobalVariables.FiltrarAtivos)).Click();

        foreach (var nomeAtivo in nomesAtivos)
        {
            Dsl.DigitarNoElementoFiltro(webDriver, nomeAtivo);
            webDriver.FindElement(By.XPath(GlobalVariables.SelecionarAtivosFiltro)).Click();
            webDriver.FindElement(By.XPath(GlobalVariables.PesquisarAtivos)).SendKeys(Keys.Control + "a" + Keys.Backspace);

            webDriver.FindElement(By.XPath(GlobalVariables.TabelaFiltro)).Click();
        }

        webDriver.FindElement(By.XPath(GlobalVariables.OkFiltroAtivos)).Click();
        webDriver.FindElement(By.XPath(GlobalVariables.SelecionarTodosAtivos)).Click();
        webDriver.FindElement(By.XPath(GlobalVariables.AplicarAtivos)).Click();

        return this;
    }

    /// <summary>
    /// Método para preencher as quantidades de ativos por loja (5 por loja)
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherQuantidadeAtivos()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.FecharDetalhamento)).Click();

        Dsl.ScrollParaElemento(webDriver, GlobalVariables.CarregarLojas);

        foreach (var nomeAtivo in nomesAtivos)
        {
            for (var i = 0; i < 5; i++)
            {
                //Informando a quantidade de ativos por loja
                webDriver.FindElement(By.XPath($"//*[text()='{nomeAtivo}']/following-sibling::td[12]//span[@aria-label='Increase Value']")).Click();
            }
        }

        return this;
    }

    /// <summary>
    /// Método para selecionar as lojas para alocação dos ativos
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SelecionarLojas()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.CarregarLojas)).Click();

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MenuLojas);
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.GerarPrePlano);

        var qtdLojas = Dsl.ObterQuantidadeLinhasNoElementoTabela(webDriver, GlobalVariables.QuantidadeLojas);

        for (var i = 1; i <= qtdLojas; i++)
        {
            webDriver.FindElement(By.XPath($"//tbody/tr[{i + 1}]/td[9]//input[@class='ant-checkbox-input']")).Click();
        }

        return this;
    }

    /// <summary>
    /// Método para gerar o pré-plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage GerarPrePlano()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.GerarPrePlano)).Click();

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarRegistro);

        return this;
    }

    /// <summary>
    /// Método para validar a tela de plano após a criação do mesmo
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarPlanoCriado()
    {
        var qtdAbasPlanos = 1;

        foreach (var abaPlano in abasPlano)
        {
            webDriver.FindElement(By.XPath($"//div[@class='ant-tabs-nav-list']/div[{qtdAbasPlanos}]")).Click();

            var valorAtual = webDriver.FindElement(By.XPath($"//div[@class='ant-tabs-nav-list']/div[{qtdAbasPlanos}]")).Text;
            var valorEsperado = abaPlano;

            Assert.That(valorAtual, Does.Contain(valorEsperado));
            Thread.Sleep(500);

            qtdAbasPlanos++;
        }

        return this;
    }

    /// <summary>
    /// Método para validar status e farol na lista de planos, após criação ou alteração 
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarStatusFarolDoPlano(string statusPlanoEsperado, string farolPlanoEsperado)
    {
        Thread.Sleep(500);

        var statusPlanoAtual = webDriver.FindElement(By.XPath(GlobalVariables.StatusPlano)).Text;
        Assert.That(statusPlanoAtual, Does.Contain(statusPlanoEsperado));

        var farolPlanoAtual = webDriver.FindElement(By.XPath(GlobalVariables.FarolPlano)).Text;
        Assert.That(farolPlanoAtual, Does.Contain(farolPlanoEsperado));

        return this;
    }

    /// <summary>
    /// Método para buscar planos
    /// </summary>
    /// <param name="nomeCampanha"></param>
    /// <returns></returns>
    public PlanosContratosPage BuscarPlanos(string nomeCampanha)
    {
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.FarolPlano);

        webDriver.FindElement(By.XPath(GlobalVariables.FiltrarPlanoPorCampanha)).Click();
        webDriver.FindElement(By.XPath(GlobalVariables.PesquisarNomeCampanha)).SendKeys(nomeCampanha);
        webDriver.FindElement(By.XPath(GlobalVariables.BuscarRegistro)).Click();

        return this;
    }

    /// <summary>
    /// Método para abrir a edição de um plano existente
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AbrirEditacaoDoPlano()
    {
        Thread.Sleep(500);

        webDriver.FindElement(By.XPath(GlobalVariables.EditarPlano)).Click();
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.AbasPlano);

        return this;
    }

    /// <summary>
    /// Método para preencher o campo Inicio Vigencia
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage EditarInicioVigencia(string contexto)
    {
        if (contexto.Equals("NovoPlano"))
        {
            webDriver.FindElement(By.XPath(GlobalVariables.InicioVigenciaNovoPlano)).Click();
            Dsl.PreencherCalendariosInicioVigencia(webDriver, GlobalVariables.AvancarCalendarioMesInicioVigencia, 2);
        }
        else if (contexto.Equals("EditarPlano"))
        {
            webDriver.FindElement(By.XPath(GlobalVariables.InicioVigenciaEditarPlano)).Click();
            Dsl.PreencherCalendariosInicioVigencia(webDriver, GlobalVariables.AvancarCalendarioMesInicioVigencia, 2);
        }

        return this;
    }

    /// <summary>
    /// Método para preencher o campo Fim vigencia
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage EditarFimVigencia(string contexto)
    {
        if (contexto.Equals("NovoPlano"))
        {
            webDriver.FindElement(By.XPath(GlobalVariables.FimVigenciaNovoPlano)).Click();
            Dsl.PreencherCalendariosFimVigencia(webDriver, GlobalVariables.AvancarCalendarioMesFimVigencia, 2);
        }
        else if (contexto.Equals("EditarPlano"))
        {
            webDriver.FindElement(By.XPath(GlobalVariables.FimVigenciaEditarPlano)).Click();
            Dsl.PreencherCalendariosFimVigencia(webDriver, GlobalVariables.AvancarCalendarioMesFimVigencia, 2);
        }

        return this;
    }

    /// <summary>
    /// Método para salvar os dados plano com diferentes status
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SalvarPlano()
    {
        var mensagemSucessoEsperada = "OPlanofoialteradocomsucesso!";

        webDriver.FindElement(By.XPath(GlobalVariables.AbaDadosPlano)).Click();

        Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarRegistro);
        webDriver.FindElement(By.XPath(GlobalVariables.SalvarRegistro)).Click();

        if (Dsl.ValidarExistenciaDoElemento(webDriver, GlobalVariables.DataCancelamentoPlano))
            ConfirmarCancelamentoDoPlano();

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.Mensagens);

        var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.Mensagens);
        ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);

        return this;
    }

    /// <summary>
    /// Método para fechar a tela com os dados do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage FecharDadosDoPlano()
    {
        webDriver.FindElement(By.XPath(GlobalVariables.FecharTela)).Click();
        Dsl.Esperar1Segundo();

        return this;
    }

    /// <summary>
    /// Método para editar as quantidades dos ativos por loja
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage EditarQuantidadesDosAtivosNoPlano()
    {
        var qtdAtivosAlocados = Dsl.ObterQuantidadeLinhasNoElementoTabelaAtivosAlocados(webDriver, GlobalVariables.TabelaAtivosAlocados);

        for (var i = 1; i <= qtdAtivosAlocados; i++)
        {
            var editarAtivo = $"//tr[{i}]//span[@aria-label='edit']";

            Dsl.EsperarElementoFicarClicavel(webDriver, editarAtivo);
            webDriver.FindElement(By.XPath(editarAtivo)).Click();
            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.QuantidadeLojasPorAtivo);
            var qtdAtivosAlocadosLoja = Dsl.RemoverLetrasEspacosDeUmTexto(webDriver, GlobalVariables.QuantidadeLojasPorAtivo);

            for (var j = 1; j <= qtdAtivosAlocadosLoja; j++)
            {
                //Aumentando a quantidade de alocação por loja
                webDriver.FindElement(By.XPath($"//body/div[last()]//div[@class='ant-modal-content']//tbody//tr[{j + 1}]/td[17]/div//span[@aria-label='Increase Value']")).Click();
                Thread.Sleep(500);
            }

            webDriver.FindElement(By.XPath(GlobalVariables.SalvarAlocacaoLoja)).Click();
        }

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemSucessoAlocacaoAtivo);
        Dsl.Esperar1Segundo();

        return this;
    }

    /// <summary>
    /// Método para abrir a aba de Ativos Alocados
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AbrirAbaAtivosAlocados()
    {
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.AbaAtivosAlocados);
        webDriver.FindElement(By.XPath(GlobalVariables.AbaAtivosAlocados)).Click();

        return this;
    }

    /// <summary>
    /// Método para alocar um novo ativo para as lojas do plano
    /// </summary>
    /// <param name="nomeAtivo"></param>
    /// <returns></returns>
    public PlanosContratosPage AlocarNovosAtivosNoPlano(string nomeAtivo)
    {
        webDriver.FindElement(By.XPath(GlobalVariables.IncluirAlocacaoAtivo)).Click();

        webDriver.FindElement(By.XPath(GlobalVariables.BuscarAtivoAlocacao)).SendKeys(nomeAtivo);
        webDriver.FindElement(By.XPath(GlobalVariables.SelecionarAtivoAlocacao)).Click();
        Dsl.Esperar1Segundo();

        AumentarQuantidadeAtivosPorLoja();

        webDriver.FindElement(By.XPath(GlobalVariables.SalvarAlocacaoLoja)).Click();
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MensagemSucessoAlocacaoAtivo);
        Dsl.Esperar1Segundo();

        return this;
    }

    /// <summary>
    /// Método para aumentar as quantidades de ativos alocados por loja
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AumentarQuantidadeAtivosPorLoja()
    {
        var qtdAtivosAlocadosLoja = Dsl.RemoverLetrasEspacosDeUmTexto(webDriver, GlobalVariables.QuantidadeLojasPorAtivo);

        for (var i = 1; i <= qtdAtivosAlocadosLoja; i++)
        {
            //Aumentando a quantidade de alocação por loja
            webDriver.FindElement(By.XPath($"//tr[{i + 1}]/td[17]/div//span[@aria-label='Increase Value']")).Click();
            Thread.Sleep(500);
        }

        return this;
    }

    /// <summary>
    /// Método para alterar a situação do plano para aprovado ou cancelado
    /// </summary>
    /// <param name="situacaoPlano"></param>
    /// <returns></returns>
    public PlanosContratosPage EditarSituacaoDoPlano(string contextoSituacao)
    {
        if (contextoSituacao.Equals("Contrato Aprovado"))
        {
            var mensagemAlertaParcelaEsperado = "Salveasparcelascomostatusdoplanosimuladoparaaprovaroplano!";

            webDriver.FindElement(By.XPath(GlobalVariables.SituacaoPlano)).Click();
            webDriver.FindElement(By.XPath($"//*[text()='{contextoSituacao}']")).Click();

            webDriver.FindElement(By.XPath(GlobalVariables.TipoCampanha)).Click();
            webDriver.FindElement(By.XPath(GlobalVariables.SelecionarTipoCampanha)).Click();

            webDriver.FindElement(By.XPath(GlobalVariables.QuantidadeParcelas)).SendKeys("1");
            webDriver.FindElement(By.XPath(GlobalVariables.AbaDadosPlano)).Click();

            var mensagemAlertaParcelaAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.Mensagens);
            ValidarMensagemDeSucessoEAlerta(mensagemAlertaParcelaAtual, mensagemAlertaParcelaEsperado);

            webDriver.FindElement(By.XPath(GlobalVariables.Setor)).Click();
            webDriver.FindElement(By.XPath(GlobalVariables.SelecionarSetor)).Click();

            webDriver.FindElement(By.XPath(GlobalVariables.Setor)).Click();
            webDriver.FindElement(By.XPath(GlobalVariables.SelecionarSetor)).Click();

            webDriver.FindElement(By.XPath(GlobalVariables.Departamento)).Click();
            webDriver.FindElement(By.XPath(GlobalVariables.SelecionarDepartamento)).Click();

            webDriver.FindElement(By.XPath(GlobalVariables.Categoria)).Click();
            webDriver.FindElement(By.XPath(GlobalVariables.SelecionarCategoria)).Click();

            Dsl.Esperar1Segundo();
        }
        else if (contextoSituacao.Equals("Cancelado"))
        {
            webDriver.FindElement(By.XPath(GlobalVariables.SituacaoPlano)).Click();
            webDriver.FindElement(By.XPath($"//*[text()='{contextoSituacao}']")).Click();
        }

        return this;
    }

    /// <summary>
    /// Método para validar mensagens de sucesso e mensagens de alertas em telas
    /// </summary>
    /// <param name="mensagemAtual"></param>
    /// <param name="mensagemEsperada"></param>
    /// <returns></returns>
    public PlanosContratosPage ValidarMensagemDeSucessoEAlerta(string mensagemAtual, string mensagemEsperada)
    {
        Assert.That(mensagemAtual, Does.Contain(mensagemEsperada));

        return this;
    }

    /// <summary>
    /// Método para preencher a data de cancelemento e confirmar o cancelamento do plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ConfirmarCancelamentoDoPlano()
    {
        Thread.Sleep(500);

        webDriver.FindElement(By.XPath(GlobalVariables.DataCancelamentoPlano)).Click();
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.SelecionarDataCancelamentoPlano);

        webDriver.FindElement(By.XPath(GlobalVariables.SelecionarDataCancelamentoPlano)).Click();
        webDriver.FindElement(By.XPath(GlobalVariables.OkCancelamento)).Click();

        return this;
    }

    /// <summary>
    /// Método para confirmar a exclusão de um plano, validando as mensagens de alerta e sucesso
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ConfirmarExclusaoDoPlano()
    {
        var mensagemAlertaExcluirPlanoEsperada = "DesejarealmenteexcluirestePlano?";
        var mensagemSucessoEsperada = "Planodeletadocomsucesso";
        var quantidadeLinhasTabela = Dsl.ObterQuantidadeLinhasNoElementoTabela(webDriver, GlobalVariables.TabelaPlanos);

        for (var i = 1; i <= quantidadeLinhasTabela; i++)
        {
            webDriver.FindElement(By.XPath(GlobalVariables.ExcluirPlano)).Click();

            var mensagemAlertaExcluirPlanoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.ExcluirPlanoMensagemConfirmacao);
            ValidarMensagemDeSucessoEAlerta(mensagemAlertaExcluirPlanoAtual, mensagemAlertaExcluirPlanoEsperada);

            webDriver.FindElement(By.XPath(GlobalVariables.OkExclusao)).Click();

            var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.Mensagens);
            ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);

            Dsl.Esperar1Segundo();
        }
        return this;
    }

    /// <summary>
    /// Método para validar os alertas apresentados com a indisponibildiade do ativo no inventário da loja
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarAlertaInventario()
    {
        var mensagemAlertaEsperada = "Algumaslojasnãoteminventáriosuficientedisponível";
        var quantidadeAlertas = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AlertaInventario);
        var quantidadeLojas = Dsl.ObterQuantidadeLinhasNoElementoTabela(webDriver, GlobalVariables.QuantidadeLojas);

        Assert.That(quantidadeAlertas.Equals(quantidadeLojas), "Quantidade de alertas não foram apresentadas corretamente");

        var mensagemAlertaAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.MensagensDadosPlano);
        ValidarMensagemDeSucessoEAlerta(mensagemAlertaAtual, mensagemAlertaEsperada);

        return this;
    }
}