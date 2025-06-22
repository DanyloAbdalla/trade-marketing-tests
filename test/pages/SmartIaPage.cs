using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenQA.Selenium;

namespace MeuClienteWebTestProject;

/// <summary>
/// Classe com métodos específicos de manipulação\interação dos elementos, pertinentes a tela SmartIA da plataforma
/// </summary>
public class SmartIaPage
{
    private IWebDriver webDriver;
    private string[] nomesAtivos = { "Adesivo de Check Out", "Woobler", "Ponta de Gôndola" };

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
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.NovoRegistro, "Botão Nova Campanha");

        return this;
    }

    /// <summary>
    /// Método para preencher os campos obrigatórios na criação da campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherCamposCampanha(string nomeCampanha, string whatsAppResponsavel, string nomeResponsavel, string mensagemCabecalho)
    {
        Dsl.EsperarLoadDaTela(webDriver, GlobalVariables.LoadDeTelaSpiner);
        if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner) > 0)
            Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.MenuCampanhas, "Menu Suspenso Campanhas");
        Dsl.Esperar();

        CarregarImagemCampanha();
        PreencherNomeCampanha(nomeCampanha);
        PreencherInicioVigencia();
        PreencherFimVigencia();
        PreencherEmailResponsavel();
        PreencherWhatsAppResponsavel(whatsAppResponsavel);
        PreencherDataLimite();
        PreencherNomeResponsavel(nomeResponsavel);
        PreencherMensagemCabecalho(mensagemCabecalho);

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.MenuCampanhas, "Menu Suspenso Campanhas");

        return this;
    }

    /// <summary>
    /// Método para carregar a imagem da campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage CarregarImagemCampanha()
    {
        Dsl.CarregarImagens(webDriver, GlobalVariables.ImagemCampanha);

        return this;
    }

    /// <summary>
    /// Método para preencher o nome da campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherNomeCampanha(string nomeCampanha)
    {
        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.NomeCampanha, nomeCampanha);

        return this;
    }

    /// <summary>
    /// Método para preencher o início da vigência da campanha
    /// Avançando 2 meses
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherInicioVigencia()
    {
        Dsl.Clicar(webDriver, GlobalVariables.InicioVigenciaCampanha, "Campo Início Vigência");
        //Dsl.PreencherCalendariosInicioVigencia(webDriver, GlobalVariables.InicioVigenciaCampanhaAvancarData, 2);

        return this;
    }

    /// <summary>
    /// Método para preencher o fim da vigência da campanha
    /// Avançando 3 meses
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherFimVigencia()
    {
        Dsl.Clicar(webDriver, GlobalVariables.FimVigenciaCampanha, "Campo Fim Vigência");
        //Dsl.PreencherCalendariosFimVigencia(webDriver, GlobalVariables.FimVigenciaCampanhaAvancarData, 3);

        return this;
    }

    /// <summary>
    /// Método para preencher o email do responsável pela campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherEmailResponsavel()
    {
        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.EmailResposavelCampanha, GlobalVariables.emailUsuarioSemPlanta);

        return this;
    }

    /// <summary>
    /// Método para preencher o whatsapp do responsável pela campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherWhatsAppResponsavel(string whatsAppResponsavel)
    {
        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.WhatsAppResposavel, whatsAppResponsavel);

        return this;
    }

    /// <summary>
    /// Método para preencher a data limte da campanha
    /// Avançando 1 mês
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherDataLimite()
    {
        Dsl.Clicar(webDriver, GlobalVariables.DataLimiteCampanha, "Campo Data Limite Campanha");
        PreencherCalendarioDataLimite(GlobalVariables.DataLimiteCampanhaAvancarData, 1);

        return this;
    }

    /// <summary>
    /// Método para preencher o nome do responsável pela campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherNomeResponsavel(string nomeResponsavel)
    {
        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.NomeResposavelCampanha, nomeResponsavel);

        return this;
    }

    /// <summary>
    /// Método para preencher mensagem de cabeçalho da campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage PreencherMensagemCabecalho(string mensagemCabecalho)
    {
        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.MensagemCabecalhoCampanha, mensagemCabecalho);

        return this;
    }

    /// <summary>
    /// Método para abrir a edição de um plano existente
    /// </summary>
    /// <returns></returns>
    public SmartIaPage AbrirEdicaoDaCampanha()
    {
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.EditarCampanha, "Botão Editar Campanha");
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.MenuSuspensoVarejos);

        return this;
    }

    /// <summary>
    /// Método para abrir o menu suspenso Varejos
    /// </summary>
    /// <returns></returns>
    public SmartIaPage AbrirMenuSuspensoVarejos()
    {
        Dsl.Clicar(webDriver, GlobalVariables.MenuSuspensoVarejos, "Menu Suspenso Varejos");

        return this;
    }

    /// <summary>
    /// Método para validar que o varejo é exibido na campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage ValidarVarejoSelecionado()
    {
        AbrirMenuSuspensoVarejos();

        var varejoSelecionado = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.DisplayVarejoSelecionado);
        Debug.Assert(varejoSelecionado == 1, "Display com o varejo selecionado não foi apresentado na tela");

        return this;
    }

    /// <summary>
    /// Método para realizar a varredura dos ativos que estão com disponibilidade de inventário, para que possam ser reservados na campanha
    /// </summary>
    /// <returns></returns>
    public SmartIaPage RealizarVarredura()
    {
        var mensagemSucessoEsperada = "Campanhacriadacomsucesso!";

        Dsl.Esperar();
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.VarrerAtivos, "Botão Executar Varredura de Ativos");
        Dsl.EsperarVisibilidadeDoElemento(webDriver,  GlobalVariables.Mensagens);

        var texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.Mensagens, "Mensagem Realizar Varredura");
        var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(texto, "Mensagem Realizar Varredura");
        Dsl.ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);

        Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.Mensagens);
        Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.LoadDeTelaSpiner);

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

            Dsl.Clicar(webDriver, GlobalVariables.SelecionarAtivoCampanha, "Selecionar Ativo para Reservar");
            Dsl.Clicar(webDriver, GlobalVariables.ReservarQuantidadeAtivoLojasCampanha, "Reservar Ativo na Loja");
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
        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.ReservarAtivosCampanha, "Botão Reservar Ativos na Campanha");
        if (Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.AvisoInexistenciaDados) > 0)
            Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.AvisoInexistenciaDados);

        return this;
    }

    /// <summary>
    /// Método para reservar os ativos por loja
    /// </summary>
    /// <returns></returns>
    public SmartIaPage ReservarAtivoPorLojas(int quantidadeReserva)
    {
        Dsl.EsperarElementoFicarClicavel(webDriver, GlobalVariables.ReservarAtivoLojasCampanha, "Botão Reservar Quantidade");

        var quantidadeLojas = Dsl.ContarExistenciaDoElemento(webDriver, GlobalVariables.QuantidadeLojasReservaCampanha);
        var valorReservadoEsperado = quantidadeLojas * quantidadeReserva;

        Dsl.Clicar(webDriver, GlobalVariables.QuantidadeReservaLojasCampanha, "Campo Reservar Quantidade");
        Dsl.Esperar();
        Dsl.DigitarNoCampoTexto(webDriver, GlobalVariables.QuantidadeReservaLojasCampanha, quantidadeReserva.ToString());
        Dsl.Clicar(webDriver, GlobalVariables.ReservarAtivoLojasCampanha, "Botão Reservar Quandtidade");
        Dsl.Clicar(webDriver, GlobalVariables.FecharReservaAtivoLojasCampanha, "Botão Fechar Reserva");

        var tipoAtributo = "value";
        var valorReservadoAtual = Convert.ToInt16(Dsl.ObterDadosDoAtributoDoElemento(webDriver, GlobalVariables.QuantidadeReservadaAtivoCampanha, "Campo Quantidade Reservada Ativo", tipoAtributo));
        Debug.Assert(valorReservadoAtual == valorReservadoEsperado, "Quantidade de reservas calculada incorretamente");

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
    public SmartIaPage SalvarCampanha(string contextoDeExecucao)
    {
        var mensagemSucessoEsperada = "";

        if (contextoDeExecucao.Contains("NovaCampanha"))
            mensagemSucessoEsperada = "Campanhacriadacomsucesso!";
        if (contextoDeExecucao.Contains("EditarCampanha"))
            mensagemSucessoEsperada = "Campanhaeditadacomsucesso!";

        Dsl.Clicar(webDriver, GlobalVariables.SalvarRegistro, "Botão Salvar Campanha");
        Dsl.EsperarVisibilidadeDoElemento(webDriver,  GlobalVariables.Mensagens);

        var texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.Mensagens, "Mensagem Salvar/Editar Campanha");
        var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(texto, "Mensagem Salvar/Editar Campanha");
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

        Dsl.EsperarElementoParaClicar(webDriver, GlobalVariables.SalvarAtivosCampanha, "Botão Salvar Ativos Reservados");
        Dsl.EsperarVisibilidadeDoElemento(webDriver,  GlobalVariables.Mensagens);

        var texto = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.Mensagens, "Mensagem Salvar Ativos Reservados");
        var mensagemSucessoAtual = Dsl.RemoverNumerosEspacosDeUmTexto(texto, "Mensagem Salvar Ativos Reservados");

        Dsl.ValidarMensagemDeSucessoEAlerta(mensagemSucessoAtual, mensagemSucessoEsperada);
        Dsl.EsperarInvisibilidadeDoElemento(webDriver, GlobalVariables.Mensagens);

        return this;
    }

    /// <summary>
    /// Método para buscar as campanhas criadas
    /// </summary>
    /// <returns></returns>
    public SmartIaPage BuscarCampanhas([Optional] string nomeCampanha)
    {
        Dsl.EsperarVisibilidadeDoElemento(webDriver, GlobalVariables.StatusCampanha);
        Dsl.BuscarRegistros(webDriver, GlobalVariables.FiltrarCampanha, GlobalVariables.PesquisarCampanha, GlobalVariables.BuscarRegistro, nomeCampanha);

        return this;
    }

    /// <summary>
    /// Método para voltar na lista de campanhas do SmartIA
    /// </summary>
    /// <returns></returns>
    public SmartIaPage FecharCampanha()
    {
        Dsl.Clicar(webDriver, GlobalVariables.VoltarTela, "Botão Voltar na Edição da Campanha");

        return this;
    }

    /// <summary>
    /// Método para validar status na lista de campanhas, após criação ou alteração 
    /// </summary>
    /// <returns></returns>
    public SmartIaPage ValidarStatusDaCampanha(string statusCampanhaEsperado)
    {
        var statusCampanhaAtual = Dsl.ObterTextoDoElemento(webDriver, GlobalVariables.StatusCampanha, "Campo Status Campanha");
        Assert.That(statusCampanhaAtual, Does.Contain(statusCampanhaEsperado));

        return this;
    }
}