// Execute Exercise 1
RunExercise1();

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