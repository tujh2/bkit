using System;
namespace Lab1 {
	public class Program {
		public static void ParseParam(String a, String name, ref double b, bool flag) {
			while(!Double.TryParse(a, out b)) {
				Console.ForegroundColor = ConsoleColor.Red;
				if(flag)
	                                Console.WriteLine("Error parsing param, put it from keyboard" + a);
                                Console.ResetColor();
				Console.Write(name + " = ");
				a = Console.ReadLine();
				flag = true;
			}
		}	
		public static void Main(String[] args) {
			double a = 0, b = 0, c = 0;
			Console.Title = "Сысойкин Егор ИУ5-34Б";
			bool flag = true;
			if(args.Length > 0) {
				ParseParam(args[0], "A", ref a, true);
				if(args.Length > 1) {
					ParseParam(args[1], "B", ref b, true);
					if(args.Length > 2)
						ParseParam(args[2], "C", ref c, true);
					else
						ParseParam(null, "C", ref c, false);
				}
				else  {
					ParseParam(null, "B", ref b, false);
					ParseParam(null, "C", ref c, false);
				}
			}
			else {
				ParseParam(null, "A", ref a, false);
				ParseParam(null, "B", ref b, false);
				ParseParam(null, "C", ref c, false);
			}

			Console.WriteLine("A = {0} B = {1} C = {2}", a, b, c);
			double y1, y2;
			double x1, x2, x3, x4;
			double D = b*b - 4*a*c;
			flag = false;
			Console.ForegroundColor = ConsoleColor.Green;
			if(D < 0) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("D < 0; no real roots");		
			}
			else if(a == 0) {
				if(b == 0) {
					if(c == 0) 
						Console.WriteLine("Anything");
					else {
						Console.ForegroundColor = ConsoleColor.Red;
                                		Console.WriteLine("no roots");
					}
				}
				else {
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
			}
			else {
				y1 = (-b + Math.Sqrt(D))/(2*a);
				y2 = (-b - Math.Sqrt(D))/(2*a);
				if(y1 < 0 && y2 < 0) {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("no real roots");
					return;
				}
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
			Console.Read();
		}
	}
}
