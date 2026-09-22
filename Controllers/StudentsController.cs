using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static List<Student> students = new()
    {
        new Student("Lewis", 85),
        new Student("Bob", 65),
        new Student("Alice", 40)
    };

    [HttpGet]
    public ActionResult<List<Student>> GetAll(
        [FromQuery] int? minScore,
        [FromQuery] string? sortBy)
    {
        List<Student> filteredStudents = students;
        if (minScore != null)
        {
            filteredStudents = filteredStudents.Where(s => s.Score >= minScore).ToList();
        }
        if (sortBy == "name")
        {
            filteredStudents = filteredStudents.OrderBy(s => s.Name).ToList();
        }
        if (sortBy == "score")
        {
            filteredStudents = filteredStudents.OrderBy(s => s.Score).ToList();
        }
        return Ok(filteredStudents);
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetById(int id)
    {
        Student? student = students.FirstOrDefault(student => student.Id == id);
        if (student != null)
        {
            return Ok(student);
        }
        else
        {
            return NotFound("Student Not Found.");
        }
    }

    [HttpPost]
    public ActionResult<Student> Create(StudentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        Student newStudent = new(name: dto.Name, score: dto.Score);

        students.Add(newStudent);

        return CreatedAtAction(nameof(GetById), new { id = newStudent.Id }, newStudent);
    }

    [HttpPut("{id}")]
    public ActionResult<Student> Update(int id, StudentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        Student? existingStudent = students.FirstOrDefault(s => s.Id == id);

        if (existingStudent == null) return NotFound("Student Not Found.");

        existingStudent.Name = dto.Name;
        existingStudent.Score = dto.Score;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Student? student = students.FirstOrDefault(s => s.Id == id);

        if (student == null) return NotFound("Student Not Found.");

        students.Remove(student);

        return NoContent();
    }

}