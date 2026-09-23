using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static List<Student> studentsTest = new()
    {
        new Student("Lewis", 85),
        new Student("Bob", 65),
        new Student("Alice", 40)
    };

    private readonly AppDbContext _context;
    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetAll(
        [FromQuery] int? minScore,
        [FromQuery] string? sortBy)
    {
        var students = await _context.Students.ToListAsync();
        List<Student> filteredStudents = students;
        if (minScore != null)
        {
            filteredStudents = filteredStudents.Where(s => s.Score >= minScore).ToList();
        }
        if (sortBy == "name")
        {
            filteredStudents = filteredStudents.OrderBy(s => s.Name).ToList();
        }
        else if (sortBy == "score")
        {
            filteredStudents = filteredStudents.OrderBy(s => s.Score).ToList();
        }
        return Ok(filteredStudents);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetById(int id)
    {
        var student = await _context.Students.FindAsync(id);
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

        studentsTest.Add(newStudent);

        return CreatedAtAction(nameof(GetById), new { id = newStudent.Id }, newStudent);
    }

    [HttpPut("{id}")]
    public ActionResult<Student> Update(int id, StudentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        Student? existingStudent = studentsTest.FirstOrDefault(s => s.Id == id);

        if (existingStudent == null) return NotFound("Student Not Found.");

        existingStudent.Name = dto.Name;
        existingStudent.Score = dto.Score;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Student? student = studentsTest.FirstOrDefault(s => s.Id == id);

        if (student == null) return NotFound("Student Not Found.");

        studentsTest.Remove(student);

        return NoContent();
    }

}