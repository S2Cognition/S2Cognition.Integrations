using Dropbox.Api.Stone;
using System.Text;

namespace S2Cognition.Integrations.Dropbox.Core.Tests;

public class GetFileTests : IntegrationsTests
{
    [Fact]
    public async Task EnsureGetFileChecksThatAccessCodeIsNotNull()
    {
        _configuration.AccessToken = null;
        await _sut.Initialize(_configuration);

        var ex = await Should.ThrowAsync<InvalidOperationException>(async () => await _sut.GetFile(new GetFileRequest()));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(nameof(DropboxConfiguration.AccessToken));
    }

    [Fact]
    public async Task EnsureGetFileChecksThatLoginEmailAddressIsNotNull()
    {
        _configuration.LoginEmailAddress = null;
        await _sut.Initialize(_configuration);

        var ex = await Should.ThrowAsync<InvalidOperationException>(async () => await _sut.GetFile(new GetFileRequest()));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(nameof(DropboxConfiguration.LoginEmailAddress));
    }

    public class DownloadResponse<T> : IDownloadResponse<T>
    {
        private readonly T _response;
        private readonly byte[] _responseBytes;
        private Stream? _responseStream;
        private readonly string _responseString;

        private bool _isDisposed = false;

        public T Response => _response;

        public DownloadResponse(T data)
        {
            _response = data;
            //_responseString = System.Text.Json.JsonSerializer.Serialize(_response);
            _responseString = "ugh";
            _responseBytes = UTF8Encoding.Default.GetBytes(_responseString);
            _responseStream = new MemoryStream(UTF8Encoding.Default.GetBytes(_responseString));
        }

        public async Task<byte[]> GetContentAsByteArrayAsync()
        {
            return await Task.FromResult(_responseBytes);
        }

        public async Task<Stream> GetContentAsStreamAsync()
        {
            if (_responseStream == null)
                throw new InvalidOperationException();

            return await Task.FromResult(_responseStream);
        }

        public async Task<string> GetContentAsStringAsync()
        {
            return await Task.FromResult(_responseString);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    _responseStream?.Dispose();
                    _responseStream = null;
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(isDisposing: true);
            GC.SuppressFinalize(this);
        }
    }

    private IDownloadResponse<FileMetadata> BuildDownloadResponse(int id)
    {
        return new DownloadResponse<FileMetadata>(new FileMetadata($"{id}", $"{id}", DateTime.UtcNow, DateTime.UtcNow, "123456789", 1) {  });
    }

    [Fact]
    public async Task EnsureGetFileReturnsExpectedResults()
    {
        var result = BuildDownloadResponse(123);

        A.CallTo(() => _client.GetFile(A<string>._, A<string>._, A<DownloadArg>._)).Returns(result);

        var resp = await _sut.GetFile(new GetFileRequest());

        resp.ShouldNotBeNull();
        resp.Data.ShouldNotBeNull();
        resp.Data.ShouldBe(await result.GetContentAsByteArrayAsync());
    }
}
