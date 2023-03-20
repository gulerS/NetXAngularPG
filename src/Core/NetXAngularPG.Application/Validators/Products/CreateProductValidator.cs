using FluentValidation;
using NetXAngularPG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please do not leave the product name blank.")
                .MaximumLength(150)
                .MinimumLength(2)
                    .WithMessage("Please enter the Product name between 2-15 characters.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please do not leave stock information blank.")
                .Must(s => s >= 0)
                 .WithMessage("Stock information cannot be negative.");

            RuleFor(p => p.Price).NotEmpty()
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please do not leave price information blank.")
                .Must(s => s >= 0)
                 .WithMessage("Price information cannot be negative.");

        }
    }
}
