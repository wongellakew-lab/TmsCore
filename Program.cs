// Execute Exercise 1
//RunExercise1();

// Execute Exercise 2
//RunExercise2();


// Execute Exercise 3
//RunExercise3Part1();
//RunExercise3Part2();
//RunExercise3Part3();
//RunExercise3B();

// Execute Session 2 Exercise 4
//RunExercise4();

// Execute Session 2 Exercise 5
RunExercise5();

void RunExercise1()
{
    Console.WriteLine("--- Exercise 1: Null Safety ---");

    string? region = null; 

    string? upperRegion = region?.ToUpper();
    Console.WriteLine($"Region (conditional): {upperRegion}");

    string displayRegion = region ?? "Unassigned";
    Console.WriteLine($"Region (coalesced): {displayRegion}");

    region ??= "Addis Ababa";
    Console.WriteLine($"Region (assigned): {region}");

    string studentName = "Abeba";
    string studentId = "STU-001";
    int enrollmentCount = 3;
    decimal grantAmount = 1999.99m; 
    DateTime enrolledAt = DateTime.UtcNow;
    string? campusRegion = null;

    Console.WriteLine($"Student: {studentName} ({studentId})");
    Console.WriteLine($"Courses: {enrollmentCount}");
    Console.WriteLine($"Grant: {grantAmount:F2}");
    Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}");
    Console.WriteLine($"Campus: {campusRegion ?? "Not assigned"}");
}

void RunExercise2()
{
    Console.WriteLine("\n--- Exercise 2: Floating Point Bug ---");

    double grantPerStudent = 1999.99;
    double totalAllocation = grantPerStudent * 100_000;

    Console.WriteLine($"Total allocated (double): {totalAllocation}");

    decimal grantPerStudentFixed = 1999.99m; 
    decimal totalAllocationFixed = grantPerStudentFixed * 100_000m;

    Console.WriteLine($"Total allocated (decimal): {totalAllocationFixed}");
    Console.WriteLine($"Total allocated (formatted): {totalAllocationFixed:F2}");
    
}

void RunExercise3Part1()
{
    Console.WriteLine("\n--- Exercise 3 Part 1: Immutability with Records ---");

    var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow);
    Console.WriteLine($"Original: {enrollment}");

    var corrected = enrollment with { CourseCode = "CS-402" };
    Console.WriteLine($"Corrected: {corrected}");

    var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt);
    Console.WriteLine($"Same data? {enrollment == duplicate}"); // True

}

void RunExercise3Part2()
{

    Console.WriteLine("\n--- Exercise 3 Part 2: Course Validation (field keyword) ---");

    var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
    Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");
    
    try
    {
        course.Capacity = -5;
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine($"Caught: {ex.Message}");
    }

    try
    {
        course.Title = "";
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Caught: {ex.Message}");
    }
}

void RunExercise3Part3()
{
     Console.WriteLine("\n--- Exercise 3 Part 3: Student Model Validation ---");

    var s = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m };
    Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}");
    
    try { s.Age = 12; }
    catch (Exception ex) { Console.WriteLine($"Age Error: {ex.Message}"); }

    try { s.GPA = 5.0m; }
    catch (Exception ex) { Console.WriteLine($"GPA Error: {ex.Message}"); }

    try { s.Name = ""; }
    catch (Exception ex) { Console.WriteLine($"Name Error: {ex.Message}"); }

}

void RunExercise3B()
{
     Console.WriteLine("\n--- Exercise 3B: Polymorphic Grade Report ---");

    void PrintGradeReport(IEnumerable<IGradable> assessments)
        {
            Console.WriteLine("--- Grade Report ---");
            foreach (var item in assessments)
            {
                Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");
            }
        }
    IGradable[] cohortAssessments = 
    [
        new Quiz { Title = "C# Basics", CorrectAnswers = 18, TotalQuestions = 20 }, 
        new LabAssignment { Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m }
    ];
    
    PrintGradeReport(cohortAssessments);
}

void RunExercise4()
{
    Console.WriteLine("\n--- Exercise 4: Guards and Pattern Matching ---");
    var service = new EnrollmentService();

    var validStudent = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m };
    var validCourse = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
    
    var result = service.ProcessRegistration(validStudent, validCourse);
    Console.WriteLine($"Enrolled: {result.StudentId} in {result.CourseCode}");

    try
    {
        service.ProcessRegistration(null, validCourse);
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine($"Guard caught: {ex.ParamName}");
    }

    var fullCourse = new Course { Code = "CS-402", Title = "Full Course", Capacity = 1 };
    fullCourse.EnrolledCount = 1;
    try
    {
        service.ProcessRegistration(validStudent, fullCourse);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Business rule: {ex.Message}");
    }
}

void RunExercise5()
{
    Console.WriteLine("\n--- Exercise 5: Analytics Dashboard (LINQ) ---");

    List<Student> students = [
        new Student { Id = "S1", Name = "Abeba", Age = 22, GPA = 3.8m },
        new Student { Id = "S2", Name = "Kidane", Age = 21, GPA = 2.4m },
        new Student { Id = "S3", Name = "Dawit", Age = 20, GPA = 3.1m },
        new Student { Id = "S4", Name = "Sara", Age = 23, GPA = 3.9m },
        new Student { Id = "S5", Name = "Frehiwot", Age = 19, GPA = 2.0m },
        new Student { Id = "S6", Name = "Yonas", Age = 24, GPA = 3.5m },
        new Student { Id = "S7", Name = "Meron", Age = 22, GPA = 1.8m },
        new Student { Id = "S8", Name = "Tesfaye", Age = 21, GPA = 2.9m }
    ];

    var leaderboard = students
        .Where(s => s.GPA >= 3.5m)            
        .OrderByDescending(s => s.GPA)        
        .Select(s => s.Name)                  
        .ToList();                            

    Console.WriteLine($"Found {leaderboard.Count} Honors Students:");
    foreach (var name in leaderboard)
    {
        Console.WriteLine($"- {name}");
    }

    decimal averageGpa = students.Average(s => s.GPA);
    Console.WriteLine($"\nClass Average GPA: {averageGpa:F2}");

    var standingGroups = students.GroupBy(s => s.GPA switch
    {
        >= 3.5m => "Honors",
        >= 2.5m => "Good Standing",
        >= 2.0m => "Probation",
        _ => "Academic Warning"
    });

    Console.WriteLine("\n--- Academic Standing Report ---");
    foreach (var group in standingGroups)
    {
        Console.WriteLine($"\n{group.Key} ({group.Count()}):");
        foreach (var s in group)
        {
            Console.WriteLine($" {s.Name} GPA: {s.GPA}");
        }
    }

     string[] backendCourses = ["C#", "ASP.NET Core"];
    string[] frontendCourses = ["TypeScript", "Angular"];
    
    string[] allCourses = [.. backendCourses, .. frontendCourses, "Capstone"];
    
    Console.WriteLine($"\nFull curriculum: {string.Join(", ", allCourses)}");
}
