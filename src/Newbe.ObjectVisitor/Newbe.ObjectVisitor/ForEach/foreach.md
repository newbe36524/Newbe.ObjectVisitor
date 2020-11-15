```cs
#StateType : IOvBuilderContext<T>
#StartObjectType : IOvBuilderContext<T>
#Namespace : Newbe.ObjectVisitor
#StateChangerType : ObjectVistorBuilderStateChanger

_PropertyFilter : FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter)

_Foreach1 : ForeachExpression<Action<IObjectVisitorContext<T, object>>> foreachAction)
_Foreach2 : Foreach(Expression<Action<string, object>> foreachAction)
_Foreach3 : Foreach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
_Foreach4 : Foreach<TValue>(Expression<Action<string, TValue>> foreachAction)

_ForeachCore1 : ForeachExpression<Action<IObjectVisitorContext<T, object>>> foreachAction)
_ForeachCore2 : Foreach(Expression<Action<string, object>> foreachAction)
_ForeachCore3 : Foreach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
_ForeachCore4 : Foreach<TValue>(Expression<Action<string, TValue>> foreachAction)

```

```mermaid
stateDiagram
    [*] --> V_T_ : GetBuilder()
    V_T_ --> VP_T_ : _PropertyFilter
    VP_T_ --> V_T_ : _Foreach1 share _ForeachCore1
    VP_T_ --> V_T_ : _Foreach2 share _ForeachCore2
    VP_T_ --> V_T_ : _Foreach3 share _ForeachCore3
    VP_T_ --> V_T_ : _Foreach4 share _ForeachCore4
    V_T_ --> V_T_ : _Foreach1 share _ForeachCore1
    V_T_ --> V_T_ : _Foreach2 share _ForeachCore2
    V_T_ --> V_T_ : _Foreach3 share _ForeachCore3
    V_T_ --> V_T_ : _Foreach4 share _ForeachCore4
    V_T_ --> [*] : CreateVisitor() return IObjectVisitor<T>
    V_T_ --> [*] : WithExtend<TExtend>(TExtend extendObj = default) return IOvBuilderContext<T, TExtend>
```
