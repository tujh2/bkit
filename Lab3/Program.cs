using System;
using System.Collections;
using System.Collections.Generic;
using Figures;

namespace Lab3 {
	class Program {
		public static void Main(string[] args) {
			Circle circle = new Circle(21);
			Rectangle rectangle1 = new Rectangle(4, 6);
			Rectangle rectangle2 = new Rectangle(1, 8);
			Square square = new Square(4);
			
			ArrayList figArrayList = new ArrayList();
			figArrayList.Add(circle); figArrayList.Add(rectangle1); figArrayList.Add(rectangle2); figArrayList.Add(square);
			Console.WriteLine("ArrayListFigures:");
			foreach (var i in figArrayList) Console.WriteLine(i);
			Console.WriteLine("");
			figArrayList.Sort();
			Console.WriteLine("ArrayListFigures sorted:");
                        foreach (var i in figArrayList) Console.WriteLine(i);
			Console.WriteLine("");

			List<Figure> figList = new List<Figure>();
			figList.Add(circle); figList.Add(rectangle1); figList.Add(rectangle2); figList.Add(square);
			Console.WriteLine("ListFigures:");
			foreach (var i in figList) Console.WriteLine(i);
			Console.WriteLine("");
			figList.Sort();
			Console.WriteLine("ListFigures sorted:");
			foreach (var i in figList) Console.WriteLine(i);

			Console.WriteLine("\nMatrix3D");
		        Matrix3D<Figure> cube = new Matrix3D<Figure>(4, 4, 4, null);
		        cube[0, 0, 0] = rectangle1;
            		cube[1, 1, 1] = rectangle2;
		        cube[2, 2, 2] = circle;
			cube[3, 3, 3] = square;
            		Console.WriteLine(cube.ToString());

			Console.WriteLine("\nSimpleStack");
			SimpleStack<Figure> simpleStack = new SimpleStack<Figure>();
			simpleStack.Push(rectangle1);
			simpleStack.Push(rectangle2);
			simpleStack.Push(square);
			simpleStack.Push(circle);
			while (simpleStack.Count > 0) {
				Figure figure = simpleStack.Pop();
				Console.WriteLine(figure);
			}
			Console.ReadLine();
		}
	}
}
