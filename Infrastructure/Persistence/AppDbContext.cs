using CIT_Portfolio_Project_API.Models.Entities;
using CIT_Portfolio_Project_API.Infrastructure.Persistence.PgSqlFunctionResults;
using Microsoft.EntityFrameworkCore;

namespace CIT_Portfolio_Project_API.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // IMDB read-only
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<MovieDetail> MovieDetails => Set<MovieDetail>();
    public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
    public DbSet<MoviePerson> MoviePeople => Set<MoviePerson>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<PersonProfession> PersonProfessions => Set<PersonProfession>();
    public DbSet<PersonKnownFor> PersonKnownFor => Set<PersonKnownFor>();
    public DbSet<WordIndex> WordIndex => Set<WordIndex>();

    // Framework CRUD
    public DbSet<User> Users => Set<User>();
    public DbSet<UserBookmark> UserBookmarks => Set<UserBookmark>();
    public DbSet<UserRating> UserRatings => Set<UserRating>();
    public DbSet<UserSearchHistory> UserSearchHistory => Set<UserSearchHistory>();

    // Function result sets (keyless)
    public DbSet<SearchRow> SearchRows => Set<SearchRow>();
    public DbSet<StructuredSearchRow> StructuredSearchRows => Set<StructuredSearchRow>();
    public DbSet<BookmarkRow> BookmarkRows => Set<BookmarkRow>();
    public DbSet<PopularActorRow> PopularActorRows => Set<PopularActorRow>();
    public DbSet<SimilarTitleRow> SimilarTitleRows => Set<SimilarTitleRow>();
    public DbSet<PersonWordRow> PersonWordRows => Set<PersonWordRow>();
    public DbSet<ExactMatchRow> ExactMatchRows => Set<ExactMatchRow>();
    public DbSet<BestMatchRow> BestMatchRows => Set<BestMatchRow>();
    public DbSet<CoPlayerRow> CoPlayerRows => Set<CoPlayerRow>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TODO: Adjust keys to match actual SQL schema from Part-1
        modelBuilder.Entity<Movie>().HasKey(x => x.Tconst);
        modelBuilder.Entity<Rating>().HasKey(x => x.Tconst);
        modelBuilder.Entity<MovieDetail>().HasKey(x => x.Tconst);
        modelBuilder.Entity<MovieGenre>().HasKey(x => new { x.Tconst, x.Genre });
        modelBuilder.Entity<MoviePerson>().HasKey(x => new { x.Tconst, x.Nconst });
        modelBuilder.Entity<Person>().HasKey(x => x.Nconst);
        modelBuilder.Entity<PersonProfession>().HasKey(x => new { x.Nconst, x.Profession });
        modelBuilder.Entity<PersonKnownFor>().HasKey(x => new { x.Nconst, x.Tconst });
        modelBuilder.Entity<WordIndex>().HasKey(x => x.Word);

        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<UserBookmark>().HasKey(x => x.Id);
        modelBuilder.Entity<UserRating>().HasKey(x => x.Id);
        modelBuilder.Entity<UserSearchHistory>().HasKey(x => x.Id);

        // Keyless types for function results
        modelBuilder.Entity<SearchRow>().HasNoKey();
        modelBuilder.Entity<StructuredSearchRow>().HasNoKey();
        modelBuilder.Entity<BookmarkRow>().HasNoKey();
        modelBuilder.Entity<PopularActorRow>().HasNoKey();
        modelBuilder.Entity<SimilarTitleRow>().HasNoKey();
        modelBuilder.Entity<PersonWordRow>().HasNoKey();
        modelBuilder.Entity<ExactMatchRow>().HasNoKey();
        modelBuilder.Entity<BestMatchRow>().HasNoKey();
        modelBuilder.Entity<CoPlayerRow>().HasNoKey();
    }

    // Function-call helpers (examples using FromSqlInterpolated)
    public IQueryable<SearchRow> CallStringSearch(int userId, string text)
        => SearchRows.FromSqlInterpolated($"select * from string_search({userId}, {text})");

    public IQueryable<StructuredSearchRow> CallStructuredStringSearch(int userId, string? title, string? plot, string? characters, string? person)
        => StructuredSearchRows.FromSqlInterpolated($"select * from structured_string_search({userId}, {title}, {plot}, {characters}, {person})");

    public async Task<int> ExecuteRateAsync(int userId, string tconst, int value, CancellationToken ct = default)
        => await Database.ExecuteSqlInterpolatedAsync($"select rate({userId}, {tconst}, {value})", ct);

    public IQueryable<BookmarkRow> CallGetBookmarks(int userId)
        => BookmarkRows.FromSqlInterpolated($"select * from get_bookmarks({userId})");

    public async Task<int> ExecuteAddBookmarkAsync(int userId, string tconst, string? note, CancellationToken ct = default)
        => await Database.ExecuteSqlInterpolatedAsync($"select add_bookmark({userId}, {tconst}, {note})", ct);

    // Analytics examples (read-only)
    public IQueryable<PopularActorRow> CallPopularActorsInMovie(string tconst)
        => PopularActorRows.FromSqlInterpolated($"select * from popular_actors_in_movie({tconst})");

    public IQueryable<SimilarTitleRow> CallSimilarMovies(string tconst)
        => SimilarTitleRows.FromSqlInterpolated($"select * from similar_movies({tconst})");

    public IQueryable<PersonWordRow> CallPersonWords(string name)
        => PersonWordRows.FromSqlInterpolated($"select * from person_words({name})");

    public IQueryable<CoPlayerRow> CallCoPlayers(string actor)
        => CoPlayerRows.FromSqlInterpolated($"select * from co_players({actor})");

    public IQueryable<ExactMatchRow> CallExactMatch(string query)
        => ExactMatchRows.FromSqlInterpolated($"select * from exact_match_query({query})");

    public IQueryable<BestMatchRow> CallBestMatch(string query)
        => BestMatchRows.FromSqlInterpolated($"select * from best_match_query({query})");
}
