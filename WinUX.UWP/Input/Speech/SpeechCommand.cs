namespace WinUX.Input.Speech
{
    using System;
    using System.Collections.Generic;

    using Windows.Media.SpeechRecognition;

    using WinUX.Extensions;

    /// <summary>
    /// Defines a model for a speech command.
    /// </summary>
    public sealed class SpeechCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeechCommand"/> class.
        /// </summary>
        /// <param name="result">
        /// The speech recognition result.
        /// </param>
        /// <param name="expectedPhraseKeys">
        /// The expected phrase keys.
        /// </param>
        public SpeechCommand(SpeechRecognitionResult result, IEnumerable<string> expectedPhraseKeys)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            this.CommandName = result.RulePath[0];
            this.TextSpoken = result.Text;
            this.CommandMode = result.GetSemanticInterpretationProperty("commandMode");
            this.Phrases = result.GetPhraseResults(expectedPhraseKeys);
        }

        /// <summary>
        /// Gets the voice command.
        /// </summary>
        public string CommandName { get; }

        /// <summary>
        /// Gets the command mode.
        /// </summary>
        public string CommandMode { get; }

        /// <summary>
        /// Gets the text spoken.
        /// </summary>
        public string TextSpoken { get; }

        /// <summary>
        /// Gets or sets the phrases.
        /// </summary>
        public Dictionary<string, string> Phrases { get; set; }
    }
}