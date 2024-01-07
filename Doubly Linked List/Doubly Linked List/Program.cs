using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doubly_Linked_List
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating a CustomLinkedList
            CustomLinkedList<string> linkedList = new CustomLinkedList<string>();

            //Creating a loop that will continuously prompt the user for input until "quit" is entered
            bool play = true;
            while(play)
            {
                //Prompting the user for input
                Console.WriteLine("What would you like to do with your linked list?\nClear\t\tPrint\t\tReverse\t\tCount\t\tRemove\t\tScramble\t\tQuit");
                Console.WriteLine("Type something not listed in the commands to add it to the list");
                string input = Console.ReadLine();

                //Using a switch case to act accordingly to the input
                switch(input.ToLower())
                {
                    //"clear" input
                    case "clear":
                        //Informing the user that the linked list is being cleared and calling the Clear Method
                        Console.WriteLine("\n\nClearing the linked list\n\n\n");
                        linkedList.Clear();
                        break;



                    //"print" input
                    case "print":
                        //Informing the user that the linked list is being printed and looping through all possible indexes using the GetData Method
                        Console.WriteLine("\n\nPrinting the linked list:\n");
                        for (int index = 0; index < linkedList.Count; index++)
                        {
                            Console.WriteLine(linkedList.GetData(index));
                        }
                        Console.WriteLine("\n\n\n");
                        break;



                    //"reverse" input
                    case "reverse":
                        //Calling the PrintReversed Method, which informs the user that the linked list is being printed in reverse order
                        linkedList.PrintReversed();
                        break;



                    //"count" input
                    case "count":
                        //Informing the user how many items are in the linked list using the Count Property
                        Console.WriteLine("\n\nThere are " + linkedList.Count + " items in the linked list\n\n");
                        break;



                    //"remove" input
                    case "remove":
                        //Creating a Random object to generate an index to remove an item from and calling the RemoveAt Method to store the data being scrambled
                        Random removeRNG = new Random();
                        int remove = removeRNG.Next(linkedList.Count);
                        string removeData = linkedList.RemoveAt(remove);

                        //Informing the user what data was removed from what index was generated
                        Console.WriteLine("\n\nRemoving the data \"" + removeData + "\" from index: " + remove + "\n\n\n");
                        break;



                    //"scramble" input
                    case "scramble":
                        //Creating a Random object to generate an index to remove an item from and calling the RemoveAt Method to store the data being scrambled
                        Random scrambleRNG = new Random();
                        int scrambleRemove = scrambleRNG.Next(linkedList.Count);
                        string scrambleData = linkedList.RemoveAt(scrambleRemove);

                        //Generating a new index to insert the item at and informing the user what data is being scrambled and what index is being placed at, before calling the Insert Method
                        int scrambleInsert = scrambleRNG.Next(linkedList.Count);
                        Console.WriteLine("\n\nRemovingling the data \"" + scrambleData + "\" from the index: " + scrambleRemove + "\nInserting the data \"" + scrambleData + "\" at the index: " + scrambleInsert + "\n\n\n");
                        linkedList.Insert(scrambleData, scrambleInsert);
                        break;



                    //"quit" input
                    case "quit":
                        //Thanking the user and breaking the loop
                        Console.WriteLine("\n\nThanks for typing!");
                        play = false;
                        break;



                    //Default case for all other input
                    default:
                        //Informing the user that their input is being added to the linked list and calling the Add Method
                        Console.WriteLine("\n\nAdding \"" + input + "\" to the linked list\n\n\n");
                        linkedList.Add(input);
                        break;
                }
            }





            //Keeps the console window open
            Console.ReadLine();
        }
    }
}
