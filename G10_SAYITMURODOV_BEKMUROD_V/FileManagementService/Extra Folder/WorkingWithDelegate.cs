namespace FileManagementService.Extra_Folder;

public class WorkingWithDelegate
{

    public static Delegate delegate1 = ThereAreUpperThenTen;
    public static Delegate delegate2 = ThereAreUpperThenThirty;
    public static Delegate delegate3 = ThereAreUpperThenTwenty;
    public static Delegate delegate4 = ThereAreUpperThenEleven;
    public static Delegate delegate5 = ThereAreUpperThenFive;


    public static Func<string, int> deligatee = new Func<string, int>(CountOfXlsxFiles);

    public static int CountOfXlsxFiles(string folderPath)
    {
        var count = 0;
        var info = Directory.EnumerateFiles(folderPath);
        foreach (var file in info)
        {
            if(Path.GetExtension(file) == ".xlsx")
            {
                count++;
            }
        }
        return count;
    }



    public static bool ThereAreUpperThenTen(int num1, int num2, int num3)
    {
        return num1 + num2 + num3 > 10;
    }
    public static bool ThereAreUpperThenThirty(int num1, int num2, int num3)
    {
        return num1 + num2 + num3 > 30;
    }
    public static bool ThereAreUpperThenTwenty(int num1, int num2, int num3)
    {
        return num1 + num2 + num3 > 20;
    }
    public static bool ThereAreUpperThenEleven(int num1, int num2, int num3)
    {
        return num1 + num2 + num3 > 11;
    }
    public static bool ThereAreUpperThenFive(int num1, int num2, int num3)
    {
        return num1 + num2 + num3 > 5;
    }
}
