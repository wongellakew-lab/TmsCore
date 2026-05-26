// Execute Exercise 1
RunExercise1();

void RunExercise1()
{
    Console.WriteLine("--- Exercise 1: Null Safety ---");

    // --- Step 1: Reproduce Legacy Bug ---
    
    string region = null; 
    // Console.WriteLine(region.ToUpper()); // Uncommenting this would cause a NullReferenceException

   
}