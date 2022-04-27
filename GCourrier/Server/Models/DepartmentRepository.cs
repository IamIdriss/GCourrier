using GCourrier.Server.Data;
using GCourrier.Shared;
using Microsoft.EntityFrameworkCore;

namespace GCourrier.Server.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly GCourrierDbContext gCourrierDbContext;

        public DepartmentRepository(GCourrierDbContext gCourrierDbContext)
        {
            this.gCourrierDbContext = gCourrierDbContext;
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            return await gCourrierDbContext.Department
                .FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await gCourrierDbContext.Department.ToListAsync();
        }
    }
}
