public interface IGradeable
{
    string GetGrade();
}

public class Student : IGradeable
{
    public static int NumStudents = 1;
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }

    public Student(string name, int score)
    {
        Id = NumStudents;
        Name = name;
        Score = score;
        NumStudents += 1;
    }

    public string GetGrade()
    {
        if (Score >= 70) return "A";
        if (Score >= 50) return "B";
        return "C";
    }


}