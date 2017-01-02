namespace WinUX.UWP.Security.Data
{
    using System;
    using System.Threading.Tasks;

    using Windows.Security.Cryptography;
    using Windows.Security.Cryptography.Core;
    using Windows.Storage.Streams;

    /// <summary>
    /// Defines a service for encrypting and decrypting data.
    /// </summary>
    public sealed class DataEncryptionService
    {
        private readonly string encryptionAlgorithm = SymmetricAlgorithmNames.AesEcbPkcs7;

        private IBuffer KeyBuffer { get; set; }

        /// <summary>
        /// Initializes the data encryption service.
        /// </summary>
        /// <remarks>
        /// This method must be called before attempting to encrypt/decrypt data.
        /// </remarks>
        /// <param name="key">
        /// A key to encrypt/decrypt with.
        /// </param>
        public void Initialize(string key)
        {
            this.KeyBuffer = HashText(key, HashAlgorithmNames.Md5);
        }

        /// <summary>
        /// Encrypts a byte array.
        /// </summary>
        /// <param name="bytes">
        /// The bytes to encrypt.
        /// </param>
        /// <returns>
        /// Returns the encrypted data.
        /// </returns>
        public async Task<IBuffer> EncryptAsync(byte[] bytes)
        {
            if (this.KeyBuffer == null)
            {
                throw new InvalidOperationException("Cannot encrypt data before Initialize has been called.");
            }

            return await Task.Run(
                       () =>
                           {
                               try
                               {
                                   var buffer = CryptographicBuffer.CreateFromByteArray(bytes);
                                   var aes = SymmetricKeyAlgorithmProvider.OpenAlgorithm(this.encryptionAlgorithm);
                                   var key = this.KeyBuffer;
                                   var cryptoKey = aes.CreateSymmetricKey(key);
                                   return CryptographicEngine.Encrypt(cryptoKey, buffer, null);
                               }
                               catch (Exception ex)
                               {
                                   throw new DataEncryptException("Failed to encrypt data.", ex);
                               }
                           });
        }

        /// <summary>
        /// Decrypts a data buffer.
        /// </summary>
        /// <param name="buffer">
        /// The data buffer to decrypt.
        /// </param>
        /// <returns>
        /// Returns the decrypted data.
        /// </returns>
        public async Task<IBuffer> DecryptAsync(IBuffer buffer)
        {
            if (this.KeyBuffer == null)
            {
                throw new InvalidOperationException("Cannot encrypt data before Initialize has been called.");
            }

            if (buffer == null) throw new ArgumentNullException(nameof(buffer));

            return await Task.Run(
                       () =>
                           {
                               try
                               {
                                   var aes = SymmetricKeyAlgorithmProvider.OpenAlgorithm(this.encryptionAlgorithm);
                                   var key = this.KeyBuffer;
                                   var cryptoKey = aes.CreateSymmetricKey(key);
                                   var decrypted = CryptographicEngine.Decrypt(cryptoKey, buffer, null);
                                   return decrypted;
                               }
                               catch (Exception ex)
                               {
                                   throw new DataEncryptException("Failed to decrypt data.", ex);
                               }
                           });
        }

        private static IBuffer HashText(string text, string algorithm)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;

            var buffUtf8Msg = CryptographicBuffer.ConvertStringToBinary(text, BinaryStringEncoding.Utf8);
            var objAlgProv = HashAlgorithmProvider.OpenAlgorithm(algorithm);
            var buffHash = objAlgProv.HashData(buffUtf8Msg);
            return buffHash.Length != objAlgProv.HashLength ? null : buffHash;
        }
    }
}