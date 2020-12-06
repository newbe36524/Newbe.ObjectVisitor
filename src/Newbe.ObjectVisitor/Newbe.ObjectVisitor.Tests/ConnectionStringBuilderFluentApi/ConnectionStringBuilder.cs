using System;
using System.Text;

namespace Newbe.ObjectVisitor.Tests.ConnectionStringBuilderFluentApi
{
    public class ConnectionStringBuilder : Newbe.ObjectVisitor.IFluentApi
        , ConnectionStringBuilder.IConnectionStringBuilder_SetHost
        , ConnectionStringBuilder.IConnectionStringBuilder_UseUsernamePassword
        , ConnectionStringBuilder.IConnectionStringBuilder_UseWindowsAuthentication
    {
        private readonly ConnectionStringModel _context;

        public ConnectionStringBuilder(ConnectionStringModel context)
        {
            _context = context;
        }

        #region UserImpl

        private void Core_SetHost(string host)
        {
            _context.Host = host;
        }


        private void Core_UseUsernamePassword(string username, string password)
        {
            _context.Username = username;
            _context.Password = password;
        }


        private void Core_UseWindowsAuthentication()
        {
            _context.IsWindowsAuthentication = true;
        }


        private static readonly ICachedObjectVisitor<ConnectionStringModel, StringBuilder> Builder =
            default(ConnectionStringModel)!.V()
                .WithExtendObject<ConnectionStringModel, StringBuilder>()
                .ForEach((name, value, sb) => Append(name, value, sb))
                .Cache();

        private static void Append(string name, object o, StringBuilder value)
        {
            if (o != null)
            {
                value.Append($"{name}={o};");
            }
        }

        private string Core_Build()
        {
            var sb = new StringBuilder();
            Builder.Run(_context, sb);
            return sb.ToString();
        }

        #endregion

        #region AutoGenerate

        public IConnectionStringBuilder_SetHost SetHost(string host)
        {
            Core_SetHost(host);
            return (IConnectionStringBuilder_SetHost) this;
        }


        IConnectionStringBuilder_UseUsernamePassword IConnectionStringBuilder_SetHost.UseUsernamePassword(
            string username, string password)
        {
            Core_UseUsernamePassword(username, password);
            return (IConnectionStringBuilder_UseUsernamePassword) this;
        }


        IConnectionStringBuilder_UseWindowsAuthentication IConnectionStringBuilder_SetHost.UseWindowsAuthentication()
        {
            Core_UseWindowsAuthentication();
            return (IConnectionStringBuilder_UseWindowsAuthentication) this;
        }


        string IConnectionStringBuilder_UseUsernamePassword.Build()
        {
            return Core_Build();
        }


        string IConnectionStringBuilder_UseWindowsAuthentication.Build()
        {
            return Core_Build();
        }


        public interface IConnectionStringBuilder_SetHost
        {
            IConnectionStringBuilder_UseUsernamePassword UseUsernamePassword(string username, string password);


            IConnectionStringBuilder_UseWindowsAuthentication UseWindowsAuthentication();
        }


        public interface IConnectionStringBuilder_UseUsernamePassword
        {
            string Build();
        }


        public interface IConnectionStringBuilder_UseWindowsAuthentication
        {
            string Build();
        }

        #endregion
    }
}