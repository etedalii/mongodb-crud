using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ImageSaveAndReadMongoDb.Models;
using ImageSaveAndReadMongoDb.Repositories.Interfaces;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace ImageSaveAndReadMongoDb.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository; 

    public HomeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<string> SaveFile([FromForm]FileUpload fileObj)
    {
        Employee employee = JsonConvert.DeserializeObject<Employee>(fileObj.Employee);
        
        if (fileObj.File.Length > 0)
        {
            using (var ms = new MemoryStream())
            {
                fileObj.File.CopyTo(ms);
                var fileBytes = ms.ToArray();
                
                // Assign the byte array to the Photo field
                employee.Photo = fileBytes;

                // Save the employee with the photo in MongoDB
                employee = await _employeeRepository.Save(employee);

                if (!string.IsNullOrEmpty(employee.Id))
                {
                    return "File saved successfully.";
                }
            }
        }

        return "Failed to save file.";
    }

    [HttpGet]
    public async Task<JsonResult> GetSavedEmployee()
    {
        var employee = await _employeeRepository.GetSavedEmployee();
        
        if (employee != null && employee.Photo != null)
        {
            // Convert the Photo byte array to a Base64 string for display
            var base64Photo = Convert.ToBase64String(employee.Photo);
            return Json(new { Name = employee.Name, Photo = base64Photo });
        }
        
        return Json(null);
    }
}