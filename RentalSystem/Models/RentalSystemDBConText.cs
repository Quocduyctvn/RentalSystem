using Microsoft.EntityFrameworkCore;
using RentalSystem.DataSeeders;
using static RentalSystem.WebConfig.Constant.AuthConst;

namespace RentalSystem.Models
{
	public class RentalSystemDBConText : DbContext
	{
		public RentalSystemDBConText(DbContextOptions options) : base(options)
		{
		}

		public DbSet<AppPosts> AppPost { get; set; }
		public DbSet<AppImgPosts> AppImgPost { get; set; }
		public DbSet<AppAddress> AppAddress { get; set; }
		public DbSet<AppCategory> AppCategory { get; set; }
		public DbSet<AppContacts> AppContact { get; set; }
		public DbSet<AppHistoryPayments> AppHistoryPayment { get; set; }
		public DbSet<AppPermissions> AppPermission { get; set; }
		public DbSet<AppRolePermission> AppRolePermission { get; set; }
		public DbSet<AppRoles> AppRole { get; set; }
		public DbSet<AppTypeOfService> AppTypeOfService { get; set; }
		public DbSet<AppUsers> AppUser { get; set; }
		//public DbSet<AppPaymentMethods> AppPaymentMethods { get; set; }
		public DbSet<AppTimes> AppTimes { get; set; }
		public DbSet<AppRentalObjects> AppRentalObject { get; set; }

		public DbSet<AppContactFeedback> AppContactFeedback { get; set; }
		public DbSet<AppPolicy> AppPolicy { get; set; }
		public DbSet<AppRequest> AppRequest { get; set; }

		public DbSet<AppCategoryNews> AppCategoryNews  { get; set; }

		public DbSet<AppNews> AppNews { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AppPosts>()
			   .HasOne(i => i.appAddress)
			   .WithOne(i => i.appPosts)
			   .HasForeignKey<AppPosts>(i => i.IdAddress);
			modelBuilder.Entity<AppPosts>()
				.HasMany(i => i.appImgPosts)
				.WithOne(i => i.appPost)
				.HasForeignKey(i => i.IdPost);
			modelBuilder.Entity<AppPosts>()
				.HasOne(i=> i.appHistoryPayments)
				.WithOne(i=> i.appPosts)
				.HasForeignKey<AppHistoryPayments>(i => i.IdPost);
			modelBuilder.Entity<AppPosts>()
				.HasOne(i => i.appTypeOfService)
				.WithMany(i => i.appPosts)
				.HasForeignKey(i => i.IdTypeOfService)
				.OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<AppPosts>()
				.HasOne(i=> i.appUsers)
				.WithMany(i=> i.appPosts)
				.HasForeignKey(i=> i.IdUser);
			modelBuilder.Entity<AppPosts>()
				.HasOne(i => i.appCategory)
				.WithMany(i => i.appPosts)
				.HasForeignKey(I => I.IdCategory);
			//modelBuilder.Entity<AppPosts>()
			//	.HasOne(i => i.appPaymentMethod)
			//	.WithMany(i => i.appPosts)
			//	.HasForeignKey(i => i.IdPaymentMethod);
			modelBuilder.Entity<AppPosts>()
				.HasOne(i => i.appRentalObject)
				.WithMany(i => i.appPosts)
				.HasForeignKey(i => i.IdRentalObject);



			modelBuilder.Entity<AppUsers>()
				.HasOne(i=> i.appRole)
				.WithMany(i=> i.appUsers)
				.HasForeignKey(i=>i.IdRole);


			modelBuilder.Entity<AppRolePermission>()
				.HasOne(i => i.appRole)
				.WithMany(i => i.appRolePermissions)
				.HasForeignKey(i => i.IdRole);
			modelBuilder.Entity<AppRolePermission>()
				.HasOne(i => i.appPermission)
				.WithMany(i => i.appRolePermissions)
				.HasForeignKey(i => i.IdPermission);

			modelBuilder.Entity<AppContactFeedback>()
				.HasOne(i=> i.appContacts)
			   .WithOne(i => i.appContactFeedback)
			   .HasForeignKey<AppContactFeedback>(i => i.IdContact);

			modelBuilder.Entity<AppContactFeedback>()
				.HasOne(i => i.appUsers)
				.WithMany(i => i.appContactFeedback)
				.HasForeignKey(i => i.IdUser);

            modelBuilder.Entity<AppPolicy>()
                .HasOne(i => i.appUsers)
                .WithMany(i => i.appPolicy)
                .HasForeignKey(i => i.IdUser);

			modelBuilder.Entity<AppRequest>()
				.HasOne(i => i.appUsers)
				.WithMany(i => i.appRequest)
				.HasForeignKey(i => i.IdUser)
				 .OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<AppRequest>()
				.HasOne(i => i.appPosts)
				.WithMany(i => i.appRequest)
				.HasForeignKey(i => i.IdPost);

            modelBuilder.Entity<AppContactFeedback>()
                .HasOne(i => i.appRequest)
                .WithOne(i => i.appContactFeedback)
                .HasForeignKey<AppContactFeedback>(i => i.IdRequest)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<AppNews>()
				.HasOne(i => i.appCategoryNews)
				.WithMany(i => i.appNews)
				.HasForeignKey(i => i.IdCateNews);
			modelBuilder.Entity<AppNews>()
				.HasOne(i => i.appUser)
				.WithMany(i => i.appNews)
				.HasForeignKey(i => i.IdUser);





			modelBuilder.Entity<AppPermissions>().SeedData();
            modelBuilder.Entity<AppRoles>().SeedData();
            modelBuilder.Entity<AppUsers>().SeedData();
			//modelBuilder.Entity<AppPaymentMethods>().SeedData();
			modelBuilder.Entity<AppRentalObjects>().SeedData();
			modelBuilder.Entity<AppTimes>().SeedData();
			
			// TẠM THỜI KHÓA ROLE+pER => lên giao diện thêm tay 
			//modelBuilder.Entity<AppRolePermission>().SeedData();
		}
	}
}
