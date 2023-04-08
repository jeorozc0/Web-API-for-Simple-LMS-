using Microsoft.AspNetCore.Mvc;
using WebApi2;

namespace WebApi2.Controllers
{
    [ApiController]
    
    [Route("/api/Course/{id:int?}/[controller]")]
    public class ModuleController : ControllerBase
    {
        DomainContext db = new DomainContext();

    [HttpGet]
    public ActionResult<IEnumerable<Module>> GetAllModules(int id)
    {
        var module = db.Modules.Where(m => m.CourseID == id);
        db.SaveChanges();
        return Ok(module);
        
    }

    [HttpGet("{ModuleId:int}")]
    public ActionResult<Module> GetModuleById(int ModuleId)
    {
        db.SaveChanges();
        var module = db.Modules.First(m => m.ID == ModuleId);
        return Ok(module);
    }

    [HttpPost]
    public ActionResult<Module> CreateModule(Module module)
    {
        db.Add(module);
        db.SaveChanges();
        return Ok();
    }

    [HttpPut("{ModuleId:int}")]
    public ActionResult UpdateModule(int ModuleId, Module module)
    {
        var existingModule = db.Modules.Find(ModuleId);

        if (existingModule == null)
        {
            return NotFound();
        }

        existingModule.Name = module.Name;


        db.SaveChanges();

        return Ok();
    }

    [HttpDelete("{ModuleId:int}")]
    public ActionResult DeleteModule(int ModuleId)
    {
        var moduleToRemove = db.Modules.Find(ModuleId);

        if (moduleToRemove == null)
        {
            return NotFound();
        }

        db.Modules.Remove(moduleToRemove);
        db.SaveChanges();

        return Ok();
    }
}
}