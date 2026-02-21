using FluentValidation;
using ProjetoFinal.DTOs;

namespace ProjetoFinal.Validators;

public class EquipamentoCreateDtoValidator 
    : AbstractValidator<EquipamentoCreateDto>
{
    public EquipamentoCreateDtoValidator()
    {
        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("Código é obrigatório.")
            .MaximumLength(100);

        RuleFor(x => x.Horimetro)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Horímetro não pode ser negativo.");
    }
}