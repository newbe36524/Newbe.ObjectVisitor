# Newbe.ObjectVisitor

You can visit all properties about your class or struct by this lib with high performance as you visit properties in hard coding way.

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

// you can also make it in one line
order.V().ForEach(c=> Console.Writeline($"{c.Name}: {c.Value}")).Run();
```

"Why I need this?"

- It is faster. This lib impletment with [Expression Trees](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/expression-trees/) that cost 1/10 time as in Reflection way.
- It is readable. This lib can generate a lambda func to handle the code flow you create that just as your hard coding without reflection.
- It is extendable. If you can visit all properties of a object in easy way, you can validate them as you wish, mask some value if there are something sensitive,

## Status

Planning. This project is in planning. All you can see below is in the plan.

## API (plan)

```cs
var o = new Yueluo();

using Newbe.ObjectVisitor;

// V is a static extension method
var visitor = o.V();

// create visitor from factory method
var visitor = ObjectVisitorFactory.Create(typeof(Yueluo));

// this is the most simple structure about this lib
// there are Name, Value, PropertyInfo, SourceObj, SourceObjType and etc in the context
o.V().ForEach((context)=>{}).Run();

// you can modify value if return a new value
o.V().ForEach((context)=>context.Value.SubString(0,1)).Run();

// get debug info about expression now
var debugInfo = o.V().ForEach((context)=>{}).GetDebugInfo();

// generate code in C# as a string about expression now
var code = o.V().ForEach((context)=>{}).GenerateCode();

// generate a lambda func
var func = o.V().ForEach((context)=>{}).GetLambda();

// foreach properties with specified type
o.V().ForEach<string>((context)=>{}).Run();

// using linq to filter
o.V().AsEnumerable().Where((context)=>context.Name == "YueLuo").ForEach((context)=>{}).Run();

// suppending visiting sub object
o.V().SuppendSubObject().ForEach((context)=>{}).Run();

// suppending visiting enumerable object
o.V().SuppendEnumerable().ForEach((context)=>{}).Run();


/**
 sample to join all properties to string
*/
var sb = new StringBuilder();
o.V().ForEach((context)=>{
    sb.Append(context.Name);
    sb.Append(context.Value);
    sb.Append(Enviroment.Newline);
}).Run();
var s = sb.ToString();

// quick style for above
var s = o.V().FormatString();

// Deconstruct as C# 7 but more flexible
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
 collect properties into a dictionary
*/

var dic1 = o.V().CollectAsDictionary().Run();
// quick style for above
var dic1 = o.V().ToDictionary();

/**
 apply value from a dictionary to object
*/
o.V().ApplyFromDictionary(dic).Run();
// quick style for above
o.V().FromDictionary(dic);


// namespace for data validation
using Newbe.ObjectVisitor.Validation;

// create rule to validation
var rule = ValidateRule<Yueluo>
    .GetBuilder()
    .Property(x=>x.Name).Required().Length(2,10)
    .Property(x=>x.Age).Range(0, int.MaxValue)
    .Property(x=>x.Password).Validate(value=>ValidatePassword(value))
    .Property(x=>x.Level).Validate(value=>value + 1 >= 0)
    .Build();

o.V().Validate(rule).Run();
o.Validate(rule);


// validate data in flunet api
// attribute-based enabled by default
o.V().Validate(v=>
    v
     .Property(x=>x.Name).Required().Length(2,10)
     .Property(x=>x.Age).Range(0, int.MaxValue)
     .Property(x=>x.Password).Validate(value=>ValidatePassword(value))
     .Property(x=>x.Level).Validate(value=>value + 1 >= 0)
).Run();

// suppending attribute-based validation
o.V().SuppendAttributeValidation()
    .Validate(v=>
        v
        .Property(x=>x.Name).Required().Length(2,10)
        .Property(x=>x.Age).Range(0, int.MaxValue)
        .Property(x=>x.Password).Validate(value=>ValidatePassword(value))
        .Property(x=>x.Level).Validate(value=>value + 1 >= 0)
).Run();

// suppending sub-object validation
// validate whole object
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

// async way
await o.V().ForEachAsync((context)=>{}).RunAsync();

// controlling concurrency
await o.V().ForEachAsync((context)=>{}).WhenAsync(tasks=>Task.WhenAll(tasks)).RunAsync();

// namespace for Microsoft.Extensions.DependencyInjection
using Newbe.ObjectVistory.DepencyInjection;

// inject services to the properties of this object
this.V().ForEach(context=>this.ServiceProvider.GetService(context.PropertyInfo.PorpertyType)).Run();

// quick style for above
this.V().PropertyInject(this.ServiceProvider);

```

## Benchmark

TODO

visit properties vs Reflection vs Directly

validate vs FluentValidation

mapper vs AutoMapper

## Stargazers over time

[![Stargazers over time](https://starchart.cc/newbe36524/Newbe.ObjectVisitor.svg)](https://starchart.cc/newbe36524/Newbe.ObjectVisitor)