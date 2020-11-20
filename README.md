# Newbe.ObjectVisitor
<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->
[![All Contributors](https://img.shields.io/badge/all_contributors-2-orange.svg?style=flat-square)](#contributors-)
<!-- ALL-CONTRIBUTORS-BADGE:END -->

<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->

[![All Contributors](https://img.shields.io/badge/all_contributors-2-orange.svg?style=flat-square)](#contributors-)

<!-- ALL-CONTRIBUTORS-BADGE:END -->

- [简体中文](README_zh_Hans.md)
- [English](README.md)

![Banner](https://github.com/newbe36524/Newbe.ObjectVisitor/raw/docs/assets/banner.svg)

You can visit all properties about your class by this lib with high performance as you visit properties in hard coding way.

For example, here is object in your code.

```cs
var order = new OrderInfo();
```

And, you want to print all properties of the order.

```cs
for(var pInfo in typeof(OrderInfo).GetProperties())
{
    Console.Writeline($"{pInfo.Name}: {pInfo.GetValue(order)}");
}
```

By using this lib, you can handle it in this way:

```cs
// call .V what is a static extension method
// you get a visitor object for order
var visitor = order.V();

visitor.ForEach(context=>{
    var name = context.Name;
    var value = context.Value;
    Console.Writeline($"{name}: {value}");
}).Run();

// you can also make it into one line
order.V().ForEach(c=> Console.Writeline($"{c.Name}: {c.Value}")).Run();

// or using quick style
order.FormatToString();
```

## Why do I need this?

- **It is faster.** This lib is impletmented with [Expression Trees](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/expression-trees/) that cost 1/10 time as in Reflection way.
- **It is readable.** This lib can generate a lambda func to handle the code flow you create that just as your hard coding without reflection.
- **It is extendable.** If you can visit all properties of a object in easy way, you can validate them as you wish, change some value if there are something sensitive, creare a mapper like automapper, and etc.

## API

| icon | remark                                                                   |
| ---- | ------------------------------------------------------------------------ |
| ✔️   | it is already avaliable in latest version                                |
| 🚧   | still in plan or development and will be implemented, changed or removed |
| ❌   | it is removed form the latest version                                    |

```cs
var o = new Yueluo();

using Newbe.ObjectVisitor;

//✔️ from 0.1
// V is a static extension method
var visitor = o.V();

//✔️ from 0.1
// create visitor from factory method
var visitor = typeof(Yueluo).V();

//✔️ from 0.1
// create and fire way.
// this is the most simple structure about this lib
// there are Name, Value, PropertyInfo, SourceObj, SourceObjType and etc in the context
o.V().ForEach((context)=>{}).Run();
o.V().ForEach((name,value)=>{}).Run();

// ✔️ from 0.2
// multiple foreach
o.V().ForEach((context)=>{}).ForEach((context)=>{}).Run();


//✔️ from 0.1
// create a visitor with extend object as parameter
o.V().WithExtendObject<Yueluo, StringBuilder>()
    .ForEach((context)=>{var _ = context.ExtendObject})
    .Run(new StringBuilder());
o.V().WithExtendObject<Yueluo, StringBuilder>()
    .ForEach((name,value,stringBuilder)=>{})
    .Run(new StringBuilder());

//✔️ from 0.1
// create and cache way. This is suggested way to use.
// cache object visitor to run it with anothor object
var cachedVisitor = deafult(Yueluo).V().ForEach((context)=>{}).Cache();
cachedVisitor.Run(new Yueluo());

//✔️ from 0.1
// cache object visitor with extend object
var cachedVisitor = deafult(Yueluo).V()
    .WithExtendObject<Yueluo, StringBuilder>()
    .ForEach((context)=>{var _ = context.ExtendObject})
    .Cache();
cachedVisitor.Run(new Yueluo(), new StringBuilder());


//✔️ from 0.2
// you can modify value if return a new value
o.V().ForEach((context) => ModifyData（context）).Run();

public static void ModifyData(IObjectVisitorContext<Yueluo,string> context)
{
    context.Value = context.Value.SubString(0,1)；
}

//✔️ from 0.1
// get debug info about expression now
var debugInfo = o.V().ForEach((context)=>{}).GetDebugInfo();

//🚧 generate code in C# as a string about expression now
var code = o.V().ForEach((context)=>{}).GenerateCode();

//✔️ from 0.1
// generate a lambda func
var func = o.V().ForEach((context)=>{}).GetLambda();


//✔️ from 0.2
// foreach properties with specified type
o.V().ForEach<Yueluo, string>((context) => {});
// the same as above
o.V().ForEach<Yueluo, string>((context) => {}, x => x.PropertyType == typeof(string));
// foreach properties with string type and marked with RequiredAttribute
o.V().ForEach<Yueluo, string>((context) => {}, x => x.PropertyType == typeof(string) && x.GetCustomAttribute<RequiredAttribute>());
// foreach properties that implemented IEnumerable<int> ,such as List<int>, int[], IEnumerable<int>, HashSet<int> and etc.
o.V().ForEach<Yueluo, IEnumerable<int>>((context) => {}, x => x.IsOrImplOf<IEnumerable<int>>());
// with extend object as parameter
o.V().WithExtendObject<Yueluo, StringBuilder>().ForEach<Yueluo, StringBuilder, string>((context) => {});

//🚧  using linq to filter
o.V().AsEnumerable().Where((context)=>context.Name == "YueLuo").ForEach((context)=>{}).Run();

//🚧  suppending visiting sub object
o.V().SuppendSubObject().ForEach((context)=>{}).Run();

//🚧  suppending visiting enumerable object
o.V().SuppendEnumerable().ForEach((context)=>{}).Run();


/**
 ✔️ from 0.1
 sample to join all properties to string
*/
var sb = new StringBuilder();
o.V()
.ForEach((context)=>s.AppendFormat("{0}:{1}{2}", name, value,Environment.NewLine))
.Run();
var s = sb.ToString();

//✔️ from 0.1
// quick style for above
var s = o.FormatString();

//🚧 Deconstruct as C# 7 but more flexible
var destructor1 = Destructor<Yueluo>
    .Property(x=>x.Name)
    .Property(x=>x.Age)

var destructor2 = Destructor<Yueluo>
    .Property(x=>x.Name)
    .Property(x=>(long)x.Age)

var destructor3 = Destructor<Yueluo>
    .Property(x=>x.Name)
    .Property(x=>x.NickName)
    .Property(x=>x.Age)

var (name, age) = o.V().Destruct(destructor1).Run();
var (name, ageInLong) = o.V().Destruct(destructor2).Run();
var (name, nickName, age) = o.V().Destruct(destructor3).Run();

// namespace for operation with collections
using Newbe.ObjectVisitor.Collections;

/**
 🚧collect properties into a dictionary
*/

var dic1 = o.V().CollectAsDictionary().Run();
// quick style for above
var dic1 = o.V().ToDictionary();

/**
 🚧apply value from a dictionary to object
*/
o.V().ApplyFromDictionary(dic).Run();
// quick style for above
o.V().FromDictionary(dic);


// namespace for data validation
using Newbe.ObjectVisitor.Validation;

// 🚧create rule to validation
var rule = ValidateRule<Yueluo>
    .GetBuilder()
    .Property(x=>x.Name).Required().Length(2,10)
    .Property(x=>x.Age).Range(0, int.MaxValue)
    .Property(x=>x.Password).Validate(value=>ValidatePassword(value))
    .Property(x=>x.Level).Validate(value=>value + 1 >= 0)
    .Build();

o.V().Validate(rule).Run();
o.Validate(rule);


// 🚧validate data in flunet api
// attribute-based enabled by default
o.V().Validate(v=>
    v
     .Property(x=>x.Name).Required().Length(2,10)
     .Property(x=>x.Age).Range(0, int.MaxValue)
     .Property(x=>x.Password).Validate(value=>ValidatePassword(value))
     .Property(x=>x.Level).Validate(value=>value + 1 >= 0)
).Run();

// 🚧suppending attribute-based validation
o.V().SuppendAttributeValidation()
    .Validate(v=>
        v
        .Property(x=>x.Name).Required().Length(2,10)
        .Property(x=>x.Age).Range(0, int.MaxValue)
        .Property(x=>x.Password).Validate(value=>ValidatePassword(value))
        .Property(x=>x.Level).Validate(value=>value + 1 >= 0)
).Run();

// 🚧suppending sub-object validation
o.V().SuppendSubObject()
    .SuppendAttributeValidation()
    .Validate(v=>
        v
        .Validate(x=>x.NewPassword == x.OldPassword)
        .Validate(x=>ValidateFormDb(x))
        .Property(x=>x.Name).Required().Length(2,10)
        .Property(x=>x.Age).Range(0, int.MaxValue)
        .Property(x=>x.Age).If(x=>x.Name == "123").Range(0, int.MaxValue)
        .Property(x=>x.Password).Validate(value=>ValidatePassword(value))
        .Property(x=>x.Level).Validate(value=>value + 1 >= 0)
).Run();

// namespace for Task
using Newbe.ObjectVisitor.Task;

// 🚧async way
await o.V().ForEachAsync((context)=>{}).RunAsync();

// 🚧controlling concurrency
await o.V().ForEachAsync((context)=>{}).WhenAsync(tasks=>Task.WhenAll(tasks)).RunAsync();

// namespace for Microsoft.Extensions.DependencyInjection
using Newbe.ObjectVistory.DepencyInjection;

// 🚧inject services to the properties of this object
this.V().ForEach(context=>this.ServiceProvider.GetService(context.PropertyInfo.PorpertyType)).Run();

// 🚧quick style for above
this.V().PropertyInject(this.ServiceProvider);

// ✔️ from 0.3
// generate api code from a fluent api DSL
var content = File.ReadAllText("SumBuilder.fluent.md");
var parser = new FluentApiDesignParser();
var re = parser.Parse(content);
var generator = new FluentApiFileGenerator();
var output = generator.Generate(re);
File.WriteAllText("SumBuilder.cs", output.FluentApiFiles.AutoGenerate);

```

## Benchmark

Machine info about benchmark.

```ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Xeon CPU E5-2678 v3 2.50GHz, 1 CPU, 24 logical and 12 physical cores
.NET Core SDK=5.0.100
  [Host]       : .NET Core 2.1.23 (CoreCLR 4.6.29321.03, CoreFX 4.6.29321.01), X64 RyuJIT
  net461       : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48        : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  netcoreapp21 : .NET Core 2.1.23 (CoreCLR 4.6.29321.03, CoreFX 4.6.29321.01), X64 RyuJIT
  netcoreapp31 : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  netcoreapp5  : .NET Core 5.0.0 (CoreCLR 5.0.20.47505, CoreFX 5.0.20.47505), X64 RyuJIT


```

### Newbe.ObjectVisitor vs Reflection vs Directly

We are going to join property infos about a object into a string. That string will be join by methods below:

| Method       | Descrption                                                                                                                  |
| ------------ | --------------------------------------------------------------------------------------------------------------------------- |
| Directly     | Join properties with StringBuilder by hard coding.                                                                          |
| CacheVisitor | Build a ObjectVisitor with Newbe.ObjectVisitor and cache it into a field. Using that cached visitor to join all properties. |
| QuickStyle   | Using a method built in Newbe.ObjectVisitor to handle this problem without building ObjectVisitor by yourself               |

chart:

![Newbe.ObjectVisitor.BenchmarkTest.FormatStringTest](https://github.com/newbe36524/Newbe.ObjectVisitor/raw/docs/assets/Newbe.ObjectVisitor.BenchmarkTest.FormatStringTest-barplot.png)

data:

| Method       | Job          | Runtime       |     Mean |    Error |   StdDev | Ratio | RatioSD | Rank |
| ------------ | ------------ | ------------- | -------: | -------: | -------: | ----: | ------: | ---: |
| Directly     | net461       | .NET 4.6.1    | 754.3 ns |  8.49 ns |  7.94 ns |  1.00 |    0.00 |    1 |
| QuickStyle   | net461       | .NET 4.6.1    | 818.3 ns | 16.29 ns | 24.87 ns |  1.10 |    0.04 |    3 |
| CacheVisitor | net461       | .NET 4.6.1    | 791.4 ns | 11.62 ns | 10.30 ns |  1.05 |    0.01 |    2 |
|              |              |               |          |          |          |       |         |      |
| Directly     | net48        | .NET 4.8      | 738.5 ns |  7.37 ns |  6.90 ns |  1.00 |    0.00 |    1 |
| QuickStyle   | net48        | .NET 4.8      | 799.0 ns | 10.63 ns |  9.42 ns |  1.08 |    0.01 |    2 |
| CacheVisitor | net48        | .NET 4.8      | 788.0 ns |  8.27 ns |  6.91 ns |  1.07 |    0.02 |    2 |
|              |              |               |          |          |          |       |         |      |
| Directly     | netcoreapp21 | .NET Core 2.1 | 768.6 ns |  9.63 ns |  9.01 ns |  1.00 |    0.00 |    1 |
| QuickStyle   | netcoreapp21 | .NET Core 2.1 | 787.6 ns |  6.11 ns |  5.42 ns |  1.02 |    0.02 |    2 |
| CacheVisitor | netcoreapp21 | .NET Core 2.1 | 768.6 ns |  5.30 ns |  4.96 ns |  1.00 |    0.01 |    1 |
|              |              |               |          |          |          |       |         |      |
| Directly     | netcoreapp31 | .NET Core 3.1 | 659.4 ns |  6.64 ns |  5.88 ns |  1.00 |    0.00 |    1 |
| QuickStyle   | netcoreapp31 | .NET Core 3.1 | 685.1 ns |  8.25 ns |  7.72 ns |  1.04 |    0.01 |    2 |
| CacheVisitor | netcoreapp31 | .NET Core 3.1 | 655.6 ns |  5.90 ns |  5.52 ns |  0.99 |    0.01 |    1 |
|              |              |               |          |          |          |       |         |      |
| Directly     | netcoreapp5  | .NET Core 5.0 | 624.2 ns |  3.59 ns |  3.00 ns |  1.00 |    0.00 |    2 |
| QuickStyle   | netcoreapp5  | .NET Core 5.0 | 641.2 ns |  5.60 ns |  4.97 ns |  1.03 |    0.01 |    3 |
| CacheVisitor | netcoreapp5  | .NET Core 5.0 | 604.2 ns |  8.19 ns |  7.66 ns |  0.97 |    0.01 |    1 |

summary:

1. By using Newbe.ObjectVisitor, it only takes very little time to achieve the same effect as hard code.
2. By using quick style, it cost very little time to build a cache visitor with few codes.

We are going to show no cache object visitor and reflection:

| Method          | Descrption                                                                                                                  |
| --------------- | --------------------------------------------------------------------------------------------------------------------------- |
| CacheVisitor    | Build a ObjectVisitor with Newbe.ObjectVisitor and cache it into a field. Using that cached visitor to join all properties. |
| NoCacheVisitor  | Build a ObjectVisitor with Newbe.ObjectVisitor and use it without cache it.                                                 |
| ReflectProperty | Using reflection to get all properties and join them into string                                                            |

chart:

![Newbe.ObjectVisitor.BenchmarkTest.CacheVisitorTest](https://github.com/newbe36524/Newbe.ObjectVisitor/raw/docs/assets/Newbe.ObjectVisitor.BenchmarkTest.CacheVisitorTest-barplot.png)

data：

| Method          | Job          | Runtime       |         Mean |       Error |      StdDev |    Ratio | RatioSD | Rank |
| --------------- | ------------ | ------------- | -----------: | ----------: | ----------: | -------: | ------: | ---: |
| CacheVisitor    | net461       | .NET 4.6.1    |     783.2 ns |     5.34 ns |     5.00 ns |     1.00 |    0.00 |    1 |
| ReflectProperty | net461       | .NET 4.6.1    |   1,528.1 ns |    16.26 ns |    14.41 ns |     1.95 |    0.02 |    2 |
| NoCacheVisitor  | net461       | .NET 4.6.1    | 988,465.0 ns | 6,373.48 ns | 5,649.92 ns | 1,261.57 |   11.70 |    3 |
|                 |              |               |              |             |             |          |         |      |
| CacheVisitor    | net48        | .NET 4.8      |     809.0 ns |    13.99 ns |    13.08 ns |     1.00 |    0.00 |    1 |
| ReflectProperty | net48        | .NET 4.8      |   1,531.4 ns |    23.96 ns |    22.41 ns |     1.89 |    0.03 |    2 |
| NoCacheVisitor  | net48        | .NET 4.8      | 996,612.1 ns | 9,277.44 ns | 8,678.12 ns | 1,232.28 |   24.07 |    3 |
|                 |              |               |              |             |             |          |         |      |
| CacheVisitor    | netcoreapp21 | .NET Core 2.1 |     793.3 ns |     6.11 ns |     5.71 ns |     1.00 |    0.00 |    1 |
| ReflectProperty | netcoreapp21 | .NET Core 2.1 |   1,425.3 ns |    26.15 ns |    25.69 ns |     1.80 |    0.04 |    2 |
| NoCacheVisitor  | netcoreapp21 | .NET Core 2.1 | 724,556.5 ns | 6,290.47 ns | 5,576.34 ns |   913.33 |   11.43 |    3 |
|                 |              |               |              |             |             |          |         |      |
| CacheVisitor    | netcoreapp31 | .NET Core 3.1 |     636.8 ns |     2.30 ns |     2.15 ns |     1.00 |    0.00 |    1 |
| ReflectProperty | netcoreapp31 | .NET Core 3.1 |   1,294.0 ns |     6.91 ns |     5.77 ns |     2.03 |    0.01 |    2 |
| NoCacheVisitor  | netcoreapp31 | .NET Core 3.1 | 625,524.4 ns | 1,596.90 ns | 1,333.49 ns |   982.17 |    3.79 |    3 |
|                 |              |               |              |             |             |          |         |      |
| CacheVisitor    | netcoreapp5  | .NET Core 5.0 |     629.6 ns |     3.81 ns |     3.56 ns |     1.00 |    0.00 |    1 |
| ReflectProperty | netcoreapp5  | .NET Core 5.0 |   1,211.6 ns |    14.09 ns |    13.18 ns |     1.92 |    0.02 |    2 |
| NoCacheVisitor  | netcoreapp5  | .NET Core 5.0 | 545,642.6 ns | 1,633.25 ns | 1,447.83 ns |   866.61 |    4.45 |    3 |

summary:

1. It will cost much more time to build a ObjectVisitor, since it will take more time to build more object and reflection. So we suggest to use build ObjectVisitor and cache it. You can still create a un-cached object visitor in cold code path since it take less then 1 ms.
2. A Cache visitor is faster than reflection way.

### Modify Data with Condition

Maybe you want to replace a property named Password with '\*\*\*' in you object. That string will be done by methods below:

| Method       | Descrption                                                                                                          |
| ------------ | ------------------------------------------------------------------------------------------------------------------- |
| Directly     | Modify data directly with assign statement                                                                          |
| UsingVisitor | Build a ObjectVisitor with Newbe.ObjectVisitor and cache it into a field. Using that cached visitor to modify info. |

chart:

![Newbe.ObjectVisitor.BenchmarkTest.ChangePasswordTest-barplot](https://github.com/newbe36524/Newbe.ObjectVisitor/raw/docs/assets/Newbe.ObjectVisitor.BenchmarkTest.ChangePasswordTest-barplot.png)

data:

| Method       | Job          | Runtime       |       Mean |    Error |   StdDev | Ratio | RatioSD | Rank |
| ------------ | ------------ | ------------- | ---------: | -------: | -------: | ----: | ------: | ---: |
| Directly     | net461       | .NET 4.6.1    | 1,205.4 ns |  8.90 ns |  7.44 ns |  1.00 |    0.00 |    1 |
| UsingVisitor | net461       | .NET 4.6.1    | 3,807.0 ns | 68.28 ns | 63.87 ns |  3.15 |    0.04 |    2 |
|              |              |               |            |          |          |       |         |      |
| Directly     | net48        | .NET 4.8      | 1,205.9 ns |  5.74 ns |  5.08 ns |  1.00 |    0.00 |    1 |
| UsingVisitor | net48        | .NET 4.8      | 3,743.3 ns | 18.51 ns | 15.46 ns |  3.11 |    0.02 |    2 |
|              |              |               |            |          |          |       |         |      |
| Directly     | netcoreapp21 | .NET Core 2.1 |   999.3 ns |  7.28 ns |  6.08 ns |  1.00 |    0.00 |    1 |
| UsingVisitor | netcoreapp21 | .NET Core 2.1 | 2,882.4 ns |  9.58 ns |  8.96 ns |  2.89 |    0.02 |    2 |
|              |              |               |            |          |          |       |         |      |
| Directly     | netcoreapp31 | .NET Core 3.1 |   807.9 ns |  3.46 ns |  3.07 ns |  1.00 |    0.00 |    1 |
| UsingVisitor | netcoreapp31 | .NET Core 3.1 | 2,614.1 ns | 13.79 ns | 12.90 ns |  3.24 |    0.02 |    2 |
|              |              |               |            |          |          |       |         |      |
| Directly     | netcoreapp5  | .NET Core 5.0 |   533.8 ns |  1.72 ns |  1.44 ns |  1.00 |    0.00 |    1 |
| UsingVisitor | netcoreapp5  | .NET Core 5.0 | 1,398.0 ns |  9.24 ns |  8.19 ns |  2.62 |    0.02 |    2 |

summary:

1. It will take 1000-3000 ns more to modify data by visitor. So you can take this way if you think it is acceptable in your case.

### validate vs FluentValidation

TODO

### mapper vs AutoMapper

TODO

## Packages

| package             | version                                                                                                                                                         | download                                                                                                                                                          | descrption                     |
| ------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------ |
| Newbe.ObjectVisitor | [![Newbe.ObjectVisitor.Version](https://img.shields.io/nuget/v/Newbe.ObjectVisitor.svg?style=flat-square)](https://www.nuget.org/packages/Newbe.ObjectVisitor/) | [![Newbe.ObjectVisitor.Download](https://img.shields.io/nuget/dt/Newbe.ObjectVisitor.svg?style=flat-square)](https://www.nuget.org/packages/Newbe.ObjectVisitor/) | Core about Newbe.ObjectVisitor |

## Contact

QQ Group: 【Newbe.Claptrap CL4P-TP 610394020 】：<https://jq.qq.com/?_wv=1027&k=Lkhbwj0o>

## Stargazers over time

[![Stargazers over time](https://starchart.cc/newbe36524/Newbe.ObjectVisitor.svg)](https://starchart.cc/newbe36524/Newbe.ObjectVisitor)

## Contributors ✨

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tr>
    <td align="center"><a href="https://www.newbe.pro"><img src="https://avatars1.githubusercontent.com/u/7685462?v=4" width="100px;" alt=""/><br /><sub><b>Newbe36524</b></sub></a><br /><a href="#blog-newbe36524" title="Blogposts">📝</a> <a href="https://github.com/newbe36524/Newbe.ObjectVisitor/commits?author=newbe36524" title="Code">💻</a> <a href="https://github.com/newbe36524/Newbe.ObjectVisitor/commits?author=newbe36524" title="Documentation">📖</a> <a href="#tutorial-newbe36524" title="Tutorials">✅</a></td>
    <td align="center"><a href="https://github.com/kotoneme"><img src="https://avatars3.githubusercontent.com/u/43395111?v=4" width="100px;" alt=""/><br /><sub><b>kotone</b></sub></a><br /><a href="#design-kotoneme" title="Design">🎨</a></td>
  </tr>
</table>

<!-- markdownlint-enable -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!