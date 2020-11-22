```cs

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

```

## Compare vs. FluentValidation

| icon | remark                                    |
| ---- | ----------------------------------------- |
| ✔️   | it is already avaliable in latest version |
| 💭   | it is still in consideration              |
| 🚧   | in plan or development                    |
| ❌   | It is not supported.                      |

| Feature                                                                                                                   | FluentValidation 9.X | Newbe.ObjectVistor |
| ------------------------------------------------------------------------------------------------------------------------- | -------------------- | ------------------ |
| [chaining-validators](https://docs.fluentvalidation.net/en/latest/start.html#chaining-validators)                         | ✔️                   | ✔️                 |
| [throwing-exceptions](https://docs.fluentvalidation.net/en/latest/start.html#throwing-exceptions)                         | ✔️                   | 🚧                 |
| [complex-properties](https://docs.fluentvalidation.net/en/latest/start.html#complex-properties)                           | ✔️                   | 🚧                 |
| [collections](https://docs.fluentvalidation.net/en/latest/collections.html)                                               | ✔️                   | 🚧                 |
| [inheritance](https://docs.fluentvalidation.net/en/latest/inheritance.html)                                               | ✔️                   | 🚧                 |
| [rulesets](https://docs.fluentvalidation.net/en/latest/rulesets.html)                                                     | ✔️                   | 🚧                 |
| [including-rules](https://docs.fluentvalidation.net/en/latest/including-rules.html)                                       | ✔️                   | 🚧                 |
| [overriding-the-message](https://docs.fluentvalidation.net/en/latest/configuring.html#overriding-the-message)             | ✔️                   | 🚧                 |
| [overriding-the-message](https://docs.fluentvalidation.net/en/latest/configuring.html#overriding-the-message)             | ✔️                   | 🚧                 |
| [overriding-the-property-name](https://docs.fluentvalidation.net/en/latest/configuring.html#overriding-the-property-name) | ✔️                   | 🚧                 |
| [conditions](https://docs.fluentvalidation.net/en/latest/conditions.html#conditions)                                      | ✔️                   | ✔️                 |
| [setting-the-cascade-mode](https://docs.fluentvalidation.net/en/latest/conditions.html#setting-the-cascade-mode)          | ✔️                   | 🚧                 |
| [dependent-rules](https://docs.fluentvalidation.net/en/latest/conditions.html#dependent-rules)                            | ✔️                   | 🚧                 |
| [setting-the-severity-level](https://docs.fluentvalidation.net/en/latest/severity.html#setting-the-severity-level)        | ✔️                   | 🚧                 |
| [custom-error-codes](https://docs.fluentvalidation.net/en/latest/error-codes.html#custom-error-codes)                     | ✔️                   | 🚧                 |
| [custom-state](https://docs.fluentvalidation.net/en/latest/custom-state.html#custom-state)                                | ✔️                   | 🚧                 |
| [built-in-validators](https://docs.fluentvalidation.net/en/latest/built-in-validators.html)                               | ✔️                   | ✔️                 |
| [custom-validators](https://docs.fluentvalidation.net/en/latest/custom-validators.html)                                   | ✔️                   | ✔️                 |
| [localization](https://docs.fluentvalidation.net/en/latest/localization.html#localization)                                | ✔️                   | 🚧                 |
| [test-extensions](https://docs.fluentvalidation.net/en/latest/testing.html#test-extensions)                               | ✔️                   | 💭                 |
| [aspnet core integration](https://docs.fluentvalidation.net/en/latest/aspnet.html)                                        | ✔️                   | 🚧                 |
| [aspnet mvc5 integration](https://docs.fluentvalidation.net/en/latest/mvc5.htmll)                                         | ✔️                   | 🚧                 |
| [aspnet webapi integration](https://docs.fluentvalidation.net/en/latest/webapi.html)                                      | ✔️                   | 🚧                 |
| [Blazor integration](https://docs.fluentvalidation.net/en/latest/webapi.html)                                             | ✔️                   | 🚧                 |
| [asynchronous-validation](https://docs.fluentvalidation.net/en/latest/async.html#asynchronous-validation)                 | ✔️                   | 🚧                 |
| [transform](https://docs.fluentvalidation.net/en/latest/transform.html)                                                   | ✔️                   | 💭                 |
| [callbacks](https://docs.fluentvalidation.net/en/latest/advanced.html#callbacks)                                          | ✔️                   | 💭                 |
| [prevalidate](https://docs.fluentvalidation.net/en/latest/advanced.html#prevalidate)                                      | ✔️                   | 💭                 |
| [root-context-data](https://docs.fluentvalidation.net/en/latest/advanced.html#root-context-data)                          | ✔️                   | 💭                 |

https://docs.fluentvalidation.net/en/latest/built-in-validators.html

all,number,string,enum,class,enumerable

| Build in Validators   | FluentValidation 9.X | Newbe.ObjectVistor                   |
| --------------------- | -------------------- | ------------------------------------ |
| NotNull               | ✔️                   | 🚧 NotNull; class                    |
| NotEmpty              | ✔️                   | 🚧 NotEmpty; string,class,enumerable |
| NotEqual              | ✔️                   | 🚧 NotEqual; all                     |
| Equal                 | ✔️                   | 🚧 Equal; all                        |
| Length                | ✔️                   | 🚧 Length; string,enumerable         |
| MaxLength             | ✔️                   | 🚧 MaxLength; string,enumerable      |
| MinLength             | ✔️                   | 🚧 MinLength; string,enumerable      |
| Less Than             | ✔️                   | ✔️ LessThan; number                  |
| Less Than Or Equal    | ✔️                   | ✔️ LessThanOrEqual; number           |
| Greater Than          | ✔️                   | ✔️ GreaterThan; number               |
| Greater Than Or Equal | ✔️                   | ✔️ GreaterThanOrEqual; number        |
| Predicate             | ✔️                   | ✔️ Validate; all                     |
| Regular Expression    | ✔️                   | 🚧 MatchRegex; string                |
| Email                 | ✔️                   | 🚧 Email; string                     |
| Credit Card           | ✔️                   | 💭                                   |
| Enum                  | ✔️                   | 🚧 IsInEnum; number,string,enum      |
| Enum Name             | ✔️                   | 🚧 IsEnumName; number,string,enum    |
| Empty                 | ✔️                   | 🚧 Empty; string,class,enumerable    |
| Null                  | ✔️                   | 🚧 Null; class                       |
| ExclusiveBetween      | ✔️                   | ✔️ IsInRange; number,enum            |
| InclusiveBetween      | ✔️                   | ✔️ IsInRange; number,enum            |
| ScalePrecision        | ✔️                   | 🚧 ScalePrecision; number            |
| Or                    | ❌                   | 🚧 Or; all                           |
| IsInSet               | ❌                   | ✔️ IsInSet; all                      |
