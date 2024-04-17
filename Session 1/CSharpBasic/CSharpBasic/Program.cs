using System.Collections;
using System.Diagnostics.SymbolStore;

Console.WriteLine("Hello World");

// variable

primitive: int, float, bool, char, double, decimal
        short < int < long
        float < double < decimal
    non-primitive: string
 

int num;

num = 1;

var num1 = 2.0f;

float f1 = 10.0f / 3;
double f2 = 10 * 1.0 / 3;
decimal f3 = (decimal)10.0 / 3;

bool isTrue = false;

char c1 = 'a';

Console.WriteLine($"{f1} {f2} {f3}");

string name = "stephen sim";
name.ToLower();
name.ToUpper();


// static array
int[] array = new int[1];
int[] array1 = { 1, 2, 3 };
int[] array3 = new int[1] { 1 };

var list = new List<int>() { 1, 2, 3 };
list.Add(3);
list.Remove(1);

Console.WriteLine(list[1]);

// selection
var random = isTrue == true ? 1 : 0;

if (isTrue == true)
{
random = 1;
}
else
{
random = 0;
}

if (true)
{

}
else if (false)
{

}

switch (name)
{
case "alex":
Console.WriteLine("is alex");
break;

case "stephen":
Console.WriteLine("is alex");
break;
}

// loop 
do
{

} while (true);

while (true) ;

for (int i = 0; i < 10; i++)
{

}

foreach (var item in list)
{
Console.WriteLine(item);
}


try
{
    var array4 = new int[5] { 1, 2, 3, 4, 5 };

    Console.WriteLine(array[4]);
}
catch (Exception)
{
    Console.WriteLine("out of range.");
}

string text = string.Empty;

void sayHello()
{
    if (string.IsNullOrEmpty(text))
    {
        return;
    }

    Console.WriteLine(text);
}

int sum(int a, int b)
{
    return a + b;
}

int passbyRef(ref int x)
{
    return x++;
}

int passbyRefOut(out int x)
{
    x = 100;
    return x;
}

int y = 10;

passbyRef(ref y);

int num9;
passbyRefOut(out num9);

passbyRefOut(out int num10);
