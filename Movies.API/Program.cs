using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.DTOs;
using Movies.API.Mappers;
using Movies.API.Models;
using Movies.API.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MovieProfile));

builder.Services.AddValidatorsFromAssemblyContaining<MovieDTOValidator>();

builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/error");
}

// GET: /movies - Retrieve all movies
app.MapGet("/movies", async (MovieDbContext db, IMapper mapper) =>
{
	var movies = await db.Movies.ToListAsync();
	return Results.Ok(mapper.Map<List<MovieDTO>>(movies));
});

// GET: /movies/{id} - Retrieve movie by id
app.MapGet("/movies/{id:int}", async (int id, MovieDbContext db, IMapper mapper) =>
{
	var movie = await db.Movies.FindAsync(id);
	if (movie is null)
	{
		return Results.NotFound(new ProblemDetails
		{
			Type = "https://httpstatuses.com/404",
			Title = "Not Found",
			Status = StatusCodes.Status404NotFound,
			Detail = $"Movie with ID {id} not found. Please check the ID and try again.",
			Instance = $"/movies/{id}"
		});
	}

	return Results.Ok(mapper.Map<MovieDTO>(movie));
});

// POST: /movies - Add movie
app.MapPost("/movies", async (MovieDTO movieDto, MovieDbContext db, IMapper mapper, IValidator<MovieDTO> validator) =>
{
	var validationResult = await validator.ValidateAsync(movieDto);
	if (!validationResult.IsValid)
	{
		return Results.ValidationProblem(new Dictionary<string, string[]>
		{
			{ "errors", validationResult.Errors.Select(e => e.ErrorMessage).ToArray() }
		});
	}

	var movie = mapper.Map<Movie>(movieDto);
	db.Movies.Add(movie);
	await db.SaveChangesAsync();

	return Results.Created($"/movies/{movie.Id}", mapper.Map<MovieDTO>(movie));
});

// PUT: /movies/{id} - Update movie
app.MapPut("/movies/{id:int}", async (int id, MovieDTO movieDto, MovieDbContext db, IMapper mapper, IValidator<MovieDTO> validator) =>
{
	var existingMovie = await db.Movies.FindAsync(id);
	if (existingMovie is null)
	{
		return Results.NotFound(new ProblemDetails
		{
			Type = "https://httpstatuses.com/404",
			Title = "Not Found",
			Status = StatusCodes.Status404NotFound,
			Detail = $"Movie with ID {id} not found.",
			Instance = $"/movies/{id}"
		});
	}

	var validationResult = await validator.ValidateAsync(movieDto);
	if (!validationResult.IsValid)
	{
		return Results.ValidationProblem(new Dictionary<string, string[]>
		{
			{ "errors", validationResult.Errors.Select(e => e.ErrorMessage).ToArray() }
		});
	}

	mapper.Map(movieDto, existingMovie);
	await db.SaveChangesAsync();

	return Results.Ok(mapper.Map<MovieDTO>(existingMovie));
});

// DELETE: /movies/{id} - Delete movie
app.MapDelete("/movies/{id:int}", async (int id, MovieDbContext db) =>
{
	var movie = await db.Movies.FindAsync(id);
	if (movie is null)
	{
		return Results.NotFound(new ProblemDetails
		{
			Type = "https://httpstatuses.com/404",
			Title = "Not Found",
			Status = StatusCodes.Status404NotFound,
			Detail = $"Movie with ID {id} not found.",
			Instance = $"/movies/{id}"
		});
	}

	db.Movies.Remove(movie);
	await db.SaveChangesAsync();

	return Results.Ok(new { Message = $"Movie with ID {id} has been deleted." });
});

app.MapHealthChecks("/health");

// Error handling endpoint
app.MapGet("/error", () =>
{
	return Results.Problem(
		new ProblemDetails
		{
			Type = "https://httpstatuses.com/500",
			Title = "Internal Server Error",
			Status = StatusCodes.Status500InternalServerError,
			Detail = "Sorry, something went wrong on our end. Please try again later.",
			Instance = "/error"
		});
});

app.Run();
