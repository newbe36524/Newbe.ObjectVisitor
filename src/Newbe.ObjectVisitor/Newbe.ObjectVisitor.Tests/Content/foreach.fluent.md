```cs
#StateType : IOvBuilderContext<T>
#BuilderContextType : IOvBuilderContext<T>
#Namespace : Newbe.ObjectVisitor
#BuilderTypeName : ObjectVisitorBuilder<T>

_PropertyFilter : FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter)

_Foreach1 : Foreach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction)
_Foreach2 : Foreach(Expression<Action<string, object>> foreachAction)
_Foreach3 : Foreach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
_Foreach4 : Foreach<TValue>(Expression<Action<string, TValue>> foreachAction)

_ForeachCore1 : Foreach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction)
_ForeachCore2 : Foreach(Expression<Action<string, object>> foreachAction)
_ForeachCore3 : Foreach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
_ForeachCore4 : Foreach<TValue>(Expression<Action<string, TValue>> foreachAction)

```

```mermaid
stateDiagram
    [*] --> V : GetBuilder()
    V --> VP : _PropertyFilter
    VP --> V : _Foreach1 share _ForeachCore1
    VP --> V : _Foreach2 share _ForeachCore2
    VP --> V : _Foreach3 share _ForeachCore3
    VP --> V : _Foreach4 share _ForeachCore4
    V --> V : _Foreach1 share _ForeachCore1
    V --> V : _Foreach2 share _ForeachCore2
    V --> V : _Foreach3 share _ForeachCore3
    V --> V : _Foreach4 share _ForeachCore4
    V --> [*] : CreateVisitor() return IObjectVisitor<T>
    V --> [*] : WithExtend<TExtend>(TExtend extendObj = default) return IOvBuilderContext<T, TExtend>
```
