using System.IO;
using System;

public interface ILetterService {
    void CombineTwoLetters (string inputfile1, string inputfile2, string resultfile);
    void DateFolder (string filename);
}

public class LetterService : ILetterService {
    public void CombineTwoLetters(string inputfile1, string inputfile2, string resultfile){
        string contents1 = File.ReadAllText(inputfile1);
        string contents2 = File.ReadAllText(inputfile2);

        File.WriteAllText(resultfile, contents1);
        File.AppendAllText(resultfile, contents2);
    }

    public void DateFolder(string filename){
        string time = DateTime.Now.ToShortDateString().ToString();
        DateTime date = DateTime.Parse(time);
        string date_f = date.ToString("yyyyMMdd");

        string path_s = @"CombinedLetters/Input/Scholarship";
        string[] sub_s = Directory.GetDirectories(path_s);
        string path_a = @"CombinedLetters/Input/Admission";
        string[] sub_a = Directory.GetDirectories(path_a);
        
        if (filename.Contains("scholarship")){
            foreach (string s in sub_s)
            {
                if (Path.GetFileName(s) == date_f)
                {
                    string targetFolder = path_s+date_f;
                    string targetFile = Path.Combine(targetFolder, Path.GetFileName(filename));
                    File.Copy(filename, targetFile, true);
                }

                else if(Path.GetFileName(s) != date_f){
                    Directory.CreateDirectory(date_f);
                    string targetFolder = path_s+date_f;
                    string targetFile = Path.Combine(targetFolder, Path.GetFileName(filename));
                    File.Copy(filename, targetFile, true);
                }
            }
        }
        else if (filename.Contains("admission")){
            foreach (string s in sub_a)
            {
                if (Path.GetFileName(s) == date_f)
                {
                    string targetFolder = path_a+date_f;
                    string targetFile = Path.Combine(targetFolder, Path.GetFileName(filename));
                    File.Copy(filename, targetFile, true);
                }

                else if(Path.GetFileName(s) != date_f){
                    Directory.CreateDirectory(date_f);
                    string targetFolder = path_a+date_f;
                    string targetFile = Path.Combine(targetFolder, Path.GetFileName(filename));
                    File.Copy(filename, targetFile, true);
                }
            }
        }

        else{
            Console.WriteLine("File name invalid");
            LetterService ls = new LetterService();
            ls.DateFolder(filename);
        }
    }

    public void checkCombineTwoLetter(){
        Dictionary<string, string> fileMap = new Dictionary<string, string>();
        string path = @"CombinedLetters/Input";
        string[] filePaths = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

        foreach (string p in filePaths)
        {
            string fileName = Path.GetFileName(p);

            if (fileMap.ContainsKey(fileName))
                CombineTwoLetters(@"CombinedLetters/Input/Scholarship"+fileName,
                @"CombinedLetters/Input/Admission"+fileName,
                @"CombinedLetters/Output/report.txt");
            else
                fileMap.Add(fileName, p);
        }
    }

    public static void Main(string[] args){
        string filename = Console.ReadLine();
        LetterService ls = new LetterService();
        ls.DateFolder(filename);
    }
}
