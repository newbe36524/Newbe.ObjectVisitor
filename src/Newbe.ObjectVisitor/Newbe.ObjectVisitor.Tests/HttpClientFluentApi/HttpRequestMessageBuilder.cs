using System;
using System.Net.Http;

namespace Newbe.ObjectVisitor.Tests.HttpClientFluentApi
{
    public class RequestBuilder : Newbe.ObjectVisitor.IFluentApi
        , RequestBuilder.IRequestBuilder_Get
        , RequestBuilder.IRequestBuilder_Delete
        , RequestBuilder.IRequestBuilder_Post
        , RequestBuilder.IRequestBuilder_PostUri
        , RequestBuilder.IRequestBuilder_Put
        , RequestBuilder.IRequestBuilder_PutUri
        , RequestBuilder.IRequestBuilder_SetContent
        , RequestBuilder.IRequestBuilder_GetUri
        , RequestBuilder.IRequestBuilder_DeleteUri
    {
        private readonly HttpRequestMessage _context;

        public RequestBuilder(HttpRequestMessage context)
        {
            _context = context;
        }

        #region UserImpl

        private void Shared_SetUri(Uri uri)
        {
            _context.RequestUri = uri;
        }

        private void Shared_SetContent(HttpContent content)
        {
            _context.Content = content;
        }


        private void Core_Get()
        {
            _context.Method = HttpMethod.Get;
        }


        private void Core_Delete()
        {
            _context.Method = HttpMethod.Delete;
        }


        private void Core_Post()
        {
            _context.Method = HttpMethod.Post;
        }


        private void Core_Put()
        {
            _context.Method = HttpMethod.Put;
        }


        private HttpRequestMessage Core_Build()
        {
            return _context;
        }


        #endregion

        #region AutoGenerate

        public IRequestBuilder_Get Get()
        {
            Core_Get();
            return (IRequestBuilder_Get) this;
        }


        IRequestBuilder_GetUri IRequestBuilder_Get.SetUri(Uri uri)
        {
            Shared_SetUri(uri);
            return (IRequestBuilder_GetUri) this;
        }


        public IRequestBuilder_Delete Delete()
        {
            Core_Delete();
            return (IRequestBuilder_Delete) this;
        }


        IRequestBuilder_DeleteUri IRequestBuilder_Delete.SetUri(Uri uri)
        {
            Shared_SetUri(uri);
            return (IRequestBuilder_DeleteUri) this;
        }


        public IRequestBuilder_Post Post()
        {
            Core_Post();
            return (IRequestBuilder_Post) this;
        }


        IRequestBuilder_PostUri IRequestBuilder_Post.SetUri(Uri uri)
        {
            Shared_SetUri(uri);
            return (IRequestBuilder_PostUri) this;
        }


        IRequestBuilder_SetContent IRequestBuilder_PostUri.SetContent(HttpContent content)
        {
            Shared_SetContent(content);
            return (IRequestBuilder_SetContent) this;
        }


        public IRequestBuilder_Put Put()
        {
            Core_Put();
            return (IRequestBuilder_Put) this;
        }


        IRequestBuilder_PutUri IRequestBuilder_Put.SetUri(Uri uri)
        {
            Shared_SetUri(uri);
            return (IRequestBuilder_PutUri) this;
        }


        IRequestBuilder_SetContent IRequestBuilder_PutUri.SetContent(HttpContent content)
        {
            Shared_SetContent(content);
            return (IRequestBuilder_SetContent) this;
        }


        HttpRequestMessage IRequestBuilder_SetContent.Build()
        {
            return Core_Build();
        }


        HttpRequestMessage IRequestBuilder_GetUri.Build()
        {
            return Core_Build();
        }


        HttpRequestMessage IRequestBuilder_DeleteUri.Build()
        {
            return Core_Build();
        }


        public interface IRequestBuilder_Get
        {
            IRequestBuilder_GetUri SetUri(Uri uri);
        }


        public interface IRequestBuilder_Delete
        {
            IRequestBuilder_DeleteUri SetUri(Uri uri);
        }


        public interface IRequestBuilder_Post
        {
            IRequestBuilder_PostUri SetUri(Uri uri);
        }


        public interface IRequestBuilder_PostUri
        {
            IRequestBuilder_SetContent SetContent(HttpContent content);
        }


        public interface IRequestBuilder_Put
        {
            IRequestBuilder_PutUri SetUri(Uri uri);
        }


        public interface IRequestBuilder_PutUri
        {
            IRequestBuilder_SetContent SetContent(HttpContent content);
        }


        public interface IRequestBuilder_SetContent
        {
            HttpRequestMessage Build();
        }


        public interface IRequestBuilder_GetUri
        {
            HttpRequestMessage Build();
        }


        public interface IRequestBuilder_DeleteUri
        {
            HttpRequestMessage Build();
        }

        #endregion
    }
}