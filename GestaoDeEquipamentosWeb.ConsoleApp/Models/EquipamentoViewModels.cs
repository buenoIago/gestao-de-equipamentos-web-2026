using System.ComponentModel.DataAnnotations;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Models;

public record ListarEquipamentosViewModel(
    string Id,
    string Nome,
    decimal PrecoAquisicao,
    DateTime DataFabricacao,
    string Fabricante
);

public record CadastrarEquipamentoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 50 caracteres.")]
    string Nome,

    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Preço de Aquisição\" deve conter um valor positivo.")]
    decimal PrecoAquisicao,

    [Required(ErrorMessage = "O campo \"Data de Fabricação\" deve ser preenchido.")]
    [DataType(DataType.Date)]
    DateTime DataFabricacao,

    [Required(ErrorMessage = "O campo \"Fabricante\" deve ser preenchido.")]
    string FabricanteId
);

public record EditarEquipamentoViewModel(
    string Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 50 caracteres.")]
    string Nome,

    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Preço de Aquisição\" deve conter um valor positivo.")]
    decimal PrecoAquisicao,

    [Required(ErrorMessage = "O campo \"Data de Fabricação\" deve ser preenchido.")]
    [DataType(DataType.Date)]
    DateTime DataFabricacao,

    [Required(ErrorMessage = "O campo \"Fabricante\" deve ser preenchido.")]
    string FabricanteId
);

public record ExcluirEquipamentoViewModel(
    string Id,
    string Nome,
    decimal PrecoAquisicao,
    DateTime DataFabricacao,
    string Fabricante
);