public class EnrollmentService
{
    public EnrollmentRecord ProcessRegistration(Student? student, Course? course)
    {
        // TODO 1: Add guard clauses - fail fast to avoid the "Pyramid of Doom"
        
        // Check if student is null
        if (student is null) throw new ArgumentNullException(nameof(student));
        
        // Check if course is null
        if (course is null) throw new ArgumentNullException(nameof(course));
        
        // Check if course capacity is zero or negative (Precondition)
        if (course.Capacity <= 0) 
            throw new ArgumentOutOfRangeException(nameof(course.Capacity), "Course capacity must be positive.");

        // Check if course is full (Business Rule)
        if (course.EnrolledCount >= course.Capacity)
            throw new InvalidOperationException("Course has reached maximum capacity.");

        // TODO 2: Use a switch expression on student.GPA to classify academic standing
        string standing = student.GPA switch
        {
            >= 3.5m => "Honors",
            >= 2.5m => "Good Standing",
            _       => "Academic Warning" 
        };

        Console.WriteLine($"{student.Name} is in {standing}.");

        // TODO 3: Return a new EnrollmentRecord with student.Id, course.Code, and DateTime.UtcNow
        return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow);
    }
}