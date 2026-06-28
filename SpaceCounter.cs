namespace DZ3;

public class SpaceCounter
{
    public record FileResult(string FilePath, int SpaceCount);

    public async Task<List<FileResult>> CountSpacesInFilesAsync(params string[] filePaths)
    {
        var tasks = filePaths.Select(async path =>
        {
            if (!File.Exists(path))
                return new FileResult(path, -1);

            string content = await File.ReadAllTextAsync(path);
            int spaces = content.Count(c => c == ' ');
            return new FileResult(path, spaces);
        });

        var results = await Task.WhenAll(tasks);
        return results.ToList();
    }

    public async Task<List<FileResult>> CountSpacesInFolderAsync(string folderPath)
    {
        if (!Directory.Exists(folderPath))
            return new List<FileResult>();

        string[] files = Directory.GetFiles(folderPath);
        if (files.Length == 0)
            return new List<FileResult>();

        return await CountSpacesInFilesAsync(files);
    }
}
