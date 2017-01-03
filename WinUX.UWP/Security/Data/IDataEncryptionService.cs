namespace WinUX.Security.Data
{
    using System.Threading.Tasks;

    using Windows.Security.Cryptography.Core;
    using Windows.Storage.Streams;

    /// <summary>
    /// Defines an interface for a data encryption service.
    /// </summary>
    public interface IDataEncryptionService
    {
        /// <summary>
        /// Initializes the service with the encryption algorithm and key to use.
        /// </summary>
        /// <remarks>
        /// This method must be called before attempting to encrypt/decrypt data.
        /// </remarks>
        /// <param name="algorithm">
        /// The encryption algorithm from <see cref="SymmetricAlgorithmNames"/>.
        /// </param>
        /// <param name="key">
        /// The string used as a key.
        /// </param>
        void Initialize(string algorithm, string key);

        /// <summary>
        /// Encrypts data.
        /// </summary>
        /// <param name="data">
        /// The bytes to encrypt.
        /// </param>
        /// <returns>
        /// Returns the encrypted data.
        /// </returns>
        Task<IBuffer> EncryptAsync(byte[] data);

        /// <summary>
        /// Decrypts data.
        /// </summary>
        /// <param name="data">
        /// The encrypted data buffer to decrypt.
        /// </param>
        /// <returns>
        /// Returns the decrypted data.
        /// </returns>
        Task<IBuffer> DecryptAsync(IBuffer data);
    }
}
