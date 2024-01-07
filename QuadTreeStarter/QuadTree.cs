using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace QuadTreeStarter
{
	class QuadTreeNode
	{
		// The maximum number of objects in a quad
		// before a subdivision occurs
		private const int MAX_OBJECTS_BEFORE_SUBDIVIDE = 3;

		// The game objects held at this level of the tree
		private List<GameObject> _objects;

		// This quad's rectangle area
		private Rectangle _rect;

		// This quad's divisions
		private QuadTreeNode[] _divisions;


		/// <summary>
		/// The divisions of this quad
		/// </summary>
		public QuadTreeNode[] Divisions { get { return _divisions; } }

		/// <summary>
		/// This quad's rectangle
		/// </summary>
		public Rectangle Rectangle { get { return _rect; } }

		/// <summary>
		/// The game objects inside this quad
		/// </summary>
		public List<GameObject> GameObjects { get { return _objects; } }
		

		/// <summary>
		/// Creates a new Quad Tree
		/// </summary>
		/// <param name="x">This quad's x position</param>
		/// <param name="y">This quad's y position</param>
		/// <param name="width">This quad's width</param>
		/// <param name="height">This quad's height</param>
		public QuadTreeNode(int x, int y, int width, int height)
		{
			// Save the rectangle
			_rect = new Rectangle(x, y, width, height);

			// Create the object list
			_objects = new List<GameObject>();

			// No divisions yet
			_divisions = null;
		}
		

		/// <summary>
		/// Adds a game object to the quad.  If the quad has too many
		/// objects in it, and hasn't been divided already, it should
		/// be divided
		/// </summary>
		/// <param name="gameObj">The object to add</param>
		public void AddObject(GameObject gameObj)
		{
			if(_rect.Contains(gameObj.Rectangle))
			{
				//Creating a boolean to record whether the object has been added to a subdivision
				bool added = false;

				//Determining whether there are any subdivisions
				if(_divisions != null)
				{
					//Incrementing through all subdivisions to see if they contain the object
					foreach (QuadTreeNode quad in _divisions)
					{
						if (quad.Rectangle.Contains(gameObj.Rectangle))
						{
							//If the subdivision does contain the object, the AddObject method is called recursively
							//	and the "added" boolean is set to true
							quad.AddObject(gameObj);
							added = true;
						}
					}
				}

				//If the object wasn't added to a subdivision, it is added to the current quad
				if(!added)
				{
					_objects.Add(gameObj);

					//If the number of objects is at/above the max count, the Divide method is called
					if(_objects.Count >= MAX_OBJECTS_BEFORE_SUBDIVIDE)
					{
						Divide();
					}
				}
			}
		}

		/// <summary>
		/// Divides this quad into 4 smaller quads.  Moves any game objects
		/// that are completely contained within the new smaller quads into
		/// those quads and removes them from this one.
		/// </summary>
		public void Divide()
		{
			//Making sure the current quad hasn't been divided yet
			if(_divisions == null)
			{
				//Creating the Divisions array
				_divisions = new QuadTreeNode[4];

				//Creating 4 new quads with all the same dimensions, but different positions
				_divisions[0] = new QuadTreeNode(_rect.X, _rect.Y, _rect.Width / 2, _rect.Height / 2);
				_divisions[1] = new QuadTreeNode(_rect.X + _rect.Width / 2, _rect.Y, _rect.Width / 2, _rect.Height / 2);
				_divisions[2] = new QuadTreeNode(_rect.X, _rect.Y + _rect.Height / 2, _rect.Width / 2, _rect.Height / 2);
				_divisions[3] = new QuadTreeNode(_rect.X + _rect.Width / 2, _rect.Y + _rect.Height / 2, _rect.Width / 2, _rect.Height / 2);

				//Incrementing through the new quads to add objects
				for(int quadIndex = 0; quadIndex < 4; quadIndex++)
				{
					//Incrementing through all possible objects to add
					for(int objIndex = 0; objIndex < _objects.Count; objIndex++)
					{
						//Determining if the quad of the current index contains the object being added
						if(_divisions[quadIndex].Rectangle.Contains(_objects[objIndex].Rectangle))
						{
							//If the new quad does contain the object, the object is added to it and removed from the current quad
							_divisions[quadIndex].AddObject(_objects[objIndex]);
							_objects.RemoveAt(objIndex);
							objIndex--;
						}
					}
				}
			}
		}

		/// <summary>
		/// Recursively populates a list with all of the rectangles in this
		/// quad and any subdivision quads.  Use the "AddRange" method of
		/// the list class to add the elements from one list to another.
		/// </summary>
		/// <returns>A list of rectangles</returns>
		public List<Rectangle> GetAllRectangles()
		{
			//Creating a new list of rectangles that will hold all rectangles stored in the current one, including the current one
			List<Rectangle> rects = new List<Rectangle>();

			//Checking whether any subdivisions exist, as they will contain aditional rectangles
			if(_divisions != null)
			{
				//Calling the GetAllRectangles method for each subdivision
				foreach (QuadTreeNode quad in _divisions)
				{
					//Adding each element from the returned list of each subdivision into rects
					List<Rectangle> moreRects = quad.GetAllRectangles();

					foreach(Rectangle rectangle in moreRects)
					{
						rects.Add(rectangle);
					}
				}
			}

			//Adding the _rect field to rects and returning
			rects.Add(_rect);
			return rects;
		}

		/// <summary>
		/// A possibly recursive method that returns the
		/// smallest quad that contains the specified rectangle
		/// </summary>
		/// <param name="rect">The rectangle to check</param>
		/// <returns>The smallest quad that contains the rectangle</returns>
		public QuadTreeNode GetContainingQuad(Rectangle rect)
		{
			//Checking whether the current node contains the rectangle at all
			if(_rect.Contains(rect))
			{
				//Checking whether there are any subdivisions of the current node
				if(_divisions != null)
				{
					//Checking whether these subdivisions contain the rect
					foreach(QuadTreeNode node in _divisions)
					{
						//If the subdivision does contain the rect, GetContainingQuad is recursively called
						if(node.Rectangle.Contains(rect))
						{
							return node.GetContainingQuad(rect);
						}
					}
				}

				//If there aren't any subdivisions or the subdivisions didnt return a value, the current node is returned
				return this;
			}

			//If the curent node does not contain the rect, null is returned
			return null;
		}
	}
}
