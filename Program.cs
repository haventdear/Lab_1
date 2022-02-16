var array1 = new List<Double>();
var array2 = new List<Double>();
var array3 = new List<Double>();
var path1 = @"D:\3 курс\2 семестр\Параллельное программирование\лабы\Lab_1\1.txt";
var path2 = @"D:\3 курс\2 семестр\Параллельное программирование\лабы\Lab_1\2.txt";
var path3 = @"D:\3 курс\2 семестр\Параллельное программирование\лабы\Lab_1\3.txt";

var timeStart1 = DateTime.Now;
var result1 = makeSum(array1, path1);
result1 += makeSum(array2, path2);
result1 += makeSum(array3, path3);
var timeEnd1 = DateTime.Now;
var duration1 = (timeEnd1 - timeStart1).TotalMilliseconds;
Console.WriteLine($"Result: {result1}, Time: {duration1}");

var timeStart2 = DateTime.Now;
var result2 = makeSumWithThreads(array1, array2, array3);
var timeEnd2 = DateTime.Now;
var duration2 = (timeEnd2 - timeStart2).TotalMilliseconds;
Console.WriteLine($"Result: {result2}, Time: {duration2}");

double makeSum(List<double> array, string path)
{
	return makeSumWithRange(array, path);
}

double makeSumWithThreads(List<double> array1, List<double> array2, List<double> array3)
{
	double sum1 = 0.0;
	double sum2 = 0.0;
	double sum3 = 0.0;

	var t1 = new Thread(() => { sum1 = makeSumWithRange(array1, path1); });
	var t2 = new Thread(() => {	sum2 = makeSumWithRange(array2, path2); });
	var t3 = new Thread(() => { sum3 = makeSumWithRange(array3, path3); });

	t1.Start();
	t2.Start();
	t3.Start();

	t1.Join();
	t2.Join();
	t3.Join();

	return sum1 + sum2 + sum3;
}

double makeSumWithRange(List<double> array, string path)
{
	try
	{
		using (StreamReader input = new StreamReader(path))
			while (!input.EndOfStream)
				array.Add(double.Parse(input.ReadLine()));
	}
	catch (FileNotFoundException)
	{
		Console.WriteLine("\nФайл не найден!");
	}
	catch (DirectoryNotFoundException)
	{
		Console.WriteLine("\nДиректория не найдена!");
	}
	catch (FormatException)
	{
		Console.WriteLine("\nНедопустимый формат входных данных!");
	}

	double sum = 0.0;
	for (var i = 0; i < array.Count; i++)
		sum = sum + array[i];
	array.Clear();
	return sum;
}