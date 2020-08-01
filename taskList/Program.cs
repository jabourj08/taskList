using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            Console.WriteLine("***** Hello and welcome to the Task Manager *****");
            Console.WriteLine();

            bool cont = true;
            while (cont)
            {
                MainMenuOptions(taskList);

                cont = ExitProgram(false);
                Console.Clear();
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("***** Thank you for using the Task Manager. Have a great day! *****");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        //Display Main Menu
        public static void MainMenuOptions(List<Tasks> taskList)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Currently you have " + taskList.Count + " tasks...");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) List Tasks");
            Console.WriteLine("2) Add Task");
            Console.WriteLine("3) Delete Task");
            Console.WriteLine("4) Mark Task Complete");
            Console.WriteLine("5) Quit");
            Console.WriteLine();
            Console.Write("Enter 1-5: ");
            Console.ResetColor();

            bool invalid = true;
            while (invalid)
            {
                try
                {
                    int userSelection = int.Parse(Console.ReadLine());

                    if (userSelection == 1)
                    {
                        ListTasks(taskList);
                        invalid = false;
                    }
                    else if (userSelection == 2)
                    {
                        AddTask(taskList);
                        invalid = false;
                    }
                    else if (userSelection == 3)
                    {
                        DeleteTask(taskList);
                        invalid = false;
                    }
                    else if (userSelection == 4)
                    {
                        MarkComplete(taskList);
                        invalid = false;
                    }
                    else if (userSelection == 5)
                    {
                        ExitProgram(true);
                        invalid = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Why u no listen??? I said gimme a number 1-5");
                        Console.ResetColor();
                        BeepBoops();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Why u no listen??? I said gimme a number 1-5");
                    Console.ResetColor();
                    BeepBoops();
                }
            }
        }
        //List tasks for a specific criteria
        public static void ListTasks(List<Tasks> taskList)
        {
            Console.Clear();
            int count = 1;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Which team member would you like to see tasks for?");
            Console.ResetColor();

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
            foreach (string member in uniqueMembersList)
            {
                Console.WriteLine(count + ") " + member);
                count++;
            }
            Console.Write("Enter 1-" + uniqueMembersList.Count + ": ");
            Console.ResetColor();

            //start validation loop
            bool invalid = true;
            while (invalid)
            {
                try
                {
                    int input = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    if (input > 0 && input <= uniqueMembersList.Count)
                    {
                        count = 1;
                        foreach (Tasks task in taskList)
                        {
                            if (task.Name == uniqueMembersList[input-1])
                            {
                                Console.WriteLine("Task Number: " + count);
                                task.PrintTasks();
                                count++;
                            }
                        }
                        invalid = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Why u no listen??? I said gimme a number 1-" + uniqueMembersList.Count);
                        Console.ResetColor();
                        BeepBoops();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Why u no listen??? I said gimme a number 1-" + uniqueMembersList.Count);
                    Console.ResetColor();
                    BeepBoops();
                }
            }
        }
        //Add a task to the task list
        public static void AddTask(List<Tasks> taskList)
        {
            Console.Clear();
            string name;
            string description;
            DateTime dueDate;
            bool highPriority = false;
            //string addAnother = "";

            //bool cont = true;
            bool invalid = true;
            while (invalid)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Please enter a team member's name to assign a task");
                    Console.ResetColor();
                    name = Console.ReadLine();
                    name.ToLower();
                    char c = name[0];
                    name = name.Remove(0, 1);
                    c = Char.ToUpper(c);
                    name = c + name;

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Please enter a description for the task.");
                    Console.ResetColor();
                    description = Console.ReadLine();

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Please enter a due date in the following format: MM/DD/YYYY");
                    Console.ResetColor();
                    dueDate = DateTime.Now;

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Is this task High Priority? Enter y/n");
                    Console.ResetColor();
                    string priorityInput = Console.ReadLine();
                
                    if (priorityInput[0] == 'y')
                    {
                        highPriority = true;
                    }
                    taskList.Add(new Tasks(name, description, dueDate, highPriority));
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Task Successfully Added.\n");
                    Console.ResetColor();

                    invalid = false;

                    //Console.WriteLine("Would you like to add another task? y/n");
                    //addAnother = Console.ReadLine();
                    //if (addAnother[0] != 'y' || addAnother == null)
                    //{
                    //    cont = false;
                    //}
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something went wrong.");
                    Console.ResetColor();
                    BeepBoops();
                }
            }
        }
        //Delete a task from the task list
        public static void DeleteTask(List<Tasks> taskList)
        {
            int count = 1;

            foreach (Tasks task in taskList)
            {
                Console.WriteLine("Task Number: " + count);
                task.PrintTasks();

                count++;
            }
            Console.WriteLine("Which task would you like to REMOVE?");

            //start validation loop
            bool invalid = true;
            while (invalid)
            {
                try
                {
                    int input = int.Parse(Console.ReadLine());

                    if (input > 0 && input <= taskList.Count)
                    {
                        taskList.RemoveAt(input - 1);

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Successfully Removed: Task Number " + input);
                        Console.ResetColor();
                        invalid = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Why u no listen??? I said gimme a number 1-" + taskList.Count);
                        Console.ResetColor();
                        BeepBoops();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Why u no listen??? I said gimme a number 1-" + taskList.Count);
                    Console.ResetColor();
                    BeepBoops();
                }
            }
        }
        public static void MarkComplete(List<Tasks> taskList)
        {
            int count = 1;

            foreach (Tasks task in taskList)
            {
                Console.WriteLine("Task Number: " + count);
                task.PrintTasks();
                count++;
            }
            Console.WriteLine("Which task would you like to Mark Complete?");

            //start validation loop
            bool invalid = true;
            while (invalid)
            {
                try
                {
                    int input = int.Parse(Console.ReadLine());

                    if (input > 0 && input <= taskList.Count)
                    {
                        taskList[input - 1].Complete = true;

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Successfully Marked Complete: Task Number " + input);
                        Console.ResetColor();
                        invalid = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Why u no listen??? I said gimme a number 1-" + taskList.Count);
                        Console.ResetColor();
                        BeepBoops();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Why u no listen??? I said gimme a number 1-" + taskList.Count);
                    Console.ResetColor();
                    BeepBoops();
                }
            }
        }
        //Determine if user wants to Exit Program
        public static bool ExitProgram(bool quit)
        {
            bool invalid = true;
            bool cont = true;

            if (quit)
            {
                invalid = false;
                cont = false;
            }

            while (invalid)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Would you like to return to the Main Menu? y/n");
                Console.ResetColor();
                try
                {
                    string input = Console.ReadLine();
                    if (input[0] == 'n')
                    {
                        cont = false;
                        invalid = false;
                    }
                    else if (input[0] == 'y')
                    {
                        cont = true;
                        invalid = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Why u no listen??? I said gimme a \"y\" or \"n\"");
                        Console.ResetColor();
                        BeepBoops();
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Why u no listen??? I said gimme a \"y\" or \"n\"");
                    Console.ResetColor();
                    BeepBoops();
                }
            }
            Console.Clear();
            return cont;
        }
        //Addz Cool Zoundz!!! Because why not??
        public static void BeepBoops()
        {
            Console.Beep(1000, 100); Console.Beep(1000, 100); Console.Beep(1000, 100);
            Console.Beep(2000, 100); Console.Beep(2000, 100); Console.Beep(2000, 100);
        }
    }
}
