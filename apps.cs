// PointOfSaleController.cs
public class PointOfSaleController : Controller
{
    private readonly ProductRepository _productRepo;
    private readonly InventoryManager _inventoryManager;
    private readonly ExternalService _externalService;

    public PointOfSaleController(ProductRepository productRepo, InventoryManager inventoryManager, ExternalService externalService)
    {
        _productRepo = productRepo;
        _inventoryManager = inventoryManager;
        _externalService = externalService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepo.GetAllProductsAsync();
        return View(products);
    }

    [HttpPost]
    public async Task<IActionResult> ProcessSale(int productId, int quantity)
    {
        var product = await _productRepo.GetProductByIdAsync(productId);
        if (product == null)
        {
            return NotFound();
        }

        // Update stock levels
        await _inventoryManager.UpdateStockLevelsAsync(productId, quantity);

        // Process payment
        var paymentResult = await _externalService.ProcessPaymentAsync(product.Price * quantity);

        // Return payment result or order confirmation
        return RedirectToAction("PaymentResult", new { result = paymentResult });
    }

    public IActionResult PaymentResult(string result)
    {
        return View("PaymentResult", result);
    }
}

// ProductRepository.cs (handles database interaction)
public class ProductRepository
{
    private readonly POSDbContext _context;

    public ProductRepository(POSDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}

// POSDbContext.cs (Database context for EF)
public class POSDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    
    public POSDbContext(DbContextOptions<POSDbContext> options) : base(options) { }
}

// InventoryManager.cs (manages stock levels)
public class InventoryManager
{
    private readonly POSDbContext _context;

    public InventoryManager(POSDbContext context)
    {
        _context = context;
    }

    public async Task UpdateStockLevelsAsync(int productId, int quantitySold)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null)
        {
            product.StockQuantity -= quantitySold;
            await _context.SaveChangesAsync();
        }
    }

    public async Task ManagePurchaseOrdersAsync(int productId, int quantityOrdered)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null)
        {
            product.StockQuantity += quantityOrdered;
            await _context.SaveChangesAsync();
        }
    }
}

// ExternalService.cs (handles payment API integration)
public class ExternalService
{
    private readonly HttpClient _httpClient;

    public ExternalService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> ProcessPaymentAsync(decimal amount)
    {
        var paymentData = new 
        {
            amount = amount,
            currency = "USD",
            payment_method = "credit_card" // This would vary depending on the gateway
        };

        var content = new StringContent(JsonConvert.SerializeObject(paymentData), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api.paymentgateway.com/process", content);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result; // Handle the response appropriately
        }
        else
        {
            throw new Exception("Payment processing failed");
        }
    }
}

// KitchenOrderSystem.cs (handles communication with the kitchen system)
public class KitchenOrderSystem
{
    public void CommunicateOrder(string orderDetails)
    {
        // Use an API, WebSocket, or other real-time communication method to notify kitchen staff
        // Example using email:
        SendEmailToKitchenStaff(orderDetails);
    }

    private void SendEmailToKitchenStaff(string orderDetails)
    {
        var emailService = new EmailService(); // Assume you have an EmailService to handle sending emails
        emailService.Send("kitchen@example.com", "New Order", orderDetails);
    }
}

// MenuCustomization.cs (handles customizing menu items)
public class MenuCustomization
{
    private readonly ProductRepository _productRepo;

    public MenuCustomization(ProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task CustomizeMenuItemAsync(int productId, string itemName, decimal newPrice)
    {
        var product = await _productRepo.GetProductByIdAsync(productId);
        if (product != null)
        {
            product.Name = itemName;
            product.Price = newPrice;
            await _productRepo.UpdateProductAsync(product);
        }
    }
}

// Product.cs (entity class for product)
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }  // Added for stock management
}

// Configure Services (Startup.cs or Program.cs)
public void ConfigureServices(IServiceCollection services)
{
    // Add DbContext
    services.AddDbContext<POSDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    
    // Add services for repositories
    services.AddScoped<ProductRepository>();
    services.AddScoped<InventoryManager>();
    services.AddScoped<ExternalService>();

    // Add controllers
    services.AddControllersWithViews();
}

// PaymentResult.cshtml (view to display the result of a payment)
@model string

<h2>Payment Result</h2>
<p>@Model</p>

// Add corresponding views and UI components as needed (Index.cshtml, ProductList.cshtml, etc.)