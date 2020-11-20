# Newbe.ObjectVisitor

- [简体中文](README_zh_Hans.md)
- [English](README.md)

![Banner](https://github.com/newbe36524/Newbe.ObjectVisitor/raw/docs/assets/banner.svg)

Newbe.ObjectVisitor 帮助开发者可以用最简单的最高效的方式访问一个普通 class 的所有属性。从而实现：验证、映射、收集等等操作。

例如, 在你的代码中有这样一个简单的类。

```cs
var order = new OrderInfo();
```

你想要将这个类所有的属性和值都打印出来，那么你可以采用反射来完成：

```cs
for(var pInfo in typeof(OrderInfo).GetProperties())
{
    Console.Writeline($"{pInfo.Name}: {pInfo.GetValue(order)}");
}
```

如果你使用这个类库，则可以采用以下方式来实现一样的效果：

```cs
// 调用扩展方法 .V
// 就可以得到一个针对 Order 类型的 visitor
var visitor = order.V();

visitor.ForEach(context=>{
    var name = context.Name;
    var value = context.Value;
    Console.Writeline($"{name}: {value}");
}).Run();

// 也可以把代码都写在一行
order.V().ForEach(c=> Console.Writeline($"{c.Name}: {c.Value}")).Run();

// 或者也可以调用这个较短的方法
order.FormatToString();
```

## 那我为什么要这样做?

- **因为这样更快！** 这个类库使用[表达式树](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/expression-trees/)实现，因此它拥有比直接使用反射快上 10 倍的性能.
- **因为这样更可读！** 通过这个类库你可以使用链式 API 和命名方法来创建一个委托，这样可以让你的代码实现和硬编码同样的可读效果。
- **因为这样更具扩展性！** 如果使用了这个类库，你就拥有了一个简便的方法来访问一个类所有的属性。因此，你就做很多你想做的事情，比如：创建一个验证器来验证你的模型，修改一些可能包含敏感数据的属性从而避免输出到日志中，创建一个类似于 AutoMapper 的对象映射器但是拥有更好的性能，诸如此类。

## API

| 图标 | 说明                                             |
| ---- | ------------------------------------------------ |
| ✔️   | 在最新的版本中已经可用                           |
| 🚧   | 仍然在计划或者开发中，未来可能可用、修改或者移除 |
| ❌   | 已经在最新的版本中被移除                         |

```cs
var o = new Yueluo();

using Newbe.ObjectVisitor;

//✔️ from 0.1
// V 是一个扩展方法
var visitor = o.V();

//✔️ from 0.1
// 使用工厂来创建
var visitor = typeof(Yueluo).V();

//✔️ from 0.1
// 创建并立即执行
// 这是使用本库最简单的一种形式
// 在 context 中包含了 Name, Value, PropertyInfo, SourceObj, SourceObjType 等属性
o.V().ForEach((context)=>{}).Run();
o.V().ForEach((name,value)=>{}).Run();

// ✔️ from 0.2
// 多重 foreach
o.V().ForEach((context)=>{}).ForEach((context)=>{}).Run();

//✔️ from 0.1
// 创建一个支持扩展多一个参数的 visitor
o.V().WithExtendObject<Yueluo, StringBuilder>()
    .ForEach((context)=>{var _ = context.ExtendObject})
    .Run(new StringBuilder());
o.V().WithExtendObject<Yueluo, StringBuilder>()
    .ForEach((name,value,stringBuilder)=>{})
    .Run(new StringBuilder());

//✔️ from 0.1
// 创建并缓存 visitor 。这是一种被建议使用的方案
// 缓存一个 visitor 对对象，然后再执行它，这样可以反复使用被缓存的 visitor ，性能更强。
var cachedVisitor = deafult(Yueluo).V().ForEach((context)=>{}).Cache();
cachedVisitor.Run(new Yueluo());

//✔️ from 0.1
// 缓存一个带有一个扩展参数的 visitor
var cachedVisitor = deafult(Yueluo).V()
    .WithExtendObject<Yueluo, StringBuilder>()
    .ForEach((context)=>{var _ = context.ExtendObject})
    .Cache();
cachedVisitor.Run(new Yueluo(), new StringBuilder());


//✔️ from 0.2
// 可以修改属性
o.V().ForEach((context) => ModifyData（context）).Run();

public static void ModifyData(IObjectVisitorContext<Yueluo,string> context)
{
    context.Value = context.Value.SubString(0,1)；
}

//✔️ from 0.1
// 可以读取当前表达式相关的调试信息
var debugInfo = o.V().ForEach((context)=>{}).GetDebugInfo();

//🚧 生成 C# 代码
var code = o.V().ForEach((context)=>{}).GenerateCode();

//✔️ from 0.1
// 生成一个 lambda 函数
var func = o.V().ForEach((context)=>{}).GetLambda();


//✔️ from 0.2
// 遍历指定类型的属性
o.V().ForEach<Yueluo, string>((context) => {});
// 和上一条完全一样
o.V().ForEach<Yueluo, string>((context) => {}, x => x.PropertyType == typeof(string));
// 遍历被标记了 RequiredAttribute 的 string 属性
o.V().ForEach<Yueluo, string>((context) => {}, x => x.PropertyType == typeof(string) && x.GetCustomAttribute<RequiredAttribute>());
// 遍历“是”或者实现了 IEnumerable<int> 接口的属性, 例如 List<int>, int[], IEnumerable<int>, HashSet<int> 等等。
o.V().ForEach<Yueluo, IEnumerable<int>>((context) => {}, x => x.IsOrImplOf<IEnumerable<int>>());
// 指定属性类型，并包含一个扩展的参数
o.V().WithExtendObject<Yueluo, StringBuilder>().ForEach<Yueluo, StringBuilder, string>((context) => {});

//🚧  使用 linq 过滤
o.V().AsEnumerable().Where((context)=>context.Name == "YueLuo").ForEach((context)=>{}).Run();

//🚧  不处理子集对象
o.V().SuppendSubObject().ForEach((context)=>{}).Run();

//🚧  不对集合内的元素进行处理
o.V().SuppendEnumerable().ForEach((context)=>{}).Run();


/**
 ✔️ from 0.1
 将所有的属性和值拼接为一个字符串
*/
var sb = new StringBuilder();
o.V()
.ForEach((context)=>s.AppendFormat("{0}:{1}{2}", name, value,Environment.NewLine))
.Run();
var s = sb.ToString();

//✔️ from 0.1
// 上面代码的一种简短形式
var s = o.FormatString();

//🚧 和 C# 7 中一样的类型解构方案，但是根据扩展性
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

// 集合操作相关的名称空间
using Newbe.ObjectVisitor.Collections;

/**
 🚧 将属性和值收集为一个字典
*/

var dic1 = o.V().CollectAsDictionary().Run();
// quick style for above
var dic1 = o.V().ToDictionary();

/**
 🚧 将字典中的值赋值到对象上
*/
o.V().ApplyFromDictionary(dic).Run();
// quick style for above
o.V().FromDictionary(dic);


// 验证器相关的名称空间
using Newbe.ObjectVisitor.Validation;

// 🚧 创建一个验证器规则
var rule = ValidateRule<Yueluo>
    .GetBuilder()
    .Property(x=>x.Name).Required().Length(2,10)
    .Property(x=>x.Age).Range(0, int.MaxValue)
    .Property(x=>x.Password).Validate(value=>ValidatePassword(value))
    .Property(x=>x.Level).Validate(value=>value + 1 >= 0)
    .Build();

o.V().Validate(rule).Run();
o.Validate(rule);


// 🚧 使用连贯API进行验证
// 此时，标签验证仍然是默认生效的
o.V().Validate(v=>
    v
     .Property(x=>x.Name).Required().Length(2,10)
     .Property(x=>x.Age).Range(0, int.MaxValue)
     .Property(x=>x.Password).Validate(value=>ValidatePassword(value))
     .Property(x=>x.Level).Validate(value=>value + 1 >= 0)
).Run();

// 🚧 移除标签验证规则
o.V().SuppendAttributeValidation()
    .Validate(v=>
        v
        .Property(x=>x.Name).Required().Length(2,10)
        .Property(x=>x.Age).Range(0, int.MaxValue)
        .Property(x=>x.Password).Validate(value=>ValidatePassword(value))
        .Property(x=>x.Level).Validate(value=>value + 1 >= 0)
).Run();

// 🚧 移除子对象的验证
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

// Task相关的名称空间
using Newbe.ObjectVisitor.Task;

// 🚧 async
await o.V().ForEachAsync((context)=>{}).RunAsync();

// 🚧 控制多 Task 的执行方式
await o.V().ForEachAsync((context)=>{}).WhenAsync(tasks=>Task.WhenAll(tasks)).RunAsync();

// namespace for Microsoft.Extensions.DependencyInjection
using Newbe.ObjectVistory.DepencyInjection;

// 🚧 实现属性注入
this.V().ForEach(context=>this.ServiceProvider.GetService(context.PropertyInfo.PorpertyType)).Run();

// 🚧 和上面代码相同的简短形式
this.V().PropertyInject(this.ServiceProvider);

// ✔️ from 0.3
// 通过 Fluent API DSL 生成一个 fluent api 代码
var content = File.ReadAllText("SumBuilder.fluent.md");
var parser = new FluentApiDesignParser();
var re = parser.Parse(content);
var generator = new FluentApiFileGenerator();
var output = generator.Generate(re);
File.WriteAllText("SumBuilder.cs", output.FluentApiFiles.AutoGenerate);

```

## 基准测试

以下基准测试所使用的物理机配置：

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

我们将会把属性的名称和值拼接为一个字符串，采用以下这些方案：

| 方法         | 描述                                                                                        |
| ------------ | ------------------------------------------------------------------------------------------- |
| Directly     | 使用 StringBuilder 硬编码进行拼接                                                           |
| CacheVisitor | 使用 Newbe.ObjectVisitor 创建一个 ObjectVisitor 并缓存它，然后使用缓存后的 visitor 进行拼接 |
| QuickStyle   | 使用 Newbe.ObjectVisitor 中内置写好的方法                                                   |

图表:

![Newbe.ObjectVisitor.BenchmarkTest.FormatStringTest](https://github.com/newbe36524/Newbe.ObjectVisitor/raw/docs/assets/Newbe.ObjectVisitor.BenchmarkTest.FormatStringTest-barplot.png)

数据:

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

结论:

1. 使用 Newbe.ObjectVisitor, 仅仅只用非常少的额外时间消耗就得到了和硬编码完全一样的效果。
2. 使用 Newbe.ObjectVisitor 内置的方法，仅仅需要消耗非常少的额外时间就可以省去自己构建 visitor 的时间。是一种值得参考的编写方式。

现在我们对比一下缓存和不缓存 visitor 的区别:

| 方法            | 描述           |
| --------------- | -------------- |
| CacheVisitor    | 缓存 Visitor   |
| NoCacheVisitor  | 不缓存 Visitor |
| ReflectProperty | 使用反射来实现 |

图表:

![Newbe.ObjectVisitor.BenchmarkTest.CacheVisitorTest](https://github.com/newbe36524/Newbe.ObjectVisitor/raw/docs/assets/Newbe.ObjectVisitor.BenchmarkTest.CacheVisitorTest-barplot.png)

数据：

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

结论:

1. 构建一个 ObjectVisitor 需要花费一些时间，因为其中需要构建一些对象并且需要反射。所以我们建议将 ObjectVisitor 缓存起来使用。当然，在一些性能不敏感的场景，不缓存也无所谓，毕竟这个构建过程小于一毫秒。
2. 缓存的 ObjectVisitor 比起反射要快多了。

### 修改对象的数据

现在，你可能需要将一个对象中的 Password 属性值替换为'\*\*\*'。我们可以采用以下方案实现：

| 方法         | 描述                              |
| ------------ | --------------------------------- |
| Directly     | 直接使用赋值语句进行修改          |
| UsingVisitor | 使用缓存的 ObjectVisitor 进行修改 |

图表:

![Newbe.ObjectVisitor.BenchmarkTest.ChangePasswordTest-barplot](https://github.com/newbe36524/Newbe.ObjectVisitor/raw/docs/assets/Newbe.ObjectVisitor.BenchmarkTest.ChangePasswordTest-barplot.png)

数据:

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

结论:

1. 使用 visitor 会额外消耗 1-3 微秒（百万分之一秒）。所以如果你觉得这点时间可以接受，那就尽管使用。

### validate vs FluentValidation

TODO

### mapper vs AutoMapper

TODO

## Packages

| package             | version                                                                                                                                                         | download                                                                                                                                                          | descrption                     |
| ------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------ |
| Newbe.ObjectVisitor | [![Newbe.ObjectVisitor.Version](https://img.shields.io/nuget/v/Newbe.ObjectVisitor.svg?style=flat-square)](https://www.nuget.org/packages/Newbe.ObjectVisitor/) | [![Newbe.ObjectVisitor.Download](https://img.shields.io/nuget/dt/Newbe.ObjectVisitor.svg?style=flat-square)](https://www.nuget.org/packages/Newbe.ObjectVisitor/) | Core about Newbe.ObjectVisitor |

## 联系方式

QQ 群: 【Newbe.Claptrap CL4P-TP 610394020 】：<https://jq.qq.com/?_wv=1027&k=Lkhbwj0o>

## Stargazers over time

[![Stargazers over time](https://starchart.cc/newbe36524/Newbe.ObjectVisitor.svg)](https://starchart.cc/newbe36524/Newbe.ObjectVisitor)
