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

   
}