using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers
{
    [ApiController]
    [Route("api/Course/{courseId}/Module/{moduleId:int?}/[controller]")]
    public class AssignmentController : ControllerBase
    {
        DomainContext db = new DomainContext();

        [HttpGet]
        public ActionResult<IEnumerable<Assignment>> GetAllAssignments(int? moduleId)
        {
            if(moduleId != null){
                var assignment = db.Assignments.Where(a => a.ModuleID == moduleId);
                db.SaveChanges();
                return Ok(assignment);
            }
            else{
                var assignment = db.Assignments;
                db.SaveChanges();
                return Ok(assignment);
            }
        }

        [HttpGet("{AssignmentId:int}")]
        public ActionResult<Assignment> GetAssignmentById(int AssignmentId)
        {
            db.SaveChanges();
            var assignment = db.Assignments.First(a => a.ID == AssignmentId);
            return Ok(assignment);
        }
        [HttpPost]
        public ActionResult<Assignment> CreateAssignment(Assignment assignment)
        {
            db.Add(assignment);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut("{AssignmentId:int}")]

        public ActionResult UpdateAssignment(int AssignmentId, Assignment assignment)
        {
            var existingAssignment = db.Assignments.Find(AssignmentId);

            if (existingAssignment == null)
            {
                return NotFound();
            }

            existingAssignment.Name = assignment.Name;
            existingAssignment.Grade = assignment.Grade;
            existingAssignment.DueDate = assignment.DueDate;
            existingAssignment.ModuleID = assignment.ModuleID;

            db.SaveChanges();
            return Ok();
            }
        
        [HttpDelete("{AssignmentId:int}")]
        public ActionResult DeleteAssignment(int AssignmentId)
        {
            var assignmentToRemove = db.Assignments.Find(AssignmentId);

            if (assignmentToRemove == null)
            {
                return NotFound();
            }

            db.Remove(assignmentToRemove);
            db.SaveChanges();
            return Ok();
        }
}
}