public class EnrollmentService
{
    public async Task<EnrollmentRecord> ProcessRegistrationAsync(Student? student, Course? course)
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
            throw new CapacityReachedException(course.Code);
        
        await Task.Delay(100);

        // TODO 2: Use a switch expression on student.GPA to classify academic standing
        string standing = student.GPA switch
        {
            >= 3.5m => "Honors",
            >= 2.5m => "Good Standing",
            _       => "Academic Warning" 
        };

        //Console.WriteLine($"{student.Name} is in {standing}.");

        // TODO 3: Return a new EnrollmentRecord with student.Id, course.Code, and DateTime.UtcNow
        return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow);
    }
    public async Task<Student> FetchStudentAsync(string id)
    {
        Console.WriteLine($"  Fetching {id}...");
        await Task.Delay(300); // Simulate database latency
        return new Student
        {
            Id = id,
            Name = $"Student-{id}",
            Age = 20,
            GPA = id switch
            {
                "S1" => 3.8m,
                "S2" => 2.4m,
                "S3" => 3.5m,
                "S4" => 1.9m,
                "S5" => 3.2m,
                _ => 2.5m
            }
        };
    }

    // Step 2: Build the Course Fetcher
    public async Task<Course> FetchCourseAsync(string code)
    {
        Console.WriteLine($"  Fetching course {code}...");
        await Task.Delay(200); // Simulate database latency
        return new Course
        {
            Code = code,
            Title = $"Course-{code}",
            Capacity = code switch
            {
                "CRS-101" => 2,
                "CRS-201" => 30,
                "CRS-301" => 15,
                _ => 25
            }
        };
    }

    public async Task<EnrollmentRecord> ProcessEnrollmentAsync(Student? student, Course? course)
    {
        // Guard clauses (Preconditions)
        if (student is null) throw new ArgumentNullException(nameof(student));
        if (course is null) throw new ArgumentNullException(nameof(course));

        // Simulate database processing time
        //await Task.Delay(100);

        // Business Rule: Check Capacity
        if (course.EnrolledCount >= course.Capacity)
        {
            throw new CapacityReachedException(course.Code);
        }

         var record = new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow);

        // --- EXERCISE 6B: SAFE FIRE-AND-FORGET ---
        // We start the email process but don't 'await' it. 
        // The service continues immediately to return the record.
        _ = SendConfirmationAsync(student);

        FinalizeEnrollment(student!);

        return record;
    }

    private async Task SendConfirmationAsync(Student student)
    {
        try
        {
            await Task.Delay(100); // Simulate network latency for email server
            Console.WriteLine($"   Email sent to {student.Name}");
        }
        catch (Exception ex)
        {
            // Log the failure but DO NOT re-throw. 
            // This is intentional fire-and-forget: we don't want an email failure 
            // to crash the main enrollment process.
            Console.WriteLine($"   Email failed for {student.Name}: {ex.Message}");
        }
    }

    public Action<Student>? OnEnrollmentSuccess { get; set; }

    public void FinalizeEnrollment(Student s)
    {
        Console.WriteLine("  Persisting to database...");

        // TODO 3: Check if the delegate listener is 'not null' and invoke it
        // The ?.Invoke syntax is the safest way to call a delegate
        OnEnrollmentSuccess?.Invoke(s);
    }
}