public record EnrollmentRecord(string StudentId, string CourseCode, DateTime EnrolledAt);
public class Course
{
    public required string Code { get; init; }
    public required string Title
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
        ? value
        : throw new ArgumentException("Title cannot be empty or whitespace.", nameof(value));
    }
    // C# 14 Auto-property validation using 'field' public int Capacity
    public int Capacity
    {
        get;
        set => field = value >= 0
        ? value
        : throw new ArgumentOutOfRangeException(nameof(value), "System constraint: Capacity must be greater than zero.");
    }
    public int EnrolledCount { get; set; }

    public override string ToString() => 
        $"Course: {Title} ({Code}), Capacity: {Capacity} (Enrolled: {EnrolledCount})";
}

public class Student
{
    public required string Id { get; init; }
    public required string Name
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
        ? value
        : throw new ArgumentException("Name cannot be empty or whitespace.", nameof(value));
    }
    public int Age
    {
        get;
        set => field = value is >= 16 and <= 100
        ? value
        : throw new ArgumentOutOfRangeException(nameof(value), "Age must be between 16 and 100.");
    }
    public decimal GPA
    {
        get;
        set => field = value is >= 0.0m and <= 4.0m
        ? value
        : throw new ArgumentOutOfRangeException(nameof(value), "GPA must be between 0.0and 4.0.");
    }

    public decimal GrantAmount
    {
        get;
        set => field = (value > 0)
        ? value
        : throw new ArgumentOutOfRangeException(nameof(value), "Grant amount cannot be negative.");
    }

    public int EnrollmentYear
    {
        get;
        set => field = (value >= 2000 && value <= 2100)
        ? value
        : throw new ArgumentOutOfRangeException(nameof(value), "Enrollment year must be between 2000 and 2100");
    }
    public override string ToString() => 
        $"Student: {Name} ({Id}), Age: {Age}, GPA: {GPA:F2}";

}

public interface IGradable
{
    string Title { get; }
    decimal CalculateGrade();
}

public class Quiz : IGradable
{
    public required string Title { get; init; }
    public required int CorrectAnswers { get; init; }
    public required int TotalQuestions { get; init; }
    public decimal CalculateGrade()
    {
        if (TotalQuestions == 0) return 0m;
        return (decimal)CorrectAnswers / TotalQuestions * 100m;
    }

     public override string ToString() => 
        $"[Quiz] {Title} - Grade: {CalculateGrade():F2}%";
}
public class LabAssignment : IGradable
{
    public required string Title { get; init; }
    public required decimal FunctionalityScore { get; init; }
    public required decimal CodeQualityScore { get; init; }
    public decimal CalculateGrade()
    {
        // 70% functionality, 30% code quality
        return (FunctionalityScore * 0.7m) + (CodeQualityScore * 0.3m);
    }
    public override string ToString() => 
        $"[Lab] {Title} - Grade: {CalculateGrade():F2}%";
}
