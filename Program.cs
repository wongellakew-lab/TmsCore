using System.Diagnostics;
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
//RunExercise5();

// Execute Session 3 Exercise 6
//await RunExercise6Step1();

// Execute Session3 Exercise 6 Step 3
//await RunExercise6Step3();

// Execute Session 3 Exercise 6 Part B
//await RunExercise6PartB();

// Execute Session 3 Exercise 7 Step 3
await RunExercise7();

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

void RunExercise5()
{
    Console.WriteLine("\n--- Exercise 5: Analytics Dashboard (LINQ) ---");

    // Step 1: Create the Student Data using C# 12+ Collection Expressions
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
        .Where(s => s.GPA >= 3.5m)            // TODO 1: Extract students where GPA is >= 3.5m
        .OrderByDescending(s => s.GPA)        // TODO 2: Sort by GPA descending
        .Select(s => s.Name)                  // TODO 3: Project to keep only the 'Name' string
        .ToList();                            // TODO 4: Materialize into a concrete List

    Console.WriteLine($"Found {leaderboard.Count} Honors Students:");
    foreach (var name in leaderboard)
    {
        Console.WriteLine($"- {name}");
    }

    // Step 3: Class Average
    // TODO 5: Calculate average GPA across all students
    decimal averageGpa = students.Average(s => s.GPA);
    Console.WriteLine($"\nClass Average GPA: {averageGpa:F2}");

    // Step 4: Group by Academic Standing
    // TODO 6: Use .GroupBy with a switch expression to classify students
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
    
    // TODO 7: Use the spread operator (..) to merge arrays and append a value
    string[] allCourses = [.. backendCourses, .. frontendCourses, "Capstone"];
    
    Console.WriteLine($"\nFull curriculum: {string.Join(", ", allCourses)}");
}

async Task RunExercise6Step1()
{
    Console.WriteLine("--- Exercise 6: Async Performance Comparison ---");
    
    
    var sw = Stopwatch.StartNew();
    for (int i = 0; i < 5; i++)
    {
        Thread.Sleep(300); // Thread is HELD for 300ms - cannot serve anyone else
    }
    Console.WriteLine($"Blocking sequential: {sw.ElapsedMilliseconds}ms");

    // 2. ASYNC BUT STILL SEQUENTIAL: Thread released, but calls are one-at-a-time
    
    sw.Restart();
    for (int i = 0; i < 5; i++)
    {
        await Task.Delay(300); // Thread released while waiting, but still sequential
    }
    Console.WriteLine($"Async sequential:    {sw.ElapsedMilliseconds}ms");

    // 3. THE RIGHT WAY: Async parallel - all 5 start simultaneously
    // Maximum performance - total time equals the slowest single call
    sw.Restart();
    var tasks = Enumerable.Range(0, 5).Select(_ => Task.Delay(300));
    await Task.WhenAll(tasks);
    Console.WriteLine($"Async parallel:      {sw.ElapsedMilliseconds}ms");
}


async Task RunExercise6Step3()
{
    Console.WriteLine("\n--- Exercise 6 Step 3: Parallel Loading ---");
    var sw = Stopwatch.StartNew();
    var service = new EnrollmentService();

    // Start all fetches simultaneously students AND courses
    string[] studentIds = ["S1", "S2", "S3", "S4", "S5"];
    string[] courseCodes = ["CRS-101", "CRS-201", "CRS-301"];

    // Use LINQ to start all tasks without awaiting them yet
    var studentTasks = studentIds.Select(id => service.FetchStudentAsync(id)).ToList();
    var courseTasks = courseCodes.Select(code => service.FetchCourseAsync(code)).ToList();

    // Both arrays load concurrently - total time is the slowest single task (~300ms)
    Student[] students = await Task.WhenAll(studentTasks);
    Course[] courses = await Task.WhenAll(courseTasks);

    sw.Stop();
    Console.WriteLine($"\nLoaded {students.Length} students and {courses.Length} courses in {sw.ElapsedMilliseconds}ms");

    foreach (var s in students)
    {
        Console.WriteLine($"  {s.Name} GPA: {s.GPA}");
    }

    foreach (var c in courses)
    {
        Console.WriteLine($"  {c.Title} Capacity: {c.Capacity}");
    }
}

async Task RunExercise6PartB()
{
    Console.WriteLine("\n--- Exercise 6 Part B: TMS Enrollment Engine: Sequential Enrollment ---");
    var service = new EnrollmentService();
    var sw = Stopwatch.StartNew();

    string[] studentIds = ["S1", "S2", "S3", "S4", "S5"];
    string[] courseCodes = ["CRS-101", "CRS-201", "CRS-301"];

    // Use LINQ to start all tasks without awaiting them yet
    var studentTasks = studentIds.Select(id => service.FetchStudentAsync(id)).ToList();
    var courseTasks = courseCodes.Select(code => service.FetchCourseAsync(code)).ToList();

    // 1. Load data in parallel (As per Image 2)
    var students = await Task.WhenAll(studentTasks);
    Course[] courses = await Task.WhenAll(courseTasks);

    Console.WriteLine($"Loaded in {sw.ElapsedMilliseconds}ms");

    // 2. Process enrollments
    var enrollments = new List<EnrollmentRecord>();
    var failures = new List<string>();

    foreach (var student in students)
    {
        try
        {
            // We await each enrollment one-by-one to maintain state (capacity) correctly
            var record = await service.ProcessEnrollmentAsync(student, courses[0]);
            
            courses[0].EnrolledCount++; // Increment current state
            enrollments.Add(record);
            
            Console.WriteLine($"  Enrolled: {student.Name} in {courses[0].Title}");
            
        }
        catch (InvalidOperationException ex)
        {
            failures.Add($"{student.Name}: {ex.Message}");
            Console.WriteLine($"  Rejected: {student.Name} for {ex.Message}");
        }
    }

     await Task.Delay(1000); 
}

async Task RunExercise7()
{
    Console.WriteLine("\n--- Exercise 7: Custom Exceptions ---");
    var enrollService = new EnrollmentService();

    try
    {
        // Create a course with 0 capacity to force an immediate error
        var overflowCourse = new Course { Code = "CRS-999", Title = "Overflow Test", Capacity = 0 };
        
        await enrollService.ProcessEnrollmentAsync(
            new Student { Id = "S99", Name = "Test", Age = 20, GPA = 3.0m },
            overflowCourse
        );
    }
    catch (CapacityReachedException ex)
    {
        // Because we caught the specific type, we can access the CourseCode property
        Console.WriteLine("\nDomain exception caught:");
        Console.WriteLine($"  Course: {ex.CourseCode}");
        Console.WriteLine($"  Message: {ex.Message}");
    }
}