using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TransactionsEF
{
	public class TransactionContext : DbContext
	{
		public DbSet<Transaction> Transactions { get; set; } = null!;

		public TransactionContext()
		{
            Database.EnsureDeleted();
            Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Transaction>(entity =>
			{
				entity.HasKey(x => x.Id).HasAnnotation("Sqlite:Autoincrement", true); ;
				entity.Property(x => x.Id).ValueGeneratedOnAdd().HasAnnotation("Sqlite:Autoincrement", true);

			});

			Assembly assembly = Assembly.GetExecutingAssembly();
			string resourceName = "TransactionsEF.Transactions.csv";
			List<Transaction> transactions;
			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			using (CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				csvReader.Context.RegisterClassMap<TransactionMap>();
				transactions = csvReader.GetRecords<Transaction>().ToList();
			}

			int count = 1;
			transactions.ForEach(transaction => { transaction.Id = count++; });

			modelBuilder.Entity<Transaction>().HasData(
				transactions
			);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=Transactions.db");
		}
	}
}
