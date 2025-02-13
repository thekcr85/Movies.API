namespace Movies.API.Models
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Director { get; set; }
		public int Year { get; set; }
		public string Format { get; set; }
		public string Genre { get; set; }
	}
}
