
namespace MeuClienteWebTestProject;

public class GlobalVariables
{
    #region Projeto
    //Executa, mostrando o Browser na tela, se handLessMode igual false
    public static bool handLessMode = false;
    public static bool devMode = false;
    public static bool hmlMode = true;
    public static bool prodMode = false;
    public static string urlDevPlataforma = "https://dev.meucliente.app.br/";
    public static string urlHmlPlataforma = "https://stage.meucliente.app.br/";
    public static string urlPrdPlataforma = "https://login.meucliente.app.br/";
    public static string emailUsuarioSemPlanta = "homologacao.sp@meucliente.app.br";
    public static string senhaUsuarioSemPlanta = "Meucliente@hml@123";
    public static string emailUsuarioComPlanta = "homologacao.cp@meucliente.app.br";
    public static string senhaUsuarioComPlanta = "Meucliente@hml@123";
    #endregion

    #region Elementos de página - Elementos Comuns
    public static string Mensagens { get; set; } = "//*[@class='ant-message-notice']";
    public static string NovoRegistro { get; set; } = "//button[@id='Buttonclass']";
    public static string SalvarRegistro { get; set; } = "//button/*[text()='Salvar']";
    public static string VoltarTela { get; set; } = "//button/*[text()='Voltar']";
    public static string FecharTela { get; set; } = "//button/*[text()='Fechar']";
    public static string PreencherFiltro { get; set; } = "//*[@class='ant-table-filter-dropdown']//input";
    public static string BuscarRegistro { get; set; } = "//button/*[text()='Buscar']";
    public static string PaginacaoTela { get; set; } = "//ul[contains(@class,'ant-pagination')]//li[2]";
    public static string PrimeiraLinhaTabelaColuna1 {get; set; } = "(//tbody)[9]/tr[2]/td[1]";
    public static string PrimeiraLinhaTabelaColuna2 {get; set; } = "(//tbody)[9]/tr[2]/td[2]";
    public static string AvisoInexistenciaDados { get; set; } = "//*[text()='Não há dados']";
    public static string LoadDeTela { get; set; } = "(//*[contains(@class,'ant-spin-dot-item')])[1]";
    #endregion

    #region Elementos de página - Login
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
    public static string DetalhesLojasAtivas {  get; set; } = "//span[text()='Lojas Ativas']/following-sibling::div//button";
    public static string TabelaListagemLojasAtivas {get; set; } = "//*[contains(text(),'Listagem de Lojas')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr";
    public static string DetalhesDisponibilidadeAtivos {  get; set; } = "//*[contains(text(),'Ativos Alocados')]/following-sibling::div//button[1]";
    public static string ColunaAtivoListagemDisponibilidadeAtivos {get; set; } = "//*[contains(text(),'Disponibilidade de Ativos')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[1]";
    public static string DetalhesNegociacaoAtivos {  get; set; } = "//*[contains(text(),'Ativos Alocados')]/following-sibling::div//button[2]";
    public static string ColunaAtivoListagemAtivosNegociados {get; set; } = "//*[contains(text(),'Ativos Negociados')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[1]";
    public static string ColunaDetalhesBotaoContratosVinculado {  get; set; } = "//*[contains(text(),'Ativos Negociados')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[10]/button";
    public static string FiltrarAtivoPorNome { get; set; } = "//span[text()='Ativo']/following-sibling::span[@role='button']";
    public static string DetalhesPotencialReceitaAtivos {  get; set; } = "//*[contains(text(),'Ativos Alocados')]/following-sibling::div//button[3]";
    public static string FiltrarAtivoPorNomePotencialReceita { get; set; } = "//th[@aria-label='Nome']/div/span[@role='button']";
    public static string ColunaNomeListagemPotencialReceita {get; set; } = "//*[contains(text(),'Potencial Receita')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[1]";
    public static string ColunaDetalhesListagemPotencialReceita {get; set; } = "(//tbody)[9]/tr[2]/td[10]/button";
    public static string FecharTelaContratosEAtivosVinculados { get; set; } = "(//button/*[text()='Fechar'])[2]";
    public static string DetalhesContratosAtivos {  get; set; } = "//*[contains(text(),'Contratos Vigentes')]/following-sibling::div//button[1]";
    public static string TabelaListagemContratosAtivos {get; set; } = "//*[contains(text(),'Contratos Ativos')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr";
    public static string ColunaContratoListagemContratosAtivos {get; set; } = "//*[contains(text(),'Contratos Ativos')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[2]";
    public static string ColunaAcoesBotaoListagemContratosAtivos {get; set; } = "//*[contains(text(),'Contratos Ativos')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[9]";
    public static string DetalhesContratosVencendo {  get; set; } = "//*[contains(text(),'Contratos Vigentes')]/following-sibling::div//button[2]";
    public static string ColunaContratoListagemContratosVencendo {get; set; } = "//*[contains(text(),'Contratos Vencendo')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[2]";
    public static string ColunaAcoesBotaoListagemContratosVencendo {get; set; } = "//*[contains(text(),'Contratos Vencendo')]/ancestor::div[@class='ant-modal-body']//table/tbody/tr[2]/td[9]";
    public static string FiltrarContratoPorCampanha { get; set; } = "//span[text()='Contrato']/following-sibling::span[@role='button']";
    public static string DetalhesContratosAtivosTotalReceita {  get; set; } = "//*[contains(text(),'Total de Receita')]/following-sibling::div//button[1]";
    public static string TextoCardTotalReceita { get; set; } = "//div[@class='DashBoardCards']//*[contains(text(),'índice de Crescimento')]";
    public static string TabelaListagemAterrissagem { get; set; } = "//*[@role='tabpanel']/div/div";
    public static string ColunaMesAterrissagem {get; set; } = "//*[text()='JANEIRO']";
    public static string DetalhesAterrissagemReceita {  get; set; } = "(//*[contains(text(),'Perfomance Receita')]/following-sibling::div//button[1])[1]";
    public static string DetalhesAterrissagemReceitaPorBandeira {  get; set; } = "(//*[contains(text(),'Receita Bandeira')]/following-sibling::div//button[1])[1]";
    public static string DetalhesAterrissagemReceitaPorTipoFornecedor {  get; set; } = "(//*[contains(text(),'Receita Tipo Fornecedor')]/following-sibling::div//button[1])[1]";
    public static string TabelaListagemParceiros { get; set; } = "//*[@role='tabpanel']/div/div";
    public static string ColunaIndustriaParcerio {get; set; } = "//*[text()='Indústria/Parceiro']";
    public static string DetalhesListaParceirosPerformance {  get; set; } = "(//*[contains(text(),'Perfomance Parceiro')]/following-sibling::div//button[1])[1]";
    public static string FiltrarNegociacoes { get; set; } = "//button/span[text()='Filtrar']";
    public static string DetalhesListaParceirosInvestimento {  get; set; } = "(//*[contains(text(),'Investimento por Parceiro')]/following-sibling::div//button[1])[1]";
    public static string DetalhesDesempenhoPorLoja {  get; set; } = "(//*[contains(text(),'Desempenho por Loja')]/following-sibling::div//button[1])[1]";
    public static string GraficoDesempenhoLoja { get; set; } = "//*[@class='ant-modal-content']//*[@class='Chart']";
    public static string FiltroDesempenhoLoja { get; set; } = "//*[@class='ant-modal-content']//*[text()='Maior Retorno']";
    public static string DetalhesDesempenhoDosAtivos {  get; set; } = "(//*[contains(text(),'Desempenho de Ativos')]/following-sibling::div//button[1])[1]";
    public static string GraficoDesempenhoAtivo { get; set; } = "//*[@class='ant-modal-content']//*[@class='Chart']";
    public static string FiltroDesempenhoAtivo { get; set; } = "//*[@class='ant-modal-content']//*[text()='Maior Retorno']";
    public static string TextoCardMaisVendidosDepartamento { get; set; } = "//*[text()='Mais vendidos do Departamento']";
    #endregion

    #region Elementos de página - Planos
    public static string TabelaPlanos { get; set; } = "//table/tbody/tr";
    public static string FiltrarPlanoPorCampanha { get; set; } = "//thead//th[@title='Nome Campanha']//span[@role='button']";
    public static string PesquisarNomeCampanha { get; set; } = "//div[@class='ant-table-filter-dropdown']//input";
    public static string ColunaFimVigencia { get; set; } = "//*[text()='Fim Vigência']";
    public static string StatusPlano { get; set; } = "//tbody/tr[2]/td[6]";
    public static string FarolPlano { get; set; } = "//tbody/tr[2]/td[7]/div";
    public static string EditarPlano { get; set; } = "//thead/tr/th[text()='Ações']/ancestor::table/tbody/tr[2]//td//button//*[@aria-label='zoom-in']";
    public static string ExcluirPlano { get; set; } = "//thead/tr/th[text()='Ações']/ancestor::table/tbody/tr[2]//td//button//*[@aria-label='delete']";
    public static string ExcluirPlanoMensagemConfirmacao { get; set; } = "//*[@class='ant-modal-confirm-body']/span[2]";
    public static string OkExclusao { get; set; } = "//*[text()='OK']";
    public static string ModalPlanos { get; set; } = "//*[@class='ant-modal-body' and @style='overflow-x: hidden; height: 85vh;']";
    #endregion

    #region Elementos de página - Planos - Novo Plano
    public static string PesquisarIndustria { get; set; } = "//div[@label='Indústria']";
    public static string PreencherIndustria { get; set; } = "//div[@label='Indústria']//input[@type='search']";
    public static string SelecionarIndustria { get; set; } = "//div[@title='Indústria 01 F']";
    public static string PreencherCampanha { get; set; } = "//input[@name='NomeCampanha']";
    public static string InicioVigenciaNovoPlano { get; set; } = "(//div[contains(@class,'date-picker')]//input)[1]";
    public static string FimVigenciaNovoPlano { get; set; } = "(//div[contains(@class,'date-picker')]//input)[2]";
    public static string AvancarCalendarioMesInicioVigencia { get; set; } = "(//*[contains(@class,'header-next-btn')])[1]";
    public static string AvancarCalendarioMesFimVigencia { get; set; } = "(//*[contains(@class,'header-next-btn')])[2]";
    public static string FecharDetalhamento { get; set; } = "//button/*[text()='Fechar Detalhamento']";
    public static string SelecionarAtivos { get; set; } = "//button/*[text()='Selecionar Ativos']";
    public static string QuantidadePorLoja { get; set; } = "//*[@title='Qtd por Loja']/parent::div/parent::div/div[2]//input";
    public static string AplicarQuantidadePorLojaMassivamente { get; set; } = "//button/*[text()='Aplicar']";
    public static string CarregarLojas { get; set; } = "//button/*[text()='Carregar Lojas']";
    public static string InventarioAlerta { get; set; } = "//td[9]//button[contains(@class,'btn-dangerous')]";
    public static string InventarioOk { get; set; } = "//td[9]//span[contains(@class,'check-inventario')]";
    public static string GerarPrePlano { get; set; } = "//button/*[text()='Gerar Pré-Plano']";
    public static string MensagensDadosPlano { get; set; } = "//*[@class='form-action'][2]/span[3]";
    #endregion

    #region Elementos de página - Planos - Novo Plano - Selecionar Ativos
    public static string FiltrarAtivos { get; set; } = "//th[@aria-label='Inventário Item']/div/span[2]/span[@aria-label='filter']";
    public static string PesquisarAtivos { get; set; } = "//div[@class='ant-table-filter-dropdown']/div/span/input";
    public static string SelecionarAtivosFiltro { get; set; } = "//li[@class='ant-dropdown-menu-item']";
    public static string TelaFiltrarAtivo { get; set; } = "//div[@class='ant-table-filter-dropdown-btns']";
    public static string OkFiltroAtivos { get; set; } = "//button/*[text()='OK']";
    public static string SelecionarTodosAtivos { get; set; } = "(//span[@class='ant-checkbox'])[1]";
    public static string ResetarFiltroAtivo { get; set; } = "//button/*[text()='Resetar']";
    public static string AplicarAtivos { get; set; } = "//button/*[text()='Aplicar']";
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
    public static string ReceitaAtivos { get; set; } = "//*[@name='VendaCalculada']";
    public static string ReceitaPlano { get; set; } = "//*[@name='ValorTotalContrato']";
    public static string TipoCampanha { get; set; } = "//*[@name='SubTipoFornecedorId']";
    public static string SelecionarTipoCampanha { get; set; } = "//div[@class='rc-virtual-list']//*[text()='Tipo Campanha 01']";
    public static string QuantidadeParcelas { get; set; } = "//*[@name='QuantidadeParcelas']";
    public static string Setor { get; set; } = "//*[@name='SetorId']//input";
    public static string SelecionarSetor { get; set; } = "(//*[text()='Geral'])[2]";
    public static string Departamento { get; set; } = "//*[@name='DepartamentoId']//input";
    public static string SelecionarDepartamento { get; set; } = "(//*[text()='Geral'])[3]";
    public static string Categoria { get; set; } = "//*[@name='CategoriaId']//input";
    public static string SelecionarCategoria { get; set; } = "(//*[text()='Geral'])[5]";
    public static string DataCancelamentoPlano { get; set; } = "//*[contains(text(),'Data de Cancelamento')]/parent::div/div/div/div[2]//input";
    public static string SelecionarDataCancelamentoPlano { get; set; } = "//*[text()='Today']";
    public static string OkCancelamento { get; set; } = "//*[text()='OK']";
    public static string FecharPlanoConfirmacao { get; set; } = "//button/*[text()='Fechar mesmo assim']";
    #endregion

    #region Elementos de página - Planos - Ativos Alocados
    public static string FiltrarAtivoAlocado { get; set; } = "//thead//th[@title='Ativo']//span[@role='button']";
    public static string PreencherNomeAtivo { get; set; } = "(//div[@style='padding: 5px;'])[2]/input";
    public static string BuscarAtivoAlocado { get; set; } = "(//div[@style='padding: 5px;'])[2]/button/span[text()='Buscar']";
    public static string QuantidadePorLojaAtivosAlocados { get; set; } = "//*[@title='Alocar']/parent::div/parent::div/div[2]//input";
    public static string AplicarQuantidadePorLojaMassivamenteAtivosAlocados { get; set; } = "(//button/*[text()='Aplicar'])[2]";
    public static string AbaAtivosAlocados { get; set; } = "//div[@class='ant-tabs-nav-list']//*[contains(text(),'Ativos Alocados')]";
    public static string TabelaAtivosPlano { get; set; } = "//div[@class='ant-modal-content']//tbody";
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
    public static string MensagemSucessoEditarQuantidadeAlocacaoAtivo { get; set; } = "//*[contains(text(), 'Produtos atualizados')]";
    public static string MensagemAvisoEditarQuantidadeAlocacaoAtivo { get; set; } = "//*[contains(text(), 'Salve suas informações')]";
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