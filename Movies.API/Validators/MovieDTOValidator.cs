using FluentValidation;
using FluentValidation.AspNetCore;
using Movies.API.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Movies.API.Validators
{
	public class MovieDTOValidator : AbstractValidator<MovieDTO>
	{
		public MovieDTOValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("Title is required")
				.Length(1, 100).WithMessage("Title must be between 1 and 100 characters.");

			RuleFor(x => x.Director)
				.NotEmpty().WithMessage("Director is required")
				.Length(1, 100).WithMessage("Director's name must be between 1 and 100 characters.");

			RuleFor(x => x.Year)
				.InclusiveBetween(1900, DateTime.Now.Year).WithMessage("Year must be between 1900 and present year.");

			RuleFor(x => x.Format)
				.NotEmpty().WithMessage("Format is required")
				.Length(1, 20).WithMessage("Format must be between 1 and 20 characters.");

			RuleFor(x => x.Genre)
				.NotEmpty().WithMessage("Genre is required")
				.Length(1, 30).WithMessage("Genre must be between 1 and 30 characters.");
		}
	}
}
