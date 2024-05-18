using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Models
{
    public class AppPolicy
    {
        [Key]
        public int IdPolicy { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(5000)]
        [MinLength(100, ErrorMessage = "Nội dung chính sách tối thiểu 100 từ")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IdUser { get; set; }
        public AppUsers appUsers { get; set; }
    }
}
