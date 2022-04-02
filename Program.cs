// Kyle Zapalac, 01-Apr-2022, GAME-1343 (SP 2022)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // for file reading/writing
using System.Text.RegularExpressions;

namespace Wk9_Regex_KZ
{
    // 1) Write a program that allows the user to enter the full name and path of a text file,
    //    reads in the text file if it exists, and displays how many words are in the text file. - DONE
    // 2) Test that the file exists before it tries to open it, and if it does not exist, the 
    //    program should gracefully handle it instead of crashing (use try/catch). - DONE
    // 3) Finally, use Regex to verify that the file entered by the user is in a format that
    //    looks like a file path for a text file (eg.  c:/school/myTest.txt ). - DONE
    class Program
    {
        static void Main(string[] args)
        {
            // declare variable for string to be analyzed
            List<string> wordList = new List<string>();
            string[] strArray = new string[args.Length]; // proper array initialization to avoid null exception
            int wordCount = 0;
            string userInput;
            string fileLocation = "";

            // get file name from user
            Console.Write("Enter the file: ");
            userInput = Console.ReadLine();

            // regex code to confirm that user entry is a compatible file directory ending in a .txt file
            // Note: my text file was located at (F:\Lone Star Game Design AAS\GAME-1343\KZ_Week9_Regex\TestFile.txt)
            var fileChecker = new Regex(@"^(?:[\w]\:)+((\\+[A-Za-z0-9 _-]*){1,})+(.txt)$");

            // validate input using regex
            while (fileLocation == "")
            {
                if (fileChecker.IsMatch(userInput))
                {
                    Console.WriteLine("\nFile found!");
                    fileLocation = userInput;
                }
                else
                {
                    Console.WriteLine("\nPlease enter a valid file format, starting with the drive and ending in '.txt':");
                    userInput = Console.ReadLine();
                }
            } 

            // check for exception (for example, in case file is not found)
            try
            {
                // create a stream reader to open the file 
                if (fileLocation != null)
                {
                    StreamReader sr = new StreamReader(fileLocation);

                    // confirm the file is found and being read
                    Console.WriteLine("\nReading...\n");

                    // read the first line from file
                    string line = sr.ReadLine();

                    // make sure line != null to avoid null exception
                    while (line != null)
                    {
                        // print the line
                        Console.WriteLine(line);

                        // split the line into an array of strings (spaces are the default delimiter)
                        strArray = line.Split();

                        // push each string to the list, unless the length of string is 0 (happens at end of each line in .txt)
                        foreach (string str in strArray)
                        {
                            if(str.Length>0)
                                wordList.Add(str);
                        }
                        // read the next line
                        line = sr.ReadLine();
                    }

                    // length of list is your wordcount
                    wordCount = wordList.Count;

                    // print the word count
                    Console.WriteLine("\nThere are " + wordCount + " words in the file:");

                    // print all the individual words in the file for verification
                    for (int i = 0; i < wordList.Count; i++)
                    { 
                        Console.WriteLine(" <" + wordList[i] + ">");
                    }
                }  
            }
            catch (Exception ex)
            {
                // print helpful error message if exception is thrown
                Console.WriteLine("There was an error! " + ex.Message);
            }
        }
    }
}

     