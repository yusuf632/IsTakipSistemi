using EmployeePortal.Models;
using EmployeePortal.Services;
using EmployeePortal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> List(
            string? searchTerm,
            int? SelectedDepartmentId,
            int? SelectedEmployeeTypeId,
            int pageNumber = 1,
            int pageSize = 5)
        {
            var (employees, totalCount) = await _employeeService.GetEmployees(searchTerm, SelectedDepartmentId, SelectedEmployeeTypeId, pageNumber, pageSize);

            var viewModel = new EmployeeListViewModel
            {
                Employees = employees,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Total = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                SearchTerm = searchTerm,
                SelectedDepartmentId = SelectedDepartmentId,
                SelectedEmployeeTypeId = SelectedEmployeeTypeId,
                Departments = await _employeeService.GetDepartmentsAsync(),
                EmployeeTypes = await _employeeService.GetEmployeeTypesAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new EmployeeCreateUpdateViewModel
            {
                Departments = await _employeeService.GetDepartmentsAsync(),
                EmployeeTypes = await _employeeService.GetEmployeeTypesAsync(),
                Designations = new List<Designation>()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    FullName = vm.AdSoyad,
                    Email = vm.Email,
                    DepartmentId = vm.DepartmentId,
                    DesignationId = vm.DesignationId,
                    HireDate = vm.Isealimtarihi,
                    DateOfBirth = vm.Dogumtarihi,
                    EmployeeTypeId = vm.EmployeeTypeId,
                    Gender = vm.Cinsiyet,
                    Salary = vm.Maas
                };

                await _employeeService.CreateEmployeeAsync(employee);
                return RedirectToAction("Success", new { id = employee.Id });
            }

            vm.Departments = await _employeeService.GetDepartmentsAsync();
            vm.EmployeeTypes = await _employeeService.GetEmployeeTypesAsync();
            vm.Designations = vm.DepartmentId != 0 ? await _employeeService.GetDesignationsByDepartmentAsync(vm.DepartmentId) : new List<Designation>();

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();

            var vm = new EmployeeCreateUpdateViewModel
            {
                Id = employee.Id,
                AdSoyad = employee.FullName,
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                DesignationId = employee.DesignationId,
                Isealimtarihi = employee.HireDate,
                Dogumtarihi = employee.DateOfBirth,
                EmployeeTypeId = employee.EmployeeTypeId,
                Cinsiyet = employee.Gender,
                Maas = employee.Salary,
                Departments = await _employeeService.GetDepartmentsAsync(),
                EmployeeTypes = await _employeeService.GetEmployeeTypesAsync(),
                Designations = await _employeeService.GetDesignationsByDepartmentAsync(employee.DepartmentId)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = vm.Id!.Value,
                    FullName = vm.AdSoyad,
                    Email = vm.Email,
                    DepartmentId = vm.DepartmentId,
                    DesignationId = vm.DesignationId,
                    HireDate = vm.Isealimtarihi,
                    DateOfBirth = vm.Dogumtarihi,
                    EmployeeTypeId = vm.EmployeeTypeId,
                    Gender = vm.Cinsiyet,
                    Salary = vm.Maas
                };

                await _employeeService.UpdateEmployeeAsync(employee);
                TempData["Mesaj"] = $"Kimliği {employee.Id} ve adı {employee.FullName} olan kişi güncellendi.";
                return RedirectToAction("List");
            }

            vm.Departments = await _employeeService.GetDepartmentsAsync();
            vm.EmployeeTypes = await _employeeService.GetEmployeeTypesAsync();
            vm.Designations = vm.DepartmentId != 0 ? await _employeeService.GetDesignationsByDepartmentAsync(vm.DepartmentId) : new List<Designation>();

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();

            await _employeeService.DeleteEmployeeAsync(id);
            TempData["Mesaj"] = $"Kimliği {id} ve adı {employee.FullName} olan kişi silindi.";
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<JsonResult> GetDesignations(int departmentId)
        {
            var designations = await _employeeService.GetDesignationsByDepartmentAsync(departmentId);
            var result = designations.Select(d => new { id = d.Id, name = d.Name }).ToList();
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Success(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }
    }
}