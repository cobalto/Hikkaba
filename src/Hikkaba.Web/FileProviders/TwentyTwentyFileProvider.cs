using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hikkaba.Common.Storage.Interfaces;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using TwentyTwenty.Storage;

namespace Hikkaba.Web.FileProviders
{
    public class TwentyTwentyFileProvider : IFileProvider
    {
        private static readonly char[] PathDelimiters = { '/' };
        private readonly IStorageProvider _storageProvider;

        public TwentyTwentyFileProvider(IStorageProviderFactory storageProviderFactory)
        {
            _storageProvider = storageProviderFactory.CreateStorageProvider();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var parts = subpath.Split(PathDelimiters, 2);
            if (parts.Length != 2)
            {
                return new NonexistentFileInfo(subpath);
            }
            else
            {
                return new TwentyTwentyFileInfo(parts[0], parts[1], _storageProvider);
            }
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            var parts = subpath.Split(PathDelimiters, 2);
            if (parts.Length != 2)
            {
                return new NonexistentDirectoryContents();
            }
            else
            {
                return new TwentyTwentyDirectoryContents(parts[0], parts[1], _storageProvider);
            }
        }

        public IChangeToken Watch(string filter)
        {
            return NonexistentChangeToken.Singleton;
        }
    }

    internal class NonexistentChangeToken : IChangeToken
    {
        public static NonexistentChangeToken Singleton { get; } = new NonexistentChangeToken();

        public bool ActiveChangeCallbacks => false;

        public bool HasChanged => false;

        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            throw new NotImplementedException();
        }
    }

    internal class TwentyTwentyFileInfo : IFileInfo
    {
        private readonly IStorageProvider _storageProvider;
        private readonly Lazy<BlobDescriptor> _lazyBlobDescriptor;

        public TwentyTwentyFileInfo(string containerName, string blobName, IStorageProvider storageProvider)
        {
            ContainerName = containerName;
            Name = blobName;

            _storageProvider = storageProvider;
            _lazyBlobDescriptor = new Lazy<BlobDescriptor>(() =>
            {
                BlobDescriptor returnValue;
                try
                {
                    returnValue = _storageProvider.GetBlobDescriptorAsync(containerName, blobName).Result;
                }
                catch (Exception ex)
                {
                    returnValue = null;
                }
                return returnValue;
            });
        }
        public bool Exists => _lazyBlobDescriptor != null;
        public bool IsDirectory => false;
        public DateTimeOffset LastModified => _lazyBlobDescriptor?.Value?.LastModified ?? DateTimeOffset.MinValue;
        public long Length => _lazyBlobDescriptor?.Value?.Length ?? -1;
        public string ContainerName { get; }
        public string Name { get; }
        public string PhysicalPath => _lazyBlobDescriptor?.Value?.Url;
        public Stream CreateReadStream()
        {
            return _storageProvider.GetBlobStreamAsync(ContainerName, Name).Result;
        }
    }

    internal class NonexistentFileInfo : IFileInfo
    {
        public NonexistentFileInfo(string name)
        {
            this.Name = name;
        }
        public bool Exists => false;
        public bool IsDirectory => false;
        public DateTimeOffset LastModified => DateTimeOffset.MinValue;
        public long Length => -1;
        public string Name { get; }
        public string PhysicalPath => null;
        public Stream CreateReadStream()
        {
            throw new FileNotFoundException(this.Name);
        }
    }

    internal class TwentyTwentyDirectoryContents : IDirectoryContents
    {
        public string ContainerName { get; }
        private readonly IList<BlobDescriptor> _blobDescriptors;
        private readonly IList<IFileInfo> _fileInfos;

        public TwentyTwentyDirectoryContents(string containerName, string blobName, IStorageProvider storageProvider)
        {
            ContainerName = containerName;
            try
            {
                _blobDescriptors = storageProvider.ListBlobsAsync(ContainerName).Result;
                _fileInfos = new List<IFileInfo>();
                foreach (var blobDescriptor in _blobDescriptors)
                {
                    _fileInfos.Add(new TwentyTwentyFileInfo(blobDescriptor.Container, blobDescriptor.Name, storageProvider));
                }
            }
            catch (Exception e)
            {
                _blobDescriptors = new List<BlobDescriptor>();
                _fileInfos = new List<IFileInfo>();
            }

        }

        public bool Exists => _blobDescriptors.Count > 0;

        public IEnumerator<IFileInfo> GetEnumerator()
        {
            if (_blobDescriptors == null || _blobDescriptors.Count == 0)
            {
                return Enumerable.Empty<IFileInfo>().GetEnumerator();
            }
            else
            {
                return _fileInfos.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    internal class NonexistentDirectoryContents : IDirectoryContents
    {
        public bool Exists => false;

        public IEnumerator<IFileInfo> GetEnumerator()
        {
            return Enumerable.Empty<IFileInfo>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
