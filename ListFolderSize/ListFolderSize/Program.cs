
if (args == null || args.Length == 0)
{
    Console.WriteLine("No directory was provided");
    return;
}
var showAllFolders = false;


if (args.Length > 1)
{
    foreach (string arg in args)
    {
        switch (arg.ToLower().Replace("-", "").Replace("/", ""))
        {
            case "a":
                showAllFolders = true; break;

        }
    }
}


ListFolderSize.Functions.ListListDirectoriesWithSizeAndCountInfo(new DirectoryInfo(args[0]), showAllFolders);




