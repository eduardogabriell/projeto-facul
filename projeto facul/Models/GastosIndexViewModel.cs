namespace projeto_facul.Models
{
    // Dados da tela "Lista de Gastos": itens da página atual + filtros escolhidos
    public class GastosIndexViewModel
    {
        public List<Gasto> Itens { get; set; } = new();

        public string? Busca { get; set; }
        public string Contexto { get; set; } = "Pessoal";
        public string Categoria { get; set; } = "Todas";
        public string Mes { get; set; } = string.Empty;   // "aaaa-mm" ou "todos"

        public List<(string Valor, string Texto)> Meses { get; set; } = new();

        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalItens { get; set; }
        public int Inicio { get; set; }
        public int Fim { get; set; }
    }
}
