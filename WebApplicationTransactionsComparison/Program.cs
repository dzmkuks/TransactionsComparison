using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TransactionsEF;
using WebApplicationTransactionsComparison.Interfaces;
using WebApplicationTransactionsComparison.Repositories;
using WebApplicationTransactionsComparison.Services;

namespace WebApplicationTransactionsComparison
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<TransactionContext>();
            builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
            builder.Services.AddTransient<IComparisonService, ComparisonService>();
            builder.Services.AddMemoryCache();
			builder.Services.TryAdd(ServiceDescriptor.Singleton<IMemoryCache, MemoryCache>());
            builder.WebHost.ConfigureKestrel(opts => {
                opts.Limits.MaxRequestBodySize = long.MaxValue;
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

			app.Run();
		}
	}
}