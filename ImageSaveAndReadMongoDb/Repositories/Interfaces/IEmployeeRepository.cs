using ImageSaveAndReadMongoDb.Models;

namespace ImageSaveAndReadMongoDb.Repositories.Interfaces;

public interface IEmployeeRepository
{
     Task<Employee> GetById(string id);
    
    Task<Employee> Save(Employee employee);
    
    Task<Employee> Update(Employee employee);
    
    Task<Employee> Delete(string id);
    
    Task<Employee> GetSavedEmployee();
    
    Task<List<Employee>> GetSavedEmployees();
}