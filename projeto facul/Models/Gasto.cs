using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projeto_facul.Models
{
    [Table("Gastos")]
    public class Gasto
    {
        public static readonly string[] Categorias =
        {
            "Supermercado", "Casa e Decoração", "Transporte", "Lazer e Serviços",
            "Alimentação", "Saúde", "Educação", "Outros"
        };

        public static readonly string[] Contextos = { "Pessoal", "Família", "Trabalho" };

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a descrição!")]
        [Display(Name = "Item / Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Obrigatório informar o estabelecimento!")]
        [Display(Name = "Estabelecimento")]
        public string Estabelecimento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Obrigatório informar a categoria!")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "Obrigatório informar o contexto!")]
        [Display(Name = "Contexto")]
        public string Contexto { get; set; } = "Pessoal";

        [Required(ErrorMessage = "Obrigatório informar o valor!")]
        [Display(Name = "Valor")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero!")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a data!")]
        [Display(Name = "Data")]
        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }
    }
}
