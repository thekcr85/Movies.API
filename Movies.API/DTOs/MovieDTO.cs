using System.ComponentModel.DataAnnotations;

namespace Movies.API.DTOs
{
	public class MovieDTO
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Title is required.")]
		[StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Director is required.")]
		[StringLength(100, MinimumLength = 1, ErrorMessage = "Director's name must be between 1 and 100 characters.")]
		public string Director { get; set; }

		[Range(1900, 2025, ErrorMessage = "Year must be between 1900 and the current year.")]
		public int Year { get; set; }

		[Required(ErrorMessage = "Format is required.")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Format must be between 1 and 50 characters.")]
		public string Format { get; set; }

		[Required(ErrorMessage = "Genre is required.")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Genre must be between 1 and 50 characters.")]
		public string Genre { get; set; }
	}
}
