using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doubly_Linked_List
{
    class CustomLinkedList<T>
    {
        //Fields
        protected CustomLinkedNode<T> head;
        protected CustomLinkedNode<T> tail;
        protected int count;



        //Properties
        //Count Property
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }



        // --------------- Methods ---------------



        //Add Method
        public void Add(T data)
        {
            //Creating CustomLinkNode
            CustomLinkedNode<T> node = new CustomLinkedNode<T>();
            node.Data = data;

            //Linking the node to the head and tail if the LinkedList is empty
            if (count == 0)
            {
                head = node;
                tail = node;
            }

            //Linking the node to the current tail, then making the node the new tail
            else
            {
                node.Prev = tail;
                tail.Next = node;
                tail = tail.Next;
            }

            //Incrementing Count
            count++;
        }



        //GetData Method
        public T GetData(int index)
        {
            //Ensuring a valid index was entered
            if (index < count && index >= 0)
            {
                //Creading a temporary node that will flow through the links of each node
                CustomLinkedNode<T> current;

                //Checking to see if the index is entered is closer to the tail node to save time
                if (index > (count - 1) / 2)
                {
                    //Assigning the tail node to current
                    current = tail;

                    //Looping through the links of each node to the index
                    for (int i = count - 1; i > index; i--)
                    {
                        current = current.Prev;
                    }
                }

                else
                {
                    //Assigning the head node to current
                    current = head;

                    //Looping through the links of each node to the index
                    for (int i = 0; i < index; i++)
                    {
                        current = current.Next;
                    }
                }

                //Returning the data at the index
                return current.Data;
            }

            //Throwing an IndexOutOfRangeException if the index is invalid
            else
            {
                throw new System.IndexOutOfRangeException("Invalid index");
            }
        }



        //Insert Method
        public void Insert(T data, int index)
        {
            //Ensuring a valid index was entered
            if (index < count && index >= 0)
            {
                //Creating CustomLinkNode that holds the data
                CustomLinkedNode<T> node = new CustomLinkedNode<T>();
                node.Data = data;

                //Creading a temporary node that will flow through the links of each node
                CustomLinkedNode<T> current = default;

                //Checking whether the index is the head
                if (index == 0)
                {
                    //Assigning the head to the node's next value, assigning the node to the head's prev value, and making the node the new head
                    node.Next = head;
                    head.Prev = node;
                    head = node;
                }

                //Checking whether the index is entered is closer to the tail node to save time
                else if (index > (count - 1) / 2)
                {
                    //Assigning the tail node to current
                    current = tail;

                    //Looping through the links of each node to the index
                    for (int i = count - 1; i > index; i--)
                    {
                        current = current.Prev;
                    }
                }

                //If it is closer to the head, it searches from the head
                else
                {
                    //Assigning the head node to current
                    current = head;

                    //Looping through the links of each node to the index
                    for (int i = 0; i < index; i++)
                    {
                        current = current.Next;
                    }
                }

                //Linking the prev node assigned to current to the new node, assigning the lead value from current to the new node,
                //  assigning current to node's next value, and assigning the node as current's new lead value
                current.Prev.Next = node;
                node.Prev = current.Prev;
                node.Next = current;
                current.Prev = node;

                //Incrementing count
                count++;
            }

            //Throwing an IndexOutOfRangeException if the index is invalid
            else
            {
                throw new System.IndexOutOfRangeException("Invalid index");
            }
        }



        //RemoveAt Method
        public T RemoveAt(int index)
        {
            //Ensuring a valid index was entered
            if (index < count && index >= 0)
            {
                //Creading a temporary node that will flow through the links of each node
                CustomLinkedNode<T> current;

                //Checking whether the index is the only possible index
                if (count == 1)
                {
                    //Assigning head to current and clearinging the LinkedList
                    current = head;
                    this.Clear();

                    //Returning the data at the index
                    return current.Data;
                }

                //Checking whether the index is the head
                else if (index == 0)
                {
                    //Assigning the head to current, making the head's next value the new head, and making the new head's prev value null
                    current = head;
                    head = head.Next;
                    head.Prev = default;

                    //Decrementing count and returning the old head's data
                    count--;
                    return current.Data;
                }

                //Checking whether the index is the tail
                else if (index == count - 1)
                {
                    //Assigning the tail to current, making the tail's prev value the new tail, and making the new tail's next value null
                    current = tail;
                    tail = tail.Prev;
                    tail.Next = default;

                    //Decrementing count and returning the old tail's data
                    count--;
                    return current.Data;
                }

                //Checking to see if the index is entered is closer to the tail node to save time
                else if (index > (count - 1) / 2)
                {
                    //Assigning the tail node to current
                    current = tail;

                    //Looping through the links of each node to the index
                    for (int i = count - 1; i > index; i--)
                    {
                        current = current.Prev;
                    }
                }

                else
                {
                    //Assigning the head node to current
                    current = head;

                    //Looping through the links of each node to the index
                    for (int i = 0; i < index; i++)
                    {
                        current = current.Next;
                    }
                }

                //Linking the prev and next nodes assigned to current to each other
                current.Next.Prev = current.Prev;
                current.Prev.Next = current.Next;

                //Decrementing count and returning the data at the index
                count--;
                return current.Data;
            }

            //Throwing an IndexOutOfRangeException if the index is invalid
            else
            {
                throw new System.IndexOutOfRangeException("Invalid index");
            }
        }



        //Clear Method
        public void Clear()
        {
            head = default;
            tail = default;
            count = 0;
        }



        //Print Method (Even though it isn't used in Main)
        public void Print()
        {
            //Simple Print Message
            Console.WriteLine("\n\nPrinting the linked list:\n");

            //Starting at the head
            CustomLinkedNode<T> current = head;

            //Looping through every node and printing the data using the Next Property
            for (int index = 0; index < count; index++)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }



        //PrintReversed Method
        public void PrintReversed()
        {
            //Simple Print Message
            Console.WriteLine("\n\nPrinting the linked list in reversed order:\n");

            //Starting at the tail
            CustomLinkedNode<T> current = tail;

            //Looping through every node and printing the data using the Prev Property
            for (int index = 0; index < count; index++)
            {
                Console.WriteLine(current.Data);
                current = current.Prev;
            }
            Console.WriteLine("\n\n\n");
        }





        //Constructor
        public CustomLinkedList()
        {
            head = default;
            tail = default;
            count = 0;
        }
    }
}
