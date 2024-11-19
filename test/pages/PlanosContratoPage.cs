using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela Planos da plataforma
/// </summary>
public class PlanosContratosPage
{
    private IWebDriver webDriver;
    private string[] nomesAtivos = { "Adesivo de Check Out", "Ponta de Gôndola", "Woobler" };
    private string[] abasPlano = { "Dados do Plano", "Ativos Alocados", "Fluxo de Pagamentos", "Histórico", "Anexos", "Book Fotográfico", "Painel da indústria" };

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
        Dsl.Clicar(webDriver, GlobalVariables.NovoRegistro, "Botão Nova Simulação");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.FecharPlano);

        return this;
    }

    /// <summary>
    /// Métodos para preencher o campo Indústria
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherCampoIndustria()
    {
        Dsl.Clicar(webDriver, GlobalVariables.CampoIndustria, "Campo Indústria");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SelecionarIndustria);

        Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.PesquisarIndustria, "Indústria 01 F");
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarIndustria, "Campo Selecionar Indústria");

        return this;
    }

    /// <summary>
    /// Método para preencher o campo Campanha
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherCampoCampanha(string nomeCampanha)
    {
        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PreencherCampanha, nomeCampanha);

        return this;
    }

    /// <summary>
    /// Método para filtrar e selecionar os ativos
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SelecionarAtivos()
    {
        Dsl.Clicar(webDriver, GlobalVariables.SelecionarAtivos, "Botão Selecionar Ativos");

        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.FiltrarAtivos);
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FiltrarAtivos, "Botão Filtrar Ativo");

        foreach (var nomeAtivo in nomesAtivos)
        {
            Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.PesquisarAtivos, nomeAtivo);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarAtivosFiltro, "Campo Seleciona Ativo Filtrado");
            webDriver.FindElement(By.XPath(GlobalVariables.PesquisarAtivos)).SendKeys(Keys.Control + "a" + Keys.Backspace); //Apagando o nome do ativo do campo de pesquisa
            Dsl.Clicar(webDriver, GlobalVariables.TelaFiltrarAtivo, "Tela Filtrar Ativos");
        }

        Dsl.Clicar(webDriver, GlobalVariables.OkFiltroAtivos, "Botão OK Ativos Selecionados no Filtro");
        Dsl.Clicar(webDriver, GlobalVariables.SelecionarTodosAtivos, "Campo Selecionar Todos os Ativos");
        Dsl.Clicar(webDriver, GlobalVariables.AplicarAtivos, "Botão Aplicar Ativos Selecionados");

        return this;
    }

    /// <summary>
    /// Método para preencher as quantidades de ativos por loja (5 por loja)
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage PreencherQuantidadeAtivos()
    {
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
        Dsl.Clicar(webDriver, GlobalVariables.CarregarLojas, "Botão Carregar Lojas");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MenuLojas);
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.GerarPrePlano);

        var qtdLojas = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaLojasPlano) - 1; //Contar linhas no elemento tbody da listagem de lojas na simulação do plano, ignorando a tag tr sem dados

        for (var i = 1; i <= qtdLojas; i++)
        {
            webDriver.FindElement(By.XPath($"//tbody/tr[{i + 1}]/td[9]//input[@class='ant-checkbox-input']")).Click();
        }

        return this;
    }

    /// <summary>
    /// Método para gerar o pré-plano, clicando no botão Gera Pré-Plano
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage GerarPrePlano()
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.GerarPrePlano, "Botão Gerar Pré-Plano");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarRegistro);

        return this;
    }

    /// <summary>
    /// Método para validar a tela de plano após a criação do mesmo
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarPlanoCriado()
    {
        var contadorAbaPlano = 1;

        Dsl.Esperar1Segundo();
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.AbasPlano);

        foreach (var abaPlano in abasPlano)
        {
            webDriver.FindElement(By.XPath($"//div[@class='ant-tabs-nav-list']/div[{contadorAbaPlano}]")).Click();

            var tituloAbaAtual = webDriver.FindElement(By.XPath($"//div[@class='ant-tabs-nav-list']/div[{contadorAbaPlano}]")).Text;
            var tituloAbaEsperado = abaPlano;

            Assert.That(tituloAbaAtual, Does.Contain(tituloAbaEsperado));
            Thread.Sleep(500);

            contadorAbaPlano++;
        }

        return this;
    }

    /// <summary>
    /// Método para validar status e farol na lista de planos, após criação ou alteração
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage ValidarStatusFarolDoPlano(string statusPlanoEsperado, string farolPlanoEsperado)
    {
        var statusPlanoAtual = webDriver.FindElement(By.XPath(GlobalVariables.StatusPlano)).Text;
        Assert.That(statusPlanoAtual, Does.Contain(statusPlanoEsperado), "Status atual não corresponde com o esperado");

        var farolPlanoAtual = webDriver.FindElement(By.XPath(GlobalVariables.FarolPlano)).Text;
        Assert.That(farolPlanoAtual, Does.Contain(farolPlanoEsperado), "Farol atual não corresponde com o esperado");

        return this;
    }

    /// <summary>
    /// Método para buscar planos
    /// </summary>
    /// <param name="nomeCampanha"></param>
    /// <returns></returns>
    public PlanosContratosPage BuscarPlanos(string textoValor)
    {
        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarPlanoPorCampanha, GlobalVariables.PesquisarNomeCampanha, GlobalVariables.BuscarRegistro, textoValor);

        return this;
    }

    /// <summary>
    /// Método para abrir a edição de um plano existente
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AbrirEdicaoDoPlano()
    {
        //Thread.Sleep(500);

        //webDriver.FindElement(By.XPath(GlobalVariables.EditarPlano)).Click();
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.EditarPlano, "Botão Editar Plano");
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
            //webDriver.FindElement(By.XPath(GlobalVariables.InicioVigenciaNovoPlano)).Click();
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.InicioVigenciaNovoPlano, "Campo Inicio Vigencia Novo Plano");
            Dsl.PreencherCalendariosInicioVigencia(webDriver, GlobalVariables.AvancarCalendarioMesInicioVigencia, 2);
        }
        else if (contexto.Equals("EditarPlano"))
        {
            //webDriver.FindElement(By.XPath(GlobalVariables.InicioVigenciaEditarPlano)).Click();
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.InicioVigenciaEditarPlano, "Campo Inicio Vigencia Editar Plano");
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
            //webDriver.FindElement(By.XPath(GlobalVariables.FimVigenciaNovoPlano)).Click();
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FimVigenciaNovoPlano, "Campo Fim Vigencia Novo Plano");
            Dsl.PreencherCalendariosFimVigencia(webDriver, GlobalVariables.AvancarCalendarioMesFimVigencia, 2);
        }
        else if (contexto.Equals("EditarPlano"))
        {
            //webDriver.FindElement(By.XPath(GlobalVariables.FimVigenciaEditarPlano)).Click();
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.FimVigenciaEditarPlano, "Campo Fim Vigencia Editar Plano");
            Dsl.PreencherCalendariosFimVigencia(webDriver, GlobalVariables.AvancarCalendarioMesFimVigencia, 2);
        }

        return this;
    }

    /// <summary>
    /// Método para salvar os dados plano com diferentes status
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage SalvarPlano(string contexto = "SalvarPlano")
    {
        var mensagemSucessoEsperada = "OPlanofoialteradocomsucesso!";

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AbaDadosPlano, "Aba Dados Plano");
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarRegistro);
        Dsl.Clicar(webDriver, GlobalVariables.SalvarRegistro, "Botão Salvar Plano");

        if (contexto.Contains("CancelarPlano"))
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
        Dsl.Clicar(webDriver, GlobalVariables.FecharPlano, "Botão Fechar Plano");
        Dsl.Esperar1Segundo();

        if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.FecharPlanoConfirmacao) > 0)
            Dsl.Clicar(webDriver, GlobalVariables.FecharPlanoConfirmacao, "Botão Confirmar Fechar Plano");

        Dsl.Esperar1Segundo();

        return this;
    }

    /// <summary>
    /// Método para editar as quantidades dos ativos por loja
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage EditarQuantidadesDosAtivosNoPlano()
    {
        var mensagemSucessoEsperada = "Alocaçãoatualizadacomsucesso!";
        var qtdAtivosAlocados = Dsl.ObterQuantidadeLinhasNoElementoTabelaComLinhaInvisivel(webDriver, GlobalVariables.TabelaAtivosAlocados);

        for (var i = 1; i <= qtdAtivosAlocados; i++)
        {
            var editarAtivo = $"//tr[{i + 1}]//button/span[@aria-label='edit']";

            Dsl.EsperarElementoParaClicar(webDriver, editarAtivo, "Botão Editar Ativo");

            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaLojasAtivoAlocados);
            Dsl.Esperar1Segundo();
            var qtdAtivosAlocadosLoja = Dsl.RemoverLetrasEspacosDeUmTexto(webDriver, GlobalVariables.QuantidadeLojasPorAtivo); //Descobrindo a quantidade de lojas no plano para o ativo alocado

            for (var j = 1; j <= qtdAtivosAlocadosLoja; j++)
            {
                webDriver.FindElement(By.XPath($"//tbody//tr[{j + 1}]/td[17]/div//span[@aria-label='Increase Value']")).Click(); //Aumentando a quantidade de alocação por loja
            }

            Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

            var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.MensagemSucessoAlocacaoAtivo);
            Dsl.ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);
            Thread.Sleep(3000);
        }

        return this;
    }

    /// <summary>
    /// Método para abrir a aba de Ativos Alocados
    /// </summary>
    /// <returns></returns>
    public PlanosContratosPage AbrirAbaAtivosAlocados()
    {
        Dsl.ScrollParaElemento(webDriver, GlobalVariables.AbaAtivosAlocados);
        Dsl.Clicar(webDriver, GlobalVariables.AbaAtivosAlocados, "Aba Ativos Alocados");

        return this;
    }

    /// <summary>
    /// Método para alocar um novo ativo para as lojas do plano
    /// </summary>
    /// <param name="nomeAtivo"></param>
    /// <returns></returns>
    public PlanosContratosPage AlocarNovosAtivosNoPlano(string nomeAtivo)
    {
        var mensagemSucessoEsperada = "Alocaçãoatualizadacomsucesso!";

        Dsl.Clicar(webDriver, GlobalVariables.IncluirAlocacaoAtivo, "Botão Incluir Ativo");
        Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.BuscarAtivoAlocacao, nomeAtivo);
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.SelecionarAtivoAlocacao);
        Dsl.Clicar(webDriver, GlobalVariables.SelecionarAtivoAlocacao, "Campo Selecionar Ativo");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.TabelaLojasAtivoAlocados);
        Dsl.Esperar1Segundo();

        AumentarQuantidadeAtivosPorLoja();

        Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarAlocacaoLoja);
        Dsl.Clicar(webDriver, GlobalVariables.SalvarAlocacaoLoja, "Botão Salvar Quantidades Alocadas do Ativo por Loja");

        var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.MensagemSucessoAlocacaoAtivo);
        Dsl.ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);
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
        var valorSetorDepartamentoCategoria = "Geral";

        if (contextoSituacao.Equals("Contrato Aprovado"))
        {
            var mensagemAlertaParcelaEsperado = "Salveasparcelascomostatusdoplanosimuladoparaaprovaroplano!";

            webDriver.FindElement(By.XPath(GlobalVariables.SituacaoPlano)).Click();
            webDriver.FindElement(By.XPath($"//*[text()='{contextoSituacao}']")).Click();

            //webDriver.FindElement(By.XPath(GlobalVariables.TipoCampanha)).Click();
            //webDriver.FindElement(By.XPath(GlobalVariables.SelecionarTipoCampanha)).Click();
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.TipoCampanha, "Campo Tipo Campanha");
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarTipoCampanha, "Campo Selecionar Tipo Campanha");

            webDriver.FindElement(By.XPath(GlobalVariables.QuantidadeParcelas)).SendKeys("1");
            //webDriver.FindElement(By.XPath(GlobalVariables.AbaDadosPlano)).Click();
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.AbaDadosPlano, "Aba Dados Plano");

            var mensagemAlertaParcelaAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.Mensagens);
            ValidarMensagemDeSucessoEAlerta(mensagemAlertaParcelaAtual, mensagemAlertaParcelaEsperado);

            Dsl.ScrollParaElemento(webDriver, GlobalVariables.SalvarRegistro);

            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.Setor, valorSetorDepartamentoCategoria);
            //webDriver.FindElement(By.XPath(GlobalVariables.SelecionarSetor)).Click();
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarSetor, "Campo Selecionar Setor");

            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.Departamento, valorSetorDepartamentoCategoria);
            //webDriver.FindElement(By.XPath(GlobalVariables.SelecionarDepartamento)).Click();
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarDepartamento, "Campo Selecionar Departamento");

            Dsl.DigitarNoCampoTextoComboList(webDriver, GlobalVariables.Categoria, valorSetorDepartamentoCategoria);
            //webDriver.FindElement(By.XPath(GlobalVariables.SelecionarCategoria)).Click();
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SelecionarCategoria, "Campo Selecionar Categoria");
        }
        else if (contextoSituacao.Equals("Cancelado"))
        {
            //webDriver.FindElement(By.XPath(GlobalVariables.SituacaoPlano)).Click();
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SituacaoPlano, "Campo Situação do Plano");
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

        Dsl.Clicar(webDriver, GlobalVariables.DataCancelamentoPlano, "Campo Data Cancelamento");
        Dsl.Clicar(webDriver, GlobalVariables.SelecionarDataCancelamentoPlano, "Botão Today Cancelamento");
        Dsl.Clicar(webDriver, GlobalVariables.OkCancelamento, "Botão OK Cancelamento");

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
        var quantidadeLinhasTabela = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaPlanos) - 1; //Contar linhas no elemento tbody da listagem de planos, ignorando a tag tr sem dados

        for (var i = 1; i <= quantidadeLinhasTabela; i++)
        {
            //webDriver.FindElement(By.XPath(GlobalVariables.ExcluirPlano)).Click();
            Dsl.Clicar(webDriver, GlobalVariables.ExcluirPlano, "Botão Excluir Plano");

            var mensagemAlertaExcluirPlanoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.ExcluirPlanoMensagemConfirmacao);
            ValidarMensagemDeSucessoEAlerta(mensagemAlertaExcluirPlanoAtual, mensagemAlertaExcluirPlanoEsperada);

            //webDriver.FindElement(By.XPath(GlobalVariables.OkExclusao)).Click();
            Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.OkExclusao, "Botão OK Exclusão");

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
        var quantidadeLojas = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.TabelaLojasPlano) - 1; //Contar linhas no elemento tbody da listagem de lojas na simulação do plano, ignorando a tag tr sem dados

        Debug.Assert(quantidadeAlertas == quantidadeLojas, "Quantidade de alertas não foram apresentadas corretamente");

        var mensagemAlertaAtual = Dsl.RemoverNumerosEspacosDeUmTexto(webDriver, GlobalVariables.MensagensDadosPlano);
        ValidarMensagemDeSucessoEAlerta(mensagemAlertaAtual, mensagemAlertaEsperada);

        return this;
    }
}