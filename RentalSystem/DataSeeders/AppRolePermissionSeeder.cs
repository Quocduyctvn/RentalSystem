//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using RentalSystem.Models;
//using RentalSystem.WebConfig.Constant;
//using System.Reflection;

//namespace RentalSystem.DataSeeders
//{
//    public static class AppRolePermissionSeeder
//    {
//        public static void SeedData(this EntityTypeBuilder<AppRolePermission> builder)
//        {
//            var now = new DateTime(year: 2024, month: 3, day: 10);
//            // Danh sách các class chứa permission
//            Type[] classType = new Type[]
//            {
//                typeof(AuthConst.AppCategory),
//                typeof(AuthConst.AppContacts),
//                typeof(AuthConst.AppHistoryPayments),
//                typeof(AuthConst.AppPosts),
//                typeof(AuthConst.AppRoles),
//                typeof(AuthConst.AppTypeOfService),
//                typeof(AuthConst.AppUsers),
//				typeof(AuthConst.AppPolicy),
//			};


//            // Cấp quyền cho vai trò
//            var rolePermission = new List<AppRolePermission>();
//            int i = 0;
//            foreach (var type in classType)
//            {
//                var allPermission = GetConstants(type);
//                foreach (var permission in allPermission)
//                {
//                    i++;
//                    rolePermission.Add(new AppRolePermission
//                    {
//                        IdRolePermission = i,
//                        IdPermission = Convert.ToInt32(permission.GetRawConstantValue()),
//                        CreatedDate = now,
//                        IdRole = 2,      // Vai trò được tạo ở AppRoleSeeder
//                    });
//                }
//            }

//            builder.HasData(rolePermission);
//        }
//        private static List<FieldInfo> GetConstants(Type type)
//        {
//            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
//                 BindingFlags.Static | BindingFlags.FlattenHierarchy);

//            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
//        }

//    }
//}
