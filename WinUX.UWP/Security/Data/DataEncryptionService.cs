namespace WinUX.Security.Data
{
    using System;
    using System.Threading.Tasks;

    using Windows.Security.Cryptography;
    using Windows.Security.Cryptography.Core;
    using Windows.Storage.Streams;

    /// <summary>
    /// Defines a service for encrypting and decrypting data.
    /// </summary>
    public sealed class DataEncryptionService : IDataEncryptionService
    {
        private string encryptionAlgorithm;

        private IBuffer keyHash;

        /// <inheritdoc />
        public void Initialize(string algorithm, string key)
        {
            this.encryptionAlgorithm = algorithm;
            this.keyHash = HashData(key, HashAlgorithmNames.Md5);
        }

        /// <inheritdoc />
        public async Task<IBuffer> EncryptAsync(byte[] data)
        {
            if (this.keyHash == null)
            {
                throw new InvalidOperationException("Cannot encrypt data before Initialize has been called.");
            }

            return await Task.Run(
                       () =>
                           {
                               try
                               {
                                   var buffer = CryptographicBuffer.CreateFromByteArray(data);
                                   var algorithm = SymmetricKeyAlgorithmProvider.OpenAlgorithm(this.encryptionAlgorithm);
                                   var key = this.keyHash;
                                   var symmetricKey = algorithm.CreateSymmetricKey(key);
                                   return CryptographicEngine.Encrypt(symmetricKey, buffer, null);
                               }
                               catch (Exception ex)
                               {
                                   throw new DataEncryptException("Failed to encrypt data.", ex);
                               }
                           });
        }

        /// <inheritdoc />
        public async Task<IBuffer> DecryptAsync(IBuffer data)
        {
            if (this.keyHash == null)
            {
                throw new InvalidOperationException("Cannot encrypt data before Initialize has been called.");
            }

            if (data == null) throw new ArgumentNullException(nameof(data));

            return await Task.Run(
                       () =>
                           {
                               try
                               {
                                   var algorithm = SymmetricKeyAlgorithmProvider.OpenAlgorithm(this.encryptionAlgorithm);
                                   var symmetricKey = algorithm.CreateSymmetricKey(this.keyHash);
                                   var buffer = CryptographicEngine.Decrypt(symmetricKey, data, null);
                                   return buffer;
                               }
                               catch (Exception ex)
                               {
                                   throw new DataEncryptException("Failed to decrypt data.", ex);
                               }
                           });
        }

        private static IBuffer HashData(string data, string algorithm)
        {
            if (string.IsNullOrWhiteSpace(data)) return null;

            var encodedBuffer = CryptographicBuffer.ConvertStringToBinary(data, BinaryStringEncoding.Utf8);
            var hashAlgorithm = HashAlgorithmProvider.OpenAlgorithm(algorithm);
            var hash = hashAlgorithm.HashData(encodedBuffer);
            return hash.Length != hashAlgorithm.HashLength ? null : hash;
        }
    }
}