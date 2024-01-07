using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doubly_Linked_List
{
    class CustomLinkedNode<T>
    {
        //Fields
        protected T data;
        protected CustomLinkedNode<T> next;
        protected CustomLinkedNode<T> prev;



        //Properties
        //Data Property
        public T Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        //Next Property
        public CustomLinkedNode<T> Next
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }

        //Prev Property
        public CustomLinkedNode<T> Prev
        {
            get
            {
                return prev;
            }
            set
            {
                prev = value;
            }
        }



        //Constructor
        public CustomLinkedNode()
        {
            data = default;
            next = default;
            prev = default;
        }
    }
}
