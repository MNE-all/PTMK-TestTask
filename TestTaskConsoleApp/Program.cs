using Microsoft.EntityFrameworkCore;
using System;
using TestTask.Context;
using TestTask.Context.Enums;
using TestTask.Context.Models;
using TestTaskConsoleApp.Classes;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            switch(args[0])
            {
                case "1":
                    using(var db = new TestTaskContext())
                    {
                        if(db.baseCreated)
                        {
                            Console.WriteLine("Таблица справочника сотрудников успешно создана");
                        }
                        else
                        {
                            Console.WriteLine("Таблица справочника сотрудников уже существует!");
                        }
                    }
                    break;
                case "2":
                    if (args.Length == 4)
                    {
                        string[] fullname = args[1].Split(' ');
                        if (fullname.Count() > 1 && fullname.Count() < 4)
                        {
                            if(DateTime.TryParse(args[2], out var date))
                            {
                                using(var db = new TestTaskContext())
                                {
                                    var entity = db.Staffs.Add(new TestTask.Context.Models.Staff
                                    {
                                        Surname = fullname[0],
                                        Name = fullname[1],
                                        Patronomyc = fullname.Count() == 3 ? fullname[2] : string.Empty,
                                        Birthday = date,
                                        Gender = args[3]
                                    }).Entity;
                                    db.SaveChanges();

                                    Console.WriteLine($"Пользователь \"{entity.Surname} {entity.Name} {entity.Patronomyc}\" успешно добавлен в базу данных под идентификатором {entity.Id}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Дата передана некорректно!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ФИО передано некорректно!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры переданы!");
                    }
                    break;
                case "3":
                    using(var db = new TestTaskContext())
                    {
                        var staffs = db.Staffs.ToList().OrderBy(x => x.Surname).ThenBy(x => x.Name).ThenBy(x => x.Patronomyc).DistinctBy(x => new
                        {
                            x.Surname,
                            x.Name,
                            x.Patronomyc,
                            x.Birthday
                        });

                        foreach (var staff in staffs)
                        {
                            Console.WriteLine($"{staff.Surname} {staff.Name} {staff.Patronomyc} {staff.Birthday.Day}.{staff.Birthday.Month}.{staff.Birthday.Year} {staff.Gender} {(int)(DateTime.Now - staff.Birthday).TotalDays/365} years old");
                        }
                    }
                    break;
                case "4":
                    var startAddTime = DateTime.Now;
                    var staffGenerator = new StaffGenerator();
                    var rnd = new Random();
                    Staff maleStaff = new Staff();

                    Array genders = Enum.GetValues(typeof(Gender));

                    int staffAmount = 0;

                    using (var db = new TestTaskContext())
                    {
                        for (var i = 0; i < 1000000; i++)
                        {
                            db.Staffs.Add(staffGenerator.GenerateStaff((Gender)genders.GetValue(rnd.Next(genders.Length))));
                        }
                        // Первый пакет данных
                        db.SaveChanges();

                        for (var i = 0; i < 100; i++)
                        {
                            maleStaff = staffGenerator.GenerateStaff(Gender.Male);
                            maleStaff.Surname = "F" + maleStaff.Surname;
                            db.Staffs.Add(maleStaff);
                        }
                        // Второй пакет данных
                        db.SaveChanges();

                        staffAmount = db.Staffs.ToList().Count();
                    }
                    TimeSpan resultAddTime = DateTime.Now - startAddTime;
                    Console.WriteLine($"Данные успешно добавлены, текущее количество записей: {staffAmount}\n" +
                        $"Затраченное время на операцию {resultAddTime}");
                    break;

                case "5":
                    var startTime = DateTime.Now;

                    using (var db = new TestTaskContext())
                    {
                        var result = db.Staffs.Where(x => x.Surname.StartsWith("F") && x.Gender == "Male").ToList();

                        foreach (var staff in result)
                        {
                            Console.WriteLine($"{staff.Surname} {staff.Name} {staff.Patronomyc} {staff.Birthday.Day}.{staff.Birthday.Month}.{staff.Birthday.Year} {staff.Gender} {(int)(DateTime.Now - staff.Birthday).TotalDays / 365} years old");
                        }
                    }

                    TimeSpan resultTime = DateTime.Now - startTime;

                    Console.WriteLine($"Затраченное время на операцию {resultTime}");
                    break;
            }
        }
        Console.WriteLine("");
    }
}