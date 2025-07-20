using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortal.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad soyad gereklidir")]
        [StringLength(100, ErrorMessage = "Ad soyad 100 karakterden uzun olamaz")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email gereklidir")]
        [EmailAddress(ErrorMessage = "Geçersiz E-posta Adresi")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Departman gereklidir")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        [Required(ErrorMessage = "Tanımlama gereklidir")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; } = null!;

        [Required(ErrorMessage = "İşe alım tarihi gereklidir")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Doğum tarihi gereklidir")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Çalışan tipi gereklidir")]
        public int EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; } = null!;

        [Required(ErrorMessage = "Cinsiyet gereklidir")]
        [StringLength(6, ErrorMessage = "Cinsiyet Erkek, Kadın veya Diğer olmalıdır")]
        public string Gender { get; set; } = null!;

        [Required(ErrorMessage = "Maaş gereklidir")]
        [Range(0, double.MaxValue, ErrorMessage = "Maaş pozitif bir sayı olmalıdır")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
    }
}