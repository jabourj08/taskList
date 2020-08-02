using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace taskList
{
    class Tasks
    {
        #region Properties
        private int _taskNum;
        private string _name;
        private string _description;
        private DateTime _dueDate;
        private bool _highPriority;
        private bool _complete;
        #endregion

        #region Main Methods
        public int TaskNum
        {
            get
            {
                return _taskNum;
            }
            set
            {
                _taskNum = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                _dueDate = value;
            }
        }
        public bool HighPriority
        {
            get
            {
                return _highPriority;
            }
            set
            {
                _highPriority = value;
            }
        }
        public bool Complete
        {
            get
            {
                return _complete;
            }
            set
            {
                _complete = value;
            }
        }
        #endregion

        #region Constructors
        public Tasks()
        {

        }
        public Tasks(string Name, string Description, DateTime DueDate, bool HighPriority)
        {
            _name = Name;
            _description = Description;
            _dueDate = DueDate;
            _highPriority = HighPriority;
        }
        public Tasks(string Name, string Description, DateTime DueDate, bool HighPriority, bool Complete)
        {
            _name = Name;
            _description = Description;
            _dueDate = DueDate;
            _highPriority = HighPriority;
            _complete = Complete;
        }
        #endregion

        #region Additional Methods
        public void PrintTasks()
        {
            Console.WriteLine();
            Console.WriteLine($"\tTeam Member: {_name}");
            if (_complete)
            {
                if (_highPriority)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\t*****VERY IMPORTANT*****");
                    Console.ResetColor();
                }
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"DONE!\tDue Date: {_dueDate.ToString("MM/dd/yyyy")}");
                Console.ResetColor();
                Console.WriteLine($"\tDescription: {_description}");
            }
            else
            {
                if (_highPriority)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\t*****VERY IMPORTANT*****");
                    Console.ResetColor();
                }
                if (_dueDate < DateTime.Now)//DETERMINE PAST DUE
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"LATE!\tDue Date: {_dueDate.ToString("MM/dd/yyyy")}");
                    Console.ResetColor();
                    Console.WriteLine($"\tDescription: {_description}");
                }
                else
                {
                    Console.WriteLine($"\tDue Date: {_dueDate.ToString("MM/dd/yyyy")}");
                    Console.WriteLine($"\tDescription: {_description}");
                }
            }
            Console.WriteLine("------------------------------------------------------");
        }
        #endregion
    }
}
