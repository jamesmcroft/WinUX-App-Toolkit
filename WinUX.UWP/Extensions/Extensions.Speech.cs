namespace WinUX
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Windows.Media.SpeechRecognition;

    /// <summary>
    /// Defines a collection of extensions for speech input.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the semantic interpretation of the specified speech result.
        /// </summary>
        /// <param name="result">
        /// The speech recognition result.
        /// </param>
        /// <param name="propertyName">
        /// The property name to extract.
        /// </param>
        /// <returns>
        /// Returns the interpretation, null if there is none.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Exception thrown if the result is null or the property name is empty.
        /// </exception>
        public static string GetSemanticInterpretationProperty(this SpeechRecognitionResult result, string propertyName)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result), "The SpeechRecognitionResult cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(
                    nameof(propertyName),
                    "The property name is required to extract the correct data");
            }

            return result.SemanticInterpretation.Properties[propertyName].FirstOrDefault();
        }

        /// <summary>
        /// Retrieves a collection of phrase key and corresponding results from a <see cref="SpeechRecognitionResult"/>.
        /// </summary>
        /// <param name="result">
        /// The speech result.
        /// </param>
        /// <param name="phraseKeys">
        /// The phrase keys to retrieve.
        /// </param>
        /// <returns>
        /// Returns a Dictionary of key value pairs containing the phrase key and spoken phrase.
        /// </returns>
        public static Dictionary<string, string> GetPhraseResults(
            this SpeechRecognitionResult result,
            IEnumerable<string> phraseKeys)
        {
            var phrases = new Dictionary<string, string>();

            if (phraseKeys != null)
            {
                foreach (var phraseKey in phraseKeys)
                {
                    var phraseResult = result.GetSemanticInterpretationProperty(phraseKey);
                    phrases.Add(phraseKey, phraseResult);
                }
            }

            return phrases;
        }
    }
}