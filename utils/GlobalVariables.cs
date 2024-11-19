
using System.Runtime.InteropServices;

namespace MeuClienteWebTestProject;

public class GlobalVariables
{
    #region Projeto
    public static bool handLessMode = false; //Executa o Browser em tela se false
    public static bool devMode = false;
    public static bool hmlMode = true;
    public static bool prodMode = false;
    public static string urlDevPlataforma = "https://dev.meucliente.app.br/";
    public static string urlHmlPlataforma = "https://stage.meucliente.app.br/";
    public static string urlPrdPlataforma = "https://login.meucliente.app.br/";
    public static string emailUsuario = "homologacao@meucliente.app.br";
    public static string senhaUsuario = "Meucliente@hml@123";
    
    public static string nomeIndustria = "";
    #endregion

    #region Elementos de página - Elementos Comuns
    public static string Mensagens { get; set; } = "//*[@class='ant-message']//*[@class='ant-message-notice']";
    public static string NovoRegistro { get; set; } = "//button[@id='Buttonclass']";
    public static string SalvarRegistro { get; set; } = "//button/*[text()='Salvar']";
    public static string VoltarTela { get; set; } = "//button/*[text()='Voltar']";
    public static string FecharPlano { get; set; } = "//button/*[text()='Fechar']";
    public static string PreencherFiltro { get; set; } = "//*[@class='ant-table-filter-dropdown']//input";
    public static string BuscarRegistro { get; set; } = "//button/*[text()='Buscar']";
    public static string TabelaRegistros { get; set; } = "//tbody";
    #endregion

    #region Elementos de página - Login
    public static string PreencherUsuarioEmail { get; set; } = "//input[@id='username']";
    public static string PreencherUsuarioSenha { get; set; } = "//input[@id='password']";
    public static string SubmeterLogin { get; set; } = "//button/*[text()='Login']";
    #endregion

    #region Elementos de página - HomePage
    public static string MenuPrincipal { get; set; } = "//header/div[1]/div[1]/div/div";
    public static string MenuVarejo { get; set; } = "//div/span[text()='Varejo']";
    public static string MenuNegociacao { get; set; } = "//div/span[text()='Negociação']";
    public static string MenuCadastros { get; set; } = "//div/span[text()='Cadastros']";
    public static string CadastroPlanosContratos { get; set; } = "//div/span[text()='Plano']";
    public static string CadastroSmartIa { get; set; } = "//div/span[text()='SmartIa']";
    #endregion

    #region Elementos de Página - Planos
    public static string TabelaPlanos { get; set; } = "//table/tbody/tr";
    public static string FiltrarPlanoPorCampanha { get; set; } = "//thead//th[@title='Nome Campanha']//span[@role='button']";
    public static string PesquisarNomeCampanha { get; set; } = "//div[@class='ant-table-filter-dropdown']//input";
    public static string ColunaFimVigencia { get; set; } = "//*[text()='Fim Vigência']";
    public static string StatusPlano { get; set; } = "//tbody/tr[2]/td[6]";
    public static string FarolPlano { get; set; } = "//tbody/tr[2]/td[7]/div";
    public static string EditarPlano { get; set; } = "//tbody/tr[2]/td[23]/div[1]/button[1]";
    public static string ExcluirPlano { get; set; } = "//table/tbody/tr[2]/td[23]/div//button//*[@aria-label='delete']";
    public static string ExcluirPlanoMensagemConfirmacao { get; set; } = "//*[@class='ant-modal-confirm-body']/span[2]";
    public static string OkExclusao { get; set; } = "//*[text()='OK']";
    #endregion

    #region Elementos de página - Planos - Novo Plano
    public static string CampoIndustria { get; set; } = "//div[@label='Indústria']";
    public static string PesquisarIndustria { get; set; } = "//div[@label='Indústria']//input[@type='search']";
    public static string SelecionarIndustria { get; set; } = "//div[@title='Indústria 01 F']";
    public static string PreencherCampanha { get; set; } = "//input[@name='NomeCampanha']";
    public static string InicioVigenciaNovoPlano { get; set; } = "(//div[contains(@class,'date-picker')]//input)[1]";
    public static string FimVigenciaNovoPlano { get; set; } = "(//div[contains(@class,'date-picker')]//input)[2]";
    public static string AvancarCalendarioMesInicioVigencia { get; set; } = "(//*[contains(@class,'header-next-btn')])[1]";
    public static string AvancarCalendarioMesFimVigencia { get; set; } = "(//*[contains(@class,'header-next-btn')])[2]";
    public static string FecharDetalhamento { get; set; } = "//button/*[text()='Fechar Detalhamento']";
    public static string SelecionarAtivos { get; set; } = "//button/*[text()='Selecionar Ativos']";
    public static string CarregarLojas { get; set; } = "//button/*[text()='Carregar Lojas']";
    public static string AlertaInventario { get; set; } = "//td[9]//button[contains(@class,'btn-dangerous')]";
    public static string GerarPrePlano { get; set; } = "//button/*[text()='Gerar Pré-Plano']";
    public static string MensagensDadosPlano { get; set; } = "//*[@class='form-action'][2]/span[3]";
    #endregion

    #region Elementos de página - Planos - Novo Plano - Selecionar Ativos
    public static string FiltrarAtivos { get; set; } = "//*[contains(@style,'width: 60')]//div[@class='ant-modal-body']//div[@class='ant-tabs-content-holder']//div[@class='ant-table-wrapper']//table/thead//tr/th[2]/div/span[@role='button']";
    public static string PesquisarAtivos { get; set; } = "//div[@class='ant-table-filter-dropdown']//span[@class='ant-input-affix-wrapper ant-table-filter-dropdown-search-input']/input";
    public static string SelecionarAtivosFiltro { get; set; } = "//li[@class='ant-dropdown-menu-item']";
    public static string TelaFiltrarAtivo { get; set; } = "//div[@class='ant-table-filter-dropdown-btns']";
    public static string OkFiltroAtivos { get; set; } = "//button/*[text()='OK']";
    public static string SelecionarTodosAtivos { get; set; } = "(//span[@class='ant-checkbox'])[1]";
    public static string ResetarFiltroAtivo { get; set; } = "//button/*[text()='Resetar']";
    public static string AplicarAtivos { get; set; } = "//*[contains(@style,'width: 60')]//button[@class='ant-btn ant-btn-primary']";
    #endregion

    #region Elementos de página - Planos - Novo Plano - Selecionar Lojas
    public static string MenuLojas { get; set; } = "//div[text()='Selecione as lojas: ']";
    public static string TabelaLojasPlano { get; set; } = "(//table/tbody)[3]/tr";
    public static string SelecionarLojas { get; set; } = "//div[@class='ant-spin-container']/div[3]//div[@class='ant-collapse-content ant-collapse-content-active']//tbody/tr[2]//input";
    #endregion

    #region Elementos de página - Planos - Dados do Plano
    public static string AbasPlano { get; set; } = "//div[@class='ant-tabs-nav-list']";
    public static string AbaDadosPlano { get; set; } = "//div[@class='ant-tabs-nav-list']//*[contains(text(),'Dados do Plano')]";
    public static string SituacaoPlano { get; set; } = "//*[@name='Status']";
    public static string Desconto { get; set; } = "//*[@name='Desconto']";
    public static string InicioVigenciaEditarPlano { get; set; } = "//form[@class='ant-form ant-form-vertical']//div[5]//div[contains(@class,'date-picker')]/div";
    public static string FimVigenciaEditarPlano { get; set; } = "//form[@class='ant-form ant-form-vertical']//div[6]//div[contains(@class,'date-picker')]/div";
    public static string AvancarCalendarioMes { get; set; } = "//*[contains(@class,'header-next-btn')]";
    public static string TipoCampanha { get; set; } = "//*[@name='SubTipoFornecedorId']";
    public static string SelecionarTipoCampanha { get; set; } = "//*[text()='Tipo Campanha 01']";
    public static string QuantidadeParcelas { get; set; } = "//*[@name='QuantidadeParcelas']";
    public static string Setor { get; set; } = "//*[@name='SetorId']//input";
    public static string SelecionarSetor { get; set; } = "(//*[text()='Geral'])[2]";
    public static string Departamento { get; set; } = "//*[@name='DepartamentoId']//input";
    public static string SelecionarDepartamento { get; set; } = "(//*[text()='Geral'])[3]";
    public static string Categoria { get; set; } = "//*[@name='CategoriaId']//input";
    public static string SelecionarCategoria { get; set; } = "(//*[text()='Geral'])[5]";
    public static string DataCancelamentoPlano { get; set; } = "//div[contains(@class,'modal-confirm-content')]//input";
    public static string SelecionarDataCancelamentoPlano { get; set; } = "//*[text()='Today']";
    public static string OkCancelamento { get; set; } = "//*[text()='OK']";
    public static string FecharPlanoConfirmacao { get; set; } = "//button/*[text()='Fechar mesmo assim']";
    #endregion

    #region Elementos de página - Planos - Ativos Alocados
    public static string AbaAtivosAlocados { get; set; } = "//div[@class='ant-tabs-nav-list']//*[contains(text(),'Ativos Alocados')]";
    public static string TabelaAtivosAlocados { get; set; } = "//div[@class='ant-modal-content']//tbody";
    #endregion

    #region Elementos de página - Planos - Ativos Alocados - Editar Alocação do Ativo por Loja
    public static string TabelaLojasAtivoAlocados {get; set; } = "(//tbody)[3]/tr[3]";
    public static string AplicarDadosLojas { get; set; } = "(//button/*[text()='Aplicar'])[2]";
    public static string BuscarAtivoAlocacao { get; set; } = "//span[contains(text(),'Selecione um Ativo')]/div/div//input";
    public static string SelecionarAtivoAlocacao { get; set; } = "//div[@class='rc-virtual-list']//*[text()='Cestão 01 - ']";
    public static string IncluirAlocacaoAtivo { get; set; } = "//button/*[text()='Incluir Ativo']";
    public static string QuantidadeLojasPorAtivo { get; set; } = "//*[contains(text(),'Total de lojas')]";
    public static string SalvarAlocacaoLoja { get; set; } = "(//button/*[text()='Salvar'])[2]";
    public static string FecharAlocacaoAtivoPorLoja { get; set; } = "(//button/*[text()='Fechar'])[2]";
    public static string MensagemSucessoAlocacaoAtivo { get; set; } = "//*[contains(text(), 'Alocação atualizada')]";
    #endregion

    #region Elementos de página - SmartIA
    public static string StatusCampanha { get; set; } = "//tbody/tr[2]/td[7]/div";
    public static string FiltrarCampanha { get; set; } = "//*[text()='Campanha']/following-sibling::span[@role='button']";
    public static string PesquisarCampanha { get; set; } = "//div[@class='ant-table-filter-dropdown']//input";
    #endregion

    #region Elementos de página - SmartIA - Nova Campanha
    public static string Campanhas { get; set; } = "//span[text()='Campanhas']";
    public static string CarregarImagem { get; set; } = "//input[@type='file']";
    public static string NomeCampanha { get; set; } = "//*[@name='Nome']";
    public static string EmailResposavel { get; set; } = "//*[@name='EmailResponsavel']";
    public static string NomeResposavel { get; set; } = "//*[@name='NomeResponsavel']";
    public static string InicioVigenciaCampanha { get; set; } = "(//div[contains(@class,'date-picker')])[1]";
    public static string InicioVigenciaCampanhaAvancarData { get; set; } = "(//*[contains(@class,'header-next-btn')])[1]";
    public static string FimVigenciaCampanha { get; set; } = "(//div[contains(@class,'date-picker')])[2]";
    public static string FimVigenciaCampanhaAvancarData { get; set; } = "(//*[contains(@class,'header-next-btn')])[2]";
    public static string WhatsAppResposavel { get; set; } = "//*[@name='WhatsAppResponsavel']";
    public static string DataLimiteCampanha { get; set; } = "(//div[contains(@class,'date-picker')])[3]";
    public static string DataLimiteCampanhaAvancarData { get; set; } = "(//*[contains(@class,'header-next-btn')])[3]";
    public static string MensagemCabecalhoCampanha { get; set; } = "//*[@name='Cabecalho']";
    #endregion

    #region Elementos de página - SmartIA - Nova Campanha - Selecionar Varejo e Varrer Ativos
    public static string EditarCampanha { get; set; } = "//tbody/tr[2]/td[9]/div[1]/button[1]";
    public static string MenuSuspensoVarejos { get; set; } = "//*[text()='Varejos']";
    public static string PesquisarVarejo { get; set; } = "//div/*[text()='Adicionar Varejo:']/following-sibling::div//input";
    public static string SelecionarVarejo { get; set; } = "//div[@title='Meu Cliente']";
    public static string AdicionarVarejo { get; set; } = "(//form/div/div[2]//button)[1]";
    public static string VarejoSelecionado { get; set; } = "//div[contains(@style,'display')]//span[text()='Meu Cliente']";
    public static string VarrerAtivos { get; set; } = "//*[@class='button-varredura-ativo']";
    #endregion

    #region Elementos de página - SmartIA - Nova Campanha - Selecionar Ativos
    public static string SelecionarAtivosCampanha { get; set; } = "//div[contains(@style,'display')]//span[text()='Meu Cliente']/../..//*[@aria-label='appstore']";
    public static string SalvarAtivosCampanha { get; set; } = "(//button/*[text()='Salvar'])[2]";
    public static string FiltrarAtivosCampanha { get; set; } = "(//*[text()='Ativo'])[3]/..//*[@aria-label='search']";
    public static string PreencherFiltroAtivosCampanha { get; set; } = "(//*[text()='Ativo'])[3]/..//*[@aria-label='search']";
    public static string BuscarAtivosCampanha { get; set; } = "(//*[text()='Ativo'])[3]/..//*[@aria-label='search']";
    public static string SelecionarAtivoCampanha { get; set; } = "(//input[@class='ant-checkbox-input'])[2]";
    public static string ReservarQuantidadeAtivoLojasCampanha { get; set; } = "//tbody/tr/td[4]/span";
    public static string PreencherReservarTodasLojasCampanha { get; set; } = "//*[text()='Reservar para Todos:']/../input";
    public static string ReservarAtivoLojasCampanha { get; set; } = "//button/*[text()='Reservar']";
    public static string FecharReservaAtivoLojaCampanha { get; set; } = "(//button/*[text()='Fechar'])[2]";
    public static string QuantidadeReservadaAtivoCampanha { get; set; } = "//tbody/tr/td[5]/input";
    public static string QuantidadeLojasReservaCampanha { get; set; } = "(//tbody)[2]";
    #endregion
}