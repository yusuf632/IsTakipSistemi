using EmployeePortal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace EmployeePortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent cascade delete on Designation FK
            modelBuilder.Entity<Employee>()
            .HasOne(e => e.Designation)
            .WithMany()
            .HasForeignKey(e => e.DesignationId)
            .OnDelete(DeleteBehavior.Restrict);

            // Seed EmployeeTypes
            modelBuilder.Entity<EmployeeType>().HasData(
                new EmployeeType { Id = 1, Name = "Kalıcı" },
                new EmployeeType { Id = 2, Name = "Geçici" },
                new EmployeeType { Id = 3, Name = "Sözleşme" },
                new EmployeeType { Id = 4, Name = "Stajyer" }
            );

            // Seed Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "IT" },
                new Department { Id = 2, Name = "İK" },
                new Department { Id = 3, Name = "Satış" },
                new Department { Id = 4, Name = "Yönetici" }
            );

            // Seed Designations with DepartmentId
            modelBuilder.Entity<Designation>().HasData(
                new Designation { Id = 1, Name = "Yazılım Geliştirici", DepartmentId = 1 },
                new Designation { Id = 2, Name = "Sistem Yöneticisi", DepartmentId = 1 },
                new Designation { Id = 3, Name = "Ağ Mühendisi", DepartmentId = 1 },

                new Designation { Id = 4, Name = "İK Uzmanı", DepartmentId = 2 },
                new Designation { Id = 5, Name = "İK Yöneticisi", DepartmentId = 2 },
                new Designation { Id = 6, Name = "Yetenek Kazanımı Koordinatörü", DepartmentId = 2 },

                new Designation { Id = 7, Name = "Satış Yöneticisi", DepartmentId = 3 },
                new Designation { Id = 8, Name = "Satış Müdürü", DepartmentId = 3 },
                new Designation { Id = 9, Name = "Müşteri Yöneticisi", DepartmentId = 3 },

                new Designation { Id = 10, Name = "Ofis Müdürü", DepartmentId = 4 },
                new Designation { Id = 11, Name = "Yönetici Asistanı", DepartmentId = 4 },
                new Designation { Id = 12, Name = "Resepsiyonist", DepartmentId = 4 }
            );

            // Seed initial data
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FullName = "Murat Kılıç", Email = "murat@example.com", DepartmentId = 1, DesignationId = 1, HireDate = new DateTime(2020, 1, 15), DateOfBirth = new DateTime(1990, 3, 12), EmployeeTypeId = 1, Gender = "Erkek", Salary = 60000m },
                new Employee { Id = 2, FullName = "Muhammet Yıldız", Email = "yildiz@example.com", DepartmentId = 2, DesignationId = 5, HireDate = new DateTime(2018, 5, 20), DateOfBirth = new DateTime(1985, 8, 22), EmployeeTypeId = 1, Gender = "Erkek", Salary = 80000m },
                new Employee { Id = 3, FullName = "Ali Oğul", Email = "ali@example.com", DepartmentId = 3, DesignationId = 7, HireDate = new DateTime(2021, 3, 10), DateOfBirth = new DateTime(1992, 6, 30), EmployeeTypeId = 3, Gender = "Erkek", Salary = 50000m },
                new Employee { Id = 4, FullName = "Sahra Güzel", Email = "sahra@example.com", DepartmentId = 4, DesignationId = 11, HireDate = new DateTime(2022, 7, 5), DateOfBirth = new DateTime(1995, 11, 15), EmployeeTypeId = 2, Gender = "Kadın", Salary = 40000m },
                new Employee { Id = 5, FullName = "Kemal Aşık", Email = "kemal@example.com", DepartmentId = 1, DesignationId = 3, HireDate = new DateTime(2019, 4, 18), DateOfBirth = new DateTime(1989, 2, 25), EmployeeTypeId = 1, Gender = "Erkek", Salary = 70000m },
                new Employee { Id = 6, FullName = "Deniz Göz", Email = "deniz@example.com", DepartmentId = 2, DesignationId = 4, HireDate = new DateTime(2017, 10, 12), DateOfBirth = new DateTime(1987, 9, 10), EmployeeTypeId = 1, Gender = "Kadın", Salary = 75000m },
                new Employee { Id = 7, FullName = "Yusuf İslam", Email = "yusuf@example.com", DepartmentId = 3, DesignationId = 8, HireDate = new DateTime(2020, 2, 20), DateOfBirth = new DateTime(1990, 1, 5), EmployeeTypeId = 3, Gender = "Erkek", Salary = 85000m },
                new Employee { Id = 8, FullName = "Tarık Demir", Email = "tarik@example.com", DepartmentId = 4, DesignationId = 10, HireDate = new DateTime(2021, 6, 8), DateOfBirth = new DateTime(1993, 4, 18), EmployeeTypeId = 1, Gender = "Erkek", Salary = 65000m },
                new Employee { Id = 9, FullName = "Nezaket Yılmaz", Email = "nezaket@example.com", DepartmentId = 1, DesignationId = 2, HireDate = new DateTime(2022, 8, 15), DateOfBirth = new DateTime(1997, 12, 20), EmployeeTypeId = 4, Gender = "Kadın", Salary = 30000m },
                new Employee { Id = 10, FullName = "Yılmaz Yıldız", Email = "yilmaz@example.com", DepartmentId = 2, DesignationId = 6, HireDate = new DateTime(2018, 11, 25), DateOfBirth = new DateTime(1986, 7, 12), EmployeeTypeId = 2, Gender = "Erkek", Salary = 55000m },
                new Employee { Id = 11, FullName = "Ayşe Gül", Email = "ayse@example.com", DepartmentId = 3, DesignationId = 7, HireDate = new DateTime(2019, 9, 10), DateOfBirth = new DateTime(1994, 5, 6), EmployeeTypeId = 1, Gender = "Kadın", Salary = 52000m },
                new Employee { Id = 12, FullName = "Yasemin Birinci", Email = "yasemin@example.com", DepartmentId = 4, DesignationId = 12, HireDate = new DateTime(2020, 10, 3), DateOfBirth = new DateTime(1996, 8, 21), EmployeeTypeId = 2, Gender = "Kadın", Salary = 38000m },
                new Employee { Id = 13, FullName = "Nazlı Duru", Email = "nazli@example.com", DepartmentId = 1, DesignationId = 2, HireDate = new DateTime(2018, 12, 1), DateOfBirth = new DateTime(1991, 9, 18), EmployeeTypeId = 1, Gender = "Kadın", Salary = 65000m },
                new Employee { Id = 14, FullName = "Yasin Yakışıklı", Email = "yasin@example.com", DepartmentId = 2, DesignationId = 4, HireDate = new DateTime(2017, 11, 30), DateOfBirth = new DateTime(1988, 4, 2), EmployeeTypeId = 1, Gender = "Erkek", Salary = 76000m },
                new Employee { Id = 15, FullName = "Mustafa Kemal", Email = "mustafa@example.com", DepartmentId = 3, DesignationId = 9, HireDate = new DateTime(2021, 7, 21), DateOfBirth = new DateTime(1993, 3, 17), EmployeeTypeId = 3, Gender = "Erkek", Salary = 62000m }
            );
        }
    }
}