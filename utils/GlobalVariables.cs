namespace MeuClienteWebTestProject;

public class GlobalVariables
{
    #region Projeto
    public static bool handLessMode = false; //Executa, mostrando o Browser na tela, se handLessMode igual false
    public static bool devMode = false;
    public static bool hmlMode = true;
    public static bool prodMode = false;
    public static string urlDevPlataforma = "https://dev.meucliente.app.br/";
    public static string urlHmlPlataforma = "https://stage.meucliente.app.br/";
    public static string urlPrdPlataforma = "https://login.meucliente.app.br/";
    public static string[] clienteUpsell = { "ClienteStart", "ClientePro", "ClienteExpert" };
    public static string[] emailUsuarios = { "homologacao.start@meucliente.app.br", "homologacao.pro@meucliente.app.br", "homologacao.expert@meucliente.app.br" };
    public static string[] senhaUsuarios = { "Meucliente@st@123", "Meucliente@pr@123", "Meucliente@ex@123" };
    public static string emailUsuarioSemPlanta = "homologacao.sp@meucliente.app.br";
    public static string senhaUsuarioSemPlanta = "Meucliente@hml@123";
    public static string emailUsuarioComPlanta = "homologacao.cp@meucliente.app.br";
    public static string senhaUsuarioComPlanta = "Meucliente@hml@123";
    #endregion

    #region Elementos de página - Elementos Comuns
    public static string MenuUsuarioLogado { get; set; } = "//header/div[@class='Log']/div[2]";
    public static string SairConta { get; set; } = "//*[contains(@class,'logout')]/ancestor::a/*[text()='Sair']";
    public static string Mensagens { get; set; } = "//*[@class='ant-message-notice']";
    public static string MensagemDeFeedback { get; set; } = "//div[contains(@data-testid, 'Mc-message')]";
    public static string NovoRegistro { get; set; } = "//button[@id='Buttonclass']";
    public static string SalvarRegistro { get; set; } = "//*[@data-testid='salvarPlano']";
    public static string VoltarTela { get; set; } = "//button/*[text()='Voltar']";
    public static string FecharTela { get; set; } = "//button/*[text()='Fechar']";
    public static string PreencherFiltro { get; set; } = "//*[@class='ant-table-filter-dropdown']//input";
    public static string BuscarRegistro { get; set; } = "//button/*[text()='Buscar']";
    public static string LimparRegistro { get; set; } = "//button/*[text()='Limpar']";
    public static string PaginacaoTela { get; set; } = "//ul[contains(@class,'ant-pagination')]//li[2]";
    public static string AvisoInexistenciaDados { get; set; } = "//*[text()='No data']";
    public static string LoadDeTelaSpiner { get; set; } = "(//*[contains(@class,'ant-spin-dot-item')])[1]";
    public static string LoadDeTelaBarra { get; set; } = "(//span[@class='anticon anticon-loading anticon-spin'])[2]";
    public static string RecarregarTela { get; set; } = "//button/*[text()='Recarregar tela']";
    public static string TituloModalConfirmacao { get; set; } = "//span[@class='ant-modal-confirm-title']";
    public static string CancelarAcao { get; set; } = "//button/*[text()='Cancelar']";
    public static string AvancarMesesCalendariosBotton { get; set; } = "//div[@class='ant-picker-dropdown ant-picker-dropdown-placement-bottomLeft ']//*[@class='ant-picker-header-next-btn']";
    public static string AvancarMesesCalendariosTop { get; set; } = "//div[@class='ant-picker-dropdown ant-picker-dropdown-placement-topLeft ']//*[@class='ant-picker-header-next-btn']";
    #endregion

    #region Elementos de página - Login
    public static string LoadCarregandoPaginaSecreta { get; set; } = "//div[text()='Carregando...']";
    public static string PreencherUsuarioEmail { get; set; } = "//input[@id='username']";
    public static string PreencherUsuarioSenha { get; set; } = "//input[@id='password']";
    public static string SubmeterLogin { get; set; } = "//button/*[text()='Login']";
    #endregion

    #region Elementos de página - HomePage
    public static string MenuPrincipal { get; set; } = "//header/div[1]/div[1]/div/div";
    public static string UltimoCadastroAcessado { get; set; } = "//header/div[1]/div/span";
    public static string MenuVarejo { get; set; } = "//div/span[text()='Varejo']";
    public static string MenuGestao { get; set; } = "//div/span[text()='Gestão']";
    public static string DashboardOperacoes { get; set; } = "//div/span[text()='Dashboard Operação']";
    public static string MenuNegociacao { get; set; } = "//div/span[text()='Negociação']";
    public static string CadastroPlanosContratos { get; set; } = "//div/span[text()='Plano']";
    public static string MenuCadastros { get; set; } = "//div/span[text()='Cadastros']";
    public static string CadastroSmartIa { get; set; } = "//div/span[text()='SmartIa']";
    #endregion

    #region Elementos de página - Dashboard de Operações
    public static string TextoCardAtivosAlocados { get; set; } = "//div[@class='DashBoardCards']//*[contains(text(),'Índice de Aproveitamento')]";
    public static string DetalhesLojasAtivas { get; set; } = "//span[text()='Lojas Ativas']/following-sibling::div//button";
    public static string TabelaListagemLojasAtivas { get; set; } = "//*[contains(text(),'Listagem de Lojas')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr";
    public static string DetalhesDisponibilidadeAtivos { get; set; } = "//*[contains(text(),'Ativos Alocados')]/following-sibling::div//button[1]";
    public static string ColunaAtivoListagemDisponibilidadeAtivos { get; set; } = "//*[contains(text(),'Disponibilidade de Ativos')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[1]";
    public static string ColunaQtdLojasListagemDisponibilidadeAtivos { get; set; } = "//*[contains(text(),'Disponibilidade de Ativos')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[2]/button";
    public static string ColunaDisponibilidadeCalendariosAtivo { get; set; } = "//*[contains(text(),'Calendários do Ativo')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[1]/td[3]/button";
    public static string TabelaDisponibilidadeAtivoPorLoja { get; set; } = "//*[contains(text(),'Ativos Disponiveis')]/ancestor::div[@class='ant-modal-content']//div[@class='ant-table-wrapper']";
    public static string TabelaDisponibilidadeCalendariosAtivo { get; set; } = "//*[contains(text(),'Calendários do Ativo')]/ancestor::div[@class='ant-modal-content']/div//div[@class='ant-tabs-content-holder']";
    public static string FecharTelaCalendariosAtivo { get; set; } = "//*[contains(text(),'Calendários do Ativo')]/ancestor::div[@class='ant-modal-content']//button/*[text()='Fechar']";
    public static string FecharTelaDisponibilidadeAtivosPorLoja { get; set; } = "//*[contains(text(),'Ativos Disponiveis')]/ancestor::div[@class='ant-modal-content']//button/*[text()='Fechar']";
    public static string DetalhesNegociacaoAtivos { get; set; } = "//*[contains(text(),'Ativos Alocados')]/following-sibling::div//button[2]";
    public static string ColunaAtivoListagemAtivosNegociados { get; set; } = "//*[contains(text(),'Ativos Negociados')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[1]";
    public static string ColunaDetalhesBotaoContratosVinculado { get; set; } = "//*[contains(text(),'Ativos Negociados')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[11]/button";
    public static string FiltrarAtivoPorNome { get; set; } = "//span[text()='Ativo']/../../../..//span[@role='button']";
    public static string DetalhesPotencialReceitaAtivos { get; set; } = "//*[contains(text(),'Ativos Alocados')]/following-sibling::div//button[3]";
    public static string FiltrarAtivoPorNomePotencialReceita { get; set; } = "//th[@aria-label='Nome']/div/span[@role='button']";
    public static string ColunaNomeListagemPotencialReceita { get; set; } = "//*[contains(text(),'Potencial Receita')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[1]";
    public static string ColunaDetalhesListagemPotencialReceita { get; set; } = "(//tbody)[9]/tr[2]/td[10]/button";
    public static string FecharTelaContratosEAtivosVinculados { get; set; } = "(//button/*[text()='Fechar'])[2]";
    public static string DetalhesContratosAtivos { get; set; } = "//*[contains(text(),'Contratos Vigentes')]/following-sibling::div//button[1]";
    public static string TabelaListagemContratosAtivos { get; set; } = "//*[contains(text(),'Contratos Ativos')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr";
    public static string ColunaContratoListagemContratosAtivos { get; set; } = "//*[contains(text(),'Contratos Ativos')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[2]";
    public static string ColunaAcoesBotaoListagemContratosAtivos { get; set; } = "//*[contains(text(),'Contratos Ativos')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[9]";
    public static string DetalhesContratosVencendo { get; set; } = "//*[contains(text(),'Contratos Vigentes')]/following-sibling::div//button[2]";
    public static string ColunaContratoListagemContratosVencendo { get; set; } = "//*[contains(text(),'Contratos Vencendo')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[2]";
    public static string ColunaAcoesBotaoListagemContratosVencendo { get; set; } = "//*[contains(text(),'Contratos Vencendo')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[9]";
    public static string FiltrarContratoPorCampanha { get; set; } = "//span[text()='Contrato']/following-sibling::span[@role='button']";
    public static string DetalhesContratosAtivosTotalReceita { get; set; } = "//*[contains(text(),'Total de Receita')]/following-sibling::div//button[1]";
    public static string TextoCardTotalReceita { get; set; } = "//div[@class='DashBoardCards']//*[contains(text(),'índice de Crescimento')]";
    public static string TabelaListagemAterrissagem { get; set; } = "//*[text()='Listagem de Aterrissagem receita']/../../../../../../..//table";
    public static string PaginacaoTelaListagemAterrissagem { get; set; } = "//*[text()='Listagem de Aterrissagem receita']/../../../../../../..//ul[contains(@class,'ant-pagination')]//li[2]";
    public static string ColunaMesAterrissagem { get; set; } = "//*[text()='JANEIRO']";
    public static string DetalhesAterrissagemReceita { get; set; } = "(//*[contains(text(),'Perfomance Receita')]/following-sibling::div//button[1])[1]";
    public static string DetalhesAterrissagemReceitaPorBandeira { get; set; } = "(//*[contains(text(),'Receita Bandeira')]/following-sibling::div//button[1])[1]";
    public static string DetalhesAterrissagemReceitaPorTipoFornecedor { get; set; } = "(//*[contains(text(),'Receita Tipo Fornecedor')]/following-sibling::div//button[1])[1]";
    public static string TabelaListagemParceiros { get; set; } = "//*[text()='Investimento Parceiro']/../../../../../../..//table";
    public static string PaginacaoTelaListagemParceiros { get; set; } = "//*[text()='Investimento Parceiro']/../../../../../../..//ul[contains(@class,'ant-pagination')]//li[2]";
    public static string ColunaIndustriaParcerio { get; set; } = "//*[text()='Indústria/Parceiro']";
    public static string DetalhesListaPerformanceFornecedor { get; set; } = "(//*[contains(text(),'Perfomance Fornecedor')]/following-sibling::div//button[1])[1]";
    public static string FiltrarNegociacoes { get; set; } = "//button/span[text()='Filtrar']";
    public static string DetalhesListaInvestimentoParceiro { get; set; } = "(//*[contains(text(),'Investimento por Parceiro')]/following-sibling::div//button[1])[1]";
    public static string DetalhesDesempenhoPorLoja { get; set; } = "(//*[contains(text(),'Desempenho por Loja')]/following-sibling::div//button[1])[1]";
    public static string GraficoDesempenhoLoja { get; set; } = "//*[@class='ant-modal-content']//*[@class='Chart']";
    public static string FiltroDesempenhoLoja { get; set; } = "//*[@class='ant-modal-content']//*[text()='Maior Retorno']";
    public static string DetalhesDesempenhoDosAtivos { get; set; } = "(//*[contains(text(),'Desempenho de Ativos')]/following-sibling::div//button[1])[1]";
    public static string GraficoDesempenhoAtivo { get; set; } = "//*[@class='ant-modal-content']//*[@class='Chart']";
    public static string FiltroDesempenhoAtivo { get; set; } = "//*[@class='ant-modal-content']//*[text()='Maior Retorno']";
    public static string TextoCardMaisVendidosDepartamento { get; set; } = "//*[text()='Mais vendidos do Departamento']";
    public static string ModalDashboardDetalhesCards { get; set; } = "//*[@class='ant-modal-content']";
    public static string LoadCarregandoDashboard { get; set; } = "//div[text()='Carregando dashboard...']";
    #endregion

    #region Elementos de página - Planos
    public static string LoadCarregandoPlanos { get; set; } = "//div[text()='Carregando cadastro contrato...']";
    public static string LoadListaPlanos { get; set; } = "//h1[text()='Gestão de Planos']/../../../../..//*[@class='ant-spin-dot ant-spin-dot-spin']/i[1]";
    public static string LoadModalPlano { get; set; } = "//div[contains(@class,'contrato-loading-overlay')]";
    public static string TabelaPlanos { get; set; } = "//table/tbody/tr";
    public static string FiltrarPlanosStatusVigencia { get; set; } = "//div[contains(@class,'select-selector')]/span[2]";
    public static string SelecionarTodosPlanos { get; set; } = "//div[@class='rc-virtual-list']//div[@title='Todos']";
    public static string FiltrarPlanoPorCampanha { get; set; } = "//thead//th[@title='Nome Campanha']//span[@role='button']";
    public static string PesquisarNomeCampanha { get; set; } = "//div[@class='ant-table-filter-dropdown']//input";
    public static string ColunaFimVigencia { get; set; } = "//*[text()='Fim Vigência']";
    public static string ColunasPlanosCadastrados { get; set; } = "//*[text()='Gestão de Planos']/../../../../../../../../../..//*[@class='ant-table-header']//thead/tr";
    public static string StatusPlano(int index) { return $"//tbody/tr[2]/td[{index}]"; }
    public static string FarolPlano(int index) { return $"//tbody/tr[2]/td[{index}]/div"; }
    public static string CadastrarPlano { get; set; } = "//*[@data-testid='cadastrarPlano']";
    public static string EditarPlano(string nomePlano) { return $"//*[@data-testid='editarPlano-{nomePlano}']"; }
    public static string ExcluirPlano(string nomePlano) { return $"//*[@data-testid='excluirPlano-{nomePlano}']"; }
    public static string ExcluirPlanoMensagemConfirmacao { get; set; } = "//*[@class='ant-modal-confirm-body']/span[2]";
    public static string OkExclusao { get; set; } = "//*[text()='OK']";
    public static string ModalPlanos { get; set; } = "//*[@class='ant-modal-body' and @style='overflow-x: hidden; height: 85vh;']";
    public static string AbaPlano { get; set; } = "//*[contains(text(),'Dados do Plano')]";
    #endregion

    #region Elementos de página - Planos - Simulação
    public static string PreencherIndustria { get; set; } = "//*[@data-testid='nomeIndustria']//*/input";
    public static string SelecionarIndustriaClienteStart { get; set; } = "//div[contains(@class,'ant-select-dropdown')]//*[@id='116873']";
    public static string SelecionarIndustriaClientePro { get; set; } = "//div[contains(@class,'ant-select-dropdown')]//*[@id='73189']";
    public static string SelecionarIndustriaClientExpert { get; set; } = "//div[contains(@class,'ant-select-dropdown')]//*[@id='76331']";
    public static string SelecionarIndustriaSemPlanta { get; set; } = "//div[contains(@class,'ant-select-dropdown')]//*[@id='73189']";
    public static string SelecionarIndustriaComPlanta { get; set; } = "//div[contains(@class,'ant-select-dropdown')]//*[@id='76331']";
    public static string PreencherCampanha { get; set; } = "//*[@data-testid='nomeCampanha']";
    public static string InicioVigenciaSimulacao { get; set; } = "//input[@data-testid='inicioVigencia']";
    public static string FimVigenciaSimulacao { get; set; } = "//input[@data-testid='fimVigencia']";
    public static string AvancarCalendarioMesInicioVigencia { get; set; } = "(//*[contains(@class,'header-next-btn')])[1]";
    public static string AvancarCalendarioMesFimVigencia { get; set; } = "(//*[contains(@class,'header-next-btn')])[2]";
    public static string FecharDetalhamento { get; set; } = "//button/*[text()='Fechar Detalhamento']";
    public static string SelecionarAtivos { get; set; } = "//*[@data-testid='selecionarAtivos']";
    public static string FiltrarInvetario { get; set; } = "//button/*[text()='Filtrar Inventários']";
    public static string AceleradorQuantidadeAlocarSimulacao { get; set; } = "//*[contains(text(),'Simulação')]/../../../../..//*[text()='Selecione a quantidade de ativos por loja:']/../..//*[@title='Qtd por Loja']/../..//input";
    public static string AplicarAceleradorPorLojaSimulacao { get; set; } = "//*[contains(text(),'Simulação')]/../../../../..//*[text()='Selecione a quantidade de ativos por loja:']/../..//button/*[text()='Aplicar']";
    public static string TipoMidia { get; set; } = "//span[text()='Tipo Mídia']/../../..//input";
    public static string Loja { get; set; } = "//span[text()='Loja']/../../..//input";
    public static string SelecionarTipoMidiaGrafica { get; set; } = "//div[@class='rc-virtual-list']//*[text()='Gráfica']";
    public static string SelecionarTipoMidiaFisica { get; set; } = "//div[@class='rc-virtual-list']//*[text()='FISICA']";
    public static string CarregarLojas { get; set; } = "//button[@data-testid='carregarLojas']";
    public static string InventarioAlerta { get; set; } = "//td[9]//*[contains(@data-testid,'inventarioIndisponivel')]";
    public static string InventarioOk { get; set; } = "//td[9]//*[contains(@data-testid,'inventarioDisponivel')]";
    public static string GerarPrePlano { get; set; } = "//*[@data-testid='gerarPrePlano']";
    public static string GerarPrePlanoComWorkflowSelecionado { get; set; } = "//*[@data-testid='gerarPlano']";
    public static string CancelarPrePlanoComWorkflowSelecionado { get; set; } = "//*[@data-testid='cancelarPlano']";
    public static string PesquisarUsuarioResponsavelEtapaWorkflow { get; set; } = "//*[contains(text(),'Selecione o usuário')]/parent::div/div/div/div/div/div/div/div/span[2]";
    public static string PreencherUsuarioResponsavelEtapaWorkflow { get; set; } = "//*[@data-testid='selecionarResponsavel']//*/input";
    public static string SelecionarUsuarioResponsavelEtapaWorkflowPro { get; set; } = "//div[@class='rc-virtual-list']//*[text()='UserHomolog02Pro']";
    public static string SelecionarUsuarioResponsavelEtapaWorkflowExpert { get; set; } = "//div[@class='rc-virtual-list']//*[text()='UserHomolog02Expert']";
    public static string LoadProcurandoEtapa { get; set; } = "//div[text()='Procurando Etapa...']";
    public static string MensagemIndisponibilidadeInventario { get; set; } = "//*[@class='form-action'][2]/div/span[3]";
    #endregion

    #region Elementos de página - Planos - Nova Tela de Simulação
    public static string FiltrarPorDisponibilidade { get; set; } = "//*[text()='Filtrar por: ']/../..//span/input";
    public static string SelecionarFiltroDisponibilidadeOcupado { get; set; } = "//*[@class='rc-virtual-list']//*[text()='Ocupado']";
    public static string FiltrarInventarios { get; set; } = "//*[@data-testid='filtrarInventarios']";
    public static string FiltroTipoMidia { get; set; } = "//*[@data-testid='filtroTipoMidia']//input";
    public static string FiltroLoja { get; set; } = "//*[@data-testid='filtroLoja']//input";
    public static string FiltroLojaPreenchido { get; set; } = "//*[@class='ant-select-selection-item' and contains(@title,'Loja')]";
    public static string FiltroAtivos { get; set; } = "//*[@data-testid='filtroAtivos']//input";
    public static string ConfirmarFiltroInventario { get; set; } = "//*[@data-testid='confirmar']";
    public static string SelecionarLojasInventario(string nomeLoja) { return $"//div[@class='rc-virtual-list']//*[contains(text(),'{nomeLoja}')]"; }
    public static string SelecionarAtivosInventario(string nomeAtivo) { return $"//div[@class='rc-virtual-list']//*[text()='{nomeAtivo}']"; }
    public static string AlocarAtivo(string nomeLoja, string inventario) { return $"//*[@data-testid='alocar-{nomeLoja} {inventario}']"; }
    public static string AlocarAtivoOcupado { get; set; } = "(//*[@class='react-window-table-cell']//*[@class='anticon anticon-question-circle'])[1]";
    public static string AlocarTodosAtivos { get; set; } = "//*[@data-testid='alocarTodos']";
    public static string QuantidadeItensAtivo { get; set; } = "//*[@data-testid='configurarQuantidadeItens']";
    public static string ReplicarConfiguracoes { get; set; } = "//*[@data-testid='replicarConfiguracoes']";
    public static string MaisInformacoesPlano { get; set; } = "//*[@class='ant-collapse-header-text' and text()='Mais Informações do Plano']";
    public static string ModalInventarioIndisponivel { get; set; } = "//*[@class='ant-modal-confirm-body']//*[@aria-label='close-circle']";
    public static string ModalInventarioIndisponivelOKButton { get; set; } = "//*[@class='ant-modal-confirm-btns']//button";
    public static string NovaSimulacaoTabSimulacao { get; set; } = "//div[@data-node-key='1']";
    public static string NovaSimulacaoTabSelecionados { get; set; } = "//div[@data-node-key='2']";
    public static string AtivosSelecionadosAlocacao { get; set; } = "//*[text()='Selecionados']";
    public static string EditarAtivoGraficoAlocacaoSimulacao { get; set; } = "//*[@data-testid='editarAlocacao-Loja 01 - E01 - Adesivo de Check Out']";
    public static string EditarAtivoFisicoAlocacaoSimulacao { get; set; } = "//*[@data-testid='editarAlocacao-Loja 01 - E01 - Cestão 01']";
    public static string ColunasAtivoAlocadoSimulado { get; set; } = "//*[text()='Vigências Configuradas']/../../../../../../..//*[@class='ant-table-header']//thead/tr";
    public static string EditarAtivoGraficoAlocadoSimulado { get; set; } = "//*[@data-testid='editar-Loja 01 - E01 - Adesivo de Check Out']";
    public static string EditarAtivoFisicoAlocadoSimulado { get; set; } = "//*[@data-testid='editar-Loja 01 - E01 - Cestão 01']";
    public static string QuantidadeAtivoAlocadoModalInfos(int posicaoColuna) { return $"//*[contains(@class,'matriz-infos-modal')]//div[@class='ant-table-body']//tbody//tr//td[{posicaoColuna}]//input"; }
    public static string SpanQuantidadeAtivosSelecionados { get; set; } = "//*[@class='estatisticas-superiores']//span[3]";
    public static string MatrizSimulacaoVazia { get; set; } = "//*[@class='matriz-simulacao-empty-state']";
    public static string MatrizSimulacao { get; set; } = "//*[@class='matriz-simulacao-container']";
    public static string MatrizSimulacaoCarregandoDados { get; set; } = "//*[@class='matriz-simulacao-container']//*[@class='loading-text']";
    public static string MensagemAvisoInventarioIndisponivel { get; set; } = "(//*[@class='matriz-simulacao-empty-state']//*[@class='ant-space-item'])[1]/div/div";
    public static string MensagemAvisoAtivoOcupado { get; set; } = "//*[@class='ant-tooltip-inner']";
    #endregion

    #region Elementos de página - Planos - Novo Plano - Selecionar Ativos
    public static string FiltrarAtivos { get; set; } = "//*[@data-testid='filtroInventarioItem']";
    public static string PesquisarAtivos { get; set; } = "//div[@class='ant-table-filter-dropdown']/div/span/input";
    public static string SelecionarAtivosFiltro { get; set; } = "//li[@class='ant-dropdown-menu-item']";
    public static string TelaFiltrarAtivo { get; set; } = "//div[@class='ant-table-filter-dropdown-btns']";
    public static string TelaFiltrarInventario { get; set; } = "(//*[@class='ant-modal-content'])[2]";
    public static string OkFiltroAtivos { get; set; } = "//button/*[text()='OK']";
    public static string SelecionarTodosAtivos { get; set; } = "(//span[@class='ant-checkbox'])[1]";
    public static string ResetarFiltroAtivo { get; set; } = "//button/*[text()='Resetar']";
    public static string AplicarAtivos { get; set; } = "//*[@data-testid='aplicarAtivosSelecionados']";
    public static string CancelarAtivos { get; set; } = "//*[@data-testid='cancelarAtivosSelecionados']";
    #endregion

    #region Elementos de página - Planos - Novo Plano - Selecionar Lojas
    public static string MenuLojas { get; set; } = "//div[text()='Selecione as lojas: ']";
    public static string TabelaLojasPlano { get; set; } = "(//table/tbody)[3]/tr";
    public static string SelecionarLojaCheckbox(string nomeLoja) { return $"//*[@data-testid='selecionarLoja-{nomeLoja}']"; }
    public static string QuantidadeLojasFiltradas { get; set; } = "//div[text()='Selecione as lojas: ']/parent::div/div[2]/div/div/div";
    #endregion

    #region Elementos de página - Planos - Dados do Plano
    public static string LoadDeTelaDadosPlano { set; get; } = "//*[contains(@id,'panel-1')]//span[contains(@class,'ant-spin-dot-spin')]";
    public static string AbasPlano { get; set; } = "//div[@class='ant-tabs-nav-list']";
    public static string AbaDadosPlano { get; set; } = "//div[@class='ant-tabs-nav-list']//*[contains(text(),'Dados do Plano')]";
    public static string SituacaoPlano { get; set; } = "//*[@data-testid='situacao']";
    public static string SituacaoPlanoAprovar { get; set; } = "//*[text()='Contrato Aprovado']";
    public static string SituacaoPlanoCancelar { get; set; } = "//*[text()='Cancelado']";
    public static string Desconto { get; set; } = "//*[@data-testid='desconto']";
    public static string AplicarDesconto { get; set; } = "//button/*[text()='Aplicar']";
    public static string InicioVigenciaPlano { get; set; } = "//*[@data-testid='inicioVigencia']";
    public static string FimVigenciaPlano { get; set; } = "//*[@data-testid='fimVigencia']";
    public static string AvancarCalendarioMes { get; set; } = "//*[contains(@class,'header-next-btn')]";
    public static string ReceitaAtivos { get; set; } = "//*[@data-testid='receitaAtivos']";
    public static string ReceitaAtivosDesconto { get; set; } = "//*[@data-testid='receitaAtivosDesconto']";
    public static string ReceitaPlano { get; set; } = "//*[@data-testid='receita']";
    public static string DespesaPlano { get; set; } = "//*[@data-testid='despesa']";
    public static string ValorComplementar { get; set; } = "//*[@data-testid='valorComplementar']";
    public static string TipoCampanha { get; set; } = "//*[@name='SubTipoFornecedorId']";
    public static string SelecionarTipoCampanha { get; set; } = "//div[@class='rc-virtual-list']//*[text()='Tipo Campanha 01']";
    public static string QuantidadeParcelas { get; set; } = "//*[@name='QuantidadeParcelas']";
    public static string Setor { get; set; } = "//*[@data-testid='nomeSetor']//input";
    public static string SelecionarSetor { get; set; } = "(//*[@class='ant-select-item-option-content'][text()='Geral'])[1]";
    public static string Departamento { get; set; } = "//*[@data-testid='nomeDepartamento']//input";
    public static string SelecionarDepartamento { get; set; } = "(//*[@class='ant-select-item-option-content'][text()='Geral'])[2]";
    public static string Categoria { get; set; } = "//*[@data-testid='nomeCategoria']//input";
    public static string SelecionarCategoria { get; set; } = "(//*[@class='ant-select-item-option-content'][text()='Geral'])[3]";
    public static string DataCancelamentoPlano { get; set; } = "//*[contains(text(),'Data de Cancelamento')]/parent::div/div/div/div[2]//input";
    public static string SelecionarDataCancelamentoPlano { get; set; } = "//*[text()='Today']";
    public static string OkCancelamento { get; set; } = "//*[text()='OK']";
    public static string FecharPlanoConfirmacao { get; set; } = "//button/*[text()='Fechar mesmo assim']";
    public static string EtapasWorkflow { get; set; } = "//div[contains(@class,'etapas-container')]";
    public static string EtapasWorkflowPlano { get; set; } = "//div[contains(@class,'etapas-item')]/span";
    public static string EtapasWorkflowGraficoPlano { get; set; } = "//div[@class='ant-row etapas-container-grafico ']";
    public static string SalvarPlano { get; set; } = "//*[@data-testid='salvarPlano']";
    public static string FecharPlano { get; set; } = "//*[@data-testid='fecharPlano']";
    #endregion

    #region Elementos de página - Planos - Ativos Alocados
    public static string FiltrarAtivoAlocado { get; set; } = "//thead//th[@title='Ativo']//span[@role='button']";
    public static string PreencherNomeAtivo { get; set; } = "(//div[@style='padding: 5px;'])[2]/input";
    public static string BuscarAtivoAlocado { get; set; } = "(//div[@style='padding: 5px;'])[2]/button/span[text()='Buscar']";
    public static string AbaAtivosAlocados { get; set; } = "//div[@class='ant-tabs-nav-list']//*[contains(text(),'Ativos Alocados')]";
    public static string TabelaAtivosPlano { get; set; } = "//*[contains(text(),'Ativos Alocados')]/../../../../../../../..//div[@class='ant-modal-content']//tbody";
    public static string AvancarCalendarioMesInicioVigenciaTrade { get; set; } = "(//*[contains(@class,'header-next-btn')])[2]";
    public static string AvancarCalendarioMesFimVigenciaTrade { get; set; } = "(//*[contains(@class,'header-next-btn')])[1]";
    public static string EditarAtivoAlocado(string nomeAtivo) { return $"//*[@data-testid='editarAtivoAlocado-{nomeAtivo}']"; }

    #endregion

    #region Elementos de página - Planos - Ativos Alocados - Editar Alocação do Ativo por Loja
    public static string LoadDeTelaAlocacaoPorLoja { get; set; } = "(//*[@class='ativos-alocados-container']//*[contains(@class,'ant-spin-dot-item')])[1]";
    public static string TabelaLojasAtivoAlocados { get; set; } = "//*[contains(text(),'Alocação por Loja')]/../../../../../div[2]/div//tbody";
    public static string LinhaTabelaLojasAtivoAlocados { get; set; } = "//*[contains(text(),'Alocação por Loja')]/../../../../../div[2]/div//tbody/tr[@data-row-key='0']";
    public static string ScrollTabelaLojasAtivoAlocados { get; set; } = "//*[contains(text(),'Alocação por Loja')]/../../../../..//div[contains(@class,'ant-table-scroll-horizontal')]";
    public static string ScrollHorizontalTabelaLojasAtivoAlocados { get; set; } = "//*[contains(text(),'Alocação por Loja')]/../../../../..//tbody[@class='ant-table-tbody']";
    public static string AceleradorInicioVigenciaTrade { get; set; } = "//*[@data-testid='inicioVigenciaAcelerador']";
    public static string AceleradorQuantidadeAlocarTrade { get; set; } = "//*[contains(text(),'Alocação por Loja')]/../../../../..//*[@title='Alocar']/../..//input";
    public static string AceleradorFimVigenciaTrade { get; set; } = "//*[@data-testid='fimVigenciaAcelerador']";
    public static string ColunaVeiculacaoTrade { get; set; } = "//div[@id='ativos-alocados-table']//thead//th[text()='Veiculação']";
    public static string ColunaVeiculacaoTradeCheckbox { get; set; } = "(//tbody[@class='ant-table-tbody']//input[@type='checkbox'])[1]";
    public static string ColunaDisponivelTrade { get; set; } = "//div[@id='ativos-alocados-table']//thead//th[text()='Disponível']";
    public static string LojasAtivoAlocados { get; set; } = "//*[contains(text(),'Alocação por Loja')]/../../../../../..//tbody";
    public static string AplicarDadosLojas { get; set; } = "(//button/*[text()='Aplicar'])[2]";
    public static string BuscarAtivoAlocacao { get; set; } = "//*[@data-testid='nomeAtivo']//*/input";
    public static string SelecionarAtivoAlocacao { get; set; } = "//div[@class='rc-virtual-list']//*[text()='Cestão 01 - ']";
    public static string IncluirAlocacaoAtivo { get; set; } = "//*[@data-testid='incluirAtivo']";
    public static string QuantidadeLojasPorAtivo { get; set; } = "//*[contains(text(),'Total de lojas')]";
    public static string QuantidadeAlocacaoAtivo(string nomeAtivo) { return $"//*[@data-testid='quantidadeAlocacaoLoja-{nomeAtivo}']"; }
    public static string QuantidadeAlocacaoLoja(string nomeLoja) { return $"//*[@data-testid='quantidadeAlocacaoLoja-{nomeLoja}']"; }
    public static string SalvarAlocacaoLoja { get; set; } = "//*[@data-testid='salvarEdicaoAtivo']";
    public static string FecharAlocacaoAtivoPorLoja { get; set; } = "//*[@data-testid='fecharEdicaoAtivo']";
    public static string MensagemSucessoAlocacaoAtivo { get; set; } = "//*[contains(text(), 'Alocação atualizada')]";
    public static string MensagemSucessoEditarQuantidadeAlocacaoAtivo { get; set; } = "//*[contains(text(), 'Produtos atualizados')]";
    public static string MensagemAvisoEditarQuantidadeAlocacaoAtivo { get; set; } = "//*[contains(text(), 'Salve suas informações')]";
    public static string NomeAtivoAlocacao { get; set; } = "//*[@data-testid='nomeAtivo']//span[2]";
    public static string AbaAlocacaoPorLojaAtivo { get; set; } = "//*[contains(text(),'Alocação por Loja')]";
    public static string AplicarAceleradorPorLojaAtivoAlocado { get; set; } = "//*[contains(text(),'Alocação por Loja')]/../../../../..//button/*[text()='Aplicar']";
    #endregion

    #region Elementos de página - Planos - Anexos
    public static string LoadDeTelaAnexos { set; get; } = "(//*[contains(@id,'panel-10')]//*[contains(@class,'ant-spin-dot-item')])[1]";
    #endregion

    #region Elementos de página - Planos - Book Fotográfico
    public static string LoadDeTelaBookFotografico { set; get; } = "(//*[contains(@id,'panel-7')]//*[contains(@class,'ant-spin-dot-item')])[1]";
    #endregion

    #region Elementos de página - Planos - Book Fotográfico
    public static string LoadDeTelaPainelIndustria { set; get; } = "(//*[contains(@id,'panel-11')]//*[contains(@class,'ant-spin-dot-item')])[1]";
    #endregion

    #region Elementos de página - Planos - Tarefas
    public static string LoadDeTelaTarefas { set; get; } = "(//*[contains(@id,'panel-workflow')]//*[contains(@class,'ant-spin-dot-item')])[1]";
    #endregion

    #region Elementos de página - SmartIA
    public static string StatusCampanha { get; set; } = "//tbody/tr[2]/td[7]/div";
    public static string FiltrarCampanha { get; set; } = "//*[text()='Campanha']/following-sibling::span[@role='button']";
    public static string PesquisarCampanha { get; set; } = "//div[@class='ant-table-filter-dropdown']//input";
    #endregion

    #region Elementos de página - SmartIA - Nova Campanha
    public static string MenuCampanhas { get; set; } = "//span[text()='Campanhas']";
    public static string ImagemCampanha { get; set; } = "//input[@type='file']";
    public static string NomeCampanha { get; set; } = "//*[@name='Nome']";
    public static string EmailResposavelCampanha { get; set; } = "//*[@name='EmailResponsavel']";
    public static string NomeResposavelCampanha { get; set; } = "//*[@name='NomeResponsavel']";
    public static string InicioVigenciaCampanha { get; set; } = "//label[text()='Inicio Vigência']/following-sibling::div/div/input";
    public static string InicioVigenciaCampanhaAvancarData { get; set; } = "(//*[contains(@class,'header-next-btn')])[1]";
    public static string FimVigenciaCampanha { get; set; } = "//label[text()='Fim Vigência']/following-sibling::div/div/input";
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
    public static string DisplayVarejoSelecionado { get; set; } = "//div[contains(@style,'display')]//span[text()='HOMOLOGAÇÃO SP']";
    public static string VarrerAtivos { get; set; } = "//button[@class='button-varredura-ativo']";
    #endregion

    #region Elementos de página - SmartIA - Nova Campanha - Selecionar Ativos
    public static string ReservarAtivosCampanha { get; set; } = "//div[contains(@style,'display')]//span[text()='HOMOLOGAÇÃO SP']/../..//*[@aria-label='appstore']";
    public static string SalvarAtivosCampanha { get; set; } = "(//button/*[text()='Salvar'])[2]";
    public static string FiltrarAtivosCampanha { get; set; } = "(//*[text()='Ativo'])[3]/..//*[@aria-label='search']";
    public static string PreencherFiltroAtivosCampanha { get; set; } = "(//*[text()='Ativo'])[3]/..//*[@aria-label='search']";
    public static string BuscarAtivosCampanha { get; set; } = "(//*[text()='Ativo'])[3]/..//*[@aria-label='search']";
    public static string SelecionarAtivoCampanha { get; set; } = "(//input[@class='ant-checkbox-input'])[2]";
    public static string ReservarQuantidadeAtivoLojasCampanha { get; set; } = "//tbody/tr/td[4]/span";
    public static string QuantidadeReservaLojasCampanha { get; set; } = "//*[text()='Reservar para Todos:']/../input";
    public static string ReservarAtivoLojasCampanha { get; set; } = "//button/*[text()='Reservar']";
    public static string FecharReservaAtivoLojasCampanha { get; set; } = "(//button/*[text()='Fechar'])[2]";
    public static string QuantidadeReservadaAtivoCampanha { get; set; } = "//tbody/tr/td[5]/input";
    public static string QuantidadeLojasReservaCampanha { get; set; } = "(//*[@class='ant-modal-content'])[2]//tbody/tr";
    #endregion
}