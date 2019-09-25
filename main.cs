using System;

public class main {

	public static void Main(String[] args) {
		double a, b, c;
		Console.Title = "Сысойкин Егор ИУ5-34Б";
		if(args.Length == 3) {
			Console.WriteLine("Taking A, B, C from args");
			if(!Double.TryParse(args[0], out a) || !Double.TryParse(args[1], out b) || !Double.TryParse(args[2], out c)) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error parsing args");
				return;
			}
		}
		else {
			Console.WriteLine("Invalid args. Put in A, B, C from keyboard");
			Console.Write("A= ");
                	while(!Double.TryParse(Console.ReadLine(), out a)) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error parsing A");
				Console.ResetColor();
				Console.Write("A= ");
			}
                	Console.Write("B= ");
                	while(!Double.TryParse(Console.ReadLine(), out b)) {
				Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Error parsing B");
                                Console.ResetColor();
                                Console.Write("B= ");
			}
                	Console.Write("C= ");
                	while(!Double.TryParse(Console.ReadLine(), out c)) {
				Console.ForegroundColor = ConsoleColor.Red;
                        	Console.WriteLine("Error parsing C");
                                Console.ResetColor();
                                Console.Write("C= ");
			}
		}
		Console.WriteLine("A = {0} B = {1} C = {2}", a, b, c);
		double y1, y2;
		double x1, x2, x3, x4;
		double D = b*b - 4*a*c;
		bool flag = false;
		Console.ForegroundColor = ConsoleColor.Green;
		if(D < 0) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("D < 0; no real roots");
			
		}
		else if( a == 0 && b == 0 && c != 0) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("no roots");
		}
		else if(a == 0 && b != 0 && c != 0) {
			if(-c/b >= 0) {
				x1 = Math.Sqrt(-c/b);
				x2 = -x1;
				Console.WriteLine("x1 = {0}, x2 = {1}", x1, x2);
			}
			else {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("no real roots");
			}
		}
		else if(a == 0 && b == 0 && c == 0) {
			Console.WriteLine("Anything");
		}
		else {
			y1 = (-b + Math.Sqrt(D))/(2*a);
			y2 = (-b - Math.Sqrt(D))/(2*a);
			if (y1 >= 0) {
				x1 = Math.Sqrt(y1);
				x2 = -x1;
				Console.WriteLine("x1 = {0}, x2 = {1}", x1, x2);
				flag = true;
			}
			if(y2 >= 0 && D != 0) {
				x3 = Math.Sqrt(y2);
				x4 = -x3;
				if(flag)
					Console.WriteLine("x3 = {0}, x4 = {1}", x3, x4);
				else 
					Console.WriteLine("x1 = {0}, x2 = {1}", x3, x4);
			}
						
		}
	}
}
