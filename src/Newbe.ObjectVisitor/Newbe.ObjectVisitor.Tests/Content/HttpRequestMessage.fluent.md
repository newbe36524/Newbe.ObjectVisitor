```cs
#StateType : HttpRequestMessage
#BuilderContextType : HttpRequestMessage
#Namespace : Newbe.ObjectVisitor.Tests.HttpClientFluentApi
#BuilderTypeName : RequestBuilder

_SetUriCore : SetUri(Uri uri)
_SetContentCore : SetUri(Uri uri)
_SetHeadersCore : SetHeaders(HttpRequestHeaders headers)

_SetHeaders : SetHeaders(HttpRequestHeaders headers)
_SetContent : SetContent(HttpContent content)
_Build : Build()

```

```mermaid
stateDiagram
    [*]  --> Get : Get()
    Get --> GetUri : SetUri(Uri uri) share _SetUriCore

    [*]  --> Delete : Delete()
    Delete --> DeleteUri : SetUri(Uri uri) share _SetUriCore

    [*]  --> Post : Post()
    Post --> PostUri : SetUri(Uri uri) share _SetUriCore
    PostUri --> SetContent : _SetContent share _SetContentCore

    [*]  --> Put : Put()
    Put --> PutUri : SetUri(Uri uri) share _SetUriCore
    PutUri --> SetContent : _SetContent share _SetContentCore

    SetContent --> [*] : _Build return HttpRequestMessage
    GetUri --> [*] : _Build return HttpRequestMessage
    DeleteUri --> [*] : _Build return HttpRequestMessage
```
