# Newbe.ObjectVisitor

- [简体中文](README_zh_Hans.md)
- [English](README.md)

<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->
[![All Contributors](https://img.shields.io/badge/all_contributors-3-orange.svg?style=flat-square)](#contributors-)
<!-- ALL-CONTRIBUTORS-BADGE:END -->

- [![build and scan](https://github.com/newbe36524/Newbe.ObjectVisitor/workflows/build%20and%20scan/badge.svg)](https://github.com/newbe36524/Newbe.ObjectVisitor/actions?query=workflow%3A%22build+and+scan%22)
- [![Codecov](https://img.shields.io/codecov/c/github/newbe36524/Newbe.ObjectVisitor)](https://codecov.io/gh/newbe36524/Newbe.ObjectVisitor)
- [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=newbe36524_Newbe.ObjectVisitor&metric=coverage)](https://sonarcloud.io/dashboard?id=newbe36524_Newbe.ObjectVisitor)
- [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=newbe36524_Newbe.ObjectVisitor&metric=alert_status)](https://sonarcloud.io/dashboard?id=newbe36524_Newbe.ObjectVisitor)
- [![Codacy Badge](https://api.codacy.com/project/badge/Grade/1fd0e7443364414ca0003dab27f9f9b8)](https://www.codacy.com/manual/472158246/Newbe.ObjectVisitor?utm_source=github.com&utm_medium=referral&utm_content=newbe36524/Newbe.ObjectVisitor&utm_campaign=Badge_Grade)
- xml document [![Crowdin](https://badges.crowdin.net/newbeobjectvisitor/localized.svg)](https://crowdin.com/project/newbeobjectvisitor)
- documents [![Crowdin](https://badges.crowdin.net/newbeobjectvisitordocs/localized.svg)](https://crowdin.com/project/newbeobjectvisitordocs)

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

Please check out the latest full API documents in site below:

- <http://ov.newbe.pro>
- <http://cn.ov.newbe.pro> more faster to China mainland
- <https://github.com/newbe36524/Newbe.ObjectVisitor.Docs>

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
    <td align="center"><a href="https://github.com/EventHorizon1024"><img src="https://avatars3.githubusercontent.com/u/21670962?v=4" width="100px;" alt=""/><br /><sub><b>黑洞视界</b></sub></a><br /><a href="#ideas-EventHorizon1024" title="Ideas, Planning, & Feedback">🤔</a></td>
  </tr>
</table>

<!-- markdownlint-enable -->
<!-- prettier-ignore-end -->
<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!
