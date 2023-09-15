using Microsoft.EntityFrameworkCore;
using MusicStreaming.Data;
using MusicStreaming.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MusicStreamingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicStreamingContext") ?? throw new InvalidOperationException("Connection string 'MusicStreamingContext' not found."));
});

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MusicStreamingContext>();
    Initialize(context);
}

app.Run();

static void Initialize(MusicStreamingContext context)
{
    context.Database.EnsureCreated();

    if (context.Albums.Any())
    {
        // Database has been seeded.
        return;
    }

    // Seed Albums
    List<Album> albums = new List<Album>
            {
                new Album { Title = "Album 1", Year = DateTime.Now },
                new Album { Title = "Album 2", Year = DateTime.Now.AddYears(-1) },
                new Album { Title = "Album 3", Year = DateTime.Now.AddYears(-2) }
            };

    // Seed Artists
    List<Artist> artists = new List<Artist>
            {
                new Artist { Name = "Artist 1" },
                new Artist { Name = "Artist 2" },
                new Artist { Name = "Artist 3" }
            };

    // Seed Songs and SongArtists
    List<Song> songs = new List<Song>
            {
                new Song { Title = "Song 1", LengthInSeconds = 180, Album = albums[0], PlaylistId = 1 },
                new Song { Title = "Song 2", LengthInSeconds = 240, Album = albums[1], PlaylistId = 2 },
                new Song { Title = "Song 3", LengthInSeconds = 200, Album = albums[2], PlaylistId = 3 }
            };

    List<SongArtists> songArtists = new List<SongArtists>
            {
                new SongArtists { Song = songs[0], Artist = artists[0] },
                new SongArtists { Song = songs[1], Artist = artists[1] },
                new SongArtists { Song = songs[2], Artist = artists[2] }
            };

    // Add entities to the context
    context.Albums.AddRange(albums);
    context.Artists.AddRange(artists);
    context.Songs.AddRange(songs);
    context.SongsArtists.AddRange(songArtists);

    // Save changes to the database
    context.SaveChanges();
}