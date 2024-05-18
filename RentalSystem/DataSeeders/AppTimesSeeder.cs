using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentalSystem.DataSeeders
{
	public static class AppTimesSeeder
	{
		public static void SeedData(this EntityTypeBuilder<Models.AppTimes> builder)
		{
			var now = new DateTime(year: 2024, month: 3, day: 10);

			builder.HasData(
				new Models.AppTimes
				{
					IdTime = 1,
					Name = "Giờ",
					Time = 0,
					CreatedDate = now,
				},
				new Models.AppTimes
				{
					IdTime = 2,
					Name = "Ngày",
					Time = 1,
					CreatedDate = now,
				},
				new Models.AppTimes
				{
					IdTime = 3,
					Name = "Tháng",
					Time = 30,
					CreatedDate = now,
				},
				new Models.AppTimes
				{
					IdTime = 4,
					Name = "Năm",
					Time = 365,
					CreatedDate = now,
				}
			);
		}
	}
}
