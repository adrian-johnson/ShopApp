using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Models;

namespace ShopApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public List<Shop> Shops { get; set; } = new();
        public Shop? ShopDetail { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Route { get; set; }

        [BindProperty] public string? Username { get; set; }
        [BindProperty] public string? Password { get; set; }

        public string? ErrorMessage { get; set; }
        public bool IsLoggedIn => HttpContext.Session.GetString("IsLoggedIn") == "true";

        public IndexModel(AppDbContext db) => _db = db;

        public async Task OnGet(string? route)
        {
            Route = route?.ToLower();

            Shops = await _db.Shops.ToListAsync();

            // Homepage
            if (string.IsNullOrEmpty(Route))
            {
                ShopDetail = null; // Just to be explicit
                return;
            }

            // Special pages
            if (Route == "login" || Route == "admin" || Route == "privacy" || Route == "logout")
            {
                return; // These are handled in the Razor page by checking Route
            }

            // Otherwise treat as shop slug
            ShopDetail = await _db.Shops.FirstOrDefaultAsync(s => s.Slug.ToLower() == Route);
        }

        public IActionResult OnPostLogin()
        {
            if (Username == "admin" && Password == "Password#123")
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                return Redirect("/admin");
            }

            ErrorMessage = "Invalid login.";
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("IsLoggedIn");
            return Redirect("/"); // or RedirectToPage("/Index", new { route = (string?)null });
        }

        public async Task<IActionResult> OnPostAddShop(string name, string slug, string? description, DateTime? dateOpened)
        {
            if (!IsLoggedIn)
                return Redirect("/login");

            _db.Shops.Add(new Shop { Name = name, Slug = slug, Description = description ?? "", DateOpened = (DateTime)dateOpened });
            await _db.SaveChangesAsync();

            return Redirect("/admin");
        }

        public async Task<IActionResult> OnPostEditShop(int id, string name, string slug, string? description, DateTime? dateOpened)
        {
            if (!IsLoggedIn)
                return Redirect("/login");

            var shop = await _db.Shops.FindAsync(id);
            if (shop != null)
            {
                shop.Name = name;
                shop.Slug = slug;
                shop.Description = description ?? "";
                shop.DateOpened = dateOpened ?? shop.DateOpened;

                await _db.SaveChangesAsync();
            }

            return Redirect("/admin");
        }

        public async Task<IActionResult> OnPostDeleteShop(int id)
        {
            if (!IsLoggedIn)
                return Redirect("/login");

            var shop = await _db.Shops.FindAsync(id);
            if (shop != null)
            {
                _db.Shops.Remove(shop);
                await _db.SaveChangesAsync();
            }

            return Redirect("/admin");
        }
    }
}
