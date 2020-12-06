```cs
#BuilderContextType : IOvBuilderContext<T>
#Namespace : Newbe.ObjectVisitor
#BuilderTypeName : ObjectVisitorBuilder<T>

_PropertyFilter : FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter)

_ForEach1 : ForEach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction)
_ForEach2 : ForEach(Expression<Action<string, object>> foreachAction)
_ForEach3 : ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
_ForEach4 : ForEach<TValue>(Expression<Action<string, TValue>> foreachAction)

_ForEachCore1 : ForEach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction)
_ForEachCore2 : ForEach(Expression<Action<string, object>> foreachAction)
_ForEachCore3 : ForEach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
_ForEachCore4 : ForEach<TValue>(Expression<Action<string, TValue>> foreachAction)

```

```mermaid
stateDiagram
    [*] --> V : GetBuilder()
    V --> VP : _PropertyFilter
    VP --> V : _ForEach1 share _ForEachCore1
    VP --> V : _ForEach2 share _ForEachCore2
    VP --> V : _ForEach3 share _ForEachCore3
    VP --> V : _ForEach4 share _ForEachCore4
    V --> V : _ForEach1 share _ForEachCore1
    V --> V : _ForEach2 share _ForEachCore2
    V --> V : _ForEach3 share _ForEachCore3
    V --> V : _ForEach4 share _ForEachCore4
    V --> [*] : CreateVisitor() return IObjectVisitor<T>
    V --> [*] : GetContext() return IOvBuilderContext
```
