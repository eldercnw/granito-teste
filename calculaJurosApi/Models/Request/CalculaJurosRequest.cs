
using FluentValidation;
using System;

namespace calculaJurosApi.Models.Request
{
    public class CalculaJurosRequest
    {
        public decimal ValorInicial { get; set; }
        public int Meses { get; set; }
    }

    public class CalculaJurosRequestValidator : AbstractValidator<CalculaJurosRequest>
    {
        public CalculaJurosRequestValidator()
        {
            RuleFor(x => x.Meses).NotNull().GreaterThanOrEqualTo(0).WithMessage("O campo meses deve ser maior ou igual a 0.");
            RuleFor(x => x.ValorInicial).NotNull().WithMessage("O campo valor inicial deve ser preenchido");

        }
    }
}
