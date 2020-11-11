using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todolist_group
{
    class Program
    {
        class Activity
        {
            public string date;
            public string status;
            public string title;
            public Activity(string D, string S, string T)
            {
                date = D; status = S; title = T;
            }
        }
        static void Main(string[] args)
        {
            //Test Path C:\Users\oskar\todo2.lis
            //Welcome
            Console.WriteLine("The Todolist!");
            Console.WriteLine("Menu:");           
            //Variables
            List<Activity> mainTodoList = new List<Activity>();
            string command;
            string[] commandWord;
            string filePath = "";
            string menu = "'Help'\n'Quit'\n'Load'\n'Save'\n'Show'\n" +
                "'Move'\n'Delete'\n'Add'\n'Status'";
            string help = "'Load' Loads specifed file\n'Save' Saves current file\n'Show' Current list\n" +
                "'Move' Moves current activity\n'Delete' Deletes specifed activity\n" +
                "'Add' Adds information to current list\n'Status' Changes status of specified activities";
            Console.WriteLine(menu);
            do
            {
                Console.WriteLine("> ");
                command = Console.ReadLine();
                commandWord = command.Split(' ');
                if (commandWord[0] == "Help")
                {
                    Console.WriteLine(help);
                }
                //Quit
                else if (commandWord[0] == "Quit")
                {
                    Console.WriteLine("Bye!");
                }
                //Load
                else if (commandWord[0] == "Load")
                {
                    try
                    {
                        filePath = commandWord[1];
                        mainTodoList = LoadFile(filePath);
                        Console.WriteLine($">{commandWord[1]} loaded<");
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect file path");
                    }
                }
                //Save
                else if (commandWord[0] == "Save")
                {
                    try
                    {
                        filePath = commandWord[1];
                    }
                    catch
                    {
                    }
                    try
                    {
                        SaveFile(filePath, mainTodoList);
                    }
                    catch
                    {
                        Console.WriteLine("Unable to save");
                    }
                    Console.WriteLine(">Saved<");
                }
                //Show
                else if (commandWord[0] == "Show")
                {
                    Console.WriteLine($"{filePath}");
                    ShowTodoList(mainTodoList, commandWord);
                }
                //Move
                else if (commandWord[0] == "Move")
                {
                    try
                    {
                        MoveMethod(mainTodoList, commandWord);
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect input");
                    }
                }
                //Delete
                else if (commandWord[0] == "Delete")
                {
                    try
                    {
                        mainTodoList.RemoveAt(int.Parse(commandWord[1]) - 1);
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect input");
                    }
                }
                //Add information
                else if (commandWord[0] == "Add")
                {
                    string title = "";
                    try
                    {
                        title = AddMethod(commandWord, title);
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect input");
                    }
                    mainTodoList.Add(new Activity(commandWord[1], "v", title));
                }
                //Set status
                else if (commandWord[0] == "Status")
                {
                    try
                    {
                        mainTodoList[int.Parse(commandWord[1]) - 1].status = commandWord[2];
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect input");
                    }
                }
                else if (commandWord[0] == "Completed")
                {
                    try
                    {
                    }
                    catch
                    { 
                    }
                }
                else
                {
                    Console.WriteLine("Unknown command");
                }
            }
            while (commandWord[0] != "Quit");
        }
        //Move method
        private static void MoveMethod(List<Activity> mainTodoList, string[] commandWord)
        {
            int firstPos = int.Parse(commandWord[1]) - 1, secPos = int.Parse(commandWord[2]) - 1;
            Activity temp = mainTodoList[firstPos];
            mainTodoList[firstPos] = mainTodoList[secPos];
            mainTodoList[secPos] = temp;
        }
        //Add method
        private static string AddMethod(string[] commandWord, string title)
        {
            for (int i = 2; i < commandWord.Length; i++)
            {
                {
                    title += commandWord[i] + " ";
                }
            }

            return title;
        }
        //Load method
        static List<Activity> LoadFile(string path)
        {            
            List<Activity> todoList = new List<Activity>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] lineWord = line.Split('#');
                    Activity A = new Activity(lineWord[0], lineWord[1], lineWord[2]);
                    todoList.Add(A);
                }
            }
            return todoList;
        }
        //Save and save new method
        static void SaveFile(string filePath, List<Activity> A)
        {
            if (!File.Exists(filePath))
            {

                using (StreamWriter sw = File.CreateText(filePath))
                {
                    for (int i = 0; i < A.Count; i++)
                    {
                        sw.WriteLine("{0}#{1}#{2}", A[i].date, A[i].status, A[i].title);
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    for (int i = 0; i < A.Count; i++)
                    {
                        sw.WriteLine("{0}#{1}#{2}", A[i].date, A[i].status, A[i].title);
                    }
                }
            }
        }
        //Show activities and show completed activities method
        static void ShowTodoList(List<Activity> A, string[] commandWord)
        {
            string tempString = "";
            try
            {
                tempString = commandWord[1];
            }
            catch (Exception)
            {

            }

            if (tempString == "completed")
            {
                Console.WriteLine("N |  Date   |  Status  |  Title  ");
                Console.WriteLine("---------------------------------------------");
                for (int i = 0; i < A.Count; i++)
                {
                    if (A[i].status == "*")
                    {
                        Console.WriteLine("{0} |{1,-8} | {2,-8} | {3,-8}", i + 1, A[i].date, A[i].status, A[i].title);
                    }
                }
                Console.WriteLine("---------------------------------------------");
            }
            else
            {
                Console.WriteLine("N |  Date   |  Status  |  Title  ");
                Console.WriteLine("---------------------------------------------");
                for (int i = 0; i < A.Count; i++)
                {
                    Console.WriteLine("{0} |{1,-8} | {2,-8} | {3,-8}", i + 1, A[i].date, A[i].status, A[i].title);
                }
                Console.WriteLine("---------------------------------------------");
            }
        }

    }
}
