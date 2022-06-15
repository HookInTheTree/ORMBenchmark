using FactoryMethod;
using FactoryMethod.Controllers;
using FactoryMethod.Data;
using FactoryMethod.Interfaces;
using FactoryMethod.Models;
using System.Collections;
using System.Diagnostics;

(string, int)[] testData = new (string, int)[]
{
    ("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark10000;", 10000),
    ("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark20000;", 20000),
    ("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark30000;", 30000),
    ("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark40000;", 40000),
    ("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark50000;", 50000),
    ("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark60000;", 60000),
    ("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark70000;", 70000),
    ("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark80000;", 80000),
    ("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark90000;", 90000),
    ("User ID=postgres;Password=15region;Host=localhost;Port=5432;Database=Benchmark100000;", 100000)
};

Stopwatch stopwatch = new Stopwatch();

NpgsqlCommandGuid person2RecordsCreator;
NpgsqlCommandInt personRecordsCreator;

List<ICrudableInt> controllersInt;

List<ICrudableGuid> controllersGuid;

List<ICrudableNpgsqlGuid> controllersNpgsqlGuid;

List<ICrudableNpgsqlInt> controllersNpgsqlInt;

List<Point> results = new List<Point>();
int iterationsCount = 1000;

foreach (var data in testData)
{
    person2RecordsCreator = new NpgsqlCommandGuid(data.Item1);
    personRecordsCreator = new NpgsqlCommandInt(data.Item1);

    List<Person2> persons2ToUpdateDelete;
    List<Person> personsToUpdateDelete;

    Console.WriteLine();
    Console.WriteLine(data.Item1);

    //Int
    controllersInt = new List<ICrudableInt>()
    {
        new DapperInt(data.Item1),
        new EfCoreIntTracking(data.Item1),
        new EfCoreIntNoTracking(data.Item1),
        new EfCoreSqlQueryInt(data.Item1),
        new NpgsqlCommandIntMapping(data.Item1),
    };
    foreach (var intController in controllersInt)
    {
        personsToUpdateDelete = intController.Select().Take(iterationsCount).ToList();

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            intController.Select();
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = intController.Name,
            Operation = "Select",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(intController.Name + "  Select was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            intController.SelectWhere($"Новый{i.ToString()}");
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = intController.Name,
            Operation = "SelectWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(intController.Name + "  SelectWhere was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            intController.UpdateWhere(personsToUpdateDelete[i].Id, "Обновлено");
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = intController.Name,
            Operation = "UpdateWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(intController.Name + "  UpdateWhere was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            intController.DeleteWhere(personsToUpdateDelete[i].Id);
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = intController.Name,
            DatabaseName = data.Item1,
            Operation = "DeleteWhere",
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(intController.Name + "  DeleteWhere was finished");

        //deleted records recovering
        List<Person> people = new List<Person>();
        for (int i = 0; i < iterationsCount; i++)
        {
            people.Add(new Person()
            {
                FirstName = personsToUpdateDelete[i].FirstName,
                LastName = personsToUpdateDelete[i].LastName,
                FIO = personsToUpdateDelete[i].FIO,
                UserName = personsToUpdateDelete[i].UserName,
                Password = personsToUpdateDelete[i].Password,
            });
        }
        personRecordsCreator.Create(people);
    }

    //Guid
    controllersGuid = new List<ICrudableGuid>()
    {
        new DapperGuid(data.Item1),
        new EfCoreGuidTracking(data.Item1),
        new EfCoreGuidNoTracking(data.Item1),
        new EfCoreSqlQueryGuid(data.Item1),
        new NpgsqlCommandGuidMapping(data.Item1),
    };
    foreach (var controller in controllersGuid)
    {
        persons2ToUpdateDelete = controller.Select().Take(iterationsCount).ToList();

        stopwatch.Start();
        for (int i = 0; i < iterationsCount; i++)
        {
            controller.Select();
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = controller.Name,
            Operation = "Select",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(controller.Name + "  Select was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            controller.SelectWhere($"Новый{i.ToString()}");
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = controller.Name,
            Operation = "SelectWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(controller.Name + "  SelectWhere was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            controller.UpdateWhere(persons2ToUpdateDelete[i].Id, "Обновлено");
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = controller.Name,
            Operation = "UpdateWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(controller.Name + "  UpdateWhere was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            controller.DeleteWhere(persons2ToUpdateDelete[i].Id);
        }
        stopwatch.Stop();
        results.Add(new Point() {
            ControllerName = controller.Name,
            Iterations = data.Item2,
            DatabaseName= data.Item1,
            Operation = "DeleteWhere",
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
        });
        Console.WriteLine(controller.Name + "  DeleteWhere was finished");

        //deleted records recovering
        List<Person2> people = new List<Person2>();
        for (int i = 0; i < iterationsCount; i++)
        {
            people.Add(new Person2()
            {
                Id = Guid.NewGuid(),
                FirstName = persons2ToUpdateDelete[i].FirstName,
                LastName = persons2ToUpdateDelete[i].LastName,
                FIO = persons2ToUpdateDelete[i].FIO,
                UserName = persons2ToUpdateDelete[i].UserName,
                Password = persons2ToUpdateDelete[i].Password,
            });
        }
        person2RecordsCreator.Create(people);
    }

    //Npgsql Guid
    controllersNpgsqlGuid = new List<ICrudableNpgsqlGuid>()
    {
        new NpgsqlDataAdapterGuid(data.Item1),
        new NpgsqlCommandGuid(data.Item1)
    };
    foreach (var npgsqlGuidController in controllersNpgsqlGuid)
    {
        persons2ToUpdateDelete = new NpgsqlCommandGuidMapping(data.Item1).Select().Take(iterationsCount).ToList();

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlGuidController.Select();
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = npgsqlGuidController.Name,
            Operation = "Select",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(npgsqlGuidController.Name + "  Select was finished");


        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlGuidController.SelectWhere($"Новый{i.ToString()}");
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = npgsqlGuidController.Name,
            Operation = "SelectWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(npgsqlGuidController.Name + "  SelectWhere was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlGuidController.UpdateWhere(persons2ToUpdateDelete[i].Id, "Обновлено");
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = npgsqlGuidController.Name,
            Operation = "UpdateWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(npgsqlGuidController.Name + "  UpdateWhere was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlGuidController.DeleteWhere(persons2ToUpdateDelete[i].Id);
        }
        results.Add(new Point()
        {
            ControllerName = npgsqlGuidController.Name,
            Operation = "DeleteWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(npgsqlGuidController.Name + "  DeleteWhere was finished");

        //deleted records recovering
        List<Person2> people = new List<Person2>();
        for (int i = 0; i < iterationsCount; i++)
        {
            people.Add(new Person2()
            {
                Id = Guid.NewGuid(),
                FirstName = persons2ToUpdateDelete[i].FirstName,
                LastName = persons2ToUpdateDelete[i].LastName,
                FIO = persons2ToUpdateDelete[i].FIO,
                UserName = persons2ToUpdateDelete[i].UserName,
                Password = persons2ToUpdateDelete[i].Password,
            });
        }
        person2RecordsCreator.Create(people);
    }

    //Npgsql Int
    controllersNpgsqlInt = new List<ICrudableNpgsqlInt>()
    {
        new NpgsqlCommandInt(data.Item1),
        new NpgsqlDataAdapterInt(data.Item1)
    };
    foreach (var npgsqlIntController in controllersNpgsqlInt)
    {
        personsToUpdateDelete = new NpgsqlCommandIntMapping(data.Item1).Select().Take(iterationsCount).ToList();

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlIntController.Select();
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = npgsqlIntController.Name,
            Operation = "Select",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(npgsqlIntController.Name + "  Select was finished");
        
        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlIntController.SelectWhere($"Новый{i.ToString()}");
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = npgsqlIntController.Name,
            Operation = "SelectWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(npgsqlIntController.Name + "  SelectWhere was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlIntController.UpdateWhere(i, "Обновлено");
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = npgsqlIntController.Name,
            Operation = "UpdateWhere",
            DatabaseName = data.Item1,
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(npgsqlIntController.Name + "  UpdateWhere was finished");

        stopwatch.Restart();
        for (int i = 0; i < iterationsCount; i++)
        {
            npgsqlIntController.DeleteWhere(personsToUpdateDelete[i].Id);
        }
        stopwatch.Stop();
        results.Add(new Point()
        {
            ControllerName = npgsqlIntController.Name,
            DatabaseName = data.Item1,
            Operation = "DeleteWhere",
            TimeInSeconds = Math.Round((double)stopwatch.ElapsedMilliseconds / 1000, 3),
            Iterations = data.Item2
        });
        Console.WriteLine(npgsqlIntController.Name + "  DeleteWhere was finished");

        //deleted records recovering
        List<Person> people = new List<Person>();
        for (int i = 0; i < iterationsCount; i++)
        {
            people.Add(new Person()
            {
                FirstName = personsToUpdateDelete[i].FirstName,
                LastName = personsToUpdateDelete[i].LastName,
                FIO = personsToUpdateDelete[i].FIO,
                UserName = personsToUpdateDelete[i].UserName,
                Password = personsToUpdateDelete[i].Password,
            });
        }
        personRecordsCreator.Create(people);
    }
}

results = results.OrderBy(x => x.Operation).ThenBy(x => x.ControllerName).ToList();
using (var writer = new StreamWriter("E:/result.txt", true))
{
    foreach (var result in results)
    {
        await writer.WriteLineAsync($"{result.Operation} ({result.Iterations}) --- {result.TimeInSeconds} --- {result.ControllerName}");
    }
}

Console.WriteLine();

Console.ReadKey();