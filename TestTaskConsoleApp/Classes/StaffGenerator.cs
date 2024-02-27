using TestTask.Context.Enums;
using TestTask.Context.Models;

namespace TestTaskConsoleApp.Classes
{
    public class StaffGenerator
    {
        private List<string> MaleNames = new List<string>();
        private List<string> MaleSurnames = new List<string>();
        private List<string> MalePatronomyc = new List<string>();
        private List<string> FemaleNames = new List<string>();
        private List<string> FemalePatronomyc = new List<string>();
        private Random random = new Random();
        private BirthdayGenerator birthdayGenerator = new BirthdayGenerator();
        public StaffGenerator()
        {
            ReadTextFileInList(ref MaleNames, "MaleNamesLocalizationEng");
            ReadTextFileInList(ref MaleSurnames, "MaleSurnamesLoacalizationEng");
            ReadTextFileInList(ref MalePatronomyc, "MalePatronomycLocalizationEng");

            ReadTextFileInList(ref FemaleNames, "FemaleNamesLocalizationEng");
            ReadTextFileInList(ref FemalePatronomyc, "FemalePatronomycLocalizationEng");
        }

        private void ReadTextFileInList(ref List<string> list, string location)
        {
            using (var reader = new StreamReader($"Classes\\Generator.Data\\{location}.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
        }

        public Staff GenerateStaff(Gender gender)
        {
            var staff = new Staff();
            if (gender == Gender.Female)
            {
                staff.Surname = MaleSurnames[random.Next(MaleSurnames.Count)] + "a";
                staff.Name = FemaleNames[random.Next(FemaleNames.Count)];
                staff.Patronomyc = FemalePatronomyc[random.Next(FemalePatronomyc.Count())];
                staff.Gender = "Female";
                
            }
            else if (gender == Gender.Male)
            {
                staff.Surname = MaleSurnames[random.Next(MaleSurnames.Count)];
                staff.Name = MaleNames[random.Next(MaleNames.Count)];
                staff.Patronomyc = MalePatronomyc[random.Next(MalePatronomyc.Count())];
                staff.Gender = "Male";
            }

            staff.Birthday = birthdayGenerator.Make();

            return staff;
        }


    }
}
