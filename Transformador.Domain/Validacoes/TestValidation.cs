using FluentValidation;
using Transformador.Domain.Entities;

namespace Transformador.Domain.Validacoes
{
    public class TestValidation : AbstractValidator<Test>
    {
        public TestValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}