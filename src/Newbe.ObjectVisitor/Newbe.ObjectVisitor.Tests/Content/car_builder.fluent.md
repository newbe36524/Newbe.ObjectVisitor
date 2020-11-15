```cs
#StateType : Car
#BuilderContextType : Car
#Namespace : Newbe.ObjectVisitor.Tests.CarBuilder
#BuilderTypeName : CarBuilder

AddWheel : AddWheel(int size)
AddEngine : AddEngine(string engine)

```

```mermaid
stateDiagram
    [*]  --> W1 : AddWheel(int size) share AddWheel
    W1 --> W2 : AddWheel(int size) share AddWheel
    W2 --> W3 : AddWheel(int size) share AddWheel
    W3 --> W4 : AddWheel(int size) share AddWheel
    
    [*] --> E : AddEngine(string engine) share AddEngine
    E --> WE1 : AddWheel(int size) share AddWheel
    WE1 --> WE2 : AddWheel(int size) share AddWheel
    WE2 --> WE3 : AddWheel(int size) share AddWheel
    WE3 --> WE4 : AddWheel(int size) share AddWheel
    
    W1 --> WE1 : AddEngine(string engine) share AddEngine
    W2 --> WE2 : AddEngine(string engine) share AddEngine
    W3 --> WE3 : AddEngine(string engine) share AddEngine
    W4 --> WE4 : AddEngine(string engine) share AddEngine
    
    WE4 --> [*] : Build() return Car
```
