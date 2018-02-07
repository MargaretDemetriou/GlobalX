using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace name_sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            // This doesn't seem to work?
            var file = args[0];
            // Get the names from the file
            List<Name> names = GetNames(file);
            // Get all the sorted names
            List<Name> namesSorted = SortNames(names);
            // Print names onto the console
            PrintNames(namesSorted);
        }

        /// <summary>
        /// Collect the list of names from the file
        /// </summary>
        /// <returns>Returns the list of names</returns>
        static List<Name> GetNames(string fileName)
        {
            string line;
            List<Name> names = new List<Name>();
            // Read the file
            StreamReader file = new StreamReader(@fileName);
            while ((line = file.ReadLine()) != null)
            {
                string[] separators = { " " };
                string[] words = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                // Check how many names are given from 2 upto 4
                if (words.Length == 2)
                {
                    Name name = new Name();
                    name.firstName = words[0];
                    // Ensure the last name is always in the lastName of Name
                    name.lastName = words[1];
                    names.Add(name);
                }
                else if (words.Length == 3)
                {
                    Name name = new Name();
                    name.firstName = words[0];
                    // Ensure the second name is always in the secondName of Name
                    name.secondName = words[1];
                    name.lastName = words[2];
                    names.Add(name);
                }
                else
                {
                    Name name = new Name();
                    name.firstName = words[0];
                    name.secondName = words[1];
                    // Ensure the third name is always in the thirdName of Name
                    name.thirdName = words[2];
                    name.lastName = words[3];
                    names.Add(name);
                }
            }

            file.Close();
            return names;
        }

        /// <summary>
        /// Takes the List of names and sorts them by last name, first name, second name and third name.
        /// </summary>
        /// <param name="names">The list of names to sort</param>
        /// <returns>Returns the list of names after being sorted</returns>
        static List<Name> SortNames(List<Name> names)
        {
            names.OrderBy(x => x.lastName).ThenBy(x => x.firstName).ThenBy(x => x.secondName).ThenBy(x => x.thirdName);
            return names;
        }

        /// <summary>
        /// Go through the sorted names and print them to the console
        /// </summary>
        /// <param name="names">List of names to print</param>
        static void PrintNames(List<Name> names)
        {
            // Go through each name
            foreach (Name name in names)
            {
                // Collect the first name
                string line = name.firstName + " ";
                // Check if there is a second name
                if (name.secondName != "")
                {
                    // Add a second name to the line
                    line = line + name.secondName + " ";
                    // Check if there is a third name
                    if (name.thirdName != "")
                    {
                        // Add a third name to the line
                        line = line + name.thirdName + " ";
                    }
                }
                else
                {
                    // Add a last name to the line
                    line = line + name.lastName;
                    // Print the line to the console
                    Console.WriteLine(line);
                    using (StreamWriter file = new StreamWriter("sorted-names-list.txt"))
                    {
                        file.WriteLine(line);
                    }
                }
                Console.Read();
            }
        }
    }
}
