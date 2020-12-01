```cs
#BuilderContextType : List<ValidationRuleGroup<T>>
#Namespace : Newbe.ObjectVisitor.Validator
#BuilderTypeName : ValidationRuleGroupBuilder<T>

```

```mermaid
stateDiagram
    [*] --> S : GetBuilder(ValidationRuleRelation relation = ValidationRuleRelation.All)
    S --> S : Validate(Expression<Func<T,bool>> func)
    S --> S : ErrorMessage(Expression<Func<T,string>> func)
    S --> S : If(Expression<Func<T,bool>> func)
    S --> S : Next()

    S --> [*] : Build() return List<ValidationRuleGroup<T>>
```
