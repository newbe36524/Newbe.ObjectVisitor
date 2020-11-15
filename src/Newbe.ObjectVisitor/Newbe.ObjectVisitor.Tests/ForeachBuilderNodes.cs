//
// using System;
// namespace Newbe.ObjectVisitor
// {
//     public class ObjectVisitorBuilder<T>:Newbe.ObjectVisitor.IFluentApi
//         ,ObjectVisitorBuilder<T>.IObjectVisitorBuilder_V
// ,ObjectVisitorBuilder<T>.IObjectVisitorBuilder_VP
//     {
//         private readonly IOvBuilderContext<T> _context;
//         public ObjectVisitorBuilder<T>(IOvBuilderContext<T> context)
//         {
//             _context = context;
//         }
//
//         #region UserImpl
//         
// private void Shared_Foreach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction)
// {
//     throw new NotImplementedException();
// }
//
//
// private void Shared_Foreach(Expression<Action<string, object>> foreachAction)
// {
//     throw new NotImplementedException();
// }
//
//
// private void Shared_Foreach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
// {
//     throw new NotImplementedException();
// }
//
//
// private void Shared_Foreach<TValue>(Expression<Action<string, TValue>> foreachAction)
// {
//     throw new NotImplementedException();
// }
//
//
// private void Core_GetBuilder()
// {
//     throw new NotImplementedException();
// }
//
//
// private void Core_FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter)
// {
//     throw new NotImplementedException();
// }
//
//
// private IObjectVisitor<T> Core_CreateVisitor()
// {
//     throw new NotImplementedException();
// }
//
//
// private IOvBuilderContext<T, TExtend> Core_WithExtend<TExtend>(TExtend extendObj = default)
// {
//     throw new NotImplementedException();
// }
//
//         #endregion
//
//         #region AutoGenerate
//         
// public IObjectVisitorBuilder_V GetBuilder()
// {
//     
//     Core_GetBuilder();
//     return (IObjectVisitorBuilder_V)this;
//
// }
//
//
// IObjectVisitorBuilder_VP IObjectVisitorBuilder_V.FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter)
// {
//     
//     Core_FilterProperty( propertyInfoFilter);
//     return (IObjectVisitorBuilder_VP)this;
//
// }
//
//
// IObjectVisitorBuilder_V IObjectVisitorBuilder_VP.Foreach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction)
// {
//     
//     Shared_Foreach( foreachAction);
//     return (IObjectVisitorBuilder_V)this;
//
// }
//
//
// IObjectVisitorBuilder_V IObjectVisitorBuilder_VP.Foreach(Expression<Action<string, object>> foreachAction)
// {
//     
//     Shared_Foreach( foreachAction);
//     return (IObjectVisitorBuilder_V)this;
//
// }
//
//
// IObjectVisitorBuilder_V IObjectVisitorBuilder_VP.Foreach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
// {
//     
//     Shared_Foreach<TValue>( foreachAction);
//     return (IObjectVisitorBuilder_V)this;
//
// }
//
//
// IObjectVisitorBuilder_V IObjectVisitorBuilder_VP.Foreach<TValue>(Expression<Action<string, TValue>> foreachAction)
// {
//     
//     Shared_Foreach<TValue>( foreachAction);
//     return (IObjectVisitorBuilder_V)this;
//
// }
//
//
// IObjectVisitorBuilder_V IObjectVisitorBuilder_V.Foreach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction)
// {
//     
//     Shared_Foreach( foreachAction);
//     return (IObjectVisitorBuilder_V)this;
//
// }
//
//
// IObjectVisitorBuilder_V IObjectVisitorBuilder_V.Foreach(Expression<Action<string, object>> foreachAction)
// {
//     
//     Shared_Foreach( foreachAction);
//     return (IObjectVisitorBuilder_V)this;
//
// }
//
//
// IObjectVisitorBuilder_V IObjectVisitorBuilder_V.Foreach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction)
// {
//     
//     Shared_Foreach<TValue>( foreachAction);
//     return (IObjectVisitorBuilder_V)this;
//
// }
//
//
// IObjectVisitorBuilder_V IObjectVisitorBuilder_V.Foreach<TValue>(Expression<Action<string, TValue>> foreachAction)
// {
//     
//     Shared_Foreach<TValue>( foreachAction);
//     return (IObjectVisitorBuilder_V)this;
//
// }
//
//
// IObjectVisitor<T> IObjectVisitorBuilder_V.CreateVisitor()
// {
//     
//     return Core_CreateVisitor();
//
// }
//
//
// IOvBuilderContext<T, TExtend> IObjectVisitorBuilder_V.WithExtend<TExtend>(TExtend extendObj = default)
// {
//     
//     return Core_WithExtend<TExtend>( extendObj);
//
// }
//
//
// public interface IObjectVisitorBuilder_V
// {
//     
// IObjectVisitorBuilder_V Foreach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction);
//
//
// IObjectVisitorBuilder_V Foreach(Expression<Action<string, object>> foreachAction);
//
//
// IObjectVisitorBuilder_V Foreach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction);
//
//
// IObjectVisitorBuilder_V Foreach<TValue>(Expression<Action<string, TValue>> foreachAction);
//
//
// IObjectVisitorBuilder_VP FilterProperty(Func<PropertyInfo, bool>? propertyInfoFilter);
//
//
// IObjectVisitor<T> CreateVisitor();
//
//
// IOvBuilderContext<T, TExtend> WithExtend<TExtend>(TExtend extendObj = default);
//
// }
//
//
// public interface IObjectVisitorBuilder_VP
// {
//     
// IObjectVisitorBuilder_V Foreach(Expression<Action<IObjectVisitorContext<T, object>>> foreachAction);
//
//
// IObjectVisitorBuilder_V Foreach(Expression<Action<string, object>> foreachAction);
//
//
// IObjectVisitorBuilder_V Foreach<TValue>(Expression<Action<IObjectVisitorContext<T, TValue>>> foreachAction);
//
//
// IObjectVisitorBuilder_V Foreach<TValue>(Expression<Action<string, TValue>> foreachAction);
//
// }
//
//         #endregion
//     }
// }
