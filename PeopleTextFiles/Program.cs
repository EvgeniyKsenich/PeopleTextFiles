using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PeopleTextFiles
{
    class Program
    {
        private static object tmp;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            string path = @"F:\html_pr\2\input.txt";
            string path_out_1 = @"F:\html_pr\2\output_name.txt";
            string path_out_2 = @"F:\html_pr\2\output.txt";
            string file = Read(path);
            string[] MAS = GetMas(file);
            string[] BORGMAS = GetBorgMas(MAS);
            string[] SORT_BORGMAS = SORT(BORGMAS);
            File_one(path_out_1, SORT_BORGMAS);
            File_two(path_out_2, SORT_BORGMAS);
        }

        static string[] SORT(string[] mas)
        {
            int cout = mas.Length;
            double[] sort = new double[cout];
            double[] sort_out = new double[cout];
            string[] oo = new string [cout];
            Regex ch = new Regex(@"(\d*[.,]?\d+)");
            MatchCollection tmp = ch.Matches("");
            for (int i=0;i< cout;i++)
            {
                tmp = ch.Matches(mas[i]);
                sort[i] = double.Parse(tmp[2].Groups[0].Value);
            }
            double i1 = 0;
            string tmp1="";
            //Array.Sort(sort_out);
            //for (int i = 0; i < sort_out.Length; i++) Console.WriteLine(sort_out[i]+" ");
            for (int o = 0; o< cout; o++)
            {
                for (int i = 0; i < cout - 1; i++)
                {
                    if (sort[i] > sort[i + 1])
                    {
                        i1 = sort[i]; tmp1 = mas[i];
                        sort[i] = sort[i + 1]; mas[i] = mas[i + 1];
                        sort[i + 1] = i1; mas[i + 1] = tmp1;
                    }
                    
                }
            }
            oo = mas;
            return oo;
        }

        static void File_one(string path,string[] mas)
        {
            int cout = mas.Length;
            string all="";
            for(int o=0;o<cout;o++)
            {
                all += mas[o] + Environment.NewLine;
            }
            all += "Borg: "+ borg(mas).ToString();
            write(path, all);
        }

        static void File_two(string path, string[] mas)
        {
            int cout = mas.Length;
            string all = "";
            Regex ch = new Regex(@"(\d*[.,]?\d+)");
            MatchCollection tmp = ch.Matches("");
            Regex ch1 = new Regex(@"(([а-яіІА-ЯєЄЇї]+)([ ІА-ЯЄЇ]{2}\.)[ІА-ЯЄЇ]\.)");
            MatchCollection tmp1 = ch1.Matches("");
            for (int o = 0; o < cout; o++)
            {
                tmp = ch.Matches(mas[o]);
                tmp1 = ch1.Matches(mas[o]);
                all += tmp[0].Groups[0].Value+" ";
                for(int i=0;i< tmp1.Count;i++)
                {
                    all += tmp1[i].Groups[0].Value + " ";
                }
                all += Environment.NewLine;
            }
            write(path, all);
        }

        static double borg(string[] mas)
        {
            int cout = mas.Length;
            double z = 0;
            Regex ch = new Regex(@"(\d*[.,]?\d+)");
            MatchCollection tmp = ch.Matches("");
            for (int i = 0; i < cout; i++)
            {
                tmp = ch.Matches(mas[i]);
               z+= double.Parse(tmp[2].Groups[0].Value);
            }
            return z;
        }
        
        static string[] GetBorgMas(string[] all)
        {
            int cout = all.Length, cout_out=0;
            Regex ch = new Regex(@"(\d*[.,]?\d+)");
            MatchCollection tmp = ch.Matches("");
            for(int i=0;i< cout;i++)
            {
                tmp = ch.Matches(all[i]);
                if (tmp.Count == 3) cout_out++;
            }
            string[] mas_out = new string[cout_out];
            for (int i = 0, i1=0; i < cout; i++)
            {
                tmp = ch.Matches(all[i]);
                if (tmp.Count == 3) { mas_out[i1] = all[i]; i1++;}
            }
            return mas_out;
            
        }

        static string[] GetMas(string all)
        {
            int cout = 0, lenth = all.Length; 
            for (int i=0;i< lenth; i++)
            {
                if (all[i].ToString() == "\n") cout++; 
            }
            cout++;
            //Console.WriteLine(cout.ToString());
            string[] mas = new string[cout];
            for (int i = 0, i1=0; i < lenth; i++)
            {
                if(all[i].ToString()!="\n") mas[i1] += all[i];
                if (all[i].ToString() =="\n") i1++;
            }
            return mas;
         }

        static void write(string path, string file)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(file);
            }
        }
        static void write(string path, string[] file)
        {
            string file1 = "";
            for(int i=0;i<file.Length;i++)
            {
                file1 += file[i] + Environment.NewLine;
            }
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(file1);
            }
        }

        static string Read(string path)
        {
            string all = "";
            try
            {
                using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(1251)))
                {
                    all = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                  Console.WriteLine(e.Message);
            }
            return all;
        }
    }
}
