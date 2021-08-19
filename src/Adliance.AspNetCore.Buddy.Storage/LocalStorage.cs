﻿using System;
using System.IO;
using System.Threading.Tasks;
using Adliance.AspNetCore.Buddy.Abstractions;

namespace Adliance.AspNetCore.Buddy.Storage
{
    public class LocalStorage : IStorage
    {
        private readonly IStorageConfiguration _configuration;

        public LocalStorage(IStorageConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc cref="IStorage.Save(byte[],string[])"/>
        public async Task Save(byte[] bytes, params string[] path)
        {
            await File.WriteAllBytesAsync(GetFilePath(path), bytes);
        }

        /// <inheritdoc cref="IStorage.Save(System.IO.Stream,string[])"/>
        public async Task Save(Stream stream, params string[] path)
        {
            await using (var fileStream = File.OpenWrite(GetFilePath(path)))
            {
                await stream.CopyToAsync(fileStream);
            }
        }

        /// <inheritdoc cref="IStorage.Load(string[])"/>
        public async Task<byte[]?> Load(params string[] path)
        {
            if (await Exists(path))
            {
                return await File.ReadAllBytesAsync(GetFilePath(path));
            }

            return null;
        }

        /// <inheritdoc cref="IStorage.Load(string[])"/>
        public async Task Load(Stream stream, params string[] path)
        {
            if (await Exists(path))
            {
                var fileStream = File.OpenRead(GetFilePath(path));
                await fileStream.CopyToAsync(stream);
                fileStream.Close();
            }
        }

        /// <inheritdoc cref="IStorage.Load(System.IO.Stream,string[])"/>
        public async Task<Uri?> GetDownloadUrl(string niceName, DateTimeOffset expiresOn, params string[] path)
        {
            if (await Exists(path))
            {
                return new Uri("file://" + GetFilePath(path));
            }

            return null;
        }

        /// <inheritdoc cref="IStorage.Exists"/>>
        public Task<bool> Exists(params string[] path)
        {
            return Task.FromResult(File.Exists(GetFilePath(path)));
        }

        /// <inheritdoc cref="IStorage.Delete" />
        public async Task Delete(params string[] path)
        {
            if (await Exists(path))
            {
                File.Delete(GetFilePath(path));
            }
        }

        private string GetFilePath(params string[] path)
        {
            var filePath = Path.Combine(path);
            filePath = Path.Combine(_configuration.LocalStorageBasePath, filePath);

            if (_configuration.AutomaticallyCreateDirectories)
            {
                var directoryPath = Path.GetDirectoryName(filePath)!;
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }

            return filePath;
        }
    }
}