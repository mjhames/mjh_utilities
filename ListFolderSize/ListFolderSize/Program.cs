
if (args == null || args.Length == 0)
{
    Console.WriteLine("No directory was provided");
    return;
}
var showAllFolders = false;

if (args.Any(a => a.ToLower().Replace("-", "").Replace("/", "") == "a"))
    showAllFolders = true;

ListFolderSize.Functions.ListDirectoriesWithSizeAndCountInfo(new DirectoryInfo(args[0]), showAllFolders, Console.Out);




