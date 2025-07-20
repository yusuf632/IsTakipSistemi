using EmployeePortal.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.ViewModels
{
    public class EmployeeCreateUpdateViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Ad Soyadı")]

        [Required(ErrorMessage = "Ad Soyad gereklidir")]
        [StringLength(100)]
        public string AdSoyad { get; set; } = null!;

        [Required(ErrorMessage = "E-posta")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name = "Departman")]

        [Required(ErrorMessage = "Departman gereklidir")]
        public int DepartmentId { get; set; }

        [Display(Name = "Tanımlama")]

        [Required(ErrorMessage = "Tanımlama gereklidir")]
        public int DesignationId { get; set; }

        [Display(Name = "İşe alım tarihi")]
        [Required(ErrorMessage = "İşe alım tarihi gereklidir")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(EmployeeCreateUpdateViewModel), nameof(ValidateHireDate))]
        public DateTime Isealimtarihi { get; set; } = DateTime.Today;

        [Display(Name = "Doğum Tarihi")]
        [Required(ErrorMessage = "Doğum tarihi gereklidir")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(EmployeeCreateUpdateViewModel), nameof(ValidateDateOfBirth))]
        public DateTime Dogumtarihi { get; set; } = DateTime.Today.AddYears(-60);

        [Display(Name = "Çalışan Tipi")]
        [Required(ErrorMessage = "Çalışan tipi gereklidir")]
        public int EmployeeTypeId { get; set; }

        [Required(ErrorMessage = "Cinsiyet gereklidir")]
        public string Cinsiyet { get; set; } = null!;

        [Required(ErrorMessage = "Maaş gereklidir")]
        [Range(0, double.MaxValue)]
        public decimal Maas { get; set; }

        // For dropdown lists
        public List<Department>? Departments { get; set; }
        public List<Designation>? Designations { get; set; }
        public List<EmployeeType>? EmployeeTypes { get; set; }

        // Validation methods:
        public static ValidationResult? ValidateHireDate(DateTime? hireDate, ValidationContext context)
        {
            if (!hireDate.HasValue)
                return new ValidationResult("İşe alım tarihi gereklidir");

            if (hireDate.Value.Date > DateTime.Today)
                return new ValidationResult("İşe Alım Tarihi gelecekte olamaz.");

            return ValidationResult.Success;
        }

        public static ValidationResult? ValidateDateOfBirth(DateTime? dob, ValidationContext context)
        {
            if (!dob.HasValue)
                return new ValidationResult("Doğum tarihi gereklidir");

            var minDob = DateTime.Today.AddYears(-60);  
            var maxDob = DateTime.Today.AddYears(-18);  

            if (dob.Value.Date < minDob || dob.Value.Date > maxDob)
                return new ValidationResult($"Doğum tarihi aşağıdaki {minDob:yyyy-MM-dd} ve {maxDob:yyyy-MM-dd} arasında olmalıdır.");

            return ValidationResult.Success;
        }
    }
}