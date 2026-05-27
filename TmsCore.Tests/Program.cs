Console.WriteLine("--- TmsCore Test Suite ---");

// Test if we can create a Student from the other project
var testStudent = new Student { Id = "TEST-01", Name = "Test User", Age = 25, GPA = 3.5m };

Console.WriteLine($"Reference Check: Student {testStudent.Name} created successfully.");