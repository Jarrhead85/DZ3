using System.Diagnostics;
using DZ3;

SpaceCounter counter = new();

string[] filePaths = ["file1.txt", "file2.txt", "file3.txt"];
string folderPath = "./TestFolder";

Console.WriteLine("Три указанных файла");

Stopwatch sw1 = Stopwatch.StartNew();
var results1 = await counter.CountSpacesInFilesAsync(filePaths);
sw1.Stop();

foreach (var r in results1)
{
    if (r.SpaceCount == -1)
        Console.WriteLine($"Файл не найден: {r.FilePath}");
    else
        Console.WriteLine($"{r.FilePath}: {r.SpaceCount} пробелов");
}

int total1 = results1.Where(r => r.SpaceCount >= 0).Sum(r => r.SpaceCount);
Console.WriteLine($"Общее количество пробелов: {total1}");
Console.WriteLine($"Время выполнения: {sw1.ElapsedMilliseconds} мс");
Console.WriteLine();

Console.WriteLine("Все файлы в папке");

Stopwatch sw2 = Stopwatch.StartNew();
var results2 = await counter.CountSpacesInFolderAsync(folderPath);
sw2.Stop();

if (results2.Count == 0)
{
    Console.WriteLine("Папка не найдена или не содержит файлов");
}
else
{
    foreach (var r in results2)
        Console.WriteLine($"{Path.GetFileName(r.FilePath)}: {r.SpaceCount} пробелов");

    int total2 = results2.Sum(r => r.SpaceCount);
    Console.WriteLine($"Общее количество пробелов: {total2}");
}

Console.WriteLine($"Время выполнения: {sw2.ElapsedMilliseconds} мс");
