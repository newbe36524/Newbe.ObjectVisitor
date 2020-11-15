```cs
#StateType : ConnectionStringModel
#BuilderContextType : ConnectionStringModel
#Namespace : Newbe.ObjectVisitor.Tests.ConnectionStringBuilderFluentApi
#BuilderTypeName : ConnectionStringBuilder

```

```mermaid
stateDiagram
    [*]  --> SetHost : SetHost(string host)
    SetHost  --> UseUsernamePassword : UseUsernamePassword(string username, string password)
    SetHost  --> UseWindowsAuthentication : UseWindowsAuthentication()
    UseUsernamePassword --> [*] : Build() return string
    UseWindowsAuthentication --> [*] : Build() return string
```
