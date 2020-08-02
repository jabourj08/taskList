using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Transactions;

namespace taskList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tasks> taskList = new List<Tasks>
            {
                new Tasks("Justin", "Feed the dogs", DateTime.Parse("10/31/2020"), false, true),
                new Tasks("Josh", "Flush the toilet", DateTime.Parse("7/16/2020"), true, true),
                new Tasks("Jordan", "Open the Door", DateTime.Parse("8/23/2020"), true),
                new Tasks("Lauren", "Brush the cat", DateTime.Parse("5/22/2020"), false),
                new Tasks("Justin", "Go ouside", DateTime.Parse("9/22/2020"), false),
                new Tasks("Josh", "Drive around the sub", DateTime.Parse("8/15/2020"), false),
                new Tasks("Jordan", "Get gas", DateTime.Parse("7/31/2020"), true, true),
                new Tasks("Lauren", "Run to the store", DateTime.Parse("8/3/2020"), false),
                new Tasks("Justin", "Get groceries", DateTime.Parse("7/5/2020"), false, true),
                new Tasks("Josh", "Build a shed", DateTime.Parse("5/8/2021"), false, true),
                new Tasks("Jordan", "Buy some batteries", DateTime.Parse("10/21/2020"), false, true),
                new Tasks("Lauren", "Make coffee", DateTime.Parse("7/30/2020"), false, true),
                new Tasks("Justin", "Play board games", DateTime.Parse("8/2/2020"), false),
                new Tasks("Josh", "Play board games", DateTime.Parse("8/2/2020"), false),
                new Tasks("Jordan", "Play board games", DateTime.Parse("8/2/2020"), false),
                new Tasks("Lauren", "Play board games", DateTime.Parse("8/2/2020"), false),
                new Tasks("Justin", "Complete this assignment", DateTime.Parse("8/3/2020"), false, true),
                new Tasks("Josh", "Complete this assignment", DateTime.Parse("8/3/2020"), false, true),
                new Tasks("Jordan", "Complete this assignment", DateTime.Parse("8/3/2020"), false, true),
                new Tasks("Lauren", "Complete this assignment", DateTime.Parse("8/3/2020"), false, true),
            };

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("***** Hello and welcome to the Task Manager *****");
            Console.ResetColor();
            Console.WriteLine();

            MainMenuOptions(taskList);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("***** Thank you for using the Task Manager. Have a great day! *****");
            Console.Write(" Press any key to exit...");
            Console.ReadKey();
        }

        //Display Main Menu
        public static void MainMenuOptions(List<Tasks> taskList)
        {
            //Create menu
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Currently you have " + taskList.Count + " tasks...");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1) List Tasks");
            Console.WriteLine("2) Add Task");
            Console.WriteLine("3) Delete Task");
            Console.WriteLine("4) Mark Task Complete");
            Console.WriteLine("5) Quit");
            Console.WriteLine();
            Console.Write("Enter 1-5: ");
            Console.ResetColor();
            string input = Console.ReadLine();

            //Validate number format and range
            int userSelection = CheckNumber(input, 5);

            //Main Menu Determination
            if (userSelection == 1)
            {
                ListTasks(taskList);
            }
            else if (userSelection == 2)
            {
                AddTask(taskList);
            }
            else if (userSelection == 3)
            {
                DeleteTask(taskList);
            }
            else if (userSelection == 4)
            {
                MarkComplete(taskList);
            }
            else if (userSelection == 5)
            {
                ExitProgram(taskList, true);
            }
        }
        //List tasks based on user input
        public static void ListTasks(List<Tasks> taskList)
        {
            Console.Clear();
            int count = 1;
            const int offset = 2;

            //Add unique members to a hashset
            HashSet<string> uniqueMembers = new HashSet<string>();

            //Add names to the hashset
            foreach (Tasks task in taskList)
            {
                uniqueMembers.Add(task.Name);
            }

            List<string> uniqueMembersList = uniqueMembers.ToList();

            //print from hashset
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Which team member would you like to see tasks for?");
            Console.WriteLine();
            Console.WriteLine("1) Show ALL tasks");
            Console.WriteLine("2) Show all tasks before a specified date");
            foreach (string member in uniqueMembersList)
            {
                Console.WriteLine((count + offset) + ") " + member);
                count++;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.Write("Enter 1-" + (uniqueMembersList.Count + offset) + ": ");
            Console.ResetColor();
            string input = Console.ReadLine();

            //reset count
            count = 1;

            //Validate number format and range
            int userSelection = CheckNumber(input, (uniqueMembersList.Count + offset));
            Console.WriteLine();
            
            if (userSelection == 1)
            {
                Console.WriteLine("------------------------------------------------------");
                foreach (Tasks task in taskList)
                {
                        Console.WriteLine("Task Number: " + count);
                        task.PrintTasks();
                        count++;
                }
            }
            else if (userSelection == 2)
            {
                Console.WriteLine("Preparing to list tasks prior to given date...");
                Console.Write("Please enter a date (MM/DD/YYYY): ");

                //Validate date input
                DateTime givenDate = CheckDate(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------");
                foreach (Tasks task in taskList)
                {
                    if (task.DueDate < givenDate)
                    {
                        Console.WriteLine("Task Number: " + count);
                        task.PrintTasks();
                        count++;
                    }
                }
            }
            else
            {
                count = 1;
                Console.WriteLine("------------------------------------------------------");
                foreach (Tasks task in taskList)
                {
                    if (task.Name == uniqueMembersList[userSelection - 3])
                    {
                        Console.WriteLine("Task Number: " + count);
                        task.PrintTasks();
                        count++;
                    }
                }
            }
            ReturnToMain(taskList);
        }

        //Add a task to the task list
        public static void AddTask(List<Tasks> taskList)
        {
            Console.Clear();
            string name;
            string description;
            DateTime dueDate;
            bool highPriority = false;

            //Name
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Please enter a team member's name to assign a task");
            Console.ResetColor();
            name = Console.ReadLine();
            name.ToLower();
            char c = name[0];
            name = name.Remove(0, 1);
            c = Char.ToUpper(c);
            name = c + name;

            //Description
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine("Please enter a description for the task.");
            Console.ResetColor();
            description = Console.ReadLine();

            //Date
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine("Please enter a due date in the following format: MM/DD/YYYY");
            Console.ResetColor();
            dueDate = CheckDate(Console.ReadLine());

            //Priority
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine("Is this task High Priority? Enter y/n");
            Console.ResetColor();
            string input = Console.ReadLine();

            //Validate input
            input = CheckDecision(input);
            if (input == "y")
            {
                highPriority = true;
            }

            taskList.Add(new Tasks(name, description, dueDate, highPriority));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task Successfully Added.");
            Console.ResetColor();

            ReturnToMain(taskList);
        }

        //Delete a task from the task list
        public static void DeleteTask(List<Tasks> taskList)
        {
            Console.Clear();
            int count = 1;

            Console.WriteLine("------------------------------------------------------");
            foreach (Tasks task in taskList)
            {
                Console.WriteLine("Task Number: " + count);
                task.PrintTasks();

                count++;
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Which task would you like to REMOVE?");
            Console.Write("Enter a number from 1-" + taskList.Count + ": ");
            Console.ResetColor();
            string input = Console.ReadLine();

            //Validate number format and range
            int userSelection = CheckNumber(input, taskList.Count);

            //Confirm delete task
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Are you sure you want to DELETE Task Number: " + userSelection + "??");
            Console.Write("Please enter y/n: ");
            Console.ResetColor();

            //Validate entry
            input = CheckDecision(Console.ReadLine());

            if (input == "y")
            {
                taskList.RemoveAt(userSelection - 1);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully Removed: Task Number " + userSelection);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Mission Aborted: Task Number " + userSelection + " remains unharmed!");
                Console.ResetColor();
            }
            ReturnToMain(taskList);
        }

        //Mark a task complete
        public static void MarkComplete(List<Tasks> taskList)
        {
            Console.Clear();
            int count = 1;

            Console.WriteLine("------------------------------------------------------");
            foreach (Tasks task in taskList)
            {
                Console.WriteLine("Task Number: " + count);
                task.PrintTasks();
                count++;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("For which task would you like to MODIFY completion status?");
            Console.Write("Please enter 1-" + taskList.Count + ": ");
            Console.ResetColor();
            string input = Console.ReadLine();

            //Validate number format and range
            int userSelection = CheckNumber(input, taskList.Count);

            //Confirm mark complete
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Are you sure you want to MODIFY the completion status of Task Number: " + userSelection);
            Console.Write("Please enter y/n: ");
            Console.ResetColor();

            //validate entry
            input = CheckDecision(Console.ReadLine());

            if (input == "y")
            {
                if (taskList[userSelection - 1].Complete == true)
                {
                    taskList[userSelection - 1].Complete = false;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successfully Marked: Task Number " + userSelection + " INCOMPLETE.");
                    Console.ResetColor();
                }
                else
                {
                    taskList[userSelection - 1].Complete = true;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successfully Marked: Task Number " + userSelection + " COMPLETE!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Mission Aborted: The completion status of Task Number " + userSelection + " remains the same.");
                Console.ResetColor();
            }
            ReturnToMain(taskList);
        }

        //Determine if user wants to Exit Program
        public static bool ExitProgram(List<Tasks> taskList, bool quit)
        {
            bool cont = true;

            if (quit)
            {
                cont = false;
            }

            return cont;
        }

        //Check for in range number
        public static int CheckNumber(string input, int num)
        {
            int validNumber = 0;
            bool invalid = true;
            while (invalid)
            {
                try
                {   
                    validNumber = int.Parse(input);
                    if (validNumber > 0 && validNumber <= num)
                    {
                        invalid = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Why u no listen??? I said gimme a number 1-" + num);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        BeepBoops();
                        Console.Write("Please try again: ");
                        Console.ResetColor();
                        input = Console.ReadLine();
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Why u no listen??? I said gimme a number 1-" + num);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    BeepBoops();
                    Console.Write("Please try again: ");
                    Console.ResetColor();
                    input = Console.ReadLine();
                }
            }
            return validNumber;
        }

        //Check for y/n
        public static string CheckDecision(string input)
        {
            bool invalid = true;
            while (invalid)
            {
                if (input.ToLower() == "y")
                {
                    input = "y";
                    invalid = false;
                }
                else if (input.ToLower() == "n")
                {
                    input = "n";
                    invalid = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Why u no listen??? I said enter y/n");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    BeepBoops();
                    Console.Write("Please try again: ");
                    Console.ResetColor();
                    input = Console.ReadLine();
                }
            }
            return input;
        }

        //Check date format
        public static DateTime CheckDate(string input)
        {
            DateTime validDate = DateTime.Now;
            bool invalid = true;
            while (invalid)
            {
                try
                {
                    if (input.Contains("-"))
                    {
                        input = input.Replace("-", "/");
                    }
                    string[] dateParts = input.Split('/');
                    validDate = new DateTime(Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[0]), Convert.ToInt32(dateParts[1]));
                    invalid = false;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is not a valid Date. You need to enter a date in the correct format. (MM/DD/YYYY)");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    BeepBoops();
                    Console.Write("Please try again: ");
                    Console.ResetColor();
                    input = Console.ReadLine();
                }
            }
            return validDate;
        }

        //Return to Main Menu
        public static void ReturnToMain(List<Tasks> taskList)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.Write("Press any key to return to the Main Menu...");
            Console.ResetColor();
            Console.ReadKey();
            Console.WriteLine();
            MainMenuOptions(taskList);
        }

        //Addz Cool Zoundz!!! Because why not??
        public static void BeepBoops()
        {
            Console.Beep(1000, 100); Console.Beep(1000, 100); Console.Beep(1000, 100);
            Console.Beep(2000, 100); Console.Beep(2000, 100); Console.Beep(2000, 100);
        }
    }
}
