// Execute Exercise 1
//RunExercise1();

// Execute Exercise 2
//RunExercise2();


// Execute Exercise 3
//RunExercise3Part1();
//RunExercise3Part2();
//RunExercise3Part3();
//RunExercise3B();

// Execute Session Exercise 4
RunExercise4();

void RunExercise1()
{
    Console.WriteLine("--- Exercise 1: Null Safety ---");

    // --- Step 1: Reproduce Legacy Bug ---
    
    string? region = null; 

    // 2. Null-conditional operator '?.' — skip the call if null
    string? upperRegion = region?.ToUpper();
    Console.WriteLine($"Region (conditional): {upperRegion}");

    // 3. Null-coalescing operator '??' — provide a fallback value
    string displayRegion = region ?? "Unassigned";
    Console.WriteLine($"Region (coalesced): {displayRegion}");

    // 4. Null-coalescing assignment '??=' — assign only if currently null
    region ??= "Addis Ababa";
    Console.WriteLine($"Region (assigned): {region}");


    // Step 3: Core TMS Domain Variables
    string studentName = "Abeba";
    string studentId = "STU-001";
    int enrollmentCount = 3;
    decimal grantAmount = 1999.99m; // 'm' suffix marks a decimal literal
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

    // Step 1: Legacy implementation — using double for money causes precision drift
    double grantPerStudent = 1999.99;
    double totalAllocation = grantPerStudent * 100_000;

    // The output will show a tiny drift (e.g., .0000000003)
    Console.WriteLine($"Total allocated (double): {totalAllocation}");

    decimal grantPerStudentFixed = 1999.99m; 
    decimal totalAllocationFixed = grantPerStudentFixed * 100_000m;

    Console.WriteLine($"Total allocated (decimal): {totalAllocationFixed}");
    Console.WriteLine($"Total allocated (formatted): {totalAllocationFixed:F2}");
    
}

void RunExercise3Part1()
{
    Console.WriteLine("\n--- Exercise 3 Part 1: Immutability with Records ---");

    // Testing the record
    var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow);
    Console.WriteLine($"Original: {enrollment}");

    var corrected = enrollment with { CourseCode = "CS-402" };
    Console.WriteLine($"Corrected: {corrected}");

    // Value equality check
    var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt);
    Console.WriteLine($"Same data? {enrollment == duplicate}"); // True

}

void RunExercise3Part2()
{

    Console.WriteLine("\n--- Exercise 3 Part 2: Course Validation (field keyword) ---");

    var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
    Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");
    
    // Invalid capacity — should throw
    try
    {
        course.Capacity = -5;
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine($"Caught: {ex.Message}");
    }

    // Invalid title — should throw
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
    
    // Test Invalid Age
    try { s.Age = 12; }
    catch (Exception ex) { Console.WriteLine($"Age Error: {ex.Message}"); }

    // Test Invalid GPA
    try { s.GPA = 5.0m; }
    catch (Exception ex) { Console.WriteLine($"GPA Error: {ex.Message}"); }

     // Test Invalid Name
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
    // Test it — one array holds two completely different types
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

    // Test 1: Valid registration
    var validStudent = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m };
    var validCourse = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
    
    var result = service.ProcessRegistration(validStudent, validCourse);
    Console.WriteLine($"Enrolled: {result.StudentId} in {result.CourseCode}");

    // Test 2: Null student should throw ArgumentNullException
    try
    {
        service.ProcessRegistration(null, validCourse);
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine($"Guard caught: {ex.ParamName}");
    }

    // Test 3: Full course should throw InvalidOperationException
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
