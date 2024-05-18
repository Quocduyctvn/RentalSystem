using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentalSystem.DataSeeders
{
	public static class AppRentalObjectsSeeder
	{
		public static void SeedData(this EntityTypeBuilder<Models.AppRentalObjects> builder)
		{
			var now = new DateTime(year: 2024, month: 3, day: 10);

			builder.HasData(
				new Models.AppRentalObjects
				{
					IdRentalObject = 1,
					Name = "Hộ gia đình",
					CreatedDate = now,
				},
				new Models.AppRentalObjects
				{
					IdRentalObject = 2,
					Name = "Nhân viên công sở",
					CreatedDate = now,
				},
				new Models.AppRentalObjects
				{
					IdRentalObject = 3,
					Name = "Học sinh - Sinh viên",
					CreatedDate = now,
				},
				new Models.AppRentalObjects
				{
					IdRentalObject = 4,
					Name = "Doanh nghiệp",
					CreatedDate = now,
				},
				new Models.AppRentalObjects
				{
					IdRentalObject = 5,
					Name = "Tùy chọn khác",
					CreatedDate = now,
				}
			);
		}
	}
}
