```cs
#StateType : List<int>
#BuilderContextType : List<int>
#Namespace : Newbe.ObjectVisitor.Tests.SumBuilderFluentApi
#BuilderTypeName : SumBuilder

```

```mermaid
stateDiagram
    [*]  --> AddNumber : AddNumber(int number)
    AddNumber  --> AddNumber : AddNumber(int number)
    AddNumber --> [*] : Sum() return int
```
