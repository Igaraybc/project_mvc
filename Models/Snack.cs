using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Snacks")]
public class Snack
{
    [Key]
    public int SnackId { get; set; }
    [StringLength(80, MinimumLength = 10, ErrorMessage = "O tamanho deve ter no mínimo {1} e no máximo {2} caracteres")]
    [Required(ErrorMessage = "Informe o nome do lanche")]
    [Display(Name = "Nome do lanche")]
    public string SnackName { get; set; }
    [Required(ErrorMessage = "A descrição do lanche deve ser informada")]
    [Display(Name = "Descrição do lanche")]
    [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres")]
    [MaxLength(200, ErrorMessage = "Descrição não pode exceder {1} caracteres")]
    public string ShortDescription { get; set; }

    [Required(ErrorMessage = "A descrição detalhada do lanche deve ser informada")]
    [Display(Name = "Descrição detalhada do lanche")]
    [MinLength(20, ErrorMessage = "Descrição detalhada deve ter no mínimo {1} caracteres")]
    [MaxLength(200, ErrorMessage = "Descrição detalhada não pode exceder {1} caracteres")]
    public string LongDescription { get; set; }
    [Required(ErrorMessage = "Informe o preço do lanche")]
    [Display(Name = "Preço")]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 999.99, ErrorMessage = "O preço deve estar entre 1 e 999,99")]
    public decimal Price { get; set; }
    [Display(Name="Caminho da Imagem Normal")]
    [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
    public string ImageUrl { get; set; }
    [Display(Name="Caminho da Imagem Miniatura")]
    [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
    public string ImageThumbnailUrl { get; set; }
    [Display(Name = "Preferido?")]
    public bool IsFavoriteSnack { get; set; }
    [Display(Name="Estoque")]
    public bool InStock { get; set; }

    /*Usado para definir o relacionamento entre snack e category - nã será criado na tabela*/
    public int CategoryId { get; set; } /*foreign key*/
    public virtual Category Category { get; set; }
}