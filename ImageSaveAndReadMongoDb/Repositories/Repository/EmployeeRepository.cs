using ImageSaveAndReadMongoDb.Models;
using ImageSaveAndReadMongoDb.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ImageSaveAndReadMongoDb.Repositories.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IMongoCollection<Employee> _mongoCollection;
    
    public EmployeeRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _mongoCollection = mongoDatabase.GetCollection<Employee>(mongoDbSettings.Value.CollectionName);
    }

    public async Task<Employee> GetById(string id)
    {
        return await _mongoCollection.Find(e => e.Id == id).FirstOrDefaultAsync(); 
    }

    public async Task<Employee> Save(Employee employee)
    {
       await _mongoCollection.InsertOneAsync(employee);
       return employee;
    }

    public async Task<Employee> Update(Employee employee)
    {
        var objectToUpdate = await _mongoCollection.Find(e => e.Id == employee.Id).FirstOrDefaultAsync();
        await _mongoCollection.ReplaceOneAsync(e => e.Id == employee.Id, objectToUpdate);
        return objectToUpdate;
    }

    public async Task<Employee> Delete(string id)
    {
        var objectToDelete = await _mongoCollection.Find(e => e.Id == id).FirstOrDefaultAsync();
        await _mongoCollection.DeleteOneAsync(e => e.Id == id);
        return objectToDelete; 
    }

    public async Task<Employee> GetSavedEmployee()
    {
        return await _mongoCollection.Find(FilterDefinition<Employee>.Empty).SingleOrDefaultAsync();
    }

    public async Task<List<Employee>> GetSavedEmployees()
    {
        return await _mongoCollection.Find(FilterDefinition<Employee>.Empty).ToListAsync(); 
    }
}