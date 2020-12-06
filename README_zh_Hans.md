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

请查看以下网址中关于本库的帮助文档:

- <http://ov.newbe.pro>
- <http://cn.ov.newbe.pro> 更适合中国大陆访问
- <https://github.com/newbe36524/Newbe.ObjectVisitor.Docs>

## Packages

| package             | version                                                                                                                                                         | download                                                                                                                                                          | descrption                     |
| ------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------ |
| Newbe.ObjectVisitor | [![Newbe.ObjectVisitor.Version](https://img.shields.io/nuget/v/Newbe.ObjectVisitor.svg?style=flat-square)](https://www.nuget.org/packages/Newbe.ObjectVisitor/) | [![Newbe.ObjectVisitor.Download](https://img.shields.io/nuget/dt/Newbe.ObjectVisitor.svg?style=flat-square)](https://www.nuget.org/packages/Newbe.ObjectVisitor/) | Core about Newbe.ObjectVisitor |

## 联系方式

QQ 群: 【Newbe.Claptrap CL4P-TP 610394020 】：<https://jq.qq.com/?_wv=1027&k=Lkhbwj0o>

## Stargazers over time

[![Stargazers over time](https://starchart.cc/newbe36524/Newbe.ObjectVisitor.svg)](https://starchart.cc/newbe36524/Newbe.ObjectVisitor)
